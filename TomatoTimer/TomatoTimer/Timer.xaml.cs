﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace TomatoTimer
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class Timer : Page
    {

        public int CurCiclo { get; set; }
        public int TotCicli { get; set; }
        public double DurataCiclo { get; set; }
        public double DurataPausa { get; set; }
        private double InizioTimer { get; set; }
        private Boolean IsFocus { get; set; }
        
        public Timer()
        {
            this.InitializeComponent();
            IsFocus = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) 
        {
            AnotherPagePayload passedParameter = e.Parameter as AnotherPagePayload;
            TotCicli = passedParameter.TotCicli;
            DurataCiclo = passedParameter.DurataCiclo;
            DurataPausa = passedParameter.DurataPausa;

            CurCiclo = 1;
            currentCicle.Text = CurCiclo.ToString();
            totCicles.Text = TotCicli.ToString();
            InizioTimer = DurataCiclo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            InizioTimer--;
            TimeSpan time = TimeSpan.FromSeconds(InizioTimer);
            string str = time.ToString(@"hh\:mm\:ss");
            TimerLabel.Text = str;

            // Fine del ciclo di focus
            if (InizioTimer == 0 && IsFocus)
            {
                IsFocus = false;
                InizioTimer = DurataPausa;
                CurCiclo++;
                currentCicle.Text = CurCiclo.ToString();

                HideCiclo();
                ShowPausa();
            }

            // Fine del ciclo di pausa
            else if (InizioTimer == 0 && !IsFocus) {

                if (CurCiclo > TotCicli)
                {
                    // Focus completato
                } else {
                    IsFocus = true;
                    InizioTimer = DurataCiclo;

                    HidePausa();
                    ShowCiclo();
                }
                  
            } 
        }

        private void ShowCiclo()
        {
            labelCicle.Visibility = Visibility.Visible;
            currentCicle.Visibility = Visibility.Visible;
            labelSlash.Visibility = Visibility.Visible;
            totCicles.Visibility = Visibility.Visible;
        }

        private void HideCiclo()
        {
            labelCicle.Visibility = Visibility.Collapsed;
            currentCicle.Visibility = Visibility.Collapsed;
            labelSlash.Visibility = Visibility.Collapsed;
            totCicles.Visibility = Visibility.Collapsed;
        }

        private void ShowPausa()
        {
            labelPausa.Visibility = Visibility.Visible;
        }

        private void HidePausa()
        {
            labelPausa.Visibility = Visibility.Collapsed;
        }

    }
}
