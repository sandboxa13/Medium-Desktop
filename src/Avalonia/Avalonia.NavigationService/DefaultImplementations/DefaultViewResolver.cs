using System;
using Avalonia.Controls;

namespace Avalonia.NavigationService.DefaultImplementations {

	public class DefaultViewResolver : IViewResolver {

		/// <summary>
		/// Resolve type.
		/// </summary>
		/// <param name="type">View type.</param>
		public Control Resolve ( Type type ) =>  Activator.CreateInstance ( type ) as Control;

	}

}