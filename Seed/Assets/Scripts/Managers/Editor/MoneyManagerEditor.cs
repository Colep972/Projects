
using UnityEditor;
using UnityEngine;

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
    }
}

