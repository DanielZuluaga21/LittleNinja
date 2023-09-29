using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    //Velocidad del enemigo
    float speed = 0.8f;

    //Limite al que llega el enemigo
    float range = 5;
    
    //Posicion en el eje X donde comienza.
    float startingX;

    //Direccion del enemigo
    int dir = 1;

    //Variable para controlar el sentido de la bola de fuego
    bool isMovingRight;

    void Start()
    {
        //Se inicializa la posicion de partida del enemigo
        startingX = transform.position.x;
    }

    // Se ejecuta varias veces por frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime * dir);

        //Se comprueba si el enemigo llega al limite para cambiar su direccion
        if(transform.position.x < startingX || transform.position.x > startingX + range)
        {
            dir *= -1;
        }

        //Comprobamos si la bola de fuego mira a la derecha.
        //Si la direccion es negativa o positiva se da la vuelta.
        if( isMovingRight && dir > 0 || !isMovingRight && dir < 0){
            Flip();
        }
    }

    //Metodo para girar la bola de fuego
    void Flip(){
        isMovingRight = !isMovingRight;

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
