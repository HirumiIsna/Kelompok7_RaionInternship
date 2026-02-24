using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NodeGenerator))]
public class InspectorButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NodeGenerator script = (NodeGenerator)target;

        if (GUILayout.Button("Generate Grid"))
        {
            script.GenerateGrid();
        }

        if (GUILayout.Button("Clear Grid"))
        {
            script.ClearGrid();
        }


    }
}