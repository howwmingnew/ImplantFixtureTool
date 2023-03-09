using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ImplantTemplate
{
    /// <summary>
    /// ImplantChart.xaml 的互動邏輯 --------------- Column超過8個就要再加
    /// </summary>
    public partial class ImplantChart : UserControl
    {
        public delegate void ClickHandler(Fixture.ImplantSystem.Implant implant);
        public event ClickHandler ClickFromImplantChart;

        DataGridTemplateColumn[] dgColumnList;
        Fixture.ImplantSystem focusedImplantSystem;

        private class PD
        {
            public float Platform { get; set; }
            public float Diameter { get; set; }
        }

        public class MyDataModel
        {
            public object Col_1 { get; set; }
            public object Col_2 { get; set; }
            public object Col_3 { get; set; }
            public object Col_4 { get; set; }
            public object Col_5 { get; set; }
            public object Col_6 { get; set; }
            public object Col_7 { get; set; }
            public object Col_8 { get; set; }
            public object Col_9 { get; set; }

            public object[] GetColArray()
            { return new object[9] { Col_1, Col_2, Col_3, Col_4, Col_5, Col_6, Col_7, Col_8, Col_9 }; }
        }

        private void DataGridCell_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            int index = cell.TabIndex;
            MyDataModel data = cell?.DataContext as MyDataModel;
            string colValue = data.GetColArray()[index].ToString();

            Fixture.ImplantSystem.Implant focusItem = focusedImplantSystem.ImplantList.Where( c => c.Name != string.Empty ).Where( c => c.Name == colValue).FirstOrDefault();
            if (focusItem == null) { return; }

            ClickFromImplantChart?.Invoke(focusItem);
        }

        public ImplantChart()
        {
            InitializeComponent();

            dgColumnList = new DataGridTemplateColumn[] { dgColumn_2, dgColumn_3, dgColumn_4, dgColumn_5, dgColumn_6, dgColumn_7, dgColumn_8, dgColumn_9 };
        }

        public void InitParams(Fixture.ImplantSystem implantSystem)
        {
            if (implantSystem.ImplantList.Count < 1) { MessageBox.Show("No implant data."); return; }
            focusedImplantSystem = implantSystem;
            for (int i = 0; i < 8; i++)
            { dgColumnList[i].Visibility = Visibility.Visible; }
            MainDataGrid.Items.Clear();
            List<List<Fixture.ImplantSystem.Implant>> lengthSortList = new List<List<Fixture.ImplantSystem.Implant>>();  //依Length做區分
            List<PD> platformSpec = new List<PD>();   //[3.40, 2.50] [3.80, 2.80]

            void CheckPD(Fixture.ImplantSystem.Implant implant)
            {
                bool isExist = false;
                foreach (PD item in platformSpec)
                {
                    if ((item.Platform == implant.Platform) && (item.Diameter == implant.Diameter))
                    { isExist = true; break; }
                }

                if (isExist) { return; }
                platformSpec.Add(new PD { Platform = implant.Platform, Diameter = implant.Diameter });
                platformSpec = platformSpec.OrderBy(x => x.Platform).ToList();
            }

            object GetCellValue(List<Fixture.ImplantSystem.Implant> implantList, float targetPlatform, float targetDiameter)
            {
                if ((targetPlatform < 0) || (targetDiameter < 0)) { return ""; }

                foreach (Fixture.ImplantSystem.Implant implant in implantList)
                {
                    if (implant.Platform != targetPlatform) { continue; }
                    if (implant.Diameter != targetDiameter) { continue; }

                    return implant.Name;
                }
                return "";
            }

            foreach (Fixture.ImplantSystem.Implant implant in implantSystem.ImplantList)
            {
                if (lengthSortList.Count < 1)   //First time
                {
                    List<Fixture.ImplantSystem.Implant> firstImplantList = new List<Fixture.ImplantSystem.Implant>
                    {
                        new Fixture.ImplantSystem.Implant {
                            Name = implant.Name,
                            Platform = implant.Platform,
                            Diameter = implant.Diameter,
                            Length = implant.Length
                        }
                    };
                    lengthSortList.Add(firstImplantList);
                    platformSpec.Add(new PD { Platform = implant.Platform, Diameter = implant.Diameter });
                    continue;
                }

                //Compare Length
                for (int i = 0; i < lengthSortList.Count; i++)
                {
                    List<Fixture.ImplantSystem.Implant> implantList = lengthSortList[i];

                    if (implantList[0].Length == implant.Length)
                    {
                        implantList.Add(implant);
                        CheckPD(implant);
                        implantList = implantList.OrderBy(x => x.Platform).ThenBy(x => x.Diameter).ToList();
                        break;
                    }
                    else if (i == lengthSortList.Count - 1) //no equal length array
                    {
                        List<Fixture.ImplantSystem.Implant> addImplantList = new List<Fixture.ImplantSystem.Implant>
                        {
                            new Fixture.ImplantSystem.Implant {
                                Name = implant.Name,
                                Platform = implant.Platform,
                                Diameter = implant.Diameter,
                                Length = implant.Length
                            }
                        };
                        lengthSortList.Add(addImplantList);
                        CheckPD(implant);
                        break;
                    }
                }
            }

            //Sort
            lengthSortList = lengthSortList.OrderBy(x => x[0].Length).ToList();

            //Hide not necessary's columns
            for (int i = platformSpec.Count; i < dgColumnList.Length; i++)
            { dgColumnList[i].Visibility = Visibility.Collapsed; }

            for (int i = -2; i < lengthSortList.Count; i++)
            {
                int lastIndex = platformSpec.Count - 1;
                MyDataModel myDataModel;
                if (i == -2)  //第一行
                {
                    myDataModel = new MyDataModel
                    {
                        Col_1 = "P",
                        Col_2 = lastIndex < 0 ? 0 : platformSpec[0].Platform,
                        Col_3 = lastIndex < 1 ? 0 : platformSpec[1].Platform,
                        Col_4 = lastIndex < 2 ? 0 : platformSpec[2].Platform,
                        Col_5 = lastIndex < 3 ? 0 : platformSpec[3].Platform,
                        Col_6 = lastIndex < 4 ? 0 : platformSpec[4].Platform,
                        Col_7 = lastIndex < 5 ? 0 : platformSpec[5].Platform,
                        Col_8 = lastIndex < 6 ? 0 : platformSpec[6].Platform,
                        Col_9 = lastIndex < 7 ? 0 : platformSpec[7].Platform
                    };
                }
                else if (i == -1)  //第二行
                {
                    myDataModel = new MyDataModel
                    {
                        Col_1 = "Ø",
                        Col_2 = lastIndex < 0 ? 0 : platformSpec[0].Diameter,
                        Col_3 = lastIndex < 1 ? 0 : platformSpec[1].Diameter,
                        Col_4 = lastIndex < 2 ? 0 : platformSpec[2].Diameter,
                        Col_5 = lastIndex < 3 ? 0 : platformSpec[3].Diameter,
                        Col_6 = lastIndex < 4 ? 0 : platformSpec[4].Diameter,
                        Col_7 = lastIndex < 5 ? 0 : platformSpec[5].Diameter,
                        Col_8 = lastIndex < 6 ? 0 : platformSpec[6].Diameter,
                        Col_9 = lastIndex < 7 ? 0 : platformSpec[7].Diameter
                    };
                }
                else
                {
                    float targetLength = lengthSortList[i][0].Length;
                    myDataModel = new MyDataModel
                    {
                        Col_1 = targetLength,
                        Col_2 = GetCellValue(lengthSortList[i], lastIndex < 0 ? -1 : platformSpec[0].Platform, lastIndex < 0 ? -1 : platformSpec[0].Diameter),
                        Col_3 = GetCellValue(lengthSortList[i], lastIndex < 1 ? -1 : platformSpec[1].Platform, lastIndex < 1 ? -1 : platformSpec[1].Diameter),
                        Col_4 = GetCellValue(lengthSortList[i], lastIndex < 2 ? -1 : platformSpec[2].Platform, lastIndex < 2 ? -1 : platformSpec[2].Diameter),
                        Col_5 = GetCellValue(lengthSortList[i], lastIndex < 3 ? -1 : platformSpec[3].Platform, lastIndex < 3 ? -1 : platformSpec[3].Diameter),
                        Col_6 = GetCellValue(lengthSortList[i], lastIndex < 4 ? -1 : platformSpec[4].Platform, lastIndex < 4 ? -1 : platformSpec[4].Diameter),
                        Col_7 = GetCellValue(lengthSortList[i], lastIndex < 5 ? -1 : platformSpec[5].Platform, lastIndex < 5 ? -1 : platformSpec[5].Diameter),
                        Col_8 = GetCellValue(lengthSortList[i], lastIndex < 6 ? -1 : platformSpec[6].Platform, lastIndex < 6 ? -1 : platformSpec[6].Diameter),
                        Col_9 = GetCellValue(lengthSortList[i], lastIndex < 7 ? -1 : platformSpec[7].Platform, lastIndex < 7 ? -1 : platformSpec[7].Diameter)
                    };
                }

                AddDataRow(myDataModel);
            }
        }

        private void AddDataRow(MyDataModel myDataModel)
        { MainDataGrid.Items.Add(new DataGridRow() { Item = myDataModel }); }


    }
}
