using TextReader.Contracts;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel.Composition;

namespace ChangeColorExt
{
    /// <summary>
    /// Interaction logic for ChangeColorUserControl.xaml
    /// </summary>
    [Export(typeof(ITextRedactorExtension))]
    public partial class ChangeColorUserControl : UserControl, ITextRedactorExtension
    {
        [Import(typeof(ITextReader))]
        ITextReader textRedactor;
       
        public ChangeColorUserControl()
        {
            InitializeComponent();
        }

        public ChangeColorUserControl(ITextReader textReader)
        {
            InitializeComponent();

            this.textRedactor = textReader;
        }

        private void body_SelctionChanged(object seder, RoutedEventArgs e)
        {
            //update the tool bar
        }

        private void Highlight_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush scb = new SolidColorBrush();
            textRedactor.ApplyToSelection(
                                  TextBlock.ForegroundProperty,
                                  new SolidColorBrush(colorPicker())
                                  );
        }
      
        private void Color_Click(object sender, RoutedEventArgs e)
        {
            textRedactor.ApplyToSelection(
                                  TextBlock.BackgroundProperty,
                                  new SolidColorBrush(colorPicker())
                                  );
        }

        private System.Windows.Media.Color colorPicker()
        {
            System.Windows.Forms.ColorDialog colorDialog =
                       new System.Windows.Forms.ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.ShowDialog();

            System.Windows.Media.Color col = new System.Windows.Media.Color();
            col.A = colorDialog.Color.A;
            col.B = colorDialog.Color.B;
            col.G = colorDialog.Color.G;
            col.R = colorDialog.Color.R;
            return col;
        }

        public FrameworkElement GetUI()
        {
            return this;
        }

        #region About AddIn
        public string AuthorName
        {
            get { return "CBS_Team"; }
        }

        public string Description
        {
            get { return "Плагин добавляет возможности форматирования текста."; }
        }

        public string Name
        {
            get { return "ChangeColorExt"; }
        }

        public Version Version
        {
            get { return new Version(1, 0, 0, 0); }
        }
        #endregion
    }
}

