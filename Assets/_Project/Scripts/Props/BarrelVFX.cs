using UnityEngine;

namespace Props.Barrel
{
    public class BarrelVFX  : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosion;

        public void PlayExplosion()
        {
            ResetPosition();
            _explosion.Play();
        }

        private void ResetPosition()
        {
            _explosion.transform.SetParent(null);
            _explosion.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
