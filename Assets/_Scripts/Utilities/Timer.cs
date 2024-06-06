using System;
using UnityEngine;

namespace Whiskey.Utilities
{
    public class Timer
    {
        public event Action OnTimerDone;
        
        private float startTime; //计时器开始时间
        private float duration; //计时器持续时间
        private float targetTime; //目标时间

        private bool isActive;
        
        public Timer(float duration)
        {
            this.duration = duration;
        }

        public void StartTimer()
        {
            startTime = Time.time;
            targetTime = startTime + duration;
            isActive = true;
        }

        public void StopTimer()
        {
            isActive = false;
        }

        public void Tick()
        {
            if(!isActive) return;
            
            if (Time.time >= targetTime)
            {
                OnTimerDone?.Invoke();
                StopTimer();
            }
        }
    }
}