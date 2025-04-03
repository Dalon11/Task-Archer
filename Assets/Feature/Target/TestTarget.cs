using Spine.Unity;
using TaskArcher.Animations;
using TaskArcher.Damage.Abstractions;
using UnityEngine;

namespace TaskArcher.Target
{
    public class TestTarget : MonoBehaviour, IDamageable
    {
        [SerializeField] private AnimationReferenceAsset _idle;
        [SerializeField] private AnimationReferenceAsset _death;

        private SkeletonAnimationController _animationController;
        private bool _isHit = false;

        private void Awake() => _animationController = GetComponent<SkeletonAnimationController>();

        private void Start() => PlayIdle();

        public void TakeDamage(float damage) => PlayDeath();

        private void PlayDeath()
        {
            if (_isHit)
                return;

            _isHit = true;
            var track = _animationController.PlayAnimation(_death, false);
            track.Complete += (_) =>
            {
                gameObject.SetActive(false);
                Invoke(nameof(PlayIdle), 5f);
            };
        }

        private void PlayIdle()
        {
            gameObject.SetActive(true);
            _isHit = false;
            _animationController.PlayAnimation(_idle, true);

        }
    }
}