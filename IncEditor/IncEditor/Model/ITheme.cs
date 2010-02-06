using System.Windows;

namespace IncEditor.Model
{
	public interface ITheme
	{
		string Name { get; }
		Window MainView { get; }
	}
}