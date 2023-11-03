
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector3 targetPosition;
    private Vector3 lastInput;
    private int[,] map;
    private Vector2Int mapPosition;
    private Vector2Int currentDirection = Vector2Int.zero;
    private Vector2Int nextDirection = Vector2Int.zero;

    void Start()
    {
        map = new int[,]
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,4,3,5,3,4,4,2},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,0,0,0,4,4,6,2},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,4,3,5,3,4,4,2},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,4,3,4,4,3,2},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,4,4,5,5,5,5,5,2},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,3,5,1,2,2,2,1},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,3,5,1,2,2,2,1},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,4,4,5,5,5,5,5,2},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,4,3,4,4,3,2},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,4,3,5,3,4,4,2},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,0,0,0,4,4,6,2},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,4,3,5,3,4,4,2},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},

        };


        mapPosition = new Vector2Int(1, 1);
        targetPosition = ArrayIndexToWorldPosition(mapPosition);
        lastInput = Vector3.zero;
        transform.position = targetPosition;
    }

    void Update()
    {
        GatherInput();
        MovePacStudent();
    }

    void GatherInput()
    {
        Vector2 input = Vector2.zero;

        // Capture input from the keyboard
        if (Input.GetKeyDown(KeyCode.W)) input = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A)) input = Vector2.left;
        if (Input.GetKeyDown(KeyCode.S)) input = Vector2.up;
        if (Input.GetKeyDown(KeyCode.D)) input = Vector2.right;

        // If there's new input and it's different from the current direction
        if (input != Vector2.zero)
        {
            Vector2Int newDirection = new Vector2Int((int)input.x, (int)input.y);

            // Check if we can immediately apply the new direction
            if (IsValidMove(mapPosition + newDirection))
            {
                // If the new input direction is perpendicular to the current movement,
                // we can change direction immediately on the next grid cell.
                if (currentDirection == Vector2Int.zero || IsPerpendicular(currentDirection, newDirection))
                {
                    nextDirection = newDirection;
                }
            }
        }
    }

    void MovePacStudent()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // When we reach a grid cell, we check if we need to change direction
        if (AtTargetPosition())
        {
            if (nextDirection != Vector2Int.zero)
            {
                // Apply the new direction
                if (IsValidMove(mapPosition + nextDirection))
                {
                    SetTargetPosition(nextDirection);
                    currentDirection = nextDirection;
                }

                nextDirection = Vector2Int.zero; // Reset the next direction
            }
            else if (currentDirection != Vector2Int.zero)
            {
                // Continue in the current direction if it's still valid
                if (IsValidMove(mapPosition + currentDirection))
                {
                    SetTargetPosition(currentDirection);
                }
                else
                {
                    currentDirection = Vector2Int.zero; // Stop moving if the current direction is not valid anymore
                }
            }
        }
    }
    private bool IsValidMove(Vector2Int newPos)
    {
        if (newPos.x < 0 || newPos.x >= map.GetLength(1) || newPos.y < 0 || newPos.y >= map.GetLength(0))
            return false;

        int tileValue = map[newPos.y, newPos.x];
        return tileValue == 5 || tileValue == 6 || tileValue == 0; // Only allow movement on tiles with a value of 5 or 6
    }

    Vector3 ArrayIndexToWorldPosition(Vector2Int arrayPosition)
    {
        return new Vector3(arrayPosition.x - 14 + 0.5f, 14 - arrayPosition.y + 0.5f, 0);
    }

    bool AtTargetPosition()
    {
        // Check if the character's current position is approximately equal to the target position
        return Vector3.Distance(transform.position, targetPosition) < 0.1f;
    }

    private bool IsPerpendicular(Vector2Int current, Vector2Int next)
    {
        return current.x != next.x && current.y != next.y;
    }
    void SetTargetPosition(Vector2Int direction)
    {
        // Update the map position with the new direction
        mapPosition += direction;
        // Convert the updated map position to the world position
        targetPosition = ArrayIndexToWorldPosition(mapPosition);
        // Store the direction as the current direction
        currentDirection = direction;
    }
}