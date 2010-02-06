using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows;
using IncEditor.Model;
using IncEditor.ViewModels;

namespace IncEditor
{
	public class App : Application
	{
		[ImportMany(typeof(ITheme))]
		public IEnumerable<ITheme> Themes { get; set; }

		public ITheme SelectedTheme { get; set; }

		[STAThread]
		public static void Main(string[] args)
		{
			App app = new App();
			app.Configure();
			app.Run();
		}

		private void Configure()
		{
			Compose();

			if (Themes.Count() == 0)
			{
				MessageBox.Show("No theme is present. Application is shutting down");
				Shutdown();
				return;
			}

			string themeName = IncEditor.Properties.Settings.Default["ThemeName"] as string;

			SelectedTheme = Themes.SingleOrDefault(x => x.Name.Equals(themeName, StringComparison.OrdinalIgnoreCase));

			SelectedTheme.MainView.DataContext = new MainViewModel();
			MainWindow = SelectedTheme.MainView;
			SelectedTheme.MainView.Show();
		}

		private void Compose()
		{
			var catalog = new DirectoryCatalog(@"..\..\Themes\");
			var container = new CompositionContainer(catalog);
			container.ComposeParts(this);
		}
	}
}