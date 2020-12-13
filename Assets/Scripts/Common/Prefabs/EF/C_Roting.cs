using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Roting : MonoBehaviour
{
    [SerializeField]
    private Vector3 speed = Vector3.zero;

    void Update()
    {
        transform.Rotate(
             speed.x * Time.deltaTime,
             speed.y * Time.deltaTime,
             speed.z * Time.deltaTime
        );
    }
}
