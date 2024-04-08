using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Props
{
    [RequireComponent(typeof(PropView))]
    [RequireComponent(typeof(PropsPhysics))]
    public abstract class PropsBase : MonoBehaviour
    {
        [Title("BaseComponents")]
        [SerializeField] private GameObject _transparentObject;
        [SerializeField] private GameObject _object;
        [SerializeField] private PropView _propView;
        [SerializeField] private PropsPhysics _propsPhysics;
        [SerializeField] private int _reward;

        public PropView View => _propView;
        public PropsPhysics PropsPhysics => _propsPhysics;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Car car))
            {
                car.GetMoneyIncome(_reward);
                InteractWith(car);
            }
        }

        protected abstract void InteractWith(Car car);

        public void EnableObject()
        {
            _transparentObject.SetActive(false);
            _object.SetActive(true);

            _propsPhysics.EnablePhysics();
        }
    }
}
