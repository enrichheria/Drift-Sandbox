using Player;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace Props
{
    public class Monster : PropsBase
    {
        [Title("Monster")]
        [SerializeField] private ParticleSystem _effect;

        private StainHandler _stainsHandler;

        private void Awake() => 
            _stainsHandler = FindObjectOfType<StainHandler>();

        protected override void InteractWith(Car car)
        {
            _stainsHandler.Show();
            _effect.transform.SetParent(null);
            _effect.Play();
            Destroy(gameObject);
        }
    }
}
