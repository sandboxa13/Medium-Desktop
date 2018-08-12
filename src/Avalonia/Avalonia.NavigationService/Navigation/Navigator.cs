using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.NavigationService.DefaultImplementations;
using Avalonia.NavigationService.Helpers;

namespace Avalonia.NavigationService.Navigation {

	/// <summary>
	/// This class contains global navigation methods and configuration.
	/// </summary>
	public class Navigator {

		private static Dictionary<string , Type> m_GlobalMapping = new Dictionary<string , Type> ();

		private static IViewFinder m_ViewFinder;

		private static string m_NavigationServicePropertyName = "NavigationService";

		private static string m_NavigateToMethodName = "NavigateTo";

		private static string m_NavigateFromMethodName = "NavigateFrom";

		private static IParameterNameResolver m_ParameterNameResolver = new DefaultParameterNameResolver ();

		static Navigator () {
			NavigationContainerProperty.Changed.Subscribe ( NavigationContainerChanged );
			ViewNameProperty.Changed.Subscribe ( ViewNameChanged );
		}

		/// <summary>
		/// Set custom <see cref="IViewFinder"/> implementation.
		/// </summary>
		/// <param name="viewFinder">Custom implementation <see cref="IViewFinder"/></param>
		public static void SetViewFinder ( IViewFinder viewFinder ) {
			m_ViewFinder = viewFinder ?? throw new ArgumentNullException ( nameof ( viewFinder ) );
		}

		/// <summary>
		/// Set custom <see cref="IParameterNameResolver"/> implementation.
		/// </summary>
		/// <param name="viewFinder">Custom implementation <see cref="IParameterNameResolver"/></param>
		public static void SetParameterNameResolver ( IParameterNameResolver parameterNameResolver ) {
			m_ParameterNameResolver = parameterNameResolver ?? throw new ArgumentNullException ( nameof ( parameterNameResolver ) );
		}

		private static void ViewNameChanged ( AvaloniaPropertyChangedEventArgs e ) {
			var control = e.Sender as Control;
			if ( control == null ) return;

			var navigationService = GetNavigationService ( control );
			if ( navigationService == null ) return;

			var viewName = (string) e.NewValue;

			if ( m_GlobalMapping.TryGetValue ( viewName , out var type ) ) {
				navigationService.Navigate ( type );
				return;
			}

			var view = m_ViewFinder?.GetView ( viewName );

			if ( view != null ) navigationService.Navigate ( view );
		}

		private static void NavigationContainerChanged ( AvaloniaPropertyChangedEventArgs e ) {
			var control = e.Sender as Control;
			if ( control == null ) return;

			var navigationService = GetNavigationService ( control );
			if ( navigationService == null ) {
				var createdNavigationService = new NavigationService {
					Name = e.NewValue.ToString () ,
					Control = new WeakReference<Control> ( control )
				};
				SetNavigationService ( control , createdNavigationService );
				ReflectionHelper.SetToProperty ( control , m_NavigationServicePropertyName , createdNavigationService );
				if ( control.DataContext != null ) ReflectionHelper.SetToProperty ( control.DataContext , m_NavigationServicePropertyName , createdNavigationService );
			}
		}

		/// <summary>
		/// Register view.
		/// </summary>
		/// <param name="viewType">View type.</param>
		/// <param name="alias">Alias (if not specified will be equal full name ViewType).</param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void RegisterView ( Type viewType , string alias = null ) {
			if ( viewType == null ) throw new ArgumentNullException ( nameof ( viewType ) );

			if ( string.IsNullOrEmpty ( alias ) ) alias = viewType.FullName;

			m_GlobalMapping.Add ( alias , viewType );
		}

		/// <summary>
		/// Get view by alias.
		/// </summary>
		/// <param name="alias">Alias.</param>
		public static Type GetView ( string alias ) {
			return m_GlobalMapping.ContainsKey ( alias ) ? m_GlobalMapping[alias] : null;
		}

		public static readonly AttachedProperty<string> NavigationContainerProperty = AvaloniaProperty.RegisterAttached<Navigator , Control , string> ( "NavigationContainer" );

		public static readonly AttachedProperty<INavigationService> NavigationServiceProperty = AvaloniaProperty.RegisterAttached<Control , INavigationService> ( "NavigationService" , typeof ( Navigator ) );

		public static readonly AttachedProperty<int> MaxHistorySizeProperty = AvaloniaProperty.RegisterAttached<Control , int> ( "MaxHistorySize" , typeof ( Navigator ) );

		public static readonly AttachedProperty<string> ViewNameProperty = AvaloniaProperty.RegisterAttached<Control , string> ( "ViewName" , typeof ( Navigator ) );

		public static void SetNavigationService ( Control control , INavigationService value ) {
			control.SetValue ( NavigationServiceProperty , value );
		}

		public static INavigationService GetNavigationService ( Control control ) {
			return control.GetValue ( NavigationServiceProperty );
		}

		public static string NavigateToMethodName => m_NavigateToMethodName;

		public static string NavigateFromMethodName => m_NavigateFromMethodName;

		public static string NavigationServicePropertyName => m_NavigationServicePropertyName;

		public static IParameterNameResolver ParameterNameResolver => m_ParameterNameResolver;

	}

}
