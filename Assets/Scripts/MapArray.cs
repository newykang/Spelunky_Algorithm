using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class MapArray : MonoBehaviour
{
    int[,] baseMap; int[,] realMap;
    int startX; int startY;
    int endX; int endY;
    int startTileX = 0; int startTileY = 0;
    int tileTraceX = 0; int tileTraceY = 0;
    int mapXLength = 10; int mapYLength = 8;

    [SerializeField]
    GameObject dwellingTile;
    [SerializeField]
    GameObject woodTile;

    enum RoomType
    {
        RandomRoom = 0,
        LeftRight = 1,
        UpLeftRight = 2,
        DownLeftRight = 3,
        AllSides = 4,
        StartRoom = 5,
        EndRoom = 6,
    }

    // 3�� ��ġ����, 4�� ����, 5�� �Ź���, 6�� ȭ�� �߻��, 7�� ���ٵ�, 8�� �����̴� Ÿ��, 9�� ���� ���
    int[,,] StartRoom = new int[2, 8, 10]
    {
        {
            { 1, 1, 1, 9, 9, 9, 9, 9, 9, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 9, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 9, 0 },
            { 0, 0, 0, 9, 9, 9, 9, 9, 9, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
            { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
        },

        {
            { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            { 0, 0, 9, 9, 9, 9, 9, 0, 0, 0 },
            { 0, 0, 0, 9, 9, 9, 9, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 9, 9, 9, 0, 0, 0 },
            { 0, 0, 0, 9, 9, 9, 9, 9, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        },
    };
    int[,,] EndRoom = new int[1, 8, 10]
    {
        {
            { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 9, 9, 9, 0, 0, 0, 0 },
            { 0, 0, 9, 9, 9, 8, 9, 0, 0, 0 },
            { 9, 9, 9, 9, 9, 9, 9, 9, 1, 1 },
        },
    };
    int[,,] RandomRoom = new int[2, 8, 10]
    {
        {
            { 1, 1, 1, 1, 0, 1, 1, 9, 9, 9 },
            { 0, 0, 0, 0, 0, 0, 1, 9, 0, 0 },
            { 0, 0, 0, 0, 0, 8, 1, 9, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 9, 9, 9 },
            { 0, 1, 1, 1, 0, 1, 9, 9, 9, 9 },
            { 0, 1, 0, 1, 0, 1, 1, 1, 1, 9 },
            { 7, 1, 1, 1, 7, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        },

        {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 7, 6, 9, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
            { 4, 1, 1, 0, 0, 0, 5, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        },
    };
    int[,,] LeftRight = new int[1, 8, 10]{
        {
            { 1, 1, 1, 0, 0, 0, 0, 1, 1, 1 },
            { 0, 1, 6, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 0, 0, 6, 9, 9, 7, 9 },
            { 0, 0, 0, 0, 0, 9, 9, 9, 0, 0 },
            { 0, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
        },
    };
    int[,,] UpLeftRight = new int[2, 8, 10]{
        {
            { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1 },
            { 0, 0, 1, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 1, 0, 0, 1, 7, 7, 7 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
            { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1 },
        },

        {
            { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 9, 9, 9, 9 },
            { 7, 7, 1, 1, 1, 1, 9, 0, 0, 9 },
            { 0, 0, 9, 9, 1, 1, 9, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 9, 9, 9, 7 },
            { 0, 0, 0, 0, 0, 0, 0, 5, 0, 0 },
            { 1, 1, 9, 9, 0, 0, 1, 1, 1, 1 },
        },
    };
    int[,,] DownLeftRight = new int[1, 8, 10]{
        {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 1, 1, 1, 0, 1, 0, 0, 1, 1 },
            { 0, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 3, 0, 0, 7, 7, 7, 7, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
        },
    };
    int[,,] AllSides = new int[1, 8, 10]{
        {
            { 1, 1, 0, 0, 0, 0, 0, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
        },
    };

    private void Awake()
    {
        // 4 * 4 �� ���� �˰���
        baseMap = new int[4, 4];
        startX = Random.Range(0, 4);
        startY = 0;
        baseMap[startY, startX] = (int)RoomType.StartRoom;
        
        int basetraceX = startX; int basetraceY = startY;
        int moveProb; 
        do moveProb = Random.Range(0, 3); while (moveProb == 1);
        while (endY != 3)
        {
            switch (moveProb)
            {
                // Move Left
                case 0:
                    // Doesn't Get out of Bounds
                    if (basetraceX - 1 >= 0)
                    {
                        baseMap[basetraceY, --basetraceX] = (int)RoomType.LeftRight;
                        moveProb = Random.Range(0, 2);
                    }
                    else
                    {
                        if (basetraceX + 1 == 0) moveProb = Random.Range(1, 3);
                        else moveProb = 1;
                    }
                    break;

                // Move Down
                case 1:
                    if (baseMap[basetraceY, basetraceX] == (int)RoomType.StartRoom)
                    {
                        do moveProb = Random.Range(0, 3); while (moveProb == 1);
                    }
                    else if (basetraceY + 1 <= 3)
                    {
                        baseMap[basetraceY, basetraceX] = (int)RoomType.DownLeftRight;
                        baseMap[++basetraceY, basetraceX] = (int)RoomType.UpLeftRight;
                        do moveProb = Random.Range(0, 3); while (moveProb == 1);
                    }
                    // Reached EndPoint
                    else
                    {
                        endX = basetraceX; endY = basetraceY;
                        baseMap[endY, endX] = (int)RoomType.EndRoom;
                    }
                    break;

                // Move Right
                case 2:
                    // Doesn't Get out of Bounds
                    if (basetraceX + 1 <= 3)
                    {
                        baseMap[basetraceY, ++basetraceX] = (int)RoomType.LeftRight;
                        moveProb = Random.Range(1, 3);
                    }
                    else
                    {
                        if (basetraceX - 1 == 0) moveProb = Random.Range(0, 2);
                        else moveProb = 1;
                    }
                    break;

                default:
                    Debug.LogError("Error with Map 2DArray Generation");
                    break;
            }
        }

        // For maps with consecutive "DownLeftRight" rooms vertically
        for (int x = 0; x < baseMap.GetLength(1); ++x)
        {
            if (baseMap[0, x] == 3 && baseMap[1, x] == 3)
            {
                baseMap[0, x] = (int)RoomType.AllSides;
                baseMap[1, x] = (int)RoomType.AllSides;
            }
            else if (baseMap[1, x] == 3 && baseMap[2, x] == 3)
            {
                baseMap[1, x] = (int)RoomType.AllSides;
                baseMap[2, x] = (int)RoomType.AllSides;
            }
            else if (baseMap[2, x] == 3 && baseMap[3, x] == 3)
            {
                baseMap[2, x] = (int)RoomType.AllSides;
                baseMap[3, x] = (int)RoomType.AllSides;
            }
            else continue;
        }
        TestBaseMapGeneration();

        // 40 * 32 �� ���� �˰���
        realMap = new int[32, 40];
        int realStartX = 0; int realStartY = 0;
        int realTraceX; int realTraceY;
        int roomNumber;
        for (int y = 0; y < baseMap.GetLength(1); ++y)
        {
            for (int x = 0; x < baseMap.GetLength(0); ++x)
            {
                realTraceX = realStartX;
                realTraceY = realStartY;

                switch ((RoomType)baseMap[y, x])
                {
                    case RoomType.RandomRoom:
                        roomNumber = Random.Range(0, RandomRoom.GetLength(0));
                        for (int roomY = 0; roomY < mapYLength; ++roomY)
                        {
                            for (int roomX = 0; roomX < mapXLength; ++roomX)
                            {
                                realMap[realTraceY, realTraceX++] = RandomRoom[roomNumber, roomY, roomX];
                            }
                            realTraceX = realStartX;
                            ++realTraceY;
                        }
                        realStartX += 10;
                        break;

                    case RoomType.LeftRight:
                        roomNumber = Random.Range(0, LeftRight.GetLength(0));
                        for (int roomY = 0; roomY < mapYLength; ++roomY)
                        {
                            for (int roomX = 0; roomX < mapXLength; ++roomX)
                            {
                                realMap[realTraceY, realTraceX++] = LeftRight[roomNumber, roomY, roomX];
                            }
                            realTraceX = realStartX;
                            ++realTraceY;
                        }
                        realStartX += 10;
                        break;

                    case RoomType.UpLeftRight:
                        roomNumber = Random.Range(0, UpLeftRight.GetLength(0));
                        for (int roomY = 0; roomY < mapYLength; ++roomY)
                        {
                            for (int roomX = 0; roomX < mapXLength; ++roomX)
                            {
                                realMap[realTraceY, realTraceX++] = UpLeftRight[roomNumber, roomY, roomX];
                            }
                            realTraceX = realStartX;
                            ++realTraceY;
                        }
                        realStartX += 10;
                        break;

                    case RoomType.DownLeftRight:
                        roomNumber = Random.Range(0, DownLeftRight.GetLength(0));
                        for (int roomY = 0; roomY < mapYLength; ++roomY)
                        {
                            for (int roomX = 0; roomX < mapXLength; ++roomX)
                            {
                                realMap[realTraceY, realTraceX++] = DownLeftRight[roomNumber, roomY, roomX];
                            }
                            realTraceX = realStartX;
                            ++realTraceY;
                        }
                        realStartX += 10;
                        break;

                    case RoomType.AllSides:
                        roomNumber = Random.Range(0, AllSides.GetLength(0));
                        for (int roomY = 0; roomY < mapYLength; ++roomY)
                        {
                            for (int roomX = 0; roomX < mapXLength; ++roomX)
                            {
                                realMap[realTraceY, realTraceX++] = AllSides[roomNumber, roomY, roomX];
                            }
                            realTraceX = realStartX;
                            ++realTraceY;
                        }
                        realStartX += 10;
                        break;

                    case RoomType.StartRoom:
                        roomNumber = Random.Range(0, StartRoom.GetLength(0));
                        for (int roomY = 0; roomY < mapYLength; ++roomY)
                        {
                            for (int roomX = 0; roomX < mapXLength; ++roomX)
                            {
                                realMap[realTraceY, realTraceX++] = StartRoom[roomNumber, roomY, roomX];
                            }
                            realTraceX = realStartX;
                            ++realTraceY;
                        }
                        realStartX += 10;
                        break;

                    case RoomType.EndRoom:
                        roomNumber = Random.Range(0, EndRoom.GetLength(0));
                        for (int roomY = 0; roomY < mapYLength; ++roomY)
                        {
                            for (int roomX = 0; roomX < mapXLength; ++roomX)
                            {
                                realMap[realTraceY, realTraceX++] = EndRoom[roomNumber, roomY, roomX];
                            }
                            realTraceX = realStartX;
                            ++realTraceY;
                        }
                        realStartX += 10;
                        break;

                    default:
                        Debug.LogError("Error: Unidentified Room is called.");
                        break;
                }

                if (realStartX >= 40)
                {
                    realStartX = 0; realStartY += 8;
                }
            }
        }

        TilePlacement(realMap);
    }

    private void TilePlacement(int[,] realMap)
    {
        for (int y = 0; y < realMap.GetLength(0); ++y)
        {
            for (int x = 0; x < realMap.GetLength(1); ++x)
            {
                if (realMap[y, x] == 1)
                {
                    Instantiate(dwellingTile, new Vector3(tileTraceX++, tileTraceY), Quaternion.identity);
                }
                else if (realMap[y, x] == 9)
                {
                    Instantiate(woodTile, new Vector3(tileTraceX++, tileTraceY), Quaternion.identity);
                }
                else
                {
                    ++tileTraceX;
                }
            }
            tileTraceX = startTileX; --tileTraceY;
        }
    }

    private void TestBaseMapGeneration()
    {
        string row = null;
        for (int i = 0; i < baseMap.GetLength(0); ++i)
        {
            for (int j = 0; j < baseMap.GetLength(1); ++j)
            {
                row += baseMap[i, j].ToString() + " ";
            }
            Debug.Log(row);
            row = null;
        }
    }
}