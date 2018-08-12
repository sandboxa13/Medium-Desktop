using System.Linq;

namespace Avalonia.NavigationService.DefaultImplementations {

	/// <summary>
	/// Class helper for properties name that transpile it to convinient view.
	/// </summary>
	public class DefaultParameterNameResolver : IParameterNameResolver {

		/// <summary>
		/// Resolve name of parameter.
		/// </summary>
		/// <param name="name">parameter name.</param>
		/// <returns>Resolved name.</returns>
		public string Resolve ( string name ) {
			var firstCharacter = name[0].ToString ().ToLowerInvariant ();
			var withoutFirstCharacter = new string ( name.Skip ( 1 ).ToArray () );
			return firstCharacter + withoutFirstCharacter;
		}

	}

}
