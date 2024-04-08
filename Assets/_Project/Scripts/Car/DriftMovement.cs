using Dreamteck.Splines;
using Player.InputReader;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player.Movement.Drift
{

    public class DriftMovement : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private SplineFollower _splineFollower;
        [Title("Speed")]
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _accelerationTime;
        [Title("Drift")]
        [SerializeField] private float _maxDriftTime;
        [SerializeField] private float _rotationTime;
        [SerializeField] private float _maxRotation;
        [Title("Offset")]
        [SerializeField] private float _minOffsetX;
        [SerializeField] private float _maxOffsetX;
        [SerializeField] private float _offsetTime;
        [Title("Curves")]
        [SerializeField] private AnimationCurve _speedUpCurve;
        [SerializeField] private AnimationCurve _slowDownCurve;
        [SerializeField] private AnimationCurve _rotationCurve;
        [SerializeField] private AnimationCurve _offsetCurve;

        private float _accelerationTimer;
        private float _rotationTimer;
        private float _offsetTimer;
        private float _curentDriftTime;

        private bool _canSpeedUp = true;

        public int Speed => (int)_splineFollower.followSpeed;
        public float Velocity => _splineFollower.followSpeed / _maxSpeed;
        public bool IsDrifting => _curentDriftTime / _maxDriftTime >= 0.45f;
        public bool IsInSkid => _splineFollower.motion.rotationOffset.y / _maxRotation >= 0.3f;
        public bool IsInPerfectDrift =>
            _curentDriftTime / _maxDriftTime >= 0.4f && _curentDriftTime / _maxDriftTime <= 0.6f;
        public bool CanSpeedUp => _canSpeedUp;

        public event Action<float, float> DriftTimeChanged;

        public void SetMaxSpeed(float speed) =>
            _maxSpeed = speed;

        public void SetMinSpeed(float speed) =>
            _minSpeed = speed;

        public void ResetValues()
        {
            _accelerationTimer = 0;
            _rotationTimer = 0;
            _offsetTimer = 0;
            _curentDriftTime = 0;

            DriftTimeChanged?.Invoke(_curentDriftTime, _maxDriftTime);
        }

        public void Drive()
        {
            if (_playerInput.IsInDrivingMode && EventSystem.current.IsPointerOverGameObject() == false
                && _playerInput.IsButtonHold && _canSpeedUp)
            {
                if (_curentDriftTime >= _maxDriftTime)
                {
                    StartCoroutine(Cooldown());

                    return;
                }

                SpeedUp();

                _curentDriftTime += Time.deltaTime;
            }
            else
            {
                SlowDown();

                _curentDriftTime -= Time.deltaTime;
            }

            _curentDriftTime = Mathf.Clamp(_curentDriftTime, 0, _maxDriftTime);

            DriftTimeChanged?.Invoke(_curentDriftTime, _maxDriftTime);
        }

        private void SpeedUp()
        {
            if (_accelerationTimer / _accelerationTime <= 1)
            {
                _splineFollower.followSpeed = Mathf.Lerp(_minSpeed, _maxSpeed,
                    _speedUpCurve.Evaluate(_accelerationTimer / _accelerationTime));
                _accelerationTimer += Time.deltaTime;
            }

            if (_rotationTimer / _rotationTime <= 1)
            {
                _splineFollower.motion.rotationOffset = Vector3.Lerp(Vector3.zero, new Vector3(0, _maxRotation),
                    _rotationCurve.Evaluate(_rotationTimer / _rotationTime));
                _rotationTimer += Time.deltaTime;
            }

            if (_offsetTimer / _offsetTime <= 1)
            {
                _splineFollower.motion.offset = Vector3.Lerp(new Vector3(_minOffsetX, 0.5f), new Vector3(_maxOffsetX, 0),
                    _offsetCurve.Evaluate(_offsetTimer / _offsetTime));
                _offsetTimer += Time.deltaTime;
            }
        }

        private void SlowDown()
        {
            if (_accelerationTimer > 0)
            {
                _splineFollower.followSpeed = Mathf.Lerp(_minSpeed, _maxSpeed,
                    _slowDownCurve.Evaluate(_accelerationTimer / _accelerationTime));
                _accelerationTimer -= Time.deltaTime;
            }

            if (_rotationTimer > 0)
            {
                _splineFollower.motion.rotationOffset = Vector3.Lerp(Vector3.zero, new Vector3(0, _maxRotation),
                    _rotationCurve.Evaluate(_rotationTimer / _rotationTime));
                _rotationTimer -= Time.deltaTime;
            }

            if (_offsetTimer > 0)
            {
                _splineFollower.motion.offset = Vector3.Lerp(new Vector3(_minOffsetX, 0.5f), new Vector3(_maxOffsetX, 0),
                    _offsetCurve.Evaluate(_offsetTimer / _offsetTime));
                _offsetTimer -= Time.deltaTime;
            }
        }

        private IEnumerator Cooldown()
        {
            _canSpeedUp = false;

            yield return new WaitUntil(() => _playerInput.IsButtonReleased);

            _canSpeedUp = true;
        }
    }
}

