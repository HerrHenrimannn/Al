using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHeld : MonoBehaviour
{
    public Transform fpsCam;

    void Update()
    {
        transform.LookAt(fpsCam);
    }
}
