using TMPro;
using UnityEngine;
using Wallets;

namespace UI.Wallets
{
    public class WalletView : MonoBehaviour, IWalletView
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Wallet _wallet;

        private void OnEnable() => 
            _wallet.ValueChanged += UpdateView;

        private void OnDisable() => 
            _wallet.ValueChanged -= UpdateView;

        public void UpdateView(float value)
        {
            if (value >= 1000)
                _value.text = (value / 1000).ToString("F2") + "K";
            else
                _value.text = value.ToString();
        }
    }
}
