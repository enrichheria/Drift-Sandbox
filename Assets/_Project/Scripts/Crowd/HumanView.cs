using Crowd.Animation;
using Crowd.Ragdoll;
using System.Collections.Generic;
using UnityEngine;

namespace Crowd.View
{
    public class HumanView : MonoBehaviour
    {
        [SerializeField] private List<Skin> _skins;
        [SerializeField] private Skin _currentSkin;
        [SerializeField] private HumanAnimator _humanAnimator;
        [SerializeField] private RagdollEnabler _ragdollEnabler;

        public void EnableRandomSkin()
        {
            _currentSkin.Disable();

            Skin skin = _skins[Random.Range(0, _skins.Count)];
            skin.Enable();

            _humanAnimator.GetAnimator(skin.Animator);
            _ragdollEnabler.GetAnimator(skin.Animator);
            _ragdollEnabler.GetRigidbodies(skin.Rigidbodies);
        }
    }
}
