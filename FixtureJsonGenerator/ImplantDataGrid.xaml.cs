using System.Collections.ObjectModel;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Documents;
using System.Windows;

namespace FixtureJsonGenerator
{
    /// <summary>
    /// ImplantDataGrid.xaml 的互動邏輯
    /// </summary>
    public partial class ImplantDataGrid : UserControl
    {
        public delegate void ClickHandler(List<Fixture.ImplantSystem.Implant> implantList);
        public event ClickHandler SelectFromDataGrid;

        Fixture CurrentFixture { get; set; }
        public int selectedImplantSystemIndex = -1;

        class ImplantDataGridData
        {
            public string Name { get; set; }
            public float Platform { get; set; }
            public float Diameter { get; set; }
            public float Length { get; set; }
            public SolidColorBrush ColorBrush { get; set; }
            public string ColorText { get; set; }
        }

        public ImplantDataGrid()
        {
            InitializeComponent();
        }

        public void InitUI(Fixture fixture = null)
        {
            CurrentFixture = fixture == null ? CurrentFixture : fixture;
            wrapPanelMain.Children.Clear();
            dataGridMain.Items.Clear();
            labelTotal.Content = "0";

            if (CurrentFixture.ImplantSystemList.Count < 1)
            {
                buttonRemove.Visibility = Visibility.Collapsed;
                stackPanelImplant.Visibility = Visibility.Collapsed;
                return;
            }

            buttonRemove.Visibility = Visibility.Visible;
            stackPanelImplant.Visibility = Visibility.Visible;

            foreach (Fixture.ImplantSystem implantSystem in CurrentFixture.ImplantSystemList)
            {
                RadioButton radioButton = new RadioButton{ Content = implantSystem.Name };
                radioButton.Checked +=RadioButton_Checked;
                wrapPanelMain.Children.Add(radioButton);
            }

            RadioButton firstRadioButton = wrapPanelMain.Children[selectedImplantSystemIndex == -1 ? 0 : selectedImplantSystemIndex] as RadioButton;
            firstRadioButton.IsChecked = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            selectedImplantSystemIndex = -1;
            labelTotal.Content = "0";
            dataGridMain.Items.Clear();
            string implantSystemName = (sender as RadioButton).Content.ToString();
            Fixture.ImplantSystem implantSystem = null;
            for (int i = 0; i < wrapPanelMain.Children.Count; i++)
            {
                RadioButton srcRadioButton = wrapPanelMain.Children[i] as RadioButton;
                if (srcRadioButton.IsChecked != true) { continue; }
                implantSystem = CurrentFixture.ImplantSystemList.Find(x => x.Name == srcRadioButton.Content.ToString());
                if (implantSystem == null) { continue; }

                selectedImplantSystemIndex = i;
                break;
            }
            if (implantSystem == null) { return; }

            foreach (Fixture.ImplantSystem.Implant implant in implantSystem.ImplantList)
            { dataGridMain.Items.Add(new DataGridRow() { Item = DataGridDataConverter(implant) }); }
            labelTotal.Content = dataGridMain.Items.Count.ToString();
        }

        private void dataGridMain_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            List<Fixture.ImplantSystem.Implant> selectedImplants = new List<Fixture.ImplantSystem.Implant>();

            foreach (DataGridRow dataGridRow in dataGridMain.SelectedItems)
            {
                ImplantDataGridData implant = dataGridRow.Item as ImplantDataGridData;
                selectedImplants.Add(DataGridDataConverter(implant));
            }

            SelectFromDataGrid?.Invoke(selectedImplants);
        }

