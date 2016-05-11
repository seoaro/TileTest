using UnityEngine;

public enum Direction
{
    North, East, South, West,
}

public class Corridor
{
    public int startPosX;
    public int startPosY;
    public int corridorLength;
    public Direction direction;

    public int EndPositionX
    {
        get
        {
            if (direction == Direction.North || direction == Direction.South)
                return startPosX;
            if (direction == Direction.East)
                return startPosX + corridorLength - 1;
            return startPosX - corridorLength + 1;
        }
        
    }

    public int EndPositionY
    {
        get
        {
            if (direction == Direction.East || direction == Direction.West)
                return startPosY;
            if (direction == Direction.North)
                return startPosY + corridorLength - 1;
            return startPosY - corridorLength + 1;
        }
    }

    public void SetupCorridor(Room room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor)
    {
        direction = (Direction)Random.Range(0, 4);

        //Direction oppositeDirection = (Direction)(((int)room.enteringCorridor +2) % 4);
    }

}
