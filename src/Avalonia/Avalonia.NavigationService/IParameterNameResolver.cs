namespace Avalonia.NavigationService {

	/// <summary>
	/// Interface for class-helper for properties name that transpile it to convinient view.
	/// </summary>
	public interface IParameterNameResolver {

		/// <summary>
		/// Resolve name of parameter.
		/// </summary>
		/// <param name="name">parameter name.</param>
		/// <returns>Resolved name.</returns>
		string Resolve ( string name );

	}
}
