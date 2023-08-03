using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    public bool isMovingRight = false;
    public float moveSpeed = 1.6f;


    void Update()
    {
        if (transform.position.x >= LevelManager.ins.currentLevel.rightBorder.position.x && isMovingRight)
        {
            isMovingRight = false;
        }
        else if (transform.position.x <= LevelManager.ins.currentLevel.leftBorder.position.x && !isMovingRight)
        {
            isMovingRight = true;
        }

        else
        {
            Vector3 patrolTarget = transform.position;
            if (isMovingRight)
            {
                patrolTarget.x = LevelManager.ins.currentLevel.rightBorder.position.x;
                transform.position = Vector3.MoveTowards(transform.position, patrolTarget, Time.deltaTime * moveSpeed);
            }
            else
            {
                patrolTarget.x = LevelManager.ins.currentLevel.leftBorder.position.x;
                transform.position = Vector3.MoveTowards(transform.position, patrolTarget, Time.deltaTime * moveSpeed);
            }
        }
    }
}
