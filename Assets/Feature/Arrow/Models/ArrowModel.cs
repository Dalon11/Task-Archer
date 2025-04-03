using System;
using UnityEngine;

namespace TaskArcher.Arrow.Models
{
    [Serializable]
    public class ArrowModel
    {
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _lifeTime = 3f;
        [SerializeField] private float _rotationSpeed = 10f;

        public float Damage => _damage;
        public float LifeTime => _lifeTime;
        public float RotationSpeed => _rotationSpeed;
    }
}
