using System;
using System.Collections.Generic;

namespace Avalonia.NavigationService.Common {

	public class HistoryItem {

		/// <summary>
		/// Type at user control.
		/// </summary>
		public Type Type
		{
			get;
			set;
		}

		/// <summary>
		/// Parameters.
		/// </summary>
		public IDictionary<string , object> Parameters
		{
			get;
			set;
		}

	}

}