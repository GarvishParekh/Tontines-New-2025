using UnityEngine;

namespace UI.ToolKit
{
    [CreateAssetMenu(fileName = "FadeIn Animation Data", menuName = "Scriptable/FadeIn Animation Data")]
    public class FadeInAnimationData : ScriptableObject
    {
        public float animationDuration = 0.25f;
    }
}