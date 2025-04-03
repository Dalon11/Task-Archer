using UnityEngine;
using Spine.Unity;
using Spine;

namespace TaskArcher.Animations
{
    public class SkeletonAnimationController : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        private AnimationReferenceAsset _nextAnimation;

        public Skeleton SkeletonObject => _skeletonAnimation.skeleton;

        public TrackEntry PlayAnimation(AnimationReferenceAsset animation, bool loop, int trackIndex = 0)
        {
            if (_skeletonAnimation.AnimationState.GetCurrent(trackIndex).Animation.Equals(animation.Animation))
                return null;

            return _skeletonAnimation.AnimationState.SetAnimation(trackIndex, animation, loop);
        }

        public TrackEntry AddAnimation(AnimationReferenceAsset animation, bool loop, int trackIndex = 0, float delay = 0)
        {
            if (_nextAnimation && _nextAnimation.Equals(animation))
                return null;

            _nextAnimation = animation;
            return _skeletonAnimation.AnimationState.AddAnimation(trackIndex, animation, loop, delay);
        }
    }
}