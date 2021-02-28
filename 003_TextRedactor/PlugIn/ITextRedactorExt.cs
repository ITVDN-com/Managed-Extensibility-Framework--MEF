using System;
using System.Windows;


namespace TextReader.Contracts
{
    public interface ITextRedactorExtension
    {
        string Name { get; }
        Version Version { get; }
        string Description { get; }
        string AuthorName { get; }

        FrameworkElement GetUI();
    }
}
