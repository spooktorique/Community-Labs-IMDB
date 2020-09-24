using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Schema;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        }

        private void vRating_SelectionChanged(object sender, RoutedEventArgs e)
        {
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
            technical.Value = 0;
            thottery.Value = 0;
            humor.Value = 0;
            scuff.Value = 0;
            lagginess.Value = 0;
            effort.Value = 0;
            edginess.Value = 0;
        }
    }
}
