using System;
using UnityEngine;

namespace Project.Scripts.Utilities
{
    public abstract class Timer
    {
        protected float initialTime;
        protected float Time { get; set; }
        public bool IsRunning { get; set; }
        public float Progress => Time / initialTime;
        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float value)
        {
            initialTime = value;
            IsRunning = false;
        }
        
        public void Start()
        {
            Time = initialTime;
            if (!IsRunning)
            {
                IsRunning = true;
                OnTimerStart.Invoke();    
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                OnTimerStop.Invoke();
            }
        }

        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;

        public abstract void Tick(float deltaTime);
    }
    
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
