using UnityEngine;

public class Room : MonoBehaviour
{
    public int xPos;                      // The x coordinate of the lower left tile of the room.
    public int zPos;                      // The y coordinate of the lower left tile of the room.
    public int roomWidth;                     // How many tiles wide the room is.
    public int roomHeight;                    // How many tiles high the room is.
    public Direction enteringCorridor;    // The direction of the corridor that is entering this room.

    public GameObject Corner1;
    public GameObject Corner2;
    public GameObject Corner3;
    public GameObject Corner4;

    public GameObject LeftMost;
    public GameObject RightMost;

    public int EnemiesInRoom;

    public GameObject DefaultEnemy;

    public GameObject[] RoomEnemies;

    public GameObject[] Enemies_Prefabs;

    //public Transform DefaultEnemyLeftMost;
    //public Transform DefaultEnemyRightMost;


    public void SetCornersAndInstantiateEnemies()
    {
        Corner1 = new GameObject("1st Corner");

        Vector3 pos;

        pos.x = xPos;
        pos.y = 0;
        pos.z = zPos;

        Corner1.transform.position = pos;


        Corner3 = new GameObject("3rd Corner");

        Vector3 pos3;

        pos3.x = xPos + roomWidth-1;
        pos3.y = 0;
        pos3.z = zPos + roomHeight-1;

        Corner3.transform.position = pos3;


        Corner2 = new GameObject("2nd Corner");

        Vector3 pos2;

        pos2.x = xPos;// + roomWidth;
        pos2.y = 0;
        pos2.z = zPos + roomHeight-1;

        Corner2.transform.position = pos2;


        Corner4 = new GameObject("4th Corner");

        Vector3 pos4;

        pos4.x = Corner3.transform.position.x-1;// + roomWidth;
        pos4.y = 0;
        pos4.z = Corner1.transform.position.z;

        Corner4.transform.position = pos4;






        LeftMost = new GameObject("LeftMost");
        Vector3 posLeftMost;

        posLeftMost.x = Corner2.transform.position.x + ((Corner3.transform.position.x - Corner2.transform.position.x) / 2);
        posLeftMost.y = 0;
        posLeftMost.z = zPos + roomHeight-1;

        LeftMost.transform.position = posLeftMost;


        RightMost = new GameObject("RightMost");
        Vector3 posRightMost;

        posRightMost.x = Corner1.transform.position.x + ((Corner4.transform.position.x - Corner1.transform.position.x) / 2);
        posRightMost.y = 0;
        posRightMost.z = Corner1.transform.position.z;// zPos + roomHeight;

        RightMost.transform.position = posRightMost;


        EnemiesInRoom= Random.Range(2, 4);


        RoomEnemies = new GameObject[EnemiesInRoom];

        for (int e = 1; e < EnemiesInRoom; e++)
        {
            int randomIndex = Random.Range(0, Enemies_Prefabs.Length);

            DefaultEnemy = Enemies_Prefabs[randomIndex];

            GameObject EnemyInstance = Instantiate(DefaultEnemy, LeftMost.transform.position, Quaternion.identity) as GameObject;

            RoomEnemies[e] = EnemyInstance;

            EnemyInstance.name = "Enemy";

            Enemy EnemyScript = EnemyInstance.GetComponent<Enemy>();
            //EnemyScript.LeftMost = Corner2.transform ;
            //EnemyScript.RightMost = Corner1.transform;

            EnemyScript.LeftMost = LeftMost.transform;// Corner2.transform;
            EnemyScript.RightMost = RightMost.transform;// Corner1.transform;

            EnemyScript.Corner1 = Corner1;
            EnemyScript.Corner2 = Corner2;
            EnemyScript.Corner3 = Corner3;
            EnemyScript.Corner4 = Corner4;

        }

           


    }
    


    // This is used for the first room.  It does not have a Corridor parameter since there are no corridors yet.
    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, GameObject[] EnemiesPrefabs)
    {
        Enemies_Prefabs = EnemiesPrefabs;
        int randomIndex = Random.Range(0, EnemiesPrefabs.Length);
               
        DefaultEnemy = EnemiesPrefabs[randomIndex];

        // Set a random width and height.
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        // Set the x and y coordinates so the room is roughly in the middle of the board.
        xPos = Mathf.RoundToInt((columns / 2f) - (roomWidth / 2f));
        zPos = Mathf.RoundToInt((rows / 2f) - (roomHeight / 2f));


        SetCornersAndInstantiateEnemies();

    }


    // This is an overload of the SetupRoom function and has a corridor parameter that represents the corridor entering the room.
    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor, GameObject[] EnemiesPrefabs)
    {
        Enemies_Prefabs = EnemiesPrefabs;

        int randomIndex = Random.Range(0, EnemiesPrefabs.Length);

        DefaultEnemy = EnemiesPrefabs[randomIndex];


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
                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.EndPositionZ);

                // The y coordinate of the room must be at the end of the corridor (since the corridor leads to the bottom of the room).
                zPos = corridor.EndPositionZ;

                // The x coordinate can be random but the left-most possibility is no further than the width
                // and the right-most possibility is that the end of the corridor is at the position of the room.
                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);

                // This must be clamped to ensure that the room doesn't go off the board.
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case Direction.East:
                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);
                xPos = corridor.EndPositionX;

                zPos = Random.Range(corridor.EndPositionZ - roomHeight + 1, corridor.EndPositionZ);
                zPos = Mathf.Clamp(zPos, 0, rows - roomHeight);
                break;
            case Direction.South:
                roomHeight = Mathf.Clamp(roomHeight, 1, corridor.EndPositionZ);
                zPos = corridor.EndPositionZ - roomHeight + 1;

                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case Direction.West:
                roomWidth = Mathf.Clamp(roomWidth, 1, corridor.EndPositionX);
                xPos = corridor.EndPositionX - roomWidth + 1;

                zPos = Random.Range(corridor.EndPositionZ - roomHeight + 1, corridor.EndPositionZ);
                zPos = Mathf.Clamp(zPos, 0, rows - roomHeight);
                break;
        }

        SetCornersAndInstantiateEnemies();

    }
}