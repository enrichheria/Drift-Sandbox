using UnityEngine;

namespace CheeringCrowd
{
    public class CheeringCrowdAnimator : MonoBehaviour
    {
        private Animator _animator;

        private readonly int CheeringHash = Animator.StringToHash("Cheering");
        private readonly int HangingOutHash = Animator.StringToHash("HangingOut");
        private readonly int ClappingHash = Animator.StringToHash("Clapping");

        private void Awake() =>
            _animator = GetComponent<Animator>();

        private void Start() =>
            SetRandomAnimation();

        private void SetRandomAnimation()
        {
            int[] hashes = { CheeringHash, HangingOutHash, ClappingHash };
            int randomIndex = Random.Range(0, hashes.Length);

            _animator.Play(hashes[randomIndex]);
        }
    }
}
