using System.Windows;

namespace TextReader.Contracts
{
    public interface ITextReader
    {
        void ApplyToSelection(DependencyProperty property, object value);
    }
}
