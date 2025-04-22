using UnityEngine;

public class MeshGeneration : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshRenderer _meshRender;
    private Mesh _mesh;

    private Vector2Int _meshSize;
    private Vector2 _topLeftSize;

    public void GenerateMesh(float[,] heightMap, Texture2D texture)
    {
        _meshSize = new Vector2Int(heightMap.GetLength(0), heightMap.GetLength(1));
        _topLeftSize = new Vector2((heightMap.GetLength(0) - 1) / -2f, (heightMap.GetLength(1) - 1) / 2f);

        int vertexIndx = 0;
        int triangleIndx = 0;

        _mesh = new Mesh();

        Vector3[] vertices = new Vector3[_meshSize.x * _meshSize.y];
        Vector2[] uv = new Vector2[_meshSize.x * _meshSize.y];
        int[] triangles = new int[(_meshSize.x - 1)*(_meshSize.y - 1)*6];

        for (int y = 0; y < _meshSize.y; ++y)
        {
            for(int x = 0; x < _meshSize.x; ++x)
            {
                vertices[vertexIndx] = new Vector3(_topLeftSize.x + x, heightMap[x, y], _topLeftSize.y - y);
                uv[vertexIndx] = new Vector2(x / (float)_meshSize.x, y / (float)_meshSize.y); 
            
                if(x < _meshSize.x - 1 && y < _meshSize.y - 1)
                {
                    triangles[triangleIndx++] = vertexIndx;
                    triangles[triangleIndx++] = vertexIndx + _meshSize.x + 1;
                    triangles[triangleIndx++] = vertexIndx + _meshSize.x;

                    triangles[triangleIndx++] = vertexIndx + _meshSize.x + 1;
                    triangles[triangleIndx++] = vertexIndx;
                    triangles[triangleIndx++] = vertexIndx + 1;
                }

                ++vertexIndx;
            }
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;

        _mesh.RecalculateNormals();
        _meshFilter.mesh = _mesh;

        _meshRender.sharedMaterial.mainTexture = texture;
    }
}
