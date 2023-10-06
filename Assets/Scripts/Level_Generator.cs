using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Generator : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    int[,] levelMap =
            {
                {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
                {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
                {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
                {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
                {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
                {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
                {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
                {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
                {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
                {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
                {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
                {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
                {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
                {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
                {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
            };
    void Start()
    {

        for(int row = 0; row < levelMap.GetLength(0); row++)
        {
            for(int col = 0; col < levelMap.GetLength(1); col++)
            {
                int tileType = levelMap[row, col];
                GameObject tilePrefab = null;
                switch (tileType)
                {
                    case 0:
                        // Empty space, do nothing
                        break;
                    case 1:
                        tilePrefab = wallPrefab;
                        break;
                    case 2:
                        tilePrefab = floorPrefab;
                        break;
                        // Add more cases for other tile types
                }
                if (tilePrefab != null)
                {

                    Instantiate(tilePrefab, new Vector3(col, -row, 0), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
