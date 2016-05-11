using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class TileMapMouse : MonoBehaviour {

    TileMap _tileMap;
    Vector3 currentTileCoord;
    public Transform selectedCube;
    int x;
    int z;
    

	// Use this for initialization
	void Start () {
        _tileMap = GetComponent<TileMap>();
        
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            x = Mathf.FloorToInt((hitInfo.point.x) / _tileMap.tileSize);
            z = Mathf.FloorToInt((hitInfo.point.z) / _tileMap.tileSize);
            Debug.Log("Tile: " + x + ", " + z);

            currentTileCoord.x = x;
            currentTileCoord.z = z;

            selectedCube.transform.position = currentTileCoord * _tileMap.tileSize;
        }
        else
        {
            //GetComponent<Renderer>().material.color = Color.green;
        }

        if(Input.GetMouseButtonDown(1) && selectedCube != null)
        {
            selectedCube.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        }else if(Input.GetMouseButtonUp(1) && selectedCube != null)
        {
            selectedCube.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
