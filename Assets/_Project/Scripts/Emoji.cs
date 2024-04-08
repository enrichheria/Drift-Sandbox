using UnityEngine;

namespace Emojiparticles
{
    public class Emoji : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public ParticleSystem ParticleSystem => _particleSystem;
    }
}
