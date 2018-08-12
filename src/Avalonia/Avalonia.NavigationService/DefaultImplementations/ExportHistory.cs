using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.NavigationService.Common;

//using Newtonsoft.Json;

namespace Avalonia.NavigationService.DefaultImplementations {
	public class ExportHistory : IHistoryExport {
		public async Task Export ( IEnumerable<HistoryItem> items , int selected ) {
			var fileName = await new SaveFileDialog () {
				Title = "Save history to file"
			}.ShowAsync ( null );

			//var content = JsonConvert.SerializeObject (
			//	new History {
			//		Selected = selected ,
			//		Items = items.Select (
			//			a => new HistoryItem {
			//				Type = a.Type.FullName ,
			//				Parameters = a.Parameters
			//			}
			//		)
			//	}
			//);
			File.WriteAllText ( fileName , "" );
		}
	}

}
