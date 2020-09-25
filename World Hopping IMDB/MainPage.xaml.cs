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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace World_Hopping_IMDB
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public double[] result = { 0, 0, 0, 0, 0, 0, 0 };

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
                }
                catch
                {
                    //make save file
                    Windows.Storage.StorageFile sampleFile =
                      await storageFolder.CreateFileAsync("save.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                }
            }
        }

        public async void newWorld()
        {
            string auth = tbxAuthor.Text;
            string world = tbxWorldName.Text;

            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.GetFileAsync("save.txt");


            //append the results to a string
            string results = "";
            foreach (double x in result)
            {
                results += x.ToString() + ", ";
            }
            results.Remove(results.Length - 1, 1);

            //add all info to a file in the order: Name, author, result
            string line = world + ", " + auth + ", " + results + "\n";
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
            newWorld();
            reset();
        }
    }
}
