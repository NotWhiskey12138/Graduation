using System;
using UnityEngine;

namespace Whiskey.CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {
        public event Action OnCurrentValueZero;

        public bool valueZero { get; private set; }
        
        [field: SerializeField] public float MaxValue { get; private set; }

        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, 0f, MaxValue);

                if (currentValue <= 0f)
                {
                    valueZero = true;
                    OnCurrentValueZero?.Invoke();
                }
                else
                {
                    valueZero = false;
                }
            }
        }
        
        private float currentValue;

        public void Init() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;

        public void Decrease(float amount) => CurrentValue -= amount;

        public void SetValue(float amount) => CurrentValue = amount;

        public void SetValueZero(bool lessZero) => valueZero = lessZero;
    }
}