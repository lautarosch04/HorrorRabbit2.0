using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrampolinScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que ha colisionado tiene un tag que indique que puede ser tocado
        if (other.CompareTag("Player"))
        {
            anim.SetBool("Trampolin",true);//opcion cambiar a trigger

        }
      
}
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("tampolin", false);
        }
    }
}
