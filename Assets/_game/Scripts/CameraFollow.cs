using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public Transform target;
    public Vector3 posOffSet;
    public float followSpeed;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.transform.position + posOffSet;

        targetPosition.x = Mathf.Lerp(transform.position.x, targetPosition.x, followSpeed * Time.deltaTime * 0.5f);

        targetPosition.y = Mathf.Lerp(transform.position.y, targetPosition.y, followSpeed * Time.deltaTime * 2222f);
        targetPosition.z = Mathf.Lerp(transform.position.z, targetPosition.z, followSpeed * Time.deltaTime * 2222f);

        transform.position = targetPosition;
    }

    public void OnStartNewLevel()
    {
        target = Player.ins.characterRoot.transform;
    }
}