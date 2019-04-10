using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410
namespace TomatoTimer
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    /// 

    public class AnotherPagePayload
    {
        public int TotCicli { get; set; }
        public double DurataCiclo { get; set; }
        public double DurataPausa { get; set; }
    }

    public sealed partial class MainPage : Page
    {
        List<Int32> Numbers = new List<Int32>();

        public static int NumberChoice { get; set; } = 0;
        public  TimeSpan? CicleLength { get => cicleLength; set => cicleLength = value; }
        public  TimeSpan? PauseLength { get => pauseLength; set => pauseLength = value; }

        private static TimeSpan? pauseLength = null;
        private static TimeSpan? cicleLength = null;
        public MainPage()
        {
            this.InitializeComponent();

            for (Int32 i=1; i<7; i++)
            {
                Numbers.Add(i);
            }

            cicleTime.SelectedTime = TimeSpan.FromMinutes(25);
            pauseTime.SelectedTime = TimeSpan.FromMinutes(5);
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NumberChoice != 0 && pauseLength != null && cicleLength != null)
            {
                AnotherPagePayload payload = new AnotherPagePayload();
                payload.TotCicli = NumberChoice;
                TimeSpan NonNullableTs;
                NonNullableTs = (TimeSpan) CicleLength;
                payload.DurataCiclo = NonNullableTs.TotalSeconds;
                NonNullableTs = (TimeSpan) PauseLength;
                payload.DurataPausa = NonNullableTs.TotalSeconds;
                this.Frame.Navigate(typeof(Timer), payload);
            }
            else
            {
                await emptyFieldsDialog.ShowAsync();
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NumberChoice = (int) MyListBox.SelectedItem;
        }

        private void CicleTime_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            CicleLength = cicleTime.SelectedTime;
        }

        private void PauseTime_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            PauseLength = pauseTime.SelectedTime;
        }
    }
}
