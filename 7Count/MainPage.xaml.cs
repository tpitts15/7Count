using System;
using System.Collections.Generic;
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
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _7Count
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();
			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromMilliseconds(500);
			timer.Tick += Timer_Tick;
            //timer.Start();			
			Unloaded += MainPage_Unloaded;
			//initGPIO();
		}
		
		/*private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin1 = null;
                pin2 = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            pin1 = gpio.OpenPin(GREEN_LED_PIN);
            pin2 = gpio.OpenPin(RED_LED_PIN);
            pin1.Write(GpioPinValue.High);
            pin2.Write(GpioPinValue.High);
            pin1.SetDriveMode(GpioPinDriveMode.Output);
            pin2.SetDriveMode(GpioPinDriveMode.Output);

            GpioStatus.Text = "GPIO pin initialized correctly.";
        }
		*/
		private void MainPage_Unloaded(object sender, object args)
        {
            // Cleanup
            //pin1.Dispose();
            //pin2.Dispose();
        }
		
		
		private void Timer_Tick(object sender, object e)
		{
            if (stepUp == true)
            {
                increment();
            }
            else
            {
                decrement();
            }
		}

		private void increment()
		{
			if(count < (9999 - stepSize))
			{
				count += stepSize;
                Count.Text = count.ToString();
            }
			else
			{
                count -= stepSize;
				switchDirection();
                SwitchDirection.Text = "Count Up";
                Count.Text = count.ToString();
            }
		}

		private void decrement()
		{
			if(count > stepSize)
			{
				count -= stepSize;
                Count.Text = count.ToString();
            }
			else
			{
                count += stepSize;
				switchDirection();
                SwitchDirection.Text = "Count Down";
                Count.Text = count.ToString();
            }
		}

		private void switchDirection()
		{
			if (stepUp == true)
			{
				stepUp = false;
			}
			else
			{
				stepUp = true;
			}
		}

        private int count;
		private bool stepUp = true;
		private int stepSize = 1;
        private DispatcherTimer timer;
        private bool timerOn = false;
        private SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush greenBrush = new SolidColorBrush(Windows.UI.Colors.Green);

        private void SwitchDirectionBox_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        
        }

        private void StepSize_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(timer == null)
            {
                return;
            }
            StepSizeText.Text = "Step size: " + e.NewValue;
            stepSize = Convert.ToInt32(e.NewValue);
        }

        private void Stop_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
        
        }

        private void SwitchDirection_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (stepUp == true)
            {
                stepUp = false;
                SwitchDirection.Text = "Count Up";
            }
            else
            {
                stepUp = true;
                SwitchDirection.Text = "Count Down";
            }
        }

        private void StopText_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (timerOn == false)
            {
                timerOn = true;
                Stop.Fill = redBrush;
                StopText.Text = "STOP";
                timer.Start();
            }
            else
            {
                timerOn = false;
                Stop.Fill = greenBrush;
                StopText.Text = "GO";
                timer.Stop();
            }
        }
    }
}

