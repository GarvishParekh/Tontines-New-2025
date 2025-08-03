using UnityEngine;
using UnityEditor;

namespace UI.ToolKit
{
    public class UiCreator : MonoBehaviour
    {
        [MenuItem("UI Creator/Create canvas")]
        private static void CreateCanvas()
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Main-Canvas");
            if (prefab == null) return;

            GameObject spawnedPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            spawnedPrefab.name = "Main-Canvas";
        }

        [MenuItem("UI Creator/Create popup")]
        private static void CreatePopup()
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Main-Popup");
            if (prefab == null) return;

            GameObject spawnedPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            spawnedPrefab.name = "Main-Popup";
        }
    }
}