using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    [SerializeField] private float yOffset;
    [SerializeField] private Transform target;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
