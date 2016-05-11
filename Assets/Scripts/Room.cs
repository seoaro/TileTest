using UnityEngine;

public class Room
{
    public int xPosRoom;
    public int yPosRoom;
    public int roomWidth;
    public int roomHeight;
    public Direction enteringCorridor;

	public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows)
    {
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        xPosRoom = Mathf.RoundToInt(columns * 0.5f - roomWidth * 0.5f);
        yPosRoom = Mathf.RoundToInt(columns * 0.5f - roomHeight * 0.5f);
        Debug.Log("First Room x: " + xPosRoom + " y: " + yPosRoom);
    }

    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor)
    {
        Debug.Log("2nd Room");
        // Set the entering corridor direction.
        enteringCorridor = corridor.direction;

        // Set random values for width and height.
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        switch (corridor.direction)
        {
            // If the corridor entering this room is going north...
            case Direction.North:
                // ... the height of the room mustn't go beyond the board so it must be clamped based
                // on the height of the board (rows) and the end of corridor that leads to the room.
                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.EndPositionY);

                // The y coordinate of the room must be at the end of the corridor (since the corridor leads to the bottom of the room).
                yPosRoom = corridor.EndPositionY;

                // The x coordinate can be random but the left-most possibility is no further than the width
                // and the right-most possibility is that the end of the corridor is at the position of the room.
                xPosRoom = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);

                // This must be clamped to ensure that the room doesn't go off the board.
                xPosRoom = Mathf.Clamp(xPosRoom, 0, columns - roomWidth);
                break;
            case Direction.East:
                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);
                xPosRoom = corridor.EndPositionX;

                yPosRoom = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
                yPosRoom = Mathf.Clamp(yPosRoom, 0, rows - roomHeight);
                break;
            case Direction.South:
                roomHeight = Mathf.Clamp(roomHeight, 1, corridor.EndPositionY);
                yPosRoom = corridor.EndPositionY - roomHeight + 1;

                xPosRoom = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPosRoom = Mathf.Clamp(xPosRoom, 0, columns - roomWidth);
                break;
            case Direction.West:
                roomWidth = Mathf.Clamp(roomWidth, 1, corridor.EndPositionX);
                xPosRoom = corridor.EndPositionX - roomWidth + 1;

                yPosRoom = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
                yPosRoom = Mathf.Clamp(yPosRoom, 0, rows - roomHeight);
                break;
        }
    }
}
