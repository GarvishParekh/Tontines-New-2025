using UnityEngine;

namespace UI.ToolKit
{
    [CreateAssetMenu(fileName = "ZoomOut-FadeIn data", menuName = "Scriptable/ZoomIn-FadeIn")]
    public class ZoomOutFadeInAnimationData : ScriptableObject
    {
        public float zoomOutDuration = 0.3f;
        public float fadeInDuration = 0.25f;

        [Space]
        public Vector3 defaultSize;
        public Vector3 endSize;
    }
}