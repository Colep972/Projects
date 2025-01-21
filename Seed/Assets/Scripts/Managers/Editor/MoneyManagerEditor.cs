
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoneyManager))]
public class MoneyManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Texture2D moneyIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Sprites/moneymanager.png");

        if (moneyIcon != null)
        {
            GUILayout.Label(new GUIContent(moneyIcon));
        }

        DrawDefaultInspector();

        MoneyManager manager = (MoneyManager)target;

        EditorGUILayout.Space(10);

        // Ajoute les boutons
        if (GUILayout.Button("Set Money"))
        {
            manager.DebugSetMoney();
        }

        if (GUILayout.Button("Add Money"))
        {
            manager.DebugAddMoney();
        }

        if (GUILayout.Button("Remove Money"))
        {
            manager.DebugRemoveMoney();
        }
    }
}

