using System;
using Xamarin.Forms;

namespace ctacke.Xamarin.Forms
{
    /// <summary>
    /// A utlity Timer class that makes the Xamarin Device timer feel more like a .NET Timer, but still uses the Device.StartTimer underpinning for UI friendliness
    /// </summary>
    public class ExtendedTimer : DisposableBase
    {
        public event EventHandler Tick;

        private int m_interval;
        private int m_lastInterval;
        private bool m_stopRequested;

        private DateTime? m_lastStart;

        public bool IsRunning { get; private set; }

        public ExtendedTimer(int interval)
        {
            Interval = interval;
        }

        protected override void ReleaseManagedResources()
        {
            base.ReleaseManagedResources();
            Stop();
        }

        public int Interval
        {
            get => m_interval;
            set
            {
                if (value == Interval) return;

                m_lastInterval = Interval;

                if (value <= 0) throw new ArgumentOutOfRangeException("Interval must be a positive integer");
                m_interval = value;
            }
        }

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            m_stopRequested = false;

            IsRunning = true;
            m_lastStart = DateTime.Now;
            if (Interval < 1000)
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(Interval), TimerProc);
            }
            else
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(1000), TimerProc);
            }
        }

        public void Stop()
        {
            m_stopRequested = true;
            IsRunning = false;
        }

        public void Reset()
        {
            if (!IsRunning)
            {
                Start();
            }
            else
            {
                m_lastStart = DateTime.Now;
            }
        }

        private bool TimerProc()
        {
            if (m_stopRequested) return false;

            var now = DateTime.Now;
            if (m_lastStart.Value.AddMilliseconds(Interval) < now)
            {
                Tick?.Invoke(this, EventArgs.Empty);

                m_lastStart = now;
            }

            // todo: look for a "partial" interval

            if (Interval != m_lastInterval)
            {
                m_lastInterval = Interval;

                // we need to start a new timer with the new interval
                if (Interval < 1000)
                {
                    Device.StartTimer(TimeSpan.FromMilliseconds(Interval), TimerProc);
                }
                else
                {
                    Device.StartTimer(TimeSpan.FromMilliseconds(1000), TimerProc);
                }

                return false;
            }

            return true;
        }

    }
}
