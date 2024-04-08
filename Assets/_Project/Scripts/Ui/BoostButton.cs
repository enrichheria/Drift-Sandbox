using Player.Movement.Boost;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BoostButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private BoostView _view;

        private Boost _boost;

        private void Awake() =>
            _boost = FindObjectOfType<Boost>();

        private void OnEnable()
        {
            _boost.FillTimeChanged += CheckBoostLevel;
            _button.onClick.AddListener(_boost.EnterBoostState);
        }

        private void Start() =>
            _button.enabled = false;

        private void OnDisable()
        {
            _boost.FillTimeChanged -= CheckBoostLevel;
            _button.onClick.RemoveListener(_boost.EnterBoostState);
        }

        private void CheckBoostLevel(float currentValue, float maxValue)
        {
            if (maxValue - currentValue <= 0.1f)
            {
                _button.enabled = true;
                _view.ChangeColorToFilled();
                _view.StartAnimation();
                _view.EnableParticle();
            }
            else
            {
                _view.StopAnimation();
                _view.DisableParticle();
                _button.enabled= false;
            }
        }
    }
}


