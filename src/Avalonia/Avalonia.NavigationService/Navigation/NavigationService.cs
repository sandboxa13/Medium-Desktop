using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.NavigationService.Common;
using Avalonia.NavigationService.DefaultImplementations;
using Avalonia.NavigationService.Helpers;

namespace Avalonia.NavigationService.Navigation {

	/// <summary>
	/// Navigation service.
	/// </summary>
	public class NavigationService : INavigationService {

		private const int DefaultMaxHistorySize = 15;

		private int m_MaxHistorySize = DefaultMaxHistorySize;

		private List<HistoryItem> m_History = new List<HistoryItem> ();

		private IViewResolver m_ViewResolver = new DefaultViewResolver ();

		private HistoryItem m_CurrentState;

		/// <summary>
		/// Name.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Control that belong navigation service.
		/// </summary>
		public WeakReference<Control> Control
		{
			get;
			set;
		}

		/// <summary>
		/// Max history size.
		/// </summary>
		public int MaxHistorySize
		{
			get => m_MaxHistorySize;
			set
			{
				m_MaxHistorySize = value < 1 ? 1 : value;
			}
		}

		/// <summary>
		/// If use as stack then after GoBack() operation previous step removed from history.
		/// </summary>
		public bool IsStack
		{
			get;
			set;
		}

		private void AddNewItemToHistory ( HistoryItem historyItem , int insertPosition = -1 ) {
			if ( m_History.Count == MaxHistorySize ) m_History.RemoveAt ( 0 );

			if ( IsStack ) {
				m_History.Add ( historyItem );
			}
			else {
				if ( insertPosition > -1 && insertPosition < m_History.Count - 1 ) m_History = m_History.GetRange ( 0 , insertPosition + 1 );

				m_History.Add ( historyItem );
			}
		}

		private void UpdateControl ( Control container , Control view ) {
			var panel = ( container as Panel );
			if ( panel != null ) {
				panel.Children.Clear ();
				panel.Children.Add ( view );
				return;
			}
			var contentControl = ( container as ContentControl );
			if ( contentControl != null ) contentControl.Content = view;
		}

		private Control GetChildrenControl ( Control container ) {
			var panel = ( container as Panel );
			if ( panel != null ) return panel.Children.OfType<Control> ().FirstOrDefault ();

			var contentControl = ( container as ContentControl );
			if ( contentControl != null ) return contentControl.Content as Control;

			return null;
		}

		private bool NavigateFromControl ( Control control , NavigationMode mode , HistoryItem historyItem ) {
			var childrenControl = GetChildrenControl ( control );
			if ( childrenControl == null ) return true;

			var result = ReflectionHelper.InvokeMethod ( childrenControl , Navigator.NavigateFromMethodName , null );

			return result is bool && (bool) result == false ? false : true;
		}

		private void ChangeContent ( HistoryItem historyItem , HistoryItem oldHistoryItem , NavigationMode mode ) {
			Control.TryGetTarget ( out var control );
			if ( control == null ) return;

			if ( !NavigateFromControl ( control , mode , historyItem ) ) return;

			var view = m_ViewResolver.Resolve ( historyItem.Type );
			ReflectionHelper.SetToProperty ( view , Navigator.NavigationServicePropertyName , this );
			if ( view.DataContext != null ) ReflectionHelper.SetToProperty ( control.DataContext , Navigator.NavigationServicePropertyName , this );

			NavigateToControl ( view , mode , historyItem );

			UpdateControl ( control , view );
		}

		private void NavigateToControl ( Control control , NavigationMode mode , HistoryItem historyItem ) {
			ReflectionHelper.InvokeMethod ( control , Navigator.NavigateToMethodName , historyItem.Parameters );
			if ( control.DataContext != null ) ReflectionHelper.InvokeMethod ( control.DataContext , Navigator.NavigateToMethodName , historyItem.Parameters );
		}

		/// <summary>
		/// Set class that will be resolve view types.
		/// </summary>
		/// <param name="viewResolver"></param>
		public void SetViewResolver ( IViewResolver viewResolver ) {
			m_ViewResolver = viewResolver;
		}

		/// <summary>
		/// Go back from history.
		/// </summary>
		public void GoBack () {
			if ( !CanGoBack () ) return;

			var currentItemIndex = m_History.IndexOf ( CurrentState );
			var newElement = m_History.ElementAt ( currentItemIndex - 1 );

			//stack mode when we delete history record immediatly after going back.
			if ( IsStack ) m_History = m_History.GetRange ( 0 , m_History.Count - ( m_History.Count - currentItemIndex ) );

			ChangeContent ( newElement , CurrentState , NavigationMode.Back );
		}

		/// <summary>
		/// Go forward for one step.
		/// </summary>
		public void GoForward () {
			if ( !CanGoForward () ) return;

			var currentIndex = m_History.IndexOf ( CurrentState );
			if ( currentIndex == m_History.Count - 1 ) return;

			var newElement = m_History.ElementAt ( currentIndex + 1 );

			ChangeContent ( newElement , CurrentState , NavigationMode.Forward );
		}

		/// <summary>
		/// Is it possible to go back to the page?
		/// </summary>
		/// <returns></returns>
		public bool CanGoBack () => m_History.IndexOf ( CurrentState ) > 0;

		/// <summary>
		/// Is it possible to go to the front page?
		/// </summary>
		/// <returns></returns>
		public bool CanGoForward () => m_History.IndexOf ( CurrentState ) < m_History.Count - 1;

		/// <summary>
		/// Current state of history.
		/// </summary>
		public HistoryItem CurrentState
		{
			get => m_CurrentState;
			set
			{
				m_CurrentState = value;
				var selectedIndex = m_History.IndexOf ( m_CurrentState );

				if ( value == null ) return;

				AddNewItemToHistory ( value , selectedIndex );

				ChangeContent ( value , null , NavigationMode.New );
			}
		}

		/// <summary>
		/// Go to a new page.
		/// </summary>
		/// <param name="type">The type of page that will be displayed in the frame.</param>
		/// <param name="parameters">Parameters wiil be passed to </param>
		public void Navigate ( Type type , object parameters ) {
			if ( type == null ) return;

			m_CurrentState = new HistoryItem {
				Type = type ,
				Parameters = parameters != null ? ReflectionHelper.MapParametersFromObjectProperties ( parameters , Navigator.ParameterNameResolver ) : null
			};
			var selectedIndex = m_History.IndexOf ( m_CurrentState );

			AddNewItemToHistory ( m_CurrentState , selectedIndex );

			ChangeContent ( m_CurrentState , null , NavigationMode.New );
		}

		/// <summary>
		/// Export history.
		/// </summary>
		public Task Export ( IHistoryExport historyExport ) {
			if ( historyExport == null ) throw new ArgumentNullException ( nameof ( historyExport ) );

			return historyExport.Export (
					m_History ,
					m_History.IndexOf ( CurrentState )
				);
		}

		/// <summary>
		/// Import history.
		/// </summary>
		public async Task Import ( IHistoryImport historyImport ) {
			if ( historyImport == null ) throw new ArgumentNullException ( nameof ( historyImport ) );

			m_History.Clear ();

			var (historyItems, selected) = await historyImport.Import ();

			m_History.AddRange (
				historyItems.Select (
					a => new HistoryItem {
						Type = a.Type ,
						Parameters = a.Parameters
					}
				)
			);

			if ( selected < 0 ) throw new ArgumentOutOfRangeException ( nameof ( selected ) );

			var selectedItem = m_History.ElementAt ( selected );
			ChangeContent ( selectedItem , null , NavigationMode.New );
		}

	}

}
