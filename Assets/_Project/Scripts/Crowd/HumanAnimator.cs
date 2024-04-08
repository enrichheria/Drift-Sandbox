using UnityEngine;

namespace Crowd.Animation
{
    public class HumanAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int RunningHash = Animator.StringToHash("Running");
        private readonly int CheeringHash = Animator.StringToHash("Cheering");

        public void GetAnimator(Animator animator) => 
            _animator = animator;

        public void PlayRunning() =>
            PlayAnimation(RunningHash);

        public void PlayCheering() =>
            PlayAnimation(CheeringHash);

        private void PlayAnimation(int hash) =>
            _animator.Play(hash);
    }
}
