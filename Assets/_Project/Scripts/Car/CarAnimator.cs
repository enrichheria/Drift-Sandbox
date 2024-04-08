using UnityEngine;

namespace Player.Animation
{
    public class CarAnimator
    {
        private readonly Animator _animator;

        private readonly int DriveHash = Animator.StringToHash("Drive");
        private readonly int DriftHash = Animator.StringToHash("Drift");

        public CarAnimator(Animator animator) => 
            _animator = animator;

        public void PlayDrive() =>
            PlayAnimation(DriveHash);

        public void PlayDrift() =>
            PlayAnimation(DriftHash);

        private void PlayAnimation(int hash) =>
            _animator.Play(hash);
    }
}

