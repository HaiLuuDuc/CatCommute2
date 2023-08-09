using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AxisType
{
    X, Y, Z
}
public class Rotate : MonoBehaviour
{
    public float rotateSpeed;
    public AxisType axisType = AxisType.Y;

    void Update()
    {
        switch (axisType)
        {
            case AxisType.X:
                transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
                break;
            case AxisType.Y:
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
                break;
            case AxisType.Z:
                transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
                break;
            default:break;
        }
    }
}
