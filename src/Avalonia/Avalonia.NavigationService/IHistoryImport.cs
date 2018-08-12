using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.NavigationService.Common;

namespace Avalonia.NavigationService {

	/// <summary>
	/// </summary>
	/// <param name="items">Item collection.</param>
	/// <param name="selected">Selected item.</param>
	public interface IHistoryImport {

		/// <summary>
		/// Import history.
		/// </summary>
		/// <returns></returns>
		Task<(IEnumerable<HistoryItem> items, int selected)> Import ();

	}

}