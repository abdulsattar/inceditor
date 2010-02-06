using System;
using System.Windows.Input;
using IncEditor.Commands;

namespace IncEditor.ViewModels
{
	public class WorkspaceViewModel : ViewModelBase
	{
		#region Commands

		private DelegateCommand _closeCommand;
		public ICommand CloseCommand
		{
			get
			{
				return _closeCommand ?? (_closeCommand = new DelegateCommand(OnRequestClose));
			}
		}

		#endregion

		#region Public Fields
		private string _headerText;
		public string HeaderText
		{
			get { return _headerText; }
			set
			{
				_headerText = value;
				OnPropertyChanged("Header");
			}
		}

		#endregion

		#region RequestClose Event

		public event EventHandler RequestClose;
		public void OnRequestClose()
		{
			if (RequestClose != null)
				RequestClose(this, EventArgs.Empty);
		}

		#endregion
	}
}