using UnityEngine;

namespace Skins
{
    public class CarSkin : MonoBehaviour
    {
        [SerializeField] private int _level;

        public int Level => _level;

        public void Enable() =>
              gameObject.SetActive(true);

        public void Disabale() =>
            gameObject.SetActive(false);
    }
}
