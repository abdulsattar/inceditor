namespace IncEditor.ViewModels
{
	public class DocumentViewModel : WorkspaceViewModel
	{
		private string _text;
		public string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				OnPropertyChanged("Text");
			}
		}
	}
}