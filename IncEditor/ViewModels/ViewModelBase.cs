using System.ComponentModel;

namespace IncEditor.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}