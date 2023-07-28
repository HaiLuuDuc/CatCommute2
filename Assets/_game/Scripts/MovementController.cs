using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : Singleton<MovementController>
{
    public float spdSwipe;
    public Transform moveObj;
    private Vector3 startPos;
    private Vector3 currentPos;
    private float diffX;
    public bool isBlockControl = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UI_HoverTest.ins.IsPointerOverUIElement())
        {
            startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            LevelManager.ins.StartLevel();
            UIManager.ins.CloseUI<Ready>();
        }
        if (Input.GetMouseButton(0) && !isBlockControl && !UI_HoverTest.ins.IsPointerOverUIElement())
        {
            currentPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            diffX = currentPos.x - startPos.x;
            moveObj.localPosition += Vector3.right * Time.smoothDeltaTime * spdSwipe * diffX;
            startPos = currentPos;
        }
    }
}
