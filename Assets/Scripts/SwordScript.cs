using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordScript : MonoBehaviour
{
    public int damage = 10; // Da�o de la espada
    public float range = 1f; // Rango del raycast
    public LayerMask damageableLayer; // Capa de objetos que pueden ser da�ados
    public int numeroRayos = 5; // N�mero de rayos paralelos para simular el ancho
    public float grosorVisual = 0.1f; // Grosor visual del raycast

    public delegate void AnimacionAtaque();
    public static event AnimacionAtaque OnAtaque;

  

    // M�todo para dibujar el raycast en el editor de Unity
    void OnDrawGizmosSelected()
    {
        // Dibujar m�ltiples rayos paralelos para simular el ancho
        for (int i = 0; i < numeroRayos; i++)
        {
            // Calcular el desplazamiento lateral para cada rayo
            float offset = (i - (numeroRayos - 1) / 2f) * grosorVisual;

            // Dibujar cada rayo
            Debug.DrawRay(transform.position +  transform.right * offset,  transform.up * range, Color.red);
        }
    }
}
