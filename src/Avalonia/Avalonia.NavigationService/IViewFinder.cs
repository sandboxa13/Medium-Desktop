using System;

namespace Avalonia.NavigationService {

	/// <summary>
	/// Interface for find View.
	/// </summary>
	public interface IViewFinder {

		/// <summary>
		/// Get <see cref="Type"/> by name.
		/// </summary>
		/// <param name="typeName">Type name.</param>
		Type GetView ( string typeName );

	}

}
