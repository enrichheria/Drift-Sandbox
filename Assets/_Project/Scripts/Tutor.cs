using DG.Tweening;
using InventorySystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class Tutor : MonoBehaviour
    {
        [SerializeField] private Image _backround;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _handPointer;
        [SerializeField] private Button _openInventory;
        [SerializeField] private Button _closeInventory;
        [SerializeField] private Inventory _inventory;

        private const string TutorSaveWord = "Tutor";

        private bool _isClicked;

        public bool IsCompleted { get; private set; }

        private void OnEnable() => 
            _openInventory.onClick.AddListener(Click);

        private void OnDisable() => 
            _openInventory.onClick.RemoveListener(Click);

        private void Start()
        {
            if (PlayerPrefs.GetInt(TutorSaveWord) == 1)
            {
                IsCompleted = true;

                return;
            }

            _openInventory.enabled = false;
            _backround.gameObject.SetActive(true);
            _rectTransform.gameObject.SetActive(true);
            _handPointer.gameObject.SetActive(false);

            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(1f);
            sequence.Append(_rectTransform.DOLocalMoveY(-304f, 1.5f));
            sequence.AppendInterval(1f);

            sequence.OnComplete(() =>
            {
                _backround.gameObject.SetActive(false);
                _rectTransform.gameObject.SetActive(false);

                IsCompleted = true;

                StartCoroutine(TutorialActivity());
            });
        }

        private IEnumerator TutorialActivity()
        {
            var waitForSeconds = new WaitForSeconds(5f);
            var waitUntilButtonClicked = new WaitUntil(() => _isClicked);
            var waitUntilItemSpawned = new WaitUntil(() => _inventory.FirstItemSpawned);

            yield return waitForSeconds;

            _openInventory.enabled = true;
            _handPointer.gameObject.SetActive(true);

            Sequence scaleSequence = StartScaleSequence();

            yield return waitUntilButtonClicked;

            _closeInventory.enabled = false;

            Sequence moveSequence = StartMotionSequence();

            yield return waitUntilItemSpawned;

            scaleSequence.Kill();
            moveSequence.Kill();

            _handPointer.gameObject.SetActive(false);
            _closeInventory.enabled = true;

            PlayerPrefs.SetInt(TutorSaveWord, 1);

            yield return new WaitForSeconds(1f);

            Destroy(gameObject);
        }

        private Sequence StartMotionSequence()
        {
            Sequence moveSequence = DOTween.Sequence();
            moveSequence.Append(_handPointer.DOAnchorPos(Vector2.zero, 2f));
            moveSequence.AppendInterval(1f);
            moveSequence.SetLoops(-1).SetUpdate(true);

            return moveSequence;
        }

        private Sequence StartScaleSequence()
        {
            Sequence scaleSequence = DOTween.Sequence();
            scaleSequence.Append(_handPointer.DOScale(1.15f, 0.25f));
            scaleSequence.AppendInterval(0.5f);
            scaleSequence.Append(_handPointer.DOScale(1f, 0.25f));
            scaleSequence.AppendInterval(0.5f);
            scaleSequence.SetLoops(-1).SetEase(Ease.Linear).SetUpdate(true);

            return scaleSequence;
        }

        private void Click() => _isClicked = true;
    }
}
