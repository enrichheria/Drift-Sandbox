using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using Player.Movement.Boost;
using Tutorial;

namespace Crowd
{
    public class CrowdAI : MonoBehaviour
    {
        [SerializeField] private float _boostHumanSpeed;
        [SerializeField] private int _boostSpawnCount;
        [SerializeField] private Transform _centerPoint;
        [Title("Spawn Points")]
        [SerializeField] private List<HumanSpawnPoint> _spawnPoints;
        [SerializeField] private List<HumanSpawnPoint> _standingHumansPoints;
        [SerializeField] private Boost _boost;
        [SerializeField] private Tutor _tutor;
        [AssetsOnly]
        [SerializeField] private Human _humanPrefab;

        private void Start()
        {
            SpawnStandingHumans();
            StartCoroutine(RegularSpawning());
            StartCoroutine(BoostSpawning());
        }

        private IEnumerator RegularSpawning()
        {
            yield return new WaitUntil(() => _tutor.IsCompleted);

            var waitForSeconds = new WaitForSeconds(Random.Range(2f, 4f));

            while (true)
            {
                var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                var human = SpawnAt(spawnPoint);
                MoveHumanToCenter(human, human.Mover.DefaultSpeed);

                yield return waitForSeconds;
            }
        }

        private void SpawnStandingHumans()
        {
            foreach (var spawnPoint in _standingHumansPoints)
            {
                var human = SpawnAt(spawnPoint);
                human.transform.LookAt(_centerPoint);
                human.Animator.PlayCheering();
                StartCoroutine(RespawningActivity(human));
            }
        }

        private IEnumerator RespawningActivity(Human human)
        {
            var waitUnitlCollided = new WaitUntil(() => human.Collided);
            var waitForSeconds = new WaitForSeconds(6f);

            yield return waitUnitlCollided;

            Vector3 spawnPosition = human.StartPostition;

            yield return waitForSeconds;

            var newHuman = Instantiate(_humanPrefab, spawnPosition, Quaternion.identity);
            newHuman.transform.LookAt(_centerPoint);
            newHuman.View.EnableRandomSkin();
            newHuman.Effect.Play();

            StartCoroutine(RespawningActivity(newHuman));
        }

        private IEnumerator BoostSpawning()
        {
            int counter = 0;
            var waitUntilBoostStarted = new WaitUntil(() => _boost.IsInBoost);
            var waitUntilBoostFinished = new WaitUntil(() => _boost.IsInBoost == false);
            var waitForSeconds = new WaitForSeconds(0.1f);

            yield return waitUntilBoostStarted;

            while (counter <= _boostSpawnCount)
            {
                var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                var human = SpawnAt(spawnPoint);
                MoveHumanToCenter(human, _boostHumanSpeed);

                counter++;

                yield return waitForSeconds;
            }

            yield return waitUntilBoostFinished;

            StartCoroutine(BoostSpawning());
        }

        private IEnumerator WaitForCheering(Human human)
        {
            var waitUntil = new WaitUntil(() =>
            human.Mover.IsTargerPointReached || human.Collided);

            yield return waitUntil;

            if (human.Collided)
                yield break;

            human.Animator.PlayCheering();
        }

        private Human SpawnAt(HumanSpawnPoint spawnPoint)
        {
            var human = Instantiate(_humanPrefab, spawnPoint.transform);
            human.View.EnableRandomSkin();

            return human;
        }

        private void MoveHumanToCenter(Human human, float speed)
        {
            human.Animator.PlayRunning();
            human.Mover.MoveTo(_centerPoint.position, speed);
            StartCoroutine(WaitForCheering(human));
        }
    }
}
