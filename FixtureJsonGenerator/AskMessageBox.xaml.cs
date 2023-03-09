using System.Windows;
using System.Windows.Controls;

namespace FixtureJsonGenerator
{
    /// <summary>
    /// AskMessageBox.xaml 的互動邏輯
    /// </summary>
    public partial class AskMessageBox : Window
    {
        public string AddName;
        public AskMessageBox()
        {
            InitializeComponent();
            AddName = "New Implant System";
        }

        public void SetHeaderText(string text)
        {
            labelHeader.Content = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button srcButton = sender as Button;

            switch (srcButton.Name)
            {
                case "buttonOK":
                    {
                        if (textBoxMain.Text != string.Empty) { AddName = textBoxMain.Text; }
                        DialogResult = true; break;
                    }
                case "buttonCancel": { DialogResult = false; break; }
            }
        }
    }
}
