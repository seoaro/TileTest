using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour
{

    public int sizeX = 100;
    public int sizeZ = 50;
    public float tileSize = 1.0f;
    //int tileResolution = 8;
	// Use this for initialization
	void Start ()
    {
        BuildMesh();
	}

    void BuildTexture()
    {
        int texWidth = 10;
        int texHeight = 10;
        //Texture2D texture = new Texture2D(sizeX * tileResolution, sizeZ * tileResolution);
        Texture2D texture = new Texture2D(texWidth, texHeight);

        for(int y = 0; y < texHeight; y++)
        {
            for(int x = 0; x < texWidth; x++)
            {
                Color c = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                
                texture.SetPixel(x, y, c);
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();

        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        mesh_renderer.sharedMaterials[0].mainTexture = texture;
        Debug.Log("Done Texture!");
    }

    public void BuildMesh()
    {
        int numTiles = sizeX * sizeZ;
        int numTris = numTiles * 2;

        int vsizeX = sizeX + 1;
        int vsizeZ = sizeZ + 1;
        int numVerts = vsizeX * vsizeZ;

        // Generate the mesh data
        Vector3[] vertices = new Vector3[numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        int[] triangles = new int[numTris * 3];

        int x, z;
        for (z = 0; z < vsizeZ; z++)
        {
            for (x = 0; x < vsizeX; x++)
            {
                vertices[z * vsizeX + x] = new Vector3(x * tileSize, Random.Range(-1f, 1f), z * tileSize);
                normals[z * vsizeX + x] = Vector3.up;
                uv[z * vsizeX + x] = new Vector2((float)x / sizeX, (float)z / sizeZ);
            }
        }
        Debug.Log("Done Verts!");
        /*
        303  304
           /
        202  203                                            302
           /
        101  102  103  104  105  106  107  108  109  110..  201
        |  /  |
        0     1    2    3    4    5    6    7    8    9..    100 

        */
        for (z = 0; z < sizeZ; z++)
        {
            for (x = 0; x < sizeX; x++)
            {
                int squareIndex = z * sizeX + x;      //0    201    401
                int triOffset = squareIndex * 6;
                // First left triangle (0, 101, 102)
                triangles[triOffset + 0] = z * vsizeX + x;              //0    101   202 (by z++)
                triangles[triOffset + 1] = z * vsizeX + x + vsizeX;     //101  202   303
                triangles[triOffset + 2] = z * vsizeX + x + vsizeX + 1; //102  203   304
                // Second right triangle (0, 102, 1)
                triangles[triOffset + 3] = z * vsizeX + x;              //0    101   202
                triangles[triOffset + 4] = z * vsizeX + x + vsizeX + 1; //102  203   304
                triangles[triOffset + 5] = z * vsizeX + x + 1;          //1    102   203
            }
        }
        Debug.Log("Done Triangles!");
        /*
        2   3

        0   1 
        Unity has ClockWise Winding Order
        023 031
         */
        /*
       vertices[0] = new Vector3(0, 0, 0);
       vertices[1] = new Vector3(1, 0, 0);
       vertices[2] = new Vector3(0, 0, 1);
       vertices[3] = new Vector3(1, 0, 1);

       triangles[0] = 0;
       triangles[1] = 2;
       triangles[2] = 3;

       triangles[3] = 0;
       triangles[4] = 3;
       triangles[5] = 1;


       normals[0] = Vector3.up;
       normals[1] = Vector3.up;
       normals[2] = Vector3.up;
       normals[3] = Vector3.up;

       uv[0] = new Vector2(0, 0);
       uv[1] = new Vector2(1, 0);
       uv[2] = new Vector2(0, 1);
       uv[3] = new Vector2(1, 1);
       */


        // Create a new mesh
        Mesh mesh = new Mesh();
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        // Assign our mesh to our filter, renderer, collider
        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        //MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        MeshCollider mesh_collider = GetComponent<MeshCollider>();

        mesh_filter.mesh = mesh;
        mesh_collider.sharedMesh = mesh;
        Debug.Log("Done Mesh!");

        BuildTexture();
    }
}
