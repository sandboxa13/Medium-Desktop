using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.NavigationService.Common;

//using Newtonsoft.Json;

namespace Avalonia.NavigationService.DefaultImplementations {

	/// <summary>
	/// Import history.
	/// </summary>
	public class ImportHistory : IHistoryImport {

		public async Task<(IEnumerable<HistoryItem> items, int selected)> Import () {
			var result = await new OpenFileDialog () {
				AllowMultiple = false ,
				Title = "Open history from file"
			}.ShowAsync ( null );

			//var history = JsonConvert.DeserializeObject<History> ( File.ReadAllText ( result[0] ) );

			//return (
			//	history.Items
			//		.Select (
			//			a => new Avalonia.Controls.HistoryItem {
			//				Type = typeof(ImportHistory).Assembly.GetType(a.Type),
			//				Parameters = a.Parameters
			//			}
			//		),
			//	history.Selected
			//);
			return (null, 0);
		}

	}

}
