using System;
using UnityEngine;

namespace TaskArcher.Other
{
    public class Collision2DHandler : MonoBehaviour
    {
        public Action<Collision2D> onCollisionEnter;

        private void OnCollisionEnter2D(Collision2D collision) => onCollisionEnter?.Invoke(collision);
    }
}