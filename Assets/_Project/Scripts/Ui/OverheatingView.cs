using DG.Tweening;
using Player.Movement.Drift;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Overheating
{
    public class OverheatingView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fill;
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _targetColor;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private Image _idealDrift;

        private DriftMovement _driftMovement;
        private bool _animationStarted;
        private Sequence _sequence;

        private void Awake() => 
            _driftMovement = FindObjectOfType<DriftMovement>();

        private void OnEnable() =>
            _driftMovement.DriftTimeChanged += UpdateView;

        private void OnDisable() =>
            _driftMovement.DriftTimeChanged -= UpdateView;

        private void UpdateView(float currentValue, float maxValue)
        {
            _slider.value = currentValue / maxValue;
            _speed.text = $"{_driftMovement.Speed} mph";

            if (_driftMovement.IsInPerfectDrift)
            {
                _fill.color = _targetColor;

                if (_animationStarted == false)
                    ShowIcon();
            }
            else
            {
                _fill.color = _startColor;

                HideIcon();
            }
        }

        private void ShowIcon()
        {
            _animationStarted = true;
            _idealDrift.gameObject.SetActive(true);
            StartAnimation();
        }

        private void HideIcon()
        {
            _sequence.Kill();
            _idealDrift.gameObject.SetActive(false);
            _animationStarted = false;
        }

        private void StartAnimation()
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(_idealDrift.rectTransform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 1f));
            _sequence.AppendInterval(1f);
            _sequence.Append(_idealDrift.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 1f));
            _sequence.AppendInterval(1f);
            _sequence.SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}
