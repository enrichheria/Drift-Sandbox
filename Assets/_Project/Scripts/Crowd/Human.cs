using Collidable;
using Crowd.Animation;
using Crowd.Ragdoll;
using Crowd.View;
using Player.Movement.Drift;
using Props;
using System.Collections;
using UnityEngine;

namespace Crowd
{
    public class Human : MonoBehaviour, ICollidable
    {
        [SerializeField] private HumanMover _mover;
        [SerializeField] private RagdollEnabler _ragdollEnabler;
        [SerializeField] private HumanView _humanView;
        [SerializeField] private HumanAnimator _animator;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private int _reward;
        [SerializeField] private ParticleSystem _effect;

        public ParticleSystem Effect => _effect;
        public HumanMover Mover => _mover;
        public HumanView View => _humanView;
        public HumanAnimator Animator => _animator;
        public bool Collided { get; private set; }
        public Vector3 StartPostition { get; private set; }
        public int Reward => _reward;
        public RagdollEnabler RagdollEnabler => _ragdollEnabler;

        private void Start()
        {
            StartPostition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DriftMovement _) || 
                other.TryGetComponent(out PropsBase _))
            {
                StartCollisionActivity();
            }
        }

        public void StartCollisionActivity()
        {
            Collided = true;
            _capsuleCollider.enabled = false;
            _mover.StopMovement();
            _ragdollEnabler.Enable();
            StartCoroutine(DestroyingBody());
        }

        private IEnumerator DestroyingBody()
        {
            float delay = 5f;
            var waitForSeconds = new WaitForSeconds(delay);

            yield return waitForSeconds;

            Destroy(gameObject);
        }
    }
}
