using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StainHandler : MonoBehaviour
    {
        [Title("Animation")]
        [SerializeField] private float _fadeDuration;
        [SerializeField] private CinemachineImpulseSource _impulseSource;
        [SerializeField] private List<Image> _stains;

        private bool _isShowing;

        public void Show()
        {
            if (_isShowing)
                return;

            _isShowing = true;
            SetActive(true);
            Animate();
        }

        private void SetActive(bool isActive)
        {
            foreach (Image stain in _stains)
                stain.gameObject.SetActive(isActive);
        }

        private void Animate()
        {
            _impulseSource.GenerateImpulse();

            foreach (Image stain in _stains)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(stain.DOFade(0, _fadeDuration).SetEase(Ease.InExpo));

                sequence.OnComplete(() =>
                {
                    SetActive(false);
                    ResetAlpha();
                    _isShowing = false;
                });
            }
        }

        private void ResetAlpha()
        {
            foreach (Image stain in _stains)
                stain.color = new Color(stain.color.r, stain.color.g, stain.color.b, 0.8f);
        }
    }
}


