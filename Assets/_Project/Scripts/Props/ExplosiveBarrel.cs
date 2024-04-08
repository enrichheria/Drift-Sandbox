using Crowd;
using UnityEngine;
using Cinemachine;
using System.Collections;
using Player;
using Sirenix.OdinInspector;
using Props.SkibidiToilet;
using DG.Tweening;

namespace Props.Barrel
{
    public class ExplosiveBarrel : PropsBase
    {
        [Title("Barrel")]
        [SerializeField] private float _exposionRadius;
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _upwardsModifier;
        [SerializeField, MinValue(0), MaxValue(1f)] private float _detonationSpeed;
        [SerializeField] private BarrelVFX _barrelVFX;
        [SerializeField] private CinemachineImpulseSource _impulse;
        [SerializeField] private bool _isFatboy;

        protected override void InteractWith(Car car)
        {
            if (car.Movement.Velocity >= _detonationSpeed)
                Explode();
        }

        private void Explode()
        {
            if (_isFatboy == false)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, _exposionRadius);

                foreach (Collider collider in colliders)
                {
                    CheckHumanCollision(collider);
                    CheckSkibidiCollision(collider);
                    CheckPropsCollision(collider);
                }

                _barrelVFX.PlayExplosion();
                _impulse.GenerateImpulse();

                StartCoroutine(DelayDestroy(0.2f));
            }
            else
            {
                StartCoroutine(ExplodingFatBoy());
            }
        }

        private void CheckHumanCollision(Collider collider)
        {
            if (collider.TryGetComponent(out Human human))
            {
                human.StartCollisionActivity();
                human.RagdollEnabler.ApplyExplosionForce(_explosionForce, transform.position,
                    _exposionRadius, _upwardsModifier);

                //car.GetMoneyIncome(human.Reward);
            }
        }

        private void CheckPropsCollision(Collider collider)
        {
            if(collider.TryGetComponent(out PropsBase prop))
            {
                if (prop == this)
                    return;

                prop.PropsPhysics.AddExposionForce(_explosionForce, transform.position, _exposionRadius, 2f);
            }
        }

        private void CheckSkibidiCollision(Collider collider)
        {
            if (collider.TryGetComponent(out Skibidi skibidi))
                skibidi.Activate();
        }

        private IEnumerator DelayDestroy(float delay)
        {
            yield return new WaitForSeconds(delay);

            Destroy(gameObject);
        }

        private IEnumerator ExplodingFatBoy()
        {
            transform.DOScale(Vector3.one * 2f, 1.5f).SetEase(Ease.InExpo);

            yield return new WaitForSeconds(1.55f);

            Collider[] colliders = Physics.OverlapSphere(transform.position, _exposionRadius);

            foreach (Collider collider in colliders)
            {
                CheckHumanCollision(collider);
                CheckSkibidiCollision(collider);
                CheckPropsCollision(collider);
            }

            _barrelVFX.PlayExplosion();
            _impulse.GenerateImpulse();

            StartCoroutine(DelayDestroy(0.05f));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _exposionRadius);
        }
    }
}
