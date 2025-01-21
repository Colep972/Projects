#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PotManager))]
public class PotManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PotManager potManager = (PotManager)target;
        Texture2D potIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Sprites/potmanager.png");
        if (potIcon != null)
        {
            GUILayout.Label(new GUIContent(potIcon));
        }
        DrawDefaultInspector();
    }
}
#endif