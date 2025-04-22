using System;
using UnityEngine;

[RequireComponent(typeof(MeshGeneration))]
public class MapGeneration : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _noiseScale;

    [SerializeField] private int _octaves;
    [SerializeField, Range(0, 1)] private float _persistance;
    [SerializeField] private float _lacunarity;

    [SerializeField] private int _seed;
    [SerializeField] private Vector2 _offset;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(_width, _height, _seed, _noiseScale, _octaves, _persistance, _lacunarity, _offset);
        Texture2D texture = TextureGeneration.GenerateTexture(noiseMap);

        GetComponent<MeshGeneration>().GenerateMesh(noiseMap, texture);
    }

    private void OnValidate()
    {
        if(_width < 1)
        {
            _width = 1;
        }
        if(_height < 1)
        {
            _height = 1;
        }
        if(_lacunarity < 1)
        {
            _lacunarity = 1;
        }
        if(_octaves < 0)
        {
            _octaves = 0;
        }
    }
}