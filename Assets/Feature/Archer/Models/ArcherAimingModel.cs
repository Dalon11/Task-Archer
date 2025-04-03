using System;
using UnityEngine;

namespace TaskArcher.Archer.Models
{
    [Serializable]
    public class ArcherAimingModel
    {
        [SerializeField] private float _maxPower = 15f;
        [SerializeField] private float _minPower = 3f;
        [SerializeField] private float _powerMultiplier = 3f;

        public float MaxPower => _maxPower;
        public float MinPower => _minPower;
        public float PowerMultiplier => _powerMultiplier;
    }
}