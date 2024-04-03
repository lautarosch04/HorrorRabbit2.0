using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerController

{
    private Animator animator; // Referencia al componente Animator del personaje

    public float attackCooldown = 4f; // Tiempo de enfriamiento entre ataques



    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // Detectar si se presiona la tecla de ataque y si ha pasado el tiempo de enfriamiento
        if (Input.GetKeyDown(KeyCode.F))
        {

            // Activar la animación de ataque
            animator.SetBool("Linterna", true);
                }


        else
        {

            animator.SetBool("Linterna", false);
        }
    }



    // Método llamado al finalizar la animación de ataque

}

