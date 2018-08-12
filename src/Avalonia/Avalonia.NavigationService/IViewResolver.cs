using System;
using Avalonia.Controls;

namespace Avalonia.NavigationService {

	public interface IViewResolver {

		/// <summary>
		/// Resolve type and it dependencies.
		/// </summary>
		/// <param name="type">Type that need receive.</param>
		Control Resolve ( Type type );

	}

}