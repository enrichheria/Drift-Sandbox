using InventorySystem;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class OpenInventoryButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private AnimationCurve _curve;

        private TimeScaller _timeScaller;

        private void OnEnable() => 
            _button.onClick.AddListener(Open);

        private void Start() => 
            _timeScaller = new TimeScaller(_curve);

        private void OnDisable() => 
            _button.onClick.RemoveListener(Open);

        public void ShowButton() =>
            gameObject.SetActive(true);

        private void Open()
        {
            _inventory.View.Show();
            HideButton();
            _timeScaller.SetTimeScale(0f);
        }

        private void HideButton() => 
            gameObject.SetActive(false);
    }
}
