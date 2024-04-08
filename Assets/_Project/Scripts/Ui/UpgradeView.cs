using Player;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wallets;

namespace UI
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _max;
        [SerializeField] private Image _icon;
        [SerializeField] private ParticleSystem _uiParticle;

        [SerializeField] private CarStats _carStats;
        [SerializeField] private MoneyWallet _moneyWallet;


        private void OnEnable()
        {
            _moneyWallet.ValueChanged += UpdateView;
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _moneyWallet.ValueChanged -= UpdateView;
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void Start()
        {
            StartCoroutine(Delay());
        }

        private void UpdateView(float value)
        {
            if (_carStats.IsMaxLevel)
            {
                _max.gameObject.SetActive(true);
                _price.gameObject.SetActive(false);
                _icon.gameObject.SetActive(false);
                _button.interactable = false;
                _uiParticle.Stop();
                return;
            }

            //_level.text = $"Level {_carStats.CarLevel + 1}";

            UpdatePiceText();
            ChangeInteractivity();
        }

        private void UpdatePiceText()
        {
            if (_carStats.UpgradePrice >= 1000)
                _price.text = $"{(float)_carStats.UpgradePrice / 1000}K";
            else
                _price.text = $"{_carStats.UpgradePrice}";
        }

        private void ChangeInteractivity()
        {
            if (_moneyWallet.CurrentValue >= _carStats.UpgradePrice)
            {
                if (_uiParticle.isPlaying == false)
                    _uiParticle.Play();

                ChangeAlpha(1f);
                _button.interactable = true;
            }
            else
            {
                _uiParticle.Stop();
                ChangeAlpha(0.75f);
                _button.interactable = false;
            }
        }

        private void ChangeAlpha(float alpha)
        {
            _icon.color = new Color(_icon.color.r, _icon.color.g, _icon.color.b, alpha);
            _price.color = new Color(_price.color.r, _price.color.g, _price.color.b, alpha);
        }

        private void OnButtonClick()
        {
            _moneyWallet.Spend(_carStats.UpgradePrice);
            _carStats.IncreaseLevel(_carStats.CarLevel + 1);
            UpdateView(_moneyWallet.CurrentValue);
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.25f);

            UpdateView(_moneyWallet.CurrentValue);
        }
    }
}
