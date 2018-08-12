using System.Collections.Generic;

namespace Avalonia.NavigationService.Common {

	public class History {

		public IEnumerable<HistoryItem> Items
		{
			get; set;
		}

		public int Selected
		{
			get; set;
		}

	}

}
