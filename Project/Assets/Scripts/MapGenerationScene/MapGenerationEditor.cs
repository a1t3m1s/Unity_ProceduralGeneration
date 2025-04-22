using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGeneration))]
public class MapGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGeneration mapGeneration = (MapGeneration)target;
        DrawDefaultInspector();

        if(GUILayout.Button("Generate"))
        {
            mapGeneration.GenerateMap();
        }
    }
}
