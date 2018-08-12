using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Avalonia.NavigationService.DefaultImplementations {

	/// <summary>
	/// Default implementation <see cref="IViewFinder"/>.
	/// </summary>
	public class AssembliesViewFinder : IViewFinder {

		IReadOnlyCollection<Assembly> m_Assemblies;

		/// <summary>
		/// Create 
		/// </summary>
		/// <param name="assemblies"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public AssembliesViewFinder ( IEnumerable<Assembly> assemblies ) {
			if ( assemblies == null ) throw new ArgumentNullException ( nameof ( assemblies ) );

			m_Assemblies = assemblies.ToList ();
		}

		/// <summary>
		/// Get <see cref="Type"/> by name.
		/// </summary>
		/// <param name="typeName">Type name.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public Type GetView ( string typeName ) {
			if ( typeName == null ) throw new ArgumentNullException (nameof( typeName ) );

			foreach ( var assembly in m_Assemblies ) {
				var type = assembly.GetType ( typeName );
				return type;
			}
			return null;
		}

	}

}
