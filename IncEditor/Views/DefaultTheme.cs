using System.ComponentModel.Composition;
using System.Windows;
using IncEditor.Model;

namespace Views
{
	[Export(typeof(ITheme))]
	public class DefaultTheme : ITheme
	{
		public string Name
		{
			get { return "Default"; }
		}

		private MainView _mainView;
		public Window MainView
		{
			get { return _mainView ?? (_mainView = new MainView()); }
		}
	}
}