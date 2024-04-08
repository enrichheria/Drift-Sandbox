using DG.Tweening;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Props.SkibidiToilet
{
    public class Skibidi : PropsBase
    {
        [Title("Skibidi")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private int _maxJumpCount;
        [SerializeField] private float _force;
        [Title("SkibidiVFX")]
        [HideLabel]
        [SerializeField] private SkbidiVFX _vfx;

        private int _currentJumpCount;
        private bool _isActive;
        private const string DropSurface = "DropSurface";

        public bool CanJump => _currentJumpCount <= _maxJumpCount;

        private void OnTriggerEnter(Collider other)
        {
            if (_isActive == false)
                return;

            if (other.CompareTag(DropSurface))
            {
                _vfx.PlayExplosion();

                if (CanJump == false)
                    Destroy(gameObject);

                if (CanJump)
                    Jump();
            }
        }

        protected override void InteractWith(Car car) =>
            Activate();

        public void Activate() =>
           _isActive = true;

        public void ApplyExplosiontForce(float explosionForce, Vector3 explosionPosition,
            float explosionRadius, float upwardsModifier)
        {
            _rigidbody.AddExplosionForce(explosionForce, explosionPosition,
                explosionRadius, upwardsModifier);
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector3.up * Random.Range(_force - 2f, _force + 2f), ForceMode.Impulse);
            transform.DOShakeScale(0.25f).OnComplete(() => ResetScale());
            _currentJumpCount++;
        }

        private void ResetScale() =>
            transform.localScale = Vector3.one;

    }
}