        public void EditData(Fixture.ImplantSystem.Implant fixedImplantInfo)
        {
            int editCounter = 0;

            if ((fixedImplantInfo.Platform == -2) &&
                    (fixedImplantInfo.Diameter == -2) &&
                    (fixedImplantInfo.Length == -2))
            {
                List<DataGridRow> delData = new List<DataGridRow>();
                //Delete info
                foreach (DataGridRow dataGridRow in dataGridMain.SelectedItems)
                {
                    ImplantDataGridData implant = dataGridRow.Item as ImplantDataGridData;
                    Fixture.ImplantSystem.Implant fixedImplant = CurrentFixture.ImplantSystemList[selectedImplantSystemIndex].ImplantList.Find(x => x.Name == implant.Name);
                    if (fixedImplant == null) { continue; }

                    CurrentFixture.ImplantSystemList[selectedImplantSystemIndex].ImplantList.Remove(fixedImplant);
                    delData.Add(dataGridRow);
                }
                foreach (DataGridRow row in delData)
                { dataGridMain.Items.Remove(row); }

                SaveJsonFile();
                return;
            }
            else
            {
                if (dataGridMain.SelectedItems.Count > 1)
                {
                    foreach (DataGridRow dataGridRow in dataGridMain.SelectedItems)
                    {
                        ImplantDataGridData implant = dataGridRow.Item as ImplantDataGridData;
                        Fixture.ImplantSystem.Implant fixedImplant = CurrentFixture.ImplantSystemList[selectedImplantSystemIndex].ImplantList.Find(x => x.Name == implant.Name);
                        if (fixedImplant == null) { continue; }

                        fixedImplant.Platform = fixedImplantInfo.Platform == -1 ? fixedImplant.Platform : fixedImplantInfo.Platform;
                        fixedImplant.Diameter = fixedImplantInfo.Diameter == -1 ? fixedImplant.Diameter : fixedImplantInfo.Diameter;
                        fixedImplant.Length = fixedImplantInfo.Length == -1 ? fixedImplant.Length : fixedImplantInfo.Length;
                        fixedImplant.Color = fixedImplantInfo.Color == null ? fixedImplant.Color : fixedImplantInfo.Color;

                        dataGridRow.Item = (ImplantDataGridData)DataGridDataConverter(fixedImplant);
                        editCounter++;
                    }
                }
                else
                {
                    DataGridRow dataGridRow = dataGridMain.SelectedItems[0] as DataGridRow;
                    ImplantDataGridData implant = dataGridRow.Item as ImplantDataGridData;
                    Fixture.ImplantSystem.Implant fixedImplant = CurrentFixture.ImplantSystemList[selectedImplantSystemIndex].ImplantList.Find(x => x.Name == implant.Name);
                    if (fixedImplant == null) { return; }

                    fixedImplant.Name = fixedImplantInfo.Name == string.Empty ? fixedImplant.Name : fixedImplantInfo.Name;
                    fixedImplant.Platform = fixedImplantInfo.Platform == -1 ? fixedImplant.Platform : fixedImplantInfo.Platform;
                    fixedImplant.Diameter = fixedImplantInfo.Diameter == -1 ? fixedImplant.Diameter : fixedImplantInfo.Diameter;
                    fixedImplant.Length = fixedImplantInfo.Length == -1 ? fixedImplant.Length : fixedImplantInfo.Length;
                    fixedImplant.Color = fixedImplantInfo.Color == null ? fixedImplant.Color : fixedImplantInfo.Color;
                    dataGridRow.Item = (ImplantDataGridData)DataGridDataConverter(fixedImplant);
                }
            }

            bool saveResult = SaveJsonFile();
            if (!saveResult) { return; }
        }

        private ImplantDataGridData DataGridDataConverter(Fixture.ImplantSystem.Implant implant)
        {
            return new ImplantDataGridData
            {
                Name = implant.Name,
                Platform = implant.Platform,
                Diameter = implant.Diameter,
                Length = implant.Length,
                ColorBrush = implant.Color,
                ColorText = implant.Color.ToString()
            };
        }

        private Fixture.ImplantSystem.Implant DataGridDataConverter(ImplantDataGridData implant)
        {
            return new Fixture.ImplantSystem.Implant
            {
                Name = implant.Name,
                Platform = implant.Platform,
                Diameter = implant.Diameter,
                Length = implant.Length,
                Color = implant.ColorBrush
            };
        }

        private bool SaveJsonFile()
        {
            string jsonPath = Properties.Settings.Default.lastJsonPath;
            if (!File.Exists(jsonPath)) { return false; }

            string jsonString = JsonConvert.SerializeObject(CurrentFixture, Formatting.Indented);
            File.WriteAllText(jsonPath, jsonString);
            return true;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Button srcButton = sender as Button;

            switch(srcButton.Name)
            {
                case "buttonAdd":
                    {
                        AskMessageBox dialog = new AskMessageBox();
                        dialog.Owner = (FixtureJsonGenerator.MainWindow)App.Current.MainWindow;
                        bool? result = dialog.ShowDialog();
                        if (result != true) { return; }

                        CurrentFixture.ImplantSystemList.Add(new Fixture.ImplantSystem() { Name = dialog.AddName});
                        SaveJsonFile();
                        InitUI();

                        break;
                    }
                case "buttonRemove":
                    {
                        if (selectedImplantSystemIndex < 0) { return; }

                        MessageBoxResult result = MessageBox.Show($"Are you sure to delete {CurrentFixture.ImplantSystemList[selectedImplantSystemIndex].Name}", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result != MessageBoxResult.Yes) { return; }

                        CurrentFixture.ImplantSystemList.RemoveAt(selectedImplantSystemIndex);
                        SaveJsonFile();
                        selectedImplantSystemIndex = -1;
                        InitUI();

                        break;
                    }
                case "buttonImplantAdd":
                    {
                        AddImplantWindow addImplantWindow = new AddImplantWindow();
                        addImplantWindow.Owner = (FixtureJsonGenerator.MainWindow)App.Current.MainWindow;
                        bool? result = addImplantWindow.ShowDialog();
                        if (result != true) { return; }

                        CurrentFixture.ImplantSystemList[selectedImplantSystemIndex].ImplantList.Add(addImplantWindow.Implant);
                        SaveJsonFile();
                        InitUI();

                        break;
                    }
            }
        }
    }
}
