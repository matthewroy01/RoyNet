using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : MonoBehaviour
{
    public object obj;

    void Start()
    {
        obj = GetComponent<PlayerMovement>().movSpd;

        return;
    }
}
