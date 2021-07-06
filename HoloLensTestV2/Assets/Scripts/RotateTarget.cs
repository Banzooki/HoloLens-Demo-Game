using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTarget : MonoBehaviour
{
    private Transform camera;

    void Start()
    {
        camera = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(camera);
    }
}



