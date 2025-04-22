using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainGeneration))]
public class TerrainGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainGeneration terrainGeneration = (TerrainGeneration)target;
        DrawDefaultInspector();

        if(GUILayout.Button("Generate"))
        {
            terrainGeneration.GenerateTerrain();
        }
    }
}
