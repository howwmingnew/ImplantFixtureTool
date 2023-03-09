using Dsafa.WpfColorPicker;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace FixtureJsonGenerator
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty EditModeProperty = DependencyProperty.Register(
        "EditMode",
        typeof(bool),
        typeof(MainWindow),
        new FrameworkPropertyMetadata(null));
        public bool EditMode
        {
            get => (bool)GetValue(EditModeProperty);
            set => SetValue(EditModeProperty, value);
        }

        private Fixture.ImplantSystem.Implant sampleImplant;

        public MainWindow()
        {
            InitializeComponent();
            InitParams();
        }

        private void InitParams()
        {
            EditMode = false;
            textblockFilePath.Text = Properties.Settings.Default.lastJsonPath;
            gridSplitterH.Visibility = Visibility.Collapsed;
            borderAdd.Visibility = Visibility.Collapsed;
            buttonOpenFile.IsEnabled = false;
            DataContext = this;

            if (textblockFilePath.Text == string.Empty) { return; }
            buttonOpenFile.IsEnabled = true;
            LoadFixtureData(textblockFilePath.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button srcButton = sender as Button;
            switch (srcButton.Name)
            {
                case "buttonSelectFile":
                    {
                        var dialog = new Microsoft.Win32.OpenFileDialog();
                        dialog.FileName = "Fixture"; // Default file name
                        dialog.DefaultExt = ".json"; // Default file extension
                        dialog.Filter = "Fixture Json file (.json)|*.json"; // Filter files by extension

                        bool? result = dialog.ShowDialog();

                        if (result != true) { return; }
                        textblockFilePath.Text = dialog.FileName;
                        LoadFixtureData(dialog.FileName);
                        break;
                    }
                case "buttonAdd":
                    {
                        AskMessageBox dialog = new AskMessageBox();
                        dialog.Owner = (FixtureJsonGenerator.MainWindow)App.Current.MainWindow;
                        dialog.SetHeaderText("New Fixture Name:");
                        bool? result = dialog.ShowDialog();
                        if (result != true) { return; }

                        //SaveFileDialog
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Json file (*.json)|*.json";
                        if (saveFileDialog.ShowDialog() != true) { return; }

                        Properties.Settings.Default.lastJsonPath = saveFileDialog.FileName;
                        Properties.Settings.Default.Save();

                        Fixture fixture = new Fixture { Name = dialog.AddName };
                        string jsonString = JsonConvert.SerializeObject(fixture, Formatting.Indented);
                        File.WriteAllText(saveFileDialog.FileName, jsonString);
                        InitParams();

                        break;
                    }
                case "buttonSave":
                    {
                        //Check all value is vaild
                        bool checkIsVaild = CheckValueIsVaild();
                        if (!checkIsVaild) { MessageBox.Show("You have filled in the wrong information, the wrong information has been restored automatically."); return; }

                        ImplantDataGridMain.EditData(new Fixture.ImplantSystem.Implant
                        {
                            Name = textBoxName.Text == "Multi-selection" ? null : textBoxName.Text,
                            Platform = textBoxPlatform.Text == "Multi-selection" ? -1 : Convert.ToSingle(textBoxPlatform.Text),
                            Diameter = textBoxDiameter.Text == "Multi-selection" ? -1 : Convert.ToSingle(textBoxDiameter.Text),
                            Length = textBoxLength.Text == "Multi-selection" ? -1 : Convert.ToSingle(textBoxLength.Text),
                            Color = textBoxHex.Text == "Multi-selection" ? null : (SolidColorBrush)rectangleColor.Fill
                        });
                        break;
                    }
                case "buttonDelete":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you sure to delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result != MessageBoxResult.Yes) { return; }

                        ImplantDataGridMain.EditData(new Fixture.ImplantSystem.Implant
                        {
                            Name = "Delete",
                            Platform = -2,
                            Diameter = -2,
                            Length = -2,
                            Color = new SolidColorBrush(Colors.Transparent)
                        });
                        break;
                    }
                case "buttonOpenFile":
                    {
                        System.Diagnostics.Process.Start(Properties.Settings.Default.lastJsonPath);
                        break;
                    }
                case "buttonRefresh":
                    {
                        LoadFixtureData(Properties.Settings.Default.lastJsonPath);
                        break;
                    }
                default:
                    break;
            }
        }

        private void LoadFixtureData(string filePath)
        {
            EditMode = false;
            string jsonString;

            try
            {
                StreamReader streamReader = new StreamReader(filePath);
                jsonString = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch
            {
                MessageBox.Show("File cannot be read or does not exist, please check again.");

                Properties.Settings.Default.lastJsonPath = "";
                Properties.Settings.Default.Save();
                gridSplitterH.Visibility = Visibility.Collapsed;
                borderAdd.Visibility = Visibility.Collapsed;
                buttonOpenFile.IsEnabled = false;
                return;
            }

            Fixture importFixture = JsonConvert.DeserializeObject<Fixture>(jsonString);
            if (importFixture == null) { MessageBox.Show("File not support."); return; }

            Properties.Settings.Default.lastJsonPath = filePath;
            Properties.Settings.Default.Save();
            labelHeader.Content = importFixture.Name;
            ImplantDataGridMain.selectedImplantSystemIndex = -1;
            ImplantDataGridMain.InitUI(importFixture);
        }

        private void ImplantDataGridMain_SelectFromDataGrid(System.Collections.Generic.List<Fixture.ImplantSystem.Implant> implantList)
        {
            rectangleColor.IsHitTestVisible = true;
            textBoxName.IsEnabled = true;

            if (implantList.Count < 1)
            {
                gridSplitterH.Visibility = Visibility.Collapsed;
                borderAdd.Visibility = Visibility.Collapsed;
                return;
            }
            else if (implantList.Count == 1)
            {
                gridSplitterH.Visibility = Visibility.Visible;
                borderAdd.Visibility = Visibility.Visible;

                sampleImplant = implantList[0];
                textBoxName.Text = sampleImplant.Name;
                textBoxPlatform.Text = sampleImplant.Platform.ToString();
                textBoxDiameter.Text = sampleImplant.Diameter.ToString();
                textBoxLength.Text = sampleImplant.Length.ToString();
                rectangleColor.Fill = sampleImplant.Color;
                textBoxHex.Text = sampleImplant.Color.ToString().Substring(1);
                return;
            }

            gridSplitterH.Visibility = Visibility.Visible;
            borderAdd.Visibility = Visibility.Visible;

            sampleImplant = implantList[0];
            bool platformIsTheSame, diameterIsTheSame, lengthIsTheSame, colorIsTheSame;
            platformIsTheSame = diameterIsTheSame = lengthIsTheSame = colorIsTheSame = true;

            textBoxPlatform.Text = sampleImplant.Platform.ToString();
            textBoxDiameter.Text = sampleImplant.Diameter.ToString();
            textBoxLength.Text = sampleImplant.Length.ToString();
            textBoxHex.Text = sampleImplant.Color.ToString().Substring(1);
            rectangleColor.Fill = sampleImplant.Color;

            for (int i = 1; i < implantList.Count; i++)
            {
                platformIsTheSame = platformIsTheSame == false ? false : sampleImplant.Platform == implantList[i].Platform;
                diameterIsTheSame = diameterIsTheSame == false ? false : sampleImplant.Diameter == implantList[i].Diameter;
                lengthIsTheSame = lengthIsTheSame == false ? false : sampleImplant.Length == implantList[i].Length;
                colorIsTheSame = colorIsTheSame == false ? false : sampleImplant.Color.ToString() == implantList[i].Color.ToString();
            }

            SetTextBoxEnabled(textBoxName, false);
            SetTextBoxEnabled(textBoxPlatform, platformIsTheSame);
            SetTextBoxEnabled(textBoxDiameter, diameterIsTheSame);
            SetTextBoxEnabled(textBoxLength, lengthIsTheSame);
            SetTextBoxEnabled(textBoxHex, colorIsTheSame);

            if (!colorIsTheSame)
            {
                rectangleColor.Fill = new SolidColorBrush(Colors.Transparent);
                rectangleColor.IsHitTestVisible = false;
            }
        }

        private void SetTextBoxEnabled(TextBox textBox, bool IsTheSame)
        {
            if (!IsTheSame) { textBox.Text = "Multi-selection"; }
            if (textBox == textBoxName) { textBoxName.IsEnabled = false; }
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

        private bool CheckValueIsVaild()
        {
            float testFloat;

            if (textBoxPlatform.Text != "Multi-selection")
            {
                try
                { testFloat = Convert.ToSingle(textBoxPlatform.Text); }
                catch
                { textBoxPlatform.Text = sampleImplant.Platform.ToString(); }
            }

            if (textBoxDiameter.Text != "Multi-selection")
            {
                try
                { testFloat = Convert.ToSingle(textBoxDiameter.Text); }
                catch
                { textBoxDiameter.Text = sampleImplant.Diameter.ToString(); }
            }

            if (textBoxLength.Text != "Multi-selection")
            {
                try
                { testFloat = Convert.ToSingle(textBoxLength.Text); }
                catch
                { textBoxLength.Text = sampleImplant.Length.ToString(); }
            }

            if (textBoxHex.Text != "Multi-selection")
            {
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
            }

            return true;
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
                textBoxHex.Text = sampleImplant.Color.ToString();
                rectangleColor.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom($"#{textBoxHex.Text}");
            }
        }

        private void textBoxName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) { return; }

            Button_Click(buttonSave, null);
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class EditModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { return (bool)value ? Visibility.Visible : Visibility.Collapsed; }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { throw new NotSupportedException(); }
    }
}
