using UnityEngine;
using System.Collections.Generic;

public class DTileMap
{
    protected class DRoom
    {
        public int left;
        public int top;
        public int width;
        public int height;

        public int right
        {
            get
            {
                return left + width - 1;
            }
        }

        public int bottom
        {
            get
            {
                return top + height - 1;
            }
        }

        public bool ColliderWith(DRoom other)
        {
            if(left > other.right)
            {
                return false;
            }

            if(top > other.bottom)
            {
                return false;
            }

            if(right < other.left)
            {
                return false;
            }

            if(bottom < other.top)
            {
                return false;
            }

            return true;
        }
    }

    int sizeX;
    int sizeY;

    int[,] map_data;

    List<DRoom> rooms;

    enum TileType
    {
        Unknown,
        LowerLeftWall,
        DownWall,
        LowerRightWall,
        LeftWall,
        RightWall,
        UpperLeftWall,
        UpWall,
        UpperRightWall,
        Floor,

    }

    public DTileMap(int size_x, int size_y)
    {
        this.sizeX = size_x;
        this.sizeY = size_y;

        map_data = new int[size_x, size_y];

        rooms = new List<DRoom>();

        for(int i = 0; i < 10; i++)
        {
            int roomSizeX = Random.Range(4, 8);
            int roomSizeY = Random.Range(4, 8);

            DRoom r = new DRoom();
            r.left = Random.Range(0, size_x - roomSizeX);
            r.top = Random.Range(0, size_y - roomSizeY);
            r.width = roomSizeX;
            r.height = roomSizeY;

            if(!RoomCollider(r))
            {
                rooms.Add(r);
                
            }

            foreach(DRoom r2 in rooms)
            {
                MakeRoom(r2);
            }
            //rooms.Add(r);
            //MakeRoom(r);
        }

    }

    bool RoomCollider(DRoom r)
    {
        foreach(DRoom r2 in rooms)
        {
            if(r.ColliderWith(r2))
            {
                return true;
            }
        }
        return false;
    }

    public int GetTileAt(int x, int y)
    {
        return map_data[x, y];
    }

    void MakeRoom(DRoom r)
    {
        for(int x = 0; x < r.width; x++)
        {
            for(int y = 0; y < r.height; y++)
            {
                if(x == 0 && y == 0) // Lower Left Wall
                {
                    map_data[r.left + x, r.top + y] = (int)TileType.LowerLeftWall;
                }
                else if (x == r.width - 1 && y == 0) // Lower Right Wall
                {
                    map_data[r.left + x, r.top + y] = (int)TileType.LowerRightWall;
                }
                else if (x == 0 && y == r.height - 1) // Upper Left Wall
                {
                    map_data[r.left + x, r.top + y] = (int)TileType.UpperLeftWall;
                }
                else if (x == r.width - 1 && y == r.height - 1) // Upper Right Wall
                {
                    map_data[r.left + x, r.top + y] = (int)TileType.UpperRightWall;
                }
                else if (y == 0) // Down Wall
                {
                    map_data[r.left + x, r.top + y] = (int)TileType.DownWall;
                }
                else if (x == 0) // Left Wall
                {
                    map_data[r.left + x, r.top + y] = (int)TileType.LeftWall;
                }
                else if (x == r.width - 1) // Right Wall
                {
                    map_data[r.left + x, r.top + y] = (int)TileType.RightWall;
                }
                else if (y == r.height - 1) // Up Wall
                {
                    map_data[r.left + x, r.top + y] = (int)TileType.UpWall;
                }
                else if (x > 0 && x < r.width - 1 && y > 0 && y < r.height -1) // Floor
                {
                    map_data[r.left + x, r.top + y] = (int)TileType.Floor;
                }
                else // Unknown
                {
                    map_data[r.left + x, r.top + y] = (int)TileType.Unknown;
                }
                //map_data[left + x, top + y] = (int)TileType.Floor;
            }
        }
    }

}
