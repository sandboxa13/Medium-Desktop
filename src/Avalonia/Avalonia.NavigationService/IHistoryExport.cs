using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.NavigationService.Common;

namespace Avalonia.NavigationService {

	public interface IHistoryExport {

		/// <summary>
		/// Export history.
		/// </summary>
		/// <param name="items">Item collection.</param>
		/// <param name="selected">Selected item.</param>
		Task Export ( IEnumerable<HistoryItem> items , int selected );

	}

}