using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crowd.Ragdoll
{
    public class RagdollEnabler : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<Rigidbody> _rigidbodies;

        public void GetAnimator(Animator animator) => 
            _animator = animator;

        public void GetRigidbodies(List<Rigidbody> rigidbodies) =>
            _rigidbodies = rigidbodies;

        public void Enable() => 
            _animator.enabled = false;

        public void ApplyExplosionForce(float force, Vector3 explosionPosition, float explosionRadius, float upwardsModifier)
        {
            foreach (var rigidbody in _rigidbodies)
                rigidbody.AddExplosionForce(force, explosionPosition, explosionRadius, upwardsModifier);
        }
    }
}
