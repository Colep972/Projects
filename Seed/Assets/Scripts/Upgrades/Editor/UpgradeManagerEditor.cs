using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradeManager))]
public class UpgradeManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Texture2D upgradeIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("../Assets/Sprites/upgrademanager.png");
        if (upgradeIcon != null)
        {
            GUILayout.Label(new GUIContent(upgradeIcon));
        }
        DrawDefaultInspector();
    }
}
