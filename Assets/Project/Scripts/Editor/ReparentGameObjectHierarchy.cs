using UnityEditor;
using UnityEngine;

namespace Project.Scripts.Editor
{
    public class ReparentGameObjectHierarchyEditor : ScriptableObject
    {
        // This method is called by the MenuItem in the Unity Editor
        [MenuItem("GameObject/Reparent GameObject Hierarchy")]
        private static void ReparentGameObjectHierarchy()
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;
            if (selectedGameObjects.Length == 0)
            {
                Debug.LogWarning("No GameObjects selected");
                return;
            }

            GameObject parentGameObject = selectedGameObjects[0];
            for (int i = 1; i < selectedGameObjects.Length; i++)
            {
                selectedGameObjects[i].transform.SetParent(parentGameObject.transform);
            }
        }
    }
}
