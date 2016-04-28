using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour
{
    public GameObject upperLeft;
    public GameObject left;
    public GameObject lowerLeft;
    public GameObject down;
    public GameObject lowerRight;
    public GameObject right;
    public GameObject upperRight;
    public GameObject up;
    public GameObject middle;

    void Start()
    {
        upperLeft = Resources.Load("Prefabs/tile_upper_left") as GameObject;
        left = Resources.Load("Prefabs/tile_left") as GameObject;
        lowerLeft = Resources.Load("Prefabs/tile_lower_left") as GameObject;
        down = Resources.Load("Prefabs/tile_down") as GameObject;
        lowerRight = Resources.Load("Prefabs/tile_lower_right") as GameObject;
        right = Resources.Load("Prefabs/tile_right") as GameObject;
        upperRight = Resources.Load("Prefabs/tile_upper_right") as GameObject;
        up = Resources.Load("Prefabs/tile_up") as GameObject;
        middle = Resources.Load("Prefabs/tile_middle") as GameObject;
    }
}
