using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class DeleteSelectedCollidersEditor : EditorWindow
{
    private bool excludeBoxCollider = false;
    private bool excludeSphereCollider = false;
    private bool excludeCapsuleCollider = false;
    private bool excludeMeshCollider = false;
    private bool excludeWheelCollider = false;
    private bool excludeCharacterController = false;

    private List<Collider> ignoredColliders = new List<Collider>();
    private Vector2 scrollPos;

    [MenuItem("Tools/Delete Selected Colliders In Scene")]
    public static void ShowWindow()
    {
        GetWindow<DeleteSelectedCollidersEditor>("Delete Colliders");
    }

    void OnGUI()
    {
        GUILayout.Label("Exclude Collider Types", EditorStyles.boldLabel);

        excludeBoxCollider = EditorGUILayout.Toggle("BoxCollider", excludeBoxCollider);
        excludeSphereCollider = EditorGUILayout.Toggle("SphereCollider", excludeSphereCollider);
        excludeCapsuleCollider = EditorGUILayout.Toggle("CapsuleCollider", excludeCapsuleCollider);
        excludeMeshCollider = EditorGUILayout.Toggle("MeshCollider", excludeMeshCollider);
        excludeWheelCollider = EditorGUILayout.Toggle("WheelCollider", excludeWheelCollider);
        excludeCharacterController = EditorGUILayout.Toggle("CharacterController", excludeCharacterController);

        GUILayout.Space(10);
        GUILayout.Label("Ignore Specific Colliders", EditorStyles.boldLabel);

        // List Field
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(150));
        for (int i = 0; i < ignoredColliders.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            ignoredColliders[i] = (Collider)EditorGUILayout.ObjectField(ignoredColliders[i], typeof(Collider), true);

            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                ignoredColliders.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

        // Add new slot
        if (GUILayout.Button("Add Collider Slot"))
        {
            ignoredColliders.Add(null);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Delete Colliders"))
        {
            DeleteColliders();
        }
    }

    void DeleteColliders()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int deletedCount = 0;

        foreach (GameObject obj in allObjects)
        {
            Collider[] colliders = obj.GetComponents<Collider>();

            foreach (Collider col in colliders)
            {
                if (!ignoredColliders.Contains(col) && ShouldDelete(col))
                {
                    Undo.DestroyObjectImmediate(col);
                    deletedCount++;
                }
            }

            if (!excludeCharacterController)
            {
                CharacterController cc = obj.GetComponent<CharacterController>();
                if (cc != null && !IsIgnored(cc))
                {
                    Undo.DestroyObjectImmediate(cc);
                    deletedCount++;
                }
            }
        }

        Debug.Log($"Deleted {deletedCount} collider(s) from the scene.");
    }

    bool ShouldDelete(Collider col)
    {
        Type type = col.GetType();

        if (excludeBoxCollider && type == typeof(BoxCollider)) return false;
        if (excludeSphereCollider && type == typeof(SphereCollider)) return false;
        if (excludeCapsuleCollider && type == typeof(CapsuleCollider)) return false;
        if (excludeMeshCollider && type == typeof(MeshCollider)) return false;
        if (excludeWheelCollider && type == typeof(WheelCollider)) return false;

        return true;
    }

    bool IsIgnored(Component comp)
    {
        foreach (var ignored in ignoredColliders)
        {
            if (ignored != null && ignored.gameObject == comp.gameObject)
                return true;
        }
        return false;
    }
}
