using UnityEngine;

namespace Props
{
    public class PropsPhysics : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;

        public void EnablePhysics()
        {
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
        }

        public void AddExposionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier)
        {
            _rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier);
        }
    }
}
