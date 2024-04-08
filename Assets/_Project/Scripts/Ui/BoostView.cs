using DG.Tweening;
using Player.Movement.Boost;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BoostView : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _filledColor;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private ParticleSystem _uiParticle;

        private Boost _boost;
        private Sequence _sequence;
        private bool _animationStarted;

        private readonly Vector3 _defaultScale = new(0.8f, 0.8f, 0.8f);

        private void Awake() =>
            _boost = FindObjectOfType<Boost>();

        private void OnEnable() =>
            _boost.FillTimeChanged += UpdateView;

        private void OnDisable() =>
            _boost.FillTimeChanged -= UpdateView;

        public void ChangeColorToFilled() =>
            ChangeColor(_filledColor);

        public void EnableParticle()
        {
            if (_uiParticle.isPlaying == false)
                _uiParticle.Play();
        }
        public void DisableParticle() =>
            _uiParticle.Stop();

        public void StartAnimation()
        {
            if (_animationStarted)
                return;

            _animationStarted = true;

            _sequence = DOTween.Sequence();
            _sequence.Append(_rectTransform.DOScale(0.9f, 0.2f));
            _sequence.AppendInterval(0.5f);
            _sequence.Append(_rectTransform.DOScale(0.8f, 0.2f));
            _sequence.AppendInterval(0.25f);
            _sequence.SetLoops(-1).SetEase(Ease.Linear);
        }

        public void StopAnimation()
        {
            _sequence.Kill();
            _rectTransform.localScale = _defaultScale;
            _animationStarted = false;
        }

        private void UpdateView(float currentValue, float maxValue)
        {
            _fill.fillAmount = currentValue / maxValue;

            if (_fill.fillAmount == 0)
                ChangeColor(_defaultColor);
        }

        private void ChangeColor(Color color) =>
            _fill.color = color;
    }
}


