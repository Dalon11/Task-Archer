using System;
using UnityEngine;
using TaskArcher.Damage.Abstractions;
using TaskArcher.Other;

namespace TaskArcher.Arrow.Controllers
{
    using Arrow.Models;
   
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private Collision2DHandler _collisionHandler;
        [SerializeField] private ArrowAnimationController _arrowAnimationController;
        [SerializeField] private Rigidbody2D _rigidbody;
        [Header("Settings")]
        [SerializeField] private ArrowModel _model;

        private bool _hasHit = false;

        public event Action<ArrowController> onDisableArrow;
        public event Action<ArrowController> onDamageArrow;

        private void Start() => _collisionHandler.onCollisionEnter += CollisionEnter;

        private void OnDestroy() => _collisionHandler.onCollisionEnter -= CollisionEnter;

        private void FixedUpdate() => Rotation();

        private void Rotation()
        {
            if (_hasHit || _rigidbody.velocity.magnitude <= 0.1f)
                return;

            transform.rotation = Quaternion.Slerp(transform.rotation, QuaternionÑalculation(), _model.RotationSpeed);
        }

        private Quaternion QuaternionÑalculation()
        {
            float angle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void CollisionEnter(Collision2D collision)
        {
            if (_hasHit)
                return;

            StopArrow();
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
                DealDamage(damageable);
        }

        private void DealDamage(IDamageable damageable)
        {
            damageable.TakeDamage(_model.Damage);
            onDamageArrow?.Invoke(this);
            _arrowAnimationController.PlayAnimationHit();
        }

        private void StopArrow()
        {
            _hasHit = true;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
            Invoke(nameof(OnDisableArrow), _model.LifeTime);
        }

        private void OnDisableArrow()
        {
            onDisableArrow?.Invoke(this);
            _hasHit = false;
            _rigidbody.isKinematic = false;
        }
    }
}