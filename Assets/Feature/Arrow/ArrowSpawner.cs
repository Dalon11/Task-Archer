using TaskArcher.Other;
using UnityEngine;

namespace TaskArcher.Arrow.Controllers
{
    public class ArrowSpawner : MonoBehaviour
    {
        [SerializeField] private ArrowController _arrowPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _arrowsParent;

        private ObjectPool _arrowPool;

        public Transform ArrowSpawnPoint => _spawnPoint;

        private void Awake() => _arrowPool = new ObjectPool(_arrowPrefab.gameObject);

        private void OnDestroy() => Unsubscribe();

        public void SpawnArrow(Vector3 direction, float power) => ApplySpeed(CreateArrow(), direction, power);

        private ArrowController CreateArrow()
        {
            ArrowController arrow = _arrowPool.GetObject(_spawnPoint.position).GetComponent<ArrowController>();
            arrow.transform.SetParent(_arrowsParent);
            arrow.onDisableArrow += DisableArrow;
            return arrow;
        }

        private void DisableArrow(ArrowController arrow)
        {
            arrow.onDisableArrow -= DisableArrow;
            _arrowPool.ReturnObject(arrow.gameObject);
        }

        private void ApplySpeed(ArrowController arrow, Vector3 direction, float power)
        {
            if (arrow.TryGetComponent(out Rigidbody2D rigidbody))
            {
                rigidbody.velocity = direction * power;
                float angle = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void Unsubscribe()
        {
            for (int i = 0; i < _arrowPool.GetPool.Count; i++)
                if (_arrowPool.GetPool[i] && _arrowPool.GetPool[i].TryGetComponent(out ArrowController arrow))
                    arrow.onDisableArrow -= DisableArrow;
        }
    }
}