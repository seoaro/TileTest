using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour
{
    public enum TileType
    {
        Floor, LowerLeftWall, DownWall, LowerRightWall, LeftWall, RightWall, UpperLeftWall, UpWall, UpperRightWall,
    }

    public GameObject lowerLeft;
    public GameObject down;
    public GameObject lowerRight;
    public GameObject left;
    public GameObject right;
    public GameObject upperLeft;
    public GameObject up;
    public GameObject upperRight;
    
    public GameObject middle;

    public int columns;
    public int rows;
    public IntRange numRooms = new IntRange(4, 4);
    public IntRange roomWidth = new IntRange(4, 4);
    public IntRange roomHeight = new IntRange(4, 4);
    public IntRange corridorLength = new IntRange(4, 4);

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outterWallTiles;
    //public GameObject player;

    private TileType[][] tiles;
    private Room[] rooms;                                     
    private Corridor[] corridors;                           
    private GameObject boardHolder;

    void Start()
    {
        lowerLeft = Resources.Load("Prefabs/tile_lower_left") as GameObject;
        down = Resources.Load("Prefabs/tile_down") as GameObject;
        lowerRight = Resources.Load("Prefabs/tile_lower_right") as GameObject;
        left = Resources.Load("Prefabs/tile_left") as GameObject;
        right = Resources.Load("Prefabs/tile_right") as GameObject;
        upperLeft = Resources.Load("Prefabs/tile_upper_left") as GameObject;
        up = Resources.Load("Prefabs/tile_up") as GameObject;
        upperRight = Resources.Load("Prefabs/tile_upper_right") as GameObject;
        
        
        middle = Resources.Load("Prefabs/tile_middle") as GameObject;

        floorTiles = new GameObject[1];
        floorTiles[0] = middle;

        wallTiles = new GameObject[8];
        wallTiles[0] = lowerLeft; ;
        wallTiles[1] = down;
        wallTiles[2] = lowerRight;
        wallTiles[3] = left;
        wallTiles[4] = right;
        wallTiles[5] = upperLeft;
        wallTiles[6] = up;
        wallTiles[7] = upperRight;

        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray();
        CreateRoomsAndCorridors();
        SetTilesValuesForRooms();

        InstantiateTiles();
        
    }

    void SetupTilesArray()
    {
        tiles = new TileType[columns][];
        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsAndCorridors()
    {
        rooms = new Room[numRooms.Random];
        corridors = new Corridor[rooms.Length - 1];

        rooms[0] = new Room();
        corridors[0] = new Corridor();

        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

        for (int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            if (i < corridors.Length)
            {
                // ... create a corridor.
                corridors[i] = new Corridor();

                // Setup the corridor based on the room that was just created.
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }
            /*
            if (i == rooms.Length * .5f)
            {
                Vector3 playerPos = new Vector3(rooms[i].xPosRoom, rooms[i].yPosRoom, 0);
                Instantiate(player, playerPos, Quaternion.identity);
            }
            */
        }

    }

    void SetTilesValuesForRooms()
    {
        
        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPosRoom + j;

                for(int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPosRoom + k;

                    tiles[xCoord][yCoord] = TileType.Floor;
                    Debug.Log("set tile value x: " + xCoord + "y: " + yCoord);
                }
            }
        }
        /*
        foreach(Room i in rooms)
        {
            Room currentRoom = i;
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPosRoom + j;

                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPosRoom + k;

                    tiles[xCoord][yCoord] = TileType.Floor;
                    Debug.Log("set tile value x: " + xCoord + "y: " + yCoord);
                }
            }
        }
        */
    }

    void InstantiateTiles()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            for(int j = 0; j < tiles[i].Length; j++)
            {
                if (i == 0 && j == 0)
                {
                    InstantiateFromArray(wallTiles[0], i, j);
                }
                else if (i == tiles.Length - 1 && j == 0)
                {
                    InstantiateFromArray(wallTiles[2], i, j);
                }
                else if (i == 0 && j == tiles[i].Length - 1)
                {
                    InstantiateFromArray(wallTiles[5], i, j);
                }
                else if (i == tiles.Length - 1 && j == tiles[i].Length - 1)
                {
                    InstantiateFromArray(wallTiles[7], i, j);
                }
                else if (j == 0)
                {
                    InstantiateFromArray(wallTiles[1], i, j);
                }
                else if (i == 0)
                {
                    InstantiateFromArray(wallTiles[3], i, j);
                }
                else if (i == tiles.Length - 1)
                {
                    InstantiateFromArray(wallTiles[4], i, j);
                }
                else if (j == tiles[i].Length - 1)
                {
                    InstantiateFromArray(wallTiles[6], i, j);
                }
                else
                {
                    InstantiateFromArray(floorTiles[0], i, j);
                }
            }
        }
    }

    void InstantiateFromArray(GameObject prefabs, float xCoord, float yCoord)
    {
        Vector3 position = new Vector3(xCoord, 0f, yCoord);

        GameObject tileInstance = Instantiate(prefabs, position, Quaternion.identity) as GameObject;
        tileInstance.transform.parent = boardHolder.transform;
    }
}
