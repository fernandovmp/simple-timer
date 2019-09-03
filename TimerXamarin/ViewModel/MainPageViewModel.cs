using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace TimerXamarin.ViewModel
{
    public enum TimerState { None, Running, Stopped }
    public class MainPageViewModel : ViewModelBase
    {
        private TimeSpan    timerValue;
        private DateTime    startTime;
        private Timer       Timer;
        private ICommand    timerCommand;
        private ICommand    resetTimerCommand;
        private TimerState  timerState = TimerState.None;
        public  TimeSpan    TimerValue
        {
            get => timerValue;
            set
            {
                timerValue = value;
                OnPropertyChanged("TimerValue");
            }
        }
        public  ICommand    TimerCommand
        {
            get
            {
                if(timerCommand == null)
                {
                    TimerCommand = new Command(StartTimer);
                }
                return timerCommand;
            }
            set
            {
                timerCommand = value;
                OnPropertyChanged("TimerCommand");
            }
        }
        public  ICommand    ResetTimerCommand
        {
            get
            {
                if(resetTimerCommand == null)
                {
                    ResetTimerCommand = new Command(ResetTimer);
                }
                return resetTimerCommand;
            }
            set
            {
                resetTimerCommand = value;
                OnPropertyChanged("ResetTimer");
            }
        }
        public  TimerState  TimerState
        {
            get => timerState;
            set
            {
                timerState = value;
                OnPropertyChanged("TimerState");
            }
        }

        private void StartTimer(object parameter)
        {
            startTime = DateTime.Now;
            Timer = new Timer(100);
            Timer.Elapsed += Timer_Elapsed;
            Timer.AutoReset = true;
            Timer.Start();
            TimerState = TimerState.Running;
            TimerCommand = new Command(StopTimer);
        }

        private void StopTimer(object parameter)
        {
            Timer.Stop();
            TimerState = TimerState.Stopped;
            TimerCommand = new Command(UnstopTimer);
        }

        private void UnstopTimer(object parameter)
        {
            Timer.Start();
            TimerState = TimerState.Running;
            startTime = DateTime.Now - TimerValue;
            TimerCommand = new Command(StopTimer);
        }

        private void ResetTimer(object parameter)
        {
            Timer.Close();
            TimerValue = TimeSpan.Zero;
            TimerState = TimerState.None;
            TimerCommand = new Command(StartTimer);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimerValue = e.SignalTime - startTime;
        }
    }
}
