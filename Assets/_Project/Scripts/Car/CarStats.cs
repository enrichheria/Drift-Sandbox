using Skins;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Player.Movement.Drift;
using Player.Movement.Boost;
using Saves;

namespace Player
{
    public class CarStats : MonoBehaviour
    {
        [SerializeField] private int _carLevel;
        [SerializeField] private int _upgradePrice;
        [SerializeField] private int _perfectDriftMoneyIncome;
        [SerializeField] private float _perfectDriftIncomeFrequency;
        [SerializeField] private List<CarSkin> _skins;
        [SerializeField] private DriftMovement _driftMovement;
        [SerializeField] private Boost _boost;

        private CarSkin _currentSkin;
        private SaveSystem _saveSystem;

        public int CarLevel => _carLevel;
        public int UpgradePrice => _upgradePrice;
        public int PerfectDriftMoneyIncome => _perfectDriftMoneyIncome;
        public float PerfectDriftIncomeFrequency => _perfectDriftIncomeFrequency;
        public bool IsMaxLevel => _carLevel >= 5;

        public event Action LevelChanged;

        private void Awake() =>
            _saveSystem = new SaveSystem();

        private void Start()
        {
            _currentSkin = _skins[0];
            LoadStats();
        }

        public void IncreaseLevel(int level)
        {
            if (IsMaxLevel)
                return;

            _carLevel++;

            SetLevelStats(level);

            _saveSystem.SaveLevel(level);
            LevelChanged?.Invoke();
        }

        private void SetLevelStats(int level)
        {
            switch (level)
            {
                case 2:
                    ChangeSkin(2);
                    _perfectDriftMoneyIncome = 6;
                    _upgradePrice = 1000;
                    _driftMovement.SetMaxSpeed(40f);
                    _driftMovement.SetMinSpeed(25f);
                    _boost.SetBoostSpeed(75f);
                    break;
                case 3:
                    ChangeSkin(3);
                    _perfectDriftMoneyIncome = 8;
                    _upgradePrice = 2000;
                    _driftMovement.SetMaxSpeed(45f);
                    _driftMovement.SetMinSpeed(30f);
                    _boost.SetBoostSpeed(80f);
                    break;
                case 4:
                    ChangeSkin(4);
                    _perfectDriftMoneyIncome = 10;
                    _upgradePrice = 4000;
                    _driftMovement.SetMaxSpeed(50f);
                    _driftMovement.SetMinSpeed(35f);
                    _boost.SetBoostSpeed(85f);
                    break;
                case 5:
                    ChangeSkin(5);
                    _perfectDriftMoneyIncome = 12;
                    _upgradePrice = 8000;
                    _driftMovement.SetMaxSpeed(55f);
                    _driftMovement.SetMinSpeed(40f);
                    _boost.SetBoostSpeed(90f);
                    break;
            }
        }

        private void ChangeSkin(int level)
        {
            _currentSkin.Disabale();
            _currentSkin = _skins.FirstOrDefault(skin => skin.Level == level);
            _currentSkin.Enable();
        }

        private void LoadStats()
        {
            _carLevel = _saveSystem.LoadLevel();

            if (_carLevel > 1)
                SetLevelStats(_carLevel);
        }
    }
}
