using UnityEngine;

namespace Crowd
{
    public class HumanSpawnPoint : MonoBehaviour
    {
        [SerializeField] private bool _isRunningHuman;
        [SerializeField] private bool _isStandingHuman;
        private void OnDrawGizmos()
        {
            if (_isRunningHuman)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.position, 2f);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + Vector3.up * 2, Vector3.zero);
            }
            else if (_isStandingHuman)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(transform.position, 2f);
            }
        }
    }
}
