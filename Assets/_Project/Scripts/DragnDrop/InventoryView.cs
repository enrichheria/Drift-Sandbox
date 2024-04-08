using System.Collections;
using UI;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private OpenInventoryButton _openButton;

        private const string HideInventory = "Hide";

        public void Show()
        {
            gameObject.SetActive(true);
            //_animator.Play(ShowInventory);
        }

        public void Hide()
        {
            //_animator.Play(HideInventory);
            gameObject.SetActive(false);
            _openButton.ShowButton();
            //StartCoroutine(DelayClose());
        }

        private IEnumerator DelayClose()
        {
            yield return null;

            yield return new WaitUntil(() => IsAnimationPlaying(HideInventory) == false);

            gameObject.SetActive(false);
            _openButton.ShowButton();
        }

        public bool IsAnimationPlaying(string animationName)
        {
            var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (animatorStateInfo.IsName(animationName))
                return true;

            return false;
        }
    }
}

