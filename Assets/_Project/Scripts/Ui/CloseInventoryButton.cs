using InventorySystem;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class CloseInventoryButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private AnimationCurve _curve;

        private TimeScaller _timeScaller;

        private void OnEnable() =>
            _button.onClick.AddListener(Close);

        private void Start() => 
            _timeScaller = new TimeScaller(_curve);

        private void OnDisable() =>
            _button.onClick.RemoveListener(Close);

        private void Close()
        {
            _timeScaller.SetTimeScale(1f);
            _inventory.View.Hide();
        }
    }
}
