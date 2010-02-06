using System.ComponentModel.Composition;
using System.Windows;
using IncEditor.Model;

namespace ExpressionTheme
{
	[Export(typeof(ITheme))]
	public class ExpressionTheme : ITheme
	{
		public string Name
		{
			get { return "Expression"; }
		}

		private MainView _mainView;
		public Window MainView
		{
			get { return _mainView ?? (_mainView = new MainView()); }
		}
	}
}