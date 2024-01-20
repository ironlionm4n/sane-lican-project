using System;

namespace Project.Scripts.Utilities
{
    public class CountDownTimer : Timer
    {
        public Action OnTimerComplete = delegate { };
        public CountDownTimer(float value) : base(value) { }

        /// <summary>
        /// Ticks the timer based on deltaTime, once time = 0, the timer is set to 0, is running set to false, and OnTimerComplete is invoked.
        /// </summary>
        /// <param name="deltaTime">Elapsed delta time</param>
        public override void Tick(float deltaTime)
        {
            if (IsRunning)
            {
                Time -= deltaTime;
                if (Time <= 0)
                {
                    Time = 0;
                    IsRunning = false;
                    OnTimerComplete.Invoke();
                }
            }
        }
    }
}