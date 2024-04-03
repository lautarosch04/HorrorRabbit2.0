using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzDetector : MonoBehaviour
{
    public float range = 8f;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, range))
        {
            EnemigoControl enemigoControl = hit.collider.GetComponent<EnemigoControl>();
            if (enemigoControl != null)
            {
                enemigoControl.enabled = false;
                Destroy(hit.collider.gameObject, 1);
            }
        }
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * range, Color.red);
    }
}
