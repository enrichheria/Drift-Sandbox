using Collidable;
using Player.Movement.Boost;
using Player.Movement.Drift;
using Player.VFX;
using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private DriftMovement _driftMovement;
        [SerializeField] private Boost _boost;
        [SerializeField] private CarVFX _carVFX;
        [SerializeField] private CarStats _carStats;

        private Coroutine _moneyIncomeCoroutine;
        private Coroutine _emojiShowCoroutine;

        public event Action<int> MoneyRewardEarned;

        public DriftMovement Movement => _driftMovement;

        private void OnEnable()
        {
            _carStats.LevelChanged += _carVFX.PlayUpgradeParticles;
            _boost.BoostStarted += EnableBoostParticles;
            _boost.BoostFinished += DisableBoostParticles;
        }

        private void OnDisable()
        {
            _carStats.LevelChanged -= _carVFX.PlayUpgradeParticles;
            _boost.BoostStarted -= EnableBoostParticles;
            _boost.BoostFinished -= DisableBoostParticles;
        }

        private void Update()
        {
            if (_boost.IsInBoost == false)
            {
                if (_driftMovement.IsInSkid)
                {
                    _carVFX.EnableWheelSmoke();
                    _carVFX.EnableSkidmarks();
                }
                else
                {
                    _carVFX.DisableWheelSmoke();
                    _carVFX.DisableSkidmarks();
                }
            }

            CheckPerfectDriftActivity();
        }


        private void FixedUpdate()
        {
            if (_boost.IsInBoost == false)
                _driftMovement.Drive();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollidable collidable))
            {
                GetMoneyIncome(collidable.Reward);
            }
        }

        private void CheckPerfectDriftActivity()
        {
            if (_driftMovement.IsInPerfectDrift && _boost.IsInBoost == false)
            {
                _boost.Fill();

                if (_moneyIncomeCoroutine == null)
                {
                    _moneyIncomeCoroutine = StartCoroutine(GettingPerfectDriftIncome(_carStats.PerfectDriftIncomeFrequency,
                        _carStats.PerfectDriftMoneyIncome));
                }

                if (_emojiShowCoroutine == null)
                    _emojiShowCoroutine = StartCoroutine(ShowingEmoji());
            }
        }

        private void EnableBoostParticles()
        {
            _carVFX.EnableFiremarks();
            _carVFX.DisableWheelSmoke();
        }

        private void DisableBoostParticles()
        {
            _carVFX.DisableFiremarks();
        }

        public void GetMoneyIncome(int value)
        {
            MoneyRewardEarned?.Invoke(value);
            _carVFX.ShowIncome(value);
        }

        private IEnumerator GettingPerfectDriftIncome(float frequency, int value)
        {
            yield return new WaitForSeconds(0.25f);

            var waitForSeconds = new WaitForSeconds(frequency);

            while (_driftMovement.IsInPerfectDrift && _boost.IsInBoost == false && _driftMovement.CanSpeedUp)
            {
                GetMoneyIncome(value);

                yield return waitForSeconds;
            }

            _moneyIncomeCoroutine = null;
        }

        private IEnumerator ShowingEmoji()
        {
            yield return new WaitForSeconds(0.1f);

            float frequency;

            while (_driftMovement.IsInPerfectDrift && _boost.IsInBoost == false && _driftMovement.CanSpeedUp)
            {
                _carVFX.ShowEmoji();

                frequency = UnityEngine.Random.Range(1.5f, 4f);

                yield return new WaitForSeconds(frequency);
            }

            _emojiShowCoroutine = null;
        }
    }
}
