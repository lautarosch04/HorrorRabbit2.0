using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public  Light linterna;

    public bool activLight;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            activLight = !activLight;

            if (activLight ==true)
            {
                linterna.enabled = true;
            }

            if(activLight == false)
            {
                linterna.enabled = false;
            }
        }
    }
}

