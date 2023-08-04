using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MovementController : Singleton<MovementController>
{
    public float spdSwipe;
    public Transform moveObj;
    public Vector3 startPos;
    public Vector3 currentPos;
    public float diffX;
    public bool isBlockControl = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UI_HoverTest.ins.IsPointerOverUIElement())
        {
            startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            LevelManager.ins.StartLevel();
            UIManager.ins.CloseUI<Home>();
        }
        if (Input.GetMouseButton(0) && !isBlockControl && !UI_HoverTest.ins.IsPointerOverUIElement() && LevelManager.ins.isStartLevel)
        {
            currentPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            diffX = currentPos.x - startPos.x;
            startPos = currentPos;
            if (Player.ins.characterRoot.wallContactPoint.x > Player.ins.characterRoot.transform.position.x
                && diffX > 0 && Player.ins.characterRoot.isCollidingWall)// wall bên phải characterroot
            {
                return;
            }
            if (Player.ins.characterRoot.wallContactPoint.x < Player.ins.characterRoot.transform.position.x
                && diffX < 0 && Player.ins.characterRoot.isCollidingWall)// wall bên trái characterroot
            {
                return;
            }

            Vector3 newPos = moveObj.transform.position + Vector3.right * Time.smoothDeltaTime * spdSwipe * diffX;

            //tránh ngã trái phải
            if (newPos.x < LevelManager.ins.currentLevel.leftBorder.position.x - 1f || newPos.x > LevelManager.ins.currentLevel.rightBorder.position.x + 1f) return;

            if (Player.ins.characterRoot.isInWallZone)
            {
                if (Player.ins.characterRoot.transform.position.x < 0 && newPos.x > 0)
                {
                    moveObj.localPosition = Vector3.zero - new Vector3(Player.ins.characterRoot.capsuleCollider.radius / 2, 0, 0);
                    return;
                }
                else if (Player.ins.characterRoot.transform.position.x > 0 && newPos.x < 0)
                {
                    moveObj.localPosition = Vector3.zero + new Vector3(Player.ins.characterRoot.capsuleCollider.radius / 2, 0, 0);
                    return;
                }
            }
            moveObj.localPosition += Vector3.right * Time.smoothDeltaTime * spdSwipe * diffX;
        }
    }
}
