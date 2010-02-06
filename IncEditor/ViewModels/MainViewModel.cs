using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using IncEditor.Commands;
using IncEditor.Model;

namespace IncEditor.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		#region Private Fields

		private DelegateCommand _exitCommand;
		private DelegateCommand _newWorkspaceCommand;
		private DelegateCommand _closeWorkspaceCommand;

		#endregion

		#region Constructor

		public MainViewModel()
		{
			Workspaces = new ObservableCollection<WorkspaceViewModel>();
			SelectedIndex = 0;
			Workspaces.CollectionChanged += Workspaces_CollectionChanged;
		}

		void Workspaces_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null && e.NewItems.Count > 0)
				foreach (WorkspaceViewModel newItem in e.NewItems)
					newItem.RequestClose += delegate { CloseWorkspace(); };
			if (e.OldItems != null && e.OldItems.Count != 0)
				foreach (WorkspaceViewModel workspace in e.OldItems)
					workspace.RequestClose -= delegate { CloseWorkspace(); };

		}

		#endregion

		#region Public Fields

		public ObservableCollection<WorkspaceViewModel> Workspaces { get; set; }

		private string _statusText;
		public string StatusText
		{
			get { return _statusText; }
			set
			{
				_statusText = value;
				OnPropertyChanged("StatusText");
			}
		}

		private int _selectedIndex;
		public int SelectedIndex
		{
			get { return _selectedIndex; }
			set
			{
				_selectedIndex = value;
				OnPropertyChanged("SelectedIndex");
			}
		}

		private ObservableCollection<ITheme> _themes;
		public ObservableCollection<ITheme> Themes
		{
			get
			{
				return _themes ?? (_themes = new ObservableCollection<ITheme>(((App) Application.Current).Themes));
			}
		}

		private ITheme _selectedTheme;
		public ITheme SelectedTheme
		{
			get
			{
				return _selectedTheme ?? (_selectedTheme = ((App)Application.Current).SelectedTheme);
			}
			set
			{
				_selectedTheme = value;
				Properties.Settings.Default["ThemeName"] = _selectedTheme.Name;
				Properties.Settings.Default.Save();
				OnPropertyChanged("SelectedTheme");
			}
		}

		#endregion

		#region Commands

		public ICommand ExitCommand
		{
			get { return _exitCommand ?? (_exitCommand = new DelegateCommand(() => Application.Current.Shutdown())); }
		}

		public ICommand NewWorkspaceCommand
		{
			get { return _newWorkspaceCommand ?? (_newWorkspaceCommand = new DelegateCommand(NewWorkspace)); }
		}

		public ICommand CloseWorkspaceCommand
		{
			get { return _closeWorkspaceCommand ?? (_closeWorkspaceCommand = new DelegateCommand(CloseWorkspace, ()=>Workspaces.Count > 0)); }
		}

		private void CloseWorkspace()
		{
			Workspaces.RemoveAt(SelectedIndex);
			SelectedIndex = 0;
		}

		#endregion

		#region Private Methods

		private void NewWorkspace()
		{
			StatusText = "Opening New Document";
			var workspace = new DocumentViewModel{ HeaderText = "New Workspace "};
			Workspaces.Add(workspace);
			this.SelectedIndex = Workspaces.IndexOf(workspace);
			StatusText = "Opened Document";
		}

		#endregion
	}
}