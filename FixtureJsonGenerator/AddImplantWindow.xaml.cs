using Dsafa.WpfColorPicker;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FixtureJsonGenerator
{
    /// <summary>
    /// AddImplantWindow.xaml 的互動邏輯
    /// </summary>
    public partial class AddImplantWindow : Window
    {
        public Fixture.ImplantSystem.Implant Implant;

        public AddImplantWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button srcButton = sender as Button;

            switch (srcButton.Name)
            {
                case "buttonAdd":
                    {
                        bool checkIsVaild = CheckValueIsVaild();
                        if (!checkIsVaild) { MessageBox.Show("You have filled in the wrong information, the wrong information has been restored automatically."); return; }

                        Implant = new Fixture.ImplantSystem.Implant
                        {
                            Name = textBoxName.Text == "Multi-selection" ? null : textBoxName.Text,
                            Platform = textBoxPlatform.Text == "Multi-selection" ? -1 : Convert.ToSingle(textBoxPlatform.Text),
                            Diameter = textBoxDiameter.Text == "Multi-selection" ? -1 : Convert.ToSingle(textBoxDiameter.Text),
                            Length = textBoxLength.Text == "Multi-selection" ? -1 : Convert.ToSingle(textBoxLength.Text),
                            Color = textBoxHex.Text == "Multi-selection" ? null : (SolidColorBrush)rectangleColor.Fill
                        };

                        DialogResult = true;
                        break;
                    }
                case "buttonCancel":
                    {
                        DialogResult = false;
                        break;
                    }
            }
        }

        private bool CheckValueIsVaild()
        {
            Fixture.ImplantSystem.Implant sampleImplant = new Fixture.ImplantSystem.Implant();
            float testFloat;

            try
            { testFloat = Convert.ToSingle(textBoxPlatform.Text); }
            catch
            { textBoxHex.Text = sampleImplant.Platform.ToString(); }

            try
            { testFloat = Convert.ToSingle(textBoxDiameter.Text); }
            catch
            { textBoxDiameter.Text = sampleImplant.Diameter.ToString(); }

            try
            { testFloat = Convert.ToSingle(textBoxLength.Text); }
            catch
            { textBoxLength.Text = sampleImplant.Length.ToString(); }

            try
            {
                rectangleColor.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom($"#{textBoxHex.Text}");
            }
            catch
            {
                textBoxHex.Text = sampleImplant.Color.ToString();
                rectangleColor.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom($"#{textBoxHex.Text}");
                return false;
            }

            return true;
        }

        private void rectangleColor_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)rectangleColor.Fill;
            if (brush.Color.A == 0) { brush = new SolidColorBrush(Color.FromArgb(255, brush.Color.R, brush.Color.G, brush.Color.B)); }
            ColorPickerDialog dialog = new ColorPickerDialog(brush.Color);
            dialog.Owner = this;
            var result = dialog.ShowDialog();
            if (!result.HasValue) { return; }

            rectangleColor.Fill = new SolidColorBrush(dialog.Color);
            textBoxHex.Text = rectangleColor.Fill.ToString().Substring(1);
        }

        private void textBoxHex_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            try
            {
                rectangleColor.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom($"#{textBoxHex.Text}");
            }
            catch
            {
                rectangleColor.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom($"#FF000000");
            }
        }
    }
}
