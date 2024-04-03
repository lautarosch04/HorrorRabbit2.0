using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoControl : MonoBehaviour
{
    [SerializeField]
    private Transform Player, Enemigo;
    [SerializeField]
    private float velocidad;
    public bool seguimiento;
    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos= new Vector3(Player.position.x,Enemigo.position.y,Player.position.z);

        if(seguimiento )
        {
            Enemigo.transform.position = Vector3.MoveTowards(transform.position,playerPos,velocidad * Time.deltaTime);
            Enemigo.transform.LookAt(Player);
        }
    }
}
