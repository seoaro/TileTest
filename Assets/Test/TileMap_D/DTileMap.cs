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

        public bool isConnected = false;

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

        public int centerX
        {
            get
            {
                return left + width / 2;
            }
        }

        public int centerY
        {
            get
            {
                return top + height / 2;
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
        DRoom r;
        this.sizeX = size_x;
        this.sizeY = size_y;

        map_data = new int[size_x, size_y];

        rooms = new List<DRoom>();

        int maxFails = 10;

        for(int i = 0; i < 10; i++)
        {
            int roomSizeX = Random.Range(4, 8);
            int roomSizeY = Random.Range(4, 8);

            r = new DRoom();
            r.left = Random.Range(0, size_x - roomSizeX);
            r.top = Random.Range(0, size_y - roomSizeY);
            r.width = roomSizeX;
            r.height = roomSizeY;

            if(!RoomCollider(r))
            {
                rooms.Add(r);
                
            }
            else
            {
                maxFails--;
                if(maxFails <= 0)
                {
                    break;
                }
            }

            foreach(DRoom r2 in rooms)
            {
                MakeRoom(r2);
            }


            for (int j = 0; j < rooms.Count; j++)
            {
                
                if (!rooms[j].isConnected)
                {
                    int k = Random.Range(1, rooms.Count);
                    MakeCorridor(rooms[j], rooms[(j + k) % rooms.Count]);
                }
                
            }
            //MakeCorridor(rooms[0], rooms[1]);

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

    void MakeCorridor(DRoom r1, DRoom r2)
    {
        int x = r1.centerX;
        int y = r1.centerY;

        while(x != r2.centerX)
        {
            map_data[x, y] = (int)TileType.Floor;
            x += x < r2.centerX ? 1 : -1;
        }

        while (y != r2.centerY)
        {
            map_data[x, y] = (int)TileType.Floor;
            y += y < r2.centerY ? 1 : -1;
        }


    }

    void MakeWalls()
    {
        for(int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                if(map_data[x, y] == (int)TileType.Floor && HasAdjacentFloor(x, y))
                {
                    map_data[x, y] = (int)TileType.UpWall;
                }
            }
        }
    }

    bool HasAdjacentFloor(int x, int y)
    {
        if (x > 0 && map_data[x - 1, y] == 1)
            return true;
        if (x < sizeX - 1 && map_data[x + 1, y] == 1)
            return true;
        if (y > 0 && map_data[x, y - 1] == 1)
            return true;
        if (y < sizeY - 1 && map_data[x, y + 1] == 1)
            return true;

        if (x > 0 && y > 0 && map_data[x - 1, y - 1] == 1)
            return true;
        if (x < sizeX - 1 && y > 0 && map_data[x + 1, y - 1] == 1)
            return true;

        if (x > 0 && y < sizeY - 1 && map_data[x - 1, y + 1] == 1)
            return true;
        if (x < sizeX - 1 && y < sizeY - 1 && map_data[x + 1, y + 1] == 1)
            return true;

        return false;
    }

}
