using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;
//using ChangeColorExt;
using TextReader.Contracts;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace TextRedactor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(ITextReader))]
    public partial class MainWindow : Window, ITextReader
    {
        string currentFile;
        DirectoryCatalog dirCatalog;
        CompositionContainer container;
        ImportManager imports;

        public MainWindow()
        {
            InitializeComponent();
            InitializeContainer();
            RefreshExtensions();
        }

        private void InitializeContainer()
        {
            dirCatalog = new DirectoryCatalog(Properties.Settings.Default.AddInDirectory);

            container = new CompositionContainer(dirCatalog);

            imports = new ImportManager();
            imports.ImportSatisfied += (sender, e) =>
                {
                    MessageBox.Show(e.Status);
                };

            container.ComposeParts(this, imports);
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            mainTextBox.Document.Blocks.Clear();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Текстовые документы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                mainTextBox.Document.Blocks.Clear();
                currentFile = openFileDialog.FileName;
                System.IO.StreamReader myRead = new System.IO.StreamReader(currentFile, System.Text.Encoding.GetEncoding(1251));
                mainTextBox.AppendText(myRead.ReadToEnd());
                myRead.Close();
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "Текстовые документы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (saveDialog.ShowDialog() == true)
            {
                currentFile = saveDialog.FileName;
                System.IO.StreamWriter myWrite = new System.IO.StreamWriter(currentFile, false, System.Text.Encoding.GetEncoding(1251));

                var textRange = new TextRange(mainTextBox.Document.ContentStart, mainTextBox.Document.ContentEnd);

                myWrite.Write(textRange.Text);
                myWrite.Close();
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ApplyToSelection(DependencyProperty property, object value)
        {
            mainTextBox.Selection.ApplyPropertyValue(property, value);
        }

        private void OnRefreshExtensions(object sender, EventArgs e)
        {
            RefreshExtensions();
        }

        public void RefreshExtensions()
        {
            dirCatalog.Refresh();

            menuAddins.Items.Clear();

            foreach (var extension in imports.readerExtCollection)
            {
                var menuItemHeader = new StackPanel { Orientation = Orientation.Horizontal };
                menuItemHeader.Children.Add(new Label { Content = extension.Value.Name });

                var menuItem = new MenuItem { Header = menuItemHeader, ToolTip = extension.Value.Description, Tag = extension };
                menuItem.Click += ShowAddIn;
                menuAddins.Items.Add(menuItem);
            }
        }

        private void ShowAddIn(object sender, RoutedEventArgs e)
        {
            var mi = e.Source as MenuItem;
            var ext = mi.Tag as Lazy<ITextRedactorExtension>;

            FrameworkElement uiControl = ext.Value.GetUI();

            addInsContainer.Children.Remove(uiControl);
            addInsContainer.Children.Add(uiControl);
        }
    }
}