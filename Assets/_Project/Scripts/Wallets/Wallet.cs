using Player;
using Saves;
using System;
using UnityEngine;

namespace Wallets
{
    public abstract class Wallet : MonoBehaviour, IWallet
    {
        [SerializeField] protected float _currentValue;
        [SerializeField] private Car _car;

        private SaveSystem _saveSystem;

        public float CurrentValue => _currentValue;

        public event Action<float> ValueChanged;

        private void Awake() => 
            _saveSystem = new SaveSystem();

        private void OnEnable() => 
            _car.MoneyRewardEarned += Add;

        private void OnDisable() => 
            _car.MoneyRewardEarned -= Add;

        private void Start()
        {
            _currentValue = _saveSystem.LoadMoney();
            ValueChanged?.Invoke(_currentValue);
        }

        public virtual void Add(int value)
        {
            _currentValue += value;
            ValueChanged?.Invoke(_currentValue);
            _saveSystem.SaveMoney(_currentValue);
        }

        public virtual void Spend(int value)
        {
            _currentValue -= value;

            if (_currentValue < 0)
                _currentValue = 0;

            ValueChanged?.Invoke(_currentValue);
            _saveSystem.SaveMoney(_currentValue);
        }
    }
}
