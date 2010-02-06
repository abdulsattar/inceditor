using System.Collections.ObjectModel;
using System.Windows;
using IncEditor.Model;

namespace IncEditor.ViewModels
{
	public class ThemeViewModel : WorkspaceViewModel
	{
		public ObservableCollection<ITheme> Themes;
		private ITheme _selectedTheme;
		public ITheme SelectedTheme
		{
			get { return _selectedTheme; }
			set
			{
				_selectedTheme = value;
				Properties.Settings.Default["ThemeName"] = _selectedTheme.Name;
				OnPropertyChanged("SelectedTheme");
			}
		}

		public ThemeViewModel()
		{
			Themes = new ObservableCollection<ITheme>(((App)Application.Current).Themes);
		}
	}
}