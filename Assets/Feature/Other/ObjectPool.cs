using System.Collections.Generic;
using UnityEngine;

namespace TaskArcher.Other
{
    public class ObjectPool
    {
        private readonly GameObject _prefab;
        private readonly List<GameObject> _pool = new List<GameObject>();

        public IReadOnlyList<GameObject> GetPool => _pool;

        public GameObject this[int index] => GetPool[index];

        public ObjectPool(GameObject prefab) => _prefab = prefab;

        public GameObject GetObject(Vector3 position = default)
        {
            GameObject obj = GetDisabledObjectOrNull();

            if (!obj)
            {
                obj = Object.Instantiate(_prefab, position, Quaternion.identity);
                _pool.Add(obj);
            }
            else
                obj.transform.position = position;

            obj.SetActive(true);
            return obj;
        }

        public void ReturnObject(GameObject obj) => obj.SetActive(false);

        private GameObject GetDisabledObjectOrNull()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].activeInHierarchy)
                    continue;

                return _pool[i];
            }

            return null;
        }
    }
}