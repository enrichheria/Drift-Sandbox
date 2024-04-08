using System.Collections;
using UnityEngine;

namespace Utility
{
    public class TimeScaller
    {
        private float _currentValue;

        private readonly float _defaultValue = 1f;
        private readonly float _changeTime = 1f;
        private readonly CoroutineRunner _runner;
        private readonly AnimationCurve _curve;

        public TimeScaller(AnimationCurve curve)
        {
            _curve = curve;
            _currentValue = _defaultValue;
            _runner = CoroutineRunner.Instance;
        }

        public void SetTimeScale(float targetValue) => 
            _runner.RunCoroutine(ChangingTimeScale(targetValue));

        private IEnumerator ChangingTimeScale(float targetValue)
        {
            float timer = 0;
            float startValue = _currentValue;

            while (timer < _changeTime)
            {
                _currentValue = Mathf.Lerp(startValue, targetValue, _curve.Evaluate(timer / _changeTime));
                Time.timeScale = _currentValue;

                timer += Time.unscaledDeltaTime;

                yield return null;
            }

            Time.timeScale = targetValue;
            _currentValue = _defaultValue;
        }
    }
}
