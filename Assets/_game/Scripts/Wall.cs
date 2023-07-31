using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public BoxCollider boxLeft;
    public BoxCollider boxRight;

    public void OnInit()
    {
        boxLeft.enabled = false;
        boxRight.enabled = false;
    }

    public void Toggle()
    {
        if (boxLeft.enabled == true)
        {
            boxLeft.enabled = false;
            boxRight.enabled = true;
        }
        else
        {
            boxRight.enabled = false;
            boxLeft.enabled = true;
        }
    }
}
