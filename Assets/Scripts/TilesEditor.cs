using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TilesManager))]
public class TilesEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TilesManager myScript = (TilesManager)target;

        if (GUILayout.Button("Generate Tile"))
        {
            myScript.GenerateAllTile();
        }
    }
}
