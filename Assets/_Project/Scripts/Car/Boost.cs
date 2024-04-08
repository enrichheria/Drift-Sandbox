using Dreamteck.Splines;
using Player.Movement.Drift;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Player.Movement.Boost
{
    public class Boost : MonoBehaviour
    {
        [SerializeField] private DriftMovement _driftMovement;
        [SerializeField] private SplineFollower _splineFollower;
        [Title("Speed")]
        [SerializeField] private float _boostSpeed;
        [SerializeField] private float _accelerationTime;
        [Title("Rotation")]
        [SerializeField] private float _maxRotation;
        [SerializeField] private float _rotationTime;
        [Title("Timers")]
        [SerializeField] private float _fillTime;
        [SerializeField] private float _boostTime;

        private float _currentTime;

        public bool IsInBoost { get; private set; }

        public event Action BoostStarted;
        public event Action BoostFinished;
        public event Action<float, float> FillTimeChanged;

        public void SetBoostSpeed(float speed) =>
            _boostSpeed = speed;

        public void Fill()
        {
            if (_currentTime < _fillTime)
                _currentTime += Time.deltaTime;

            FillTimeChanged?.Invoke(_currentTime, _fillTime);
        }

        public void EnterBoostState()
        {
            BoostStarted?.Invoke();
            IsInBoost = true;
            _driftMovement.ResetValues();
            SpeedUp();
            StartCoroutine(CountingDown());
        }

        private void SpeedUp()
        {
            StartCoroutine(IncreaseSpeed());
            StartCoroutine(Rotating());
        }

        private void SlowDown()
        {
            StartCoroutine(DecreaseSpeed());
            StartCoroutine(Rotating2());
        }

        private IEnumerator IncreaseSpeed()
        {
            float startSpeed = _splineFollower.followSpeed;
            float accelerationTimer = 0;

            while (_splineFollower.followSpeed < _boostSpeed)
            {
                _splineFollower.followSpeed = Mathf.Lerp(startSpeed, _boostSpeed, accelerationTimer / _accelerationTime);
                accelerationTimer += Time.deltaTime;

                yield return null;
            }
        }

        private IEnumerator DecreaseSpeed()
        {
            float startSpeed = _splineFollower.followSpeed;
            float accelerationTimer = 0;

            while (_splineFollower.followSpeed > 25f)
            {
                _splineFollower.followSpeed = Mathf.Lerp(startSpeed, 25f, accelerationTimer / _accelerationTime);
                accelerationTimer += Time.deltaTime;

                yield return null;
            }
        }

        private IEnumerator Rotating()
        {
            float startRotation = _splineFollower.motion.rotationOffset.y;
            float rotationTimer = 0;

            while (_splineFollower.motion.rotationOffset.y < _maxRotation)
            {
                _splineFollower.motion.rotationOffset = Vector3.Lerp(new Vector3(0f, startRotation),
                    new Vector3(0f, _maxRotation), rotationTimer / _rotationTime);
                rotationTimer += Time.deltaTime;

                yield return null;
            }
        }

        private IEnumerator Rotating2()
        {
            float startRotation = _splineFollower.motion.rotationOffset.y;
            float rotationTimer = 0;
           
            while (_splineFollower.motion.rotationOffset.y > 0f)
            {
                _splineFollower.motion.rotationOffset = Vector3.Lerp(new Vector3(0f, startRotation),
                    new Vector3(0f, 0f), rotationTimer / _rotationTime);
                rotationTimer += Time.deltaTime;

                yield return null;
            }
        }

        private IEnumerator CountingDown()
        {
            float timer = _boostTime;

            while (timer > 0)
            {
                timer -= Time.deltaTime;

                FillTimeChanged?.Invoke(timer, _boostTime);

                yield return null;
            }

            _currentTime = 0;
            SlowDown();

            yield return new WaitForSeconds(_accelerationTime + 0.5f);

            IsInBoost = false;
            BoostFinished?.Invoke();
        }
    }
}

