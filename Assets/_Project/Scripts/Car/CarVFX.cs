using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using Emojiparticles;
using System.Collections.Generic;
using CheeringCrowd;

namespace Player.VFX
{
    public class CarVFX : MonoBehaviour
    {
        [Title("UpgradeParticles")]
        [SerializeField] private ParticleSystem _confetti;
        [SerializeField] private ParticleSystem _upgradePoof;
        [Title("Wheel smoke")]
        [SerializeField] private ParticleSystem _rightWheelSmoke;
        [SerializeField] private ParticleSystem _leftWheelSmoke;
        [Title("Skidmarks")]
        [SerializeField] private TrailRenderer _rightSkidmark;
        [SerializeField] private TrailRenderer _leftSkidmark;
        [Title("Firemarks")]
        [SerializeField] private ParticleSystem _rightFiremark;
        [SerializeField] private ParticleSystem _leftFiremark;
        [Title("Money Income")]
        [SerializeField] private TMP_Text _moneyIncomePrefab;
        [Title("Emoji")]
        [SerializeField] private List<Emoji> _emojiPrefabs;
        [SerializeField] private List<CheeringCrowdAnimator> _cheeringCrowd;

        private readonly Vector3 _rotation = new(45f, -30f, 0);
        private readonly int _offsetYMonyeIncome = 6;
        private readonly int _offsetYEmoji = 15;

        public void PlayUpgradeParticles()
        {
            var poof = Instantiate(_upgradePoof, transform);
            StartCoroutine(PlayingParticle(poof));

            var confetti = Instantiate(_confetti, transform);
            StartCoroutine(PlayingParticle(confetti));
        }

        public void EnableFiremarks()
        {
            _rightFiremark.Play();
            _leftFiremark.Play();
        }

        public void DisableFiremarks()
        {
            _rightFiremark.Stop();
            _leftFiremark.Stop();
        }

        public void EnableWheelSmoke()
        {
            _rightWheelSmoke.Play();
            _leftWheelSmoke.Play();
        }

        public void DisableWheelSmoke()
        {
            _rightWheelSmoke.Stop();
            _leftWheelSmoke.Stop();
        }

        public void EnableSkidmarks()
        {
            _rightSkidmark.emitting = true;
            _leftSkidmark.emitting = true;
        }

        public void DisableSkidmarks()
        {
            _rightSkidmark.emitting = false;
            _leftSkidmark.emitting = false;
        }

        public void ShowIncome(int value)
        {
            var moneyIncome = Instantiate(_moneyIncomePrefab,
                transform.position + Vector3.up * _offsetYMonyeIncome, Quaternion.identity);
            moneyIncome.transform.Rotate(_rotation);
            moneyIncome.text = $"${value}";

            StartShowingSequence(moneyIncome);
        }

        public void ShowEmoji()
        {
            var crowdPerson = _cheeringCrowd[Random.Range(0, _cheeringCrowd.Count)];
            var emoji = Instantiate(_emojiPrefabs[Random.Range(0, _emojiPrefabs.Count)], 
                crowdPerson.transform.position + Vector3.up * _offsetYEmoji, Quaternion.identity);

            StartCoroutine(PlayingParticle(emoji.ParticleSystem));
        }

        private void StartShowingSequence(TMP_Text moneyIncome)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(moneyIncome.DOFade(0, 0));
            sequence.Append(moneyIncome.DOFade(1f, 1f));
            sequence.Insert(0, moneyIncome.transform.DOMoveY(15f, 1f));
            sequence.Append(moneyIncome.DOFade(0, 0.5f));

            sequence.OnComplete(() =>
            {
                Destroy(moneyIncome.gameObject);
                sequence.Kill();
            });
        }

        private IEnumerator PlayingParticle(ParticleSystem particle)
        {
            particle.Play();

            yield return new WaitForSeconds(0.1f);

            particle.transform.SetParent(null);

            yield return new WaitUntil(() => particle.isPlaying == false);

            Destroy(particle.gameObject);
        }
    }
}
