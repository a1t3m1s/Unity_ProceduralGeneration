using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainGeneration : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private int _depth;

    [SerializeField] private float _noiseScale;

    [SerializeField] private int _seed;

    private float _offsetX;
    private float _offsetY;

    public void GenerateTerrain()
    {
        System.Random rnd = new System.Random(_seed);
        _offsetX = rnd.Next(-100000, 100000);
        _offsetY = rnd.Next(-100000, 100000);

        Terrain terrain = GetComponent<Terrain>();
        TerrainData terrainData = terrain.terrainData;
        terrainData.heightmapResolution = _width + 1;
        terrainData.size = new Vector3(_width, _depth, _height);
        terrainData.SetHeights(0, 0, GenerateHeights());
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[_width, _height];
        
        for(int x = 0; x < _width; ++x)
        {
            for(int y = 0; y < _height; ++y)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    private float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / _width * _noiseScale + _offsetX;
        float yCoord = (float)y / _height * _noiseScale + _offsetY;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
