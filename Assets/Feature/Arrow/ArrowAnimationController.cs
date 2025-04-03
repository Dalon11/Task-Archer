using Spine.Unity;
using TaskArcher.Animations;
using UnityEngine;

namespace TaskArcher.Arrow.Controllers
{
    public class ArrowAnimationController : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimationController _animationController;
        [SerializeField] private AnimationReferenceAsset _idleAnimation;
        [SerializeField] private AnimationReferenceAsset _hitAnimation;

        private void OnEnable() => _animationController.PlayAnimation(_idleAnimation, true);

        public void PlayAnimationHit() => _animationController.PlayAnimation(_hitAnimation, false);
    }
}