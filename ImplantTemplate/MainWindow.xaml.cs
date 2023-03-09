using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace ImplantTemplate
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        Fixture importFixture;

        public MainWindow()
        {
            InitializeComponent();
            InitPrarms();
        }

        private void InitPrarms()
        {
            //Import json
            StreamReader streamReader = new StreamReader(@"D:\Fixture.json");
            string jsonString = streamReader.ReadToEnd();
            streamReader.Close();
            importFixture = JsonConvert.DeserializeObject<Fixture>(jsonString);
            stackPanel_ImplantSystem.Children.Clear();

            if (importFixture.ImplantSystemList.Count < 1) { return; }
            labelHeader.Content = importFixture.Name;
            foreach (Fixture.ImplantSystem implantSystem in importFixture.ImplantSystemList)
            {
                ImplantSystemItem implantSystemItem = new ImplantSystemItem();
                implantSystemItem.Text = implantSystem.Name;
                implantSystemItem.SelectedFromISItem +=ImplantSystemItem_SelectedFromISItem;
                stackPanel_ImplantSystem.Children.Add(implantSystemItem);
            }

            textBlockName.Text = string.Empty;
            textBlockPlatform.Text = string.Empty;
            textBlockDiameter.Text = string.Empty;
            textBlockLength.Text = string.Empty;
        }

        private void ImplantSystemItem_SelectedFromISItem(ImplantSystemItem item)
        {
            if (stackPanel_ImplantSystem.Children.Count < 1) { return; }
            foreach (ImplantSystemItem implantSystemItem in stackPanel_ImplantSystem.Children)
            {
                if (implantSystemItem == item) { continue; }
                implantSystemItem.IsSelected = false;
            }

            implantChart.InitParams(importFixture.ImplantSystemList[importFixture.ImplantSystemList.FindIndex(w => w.Name == item.Text)]);

            textBlockName.Text = string.Empty;
            textBlockPlatform.Text = string.Empty;
            textBlockDiameter.Text = string.Empty;
            textBlockLength.Text = string.Empty;
        }

        private void implantChart_ClickFromImplantChart(Fixture.ImplantSystem.Implant implant)
        {
            textBlockName.Text = implant.Name;
            textBlockPlatform.Text = implant.Platform.ToString();
            textBlockDiameter.Text = implant.Diameter.ToString();
            textBlockLength.Text = implant.Length.ToString();
        }
    }
}
