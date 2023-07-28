using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public Transform target;
    public Vector3 posOffSet;
    public float followSpeed;
    public Vector3 initialPos;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.transform.position + posOffSet;

        targetPosition.x = Mathf.Lerp(transform.position.x, targetPosition.x, followSpeed * Time.deltaTime * 0.5f);

        //targetPosition.y = Mathf.Lerp(transform.position.y, targetPosition.y, followSpeed * Time.deltaTime * 1.2f);
        targetPosition.y = this.transform.position.y;
        
        targetPosition.z = Mathf.Lerp(transform.position.z, targetPosition.z, followSpeed * Time.deltaTime * 2f);

        transform.position = targetPosition;
    }

    public void OnStartNewLevel()
    {
        this.transform.position = initialPos;
        target = Player.ins.characterRoot.transform;
    }
}