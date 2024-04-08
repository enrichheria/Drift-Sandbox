using System;
using UnityEngine;

namespace Props.SkibidiToilet
{
    [Serializable]
    public class SkbidiVFX
    {
        [SerializeField] private ParticleSystem _explosion;
        [SerializeField] private Transform _transform;

        public void PlayExplosion()
        {
            var vfx = UnityEngine.Object.Instantiate(_explosion, _transform);
            vfx.transform.SetParent(null);
        }
    }
}
