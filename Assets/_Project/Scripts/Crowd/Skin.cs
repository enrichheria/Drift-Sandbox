using Crowd.Animation;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crowd.View
{
    public class Skin : MonoBehaviour
    {
        [SerializeField] private Animator _skinAnimator;
        [SerializeField] private List<Rigidbody> _rigidbodies;

        public Animator Animator => _skinAnimator;
        public List<Rigidbody> Rigidbodies => _rigidbodies;

        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);

        [Button("Get rigidbodies")]
        private void GetRigidbodies()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
        }
    }
}
