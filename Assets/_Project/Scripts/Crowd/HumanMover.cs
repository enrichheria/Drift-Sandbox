using System.Collections;
using UnityEngine;

namespace Crowd
{
    public class HumanMover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Coroutine _coroutine;

        public float DefaultSpeed => _speed;
        public bool IsTargerPointReached { get; private set; }

        public void MoveTo(Vector3 targetPosition, float speed)
        {
            transform.LookAt(targetPosition);
            _coroutine = StartCoroutine(MovingTo(targetPosition, speed));
        }

        public void StopMovement()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private IEnumerator MovingTo(Vector3 targetPosition, float speed)
        {
            float stopDistance = Random.Range(22f, 24f);

            while (Vector3.Distance(transform.position, targetPosition) >= stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                yield return null;
            }

            IsTargerPointReached = true;
        }
    }
}
