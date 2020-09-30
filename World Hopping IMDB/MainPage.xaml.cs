using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Schema;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.WebUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
/*using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;
using X15 = DocumentFormat.OpenXml.Office2013.Excel;
using Windows.Storage.AccessCache;*/

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace World_Hopping_IMDB
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        public double[] result = { 0, 0, 0, 0, 0, 0, 0};
        int counter = 1;
        public MainPage()
        {

            this.InitializeComponent();
            FileInit();
        }

        public async void FileInit()
        {
            string path = "save.txt";
            if (!File.Exists(path))
            {
                // Create sample file; replace if exists.

                Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                //check save exists
                try
                {
                    StorageFile file = await storageFolder.GetFileAsync("save.txt");
                    var lines = await Windows.Storage.FileIO.ReadLinesAsync(file);
                    //check file ver
                    string line = lines.First();
                    if (line != "1.19")
                    {
                        string outlines = "1.19\n";
                        foreach (string aline in lines)
                        {
                            outlines += aline + "\n";
                        }
                        await Windows.Storage.FileIO.WriteTextAsync(file, outlines);
                    }
                }
                catch
                {
                    //make save file
                    Windows.Storage.StorageFile sampleFile =
                      await storageFolder.CreateFileAsync("save.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                }
            }
        }

        public async void newWorld(string wnam, string waut, double eff, double edg, double scu, double lag, double fun, double thot, double tech)
        {

            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.GetFileAsync("save.txt");


            //append the results to a string
            string results = 
                eff.ToString() + ", " + edg.ToString() + ", " + scu.ToString() + ", " + lag.ToString() + ", " + 
                fun.ToString() + ", " + thot.ToString() + ", " + tech.ToString();

            //add all info to a file in the order: Name, author, result
            string line = wnam + ", " + waut + ", " + results + "\n";
            await Windows.Storage.FileIO.AppendTextAsync(sampleFile, line);

        }

        private void vRating_SelectionChanged(object sender, RoutedEventArgs e)
        {
        }


        public void reset(){
            technical.Value = 0;
            thottery.Value = 0;
            humor.Value = 0;
            scuff.Value = 0;
            lagginess.Value = 0;
            effort.Value = 0;
            edginess.Value = 0;
            tbxAuthor.Text = "";
            tbxWorldName.Text = "";
        }

        public void UpdateResult(double ipu, int type)
        {
            double total = 0;
            double sum = 0;
            result[type] = ipu;
            foreach(double value in result)
            {
                sum += value;
            }
            total = sum / 7;
            vRating.Text = total.ToString("0.0");
        }
        private void Slider_ValueChanged (object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateResult(e.NewValue, 0);
        }

        private void Edginess_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateResult(e.NewValue, 1);
        }

        private void Scuff_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateResult(e.NewValue, 2);
        }
        private void rctl_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateResult(e.NewValue, 2);
        }

        private void Lagginess_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateResult(e.NewValue, 3);
        }

        private void Humor_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateResult(e.NewValue, 4);
        }

        private void Thottery_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateResult(e.NewValue, 5);
        }

        private void Technical_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateResult(e.NewValue, 6);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newWorld(tbxWorldName.Text, tbxAuthor.Text, result[0], result[1], result[2], result[3], result[4], result[5], result[6]);
            reset();
            counter++;
            lblCounter.Text = counter.ToString();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
             //ExcelGenerate();
            
        }
       /* private async System.Threading.Tasks.Task ExcelGenerate()
        {

            TestModelList tmList = new TestModelList();
            tmList.testData = new List<TestModel>();
            TestModel tm = new TestModel();
            tm.TestId = 1;
            tm.TestName = "Test1";
            tm.TestDesc = "Tested 1 time";
            tm.TestDate = DateTime.Now.Date;
            tmList.testData.Add(tm);

            TestModel tm1 = new TestModel();
            tm1.TestId = 2;
            tm1.TestName = "Test2";
            tm1.TestDesc = "Tested 2 times";
            tm1.TestDate = DateTime.Now.AddDays(-1);
            tmList.testData.Add(tm1);

            TestModel tm2 = new TestModel();
            tm2.TestId = 3;
            tm2.TestName = "Test3";
            tm2.TestDesc = "Tested 3 times";
            tm2.TestDate = DateTime.Now.AddDays(-2);
            tmList.testData.Add(tm2);

            TestModel tm3 = new TestModel();
            tm3.TestId = 4;
            tm3.TestName = "Test4";
            tm3.TestDesc = "Tested 4 times";
            tm3.TestDate = DateTime.Now.AddDays(-3);
            tmList.testData.Add(tm);


            var datetime = DateTime.Now.ToString().Replace("/", "_").Replace(":", "_");
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                StorageFolder newFolder;

                newFolder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("PickedFolderToken");
                await newFolder.CreateFileAsync("PubCrawlIMDB.xlsx");
                //output file to this directory
                string fileFullName = Path.Combine(folder.Path, "PubCrawlIMDB.xlsx");



                /*
                ########################################### 




                ###########################################
                This area needs changing to UWP formatting. 
                ###########################################
                
                


                ###########################################
                 */
       /*
                if (File.Exists(fileFullName))
                {
                    fileFullName = Path.Combine(folder.Path, "Output_" + datetime + ".xlsx");
                }

                using (SpreadsheetDocument package = SpreadsheetDocument.Create(fileFullName, SpreadsheetDocumentType.Workbook))
                {
                    CreatePartsForExcel(package, tmList);
                }
            }
            else
            {
            }
        }

        public class TestModelList
        {
            public List<TestModel> testData { get; set; }
        }
        public class TestModel
        {
            public int TestId { get; set; }
            public string TestName { get; set; }
            public string TestDesc { get; set; }
            public DateTime TestDate { get; set; }
        }
        private void CreatePartsForExcel(SpreadsheetDocument document, TestModelList data)
        {
            SheetData partSheetData = GenerateSheetdataForDetails(data);

            WorkbookPart workbookPart1 = document.AddWorkbookPart();
            GenerateWorkbookPartContent(workbookPart1);

            WorkbookStylesPart workbookStylesPart1 = workbookPart1.AddNewPart<WorkbookStylesPart>("rId3");
            GenerateWorkbookStylesPartContent(workbookStylesPart1);

            WorksheetPart worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>("rId1");
            GenerateWorksheetPartContent(worksheetPart1, partSheetData);
        }
        private void GenerateWorkbookPartContent(WorkbookPart workbookPart1)
        {
            Workbook workbook1 = new Workbook();
            Sheets sheets1 = new Sheets();
            Sheet sheet1 = new Sheet() { Name = "Sheet1", SheetId = (UInt32Value)1U, Id = "rId1" };
            sheets1.Append(sheet1);
            workbook1.Append(sheets1);
            workbookPart1.Workbook = workbook1;
        }

        private void GenerateWorksheetPartContent(WorksheetPart worksheetPart1, SheetData sheetData1)
        {
            Worksheet worksheet1 = new Worksheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            worksheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            worksheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");
            SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1" };

            SheetViews sheetViews1 = new SheetViews();

            SheetView sheetView1 = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            Selection selection1 = new Selection() { ActiveCell = "A1", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1" } };

            sheetView1.Append(selection1);

            sheetViews1.Append(sheetView1);
            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties() { DefaultRowHeight = 15D, DyDescent = 0.25D };

            PageMargins pageMargins1 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };
            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(pageMargins1);
            worksheetPart1.Worksheet = worksheet1;
        }
        private void GenerateWorkbookStylesPartContent(WorkbookStylesPart workbookStylesPart1)
        {
            Stylesheet stylesheet1 = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)2U, KnownFonts = true };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            Color color1 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);

            Font font2 = new Font();
            Bold bold1 = new Bold();
            FontSize fontSize2 = new FontSize() { Val = 11D };
            Color color2 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName2 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            font2.Append(bold1);
            font2.Append(fontSize2);
            font2.Append(color2);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);
            font2.Append(fontScheme2);

            fonts1.Append(font1);
            fonts1.Append(font2);

            Fills fills1 = new Fills() { Count = (UInt32Value)2U };

            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };

            fill1.Append(patternFill1);

            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };

            fill2.Append(patternFill2);

            fills1.Append(fill1);
            fills1.Append(fill2);

            Borders borders1 = new Borders() { Count = (UInt32Value)2U };

            DocumentFormat.OpenXml.Spreadsheet.Border border1 = new DocumentFormat.OpenXml.Spreadsheet.Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            DocumentFormat.OpenXml.Spreadsheet.Border border2 = new DocumentFormat.OpenXml.Spreadsheet.Border();

            LeftBorder leftBorder2 = new LeftBorder() { Style = BorderStyleValues.Thin };
            Color color3 = new Color() { Indexed = (UInt32Value)64U };

            leftBorder2.Append(color3);

            RightBorder rightBorder2 = new RightBorder() { Style = BorderStyleValues.Thin };
            Color color4 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder2.Append(color4);

            TopBorder topBorder2 = new TopBorder() { Style = BorderStyleValues.Thin };
            Color color5 = new Color() { Indexed = (UInt32Value)64U };

            topBorder2.Append(color5);

            BottomBorder bottomBorder2 = new BottomBorder() { Style = BorderStyleValues.Thin };
            Color color6 = new Color() { Indexed = (UInt32Value)64U };

            bottomBorder2.Append(color6);
            DiagonalBorder diagonalBorder2 = new DiagonalBorder();

            border2.Append(leftBorder2);
            border2.Append(rightBorder2);
            border2.Append(topBorder2);
            border2.Append(bottomBorder2);
            border2.Append(diagonalBorder2);

            borders1.Append(border1);
            borders1.Append(border2);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)3U };
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyBorder = true };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true };

            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleLight16" };

            StylesheetExtensionList stylesheetExtensionList1 = new StylesheetExtensionList();

            StylesheetExtension stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            X14.SlicerStyles slicerStyles1 = new X14.SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" };

            stylesheetExtension1.Append(slicerStyles1);

            StylesheetExtension stylesheetExtension2 = new StylesheetExtension() { Uri = "{9260A510-F301-46a8-8635-F512D64BE5F5}" };
            stylesheetExtension2.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            X15.TimelineStyles timelineStyles1 = new X15.TimelineStyles() { DefaultTimelineStyle = "TimeSlicerStyleLight1" };

            stylesheetExtension2.Append(timelineStyles1);

            stylesheetExtensionList1.Append(stylesheetExtension1);
            stylesheetExtensionList1.Append(stylesheetExtension2);

            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(stylesheetExtensionList1);

            workbookStylesPart1.Stylesheet = stylesheet1;
        }
        private SheetData GenerateSheetdataForDetails(TestModelList data)
        {
            SheetData sheetData1 = new SheetData();
            sheetData1.Append(CreateHeaderRowForExcel());

            foreach (TestModel testmodel in data.testData)
            {
                Row partsRows = GenerateRowForChildPartDetail(testmodel);
                sheetData1.Append(partsRows);
            }
            return sheetData1;
        }
        private Row CreateHeaderRowForExcel()
        {
            Row workRow = new Row();
            workRow.Append(CreateCell("Test Id", 2U));
            workRow.Append(CreateCell("Test Name", 2U));
            workRow.Append(CreateCell("Test Description", 2U));
            workRow.Append(CreateCell("Test Date", 2U));
            return workRow;
        }
        private Row GenerateRowForChildPartDetail(TestModel testmodel)
        {
            Row tRow = new Row();
            tRow.Append(CreateCell(testmodel.TestId.ToString()));
            tRow.Append(CreateCell(testmodel.TestName));
            tRow.Append(CreateCell(testmodel.TestDesc));
            tRow.Append(CreateCell(testmodel.TestDate.ToShortDateString()));

            return tRow;
        }
        private Cell CreateCell(string text)
        {
            Cell cell = new Cell();
            cell.StyleIndex = 1U;
            cell.DataType = ResolveCellDataTypeOnValue(text);
            cell.CellValue = new CellValue(text);
            return cell;
        }
        private Cell CreateCell(string text, uint styleIndex)
        {
            Cell cell = new Cell();
            cell.StyleIndex = styleIndex;
            cell.DataType = ResolveCellDataTypeOnValue(text);
            cell.CellValue = new CellValue(text);
            return cell;
        }
        private EnumValue<CellValues> ResolveCellDataTypeOnValue(string text)
        {
            int intVal;
            double doubleVal;
            if (int.TryParse(text, out intVal) || double.TryParse(text, out doubleVal))
            {
                return CellValues.Number;
            }
            else
            {
                return CellValues.String;
            }
        }
        */


        /*end of file*/
    }
}
