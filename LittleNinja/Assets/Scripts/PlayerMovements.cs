using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{
    PlayerControls controls;

    //Variable para conocer la direccion del personaje
    float direction = 0;

    //Variable para la velocidad
    float speed = 200;

    //Variable para controlar hacia donde mira el personaje.
    bool isFacingRigth = true;

    //Variable que controla la velocidad del salto.
    float jumpForce = 9;

    //Variale para comprobar si el personaje esta en el suelo
    bool isGrounded;

    //Referencia al objeto groundCheck del juego
    public Transform groundCheck;

    //Referencia la capa del suelo
    public LayerMask groundLayer;

    //Referencia al Rigidbody2D
    public Rigidbody2D playerRB;

    //Referencia al Animator
    public Animator animator;

    //Variable para el componente AudioSource
    AudioSource jumpSound;

    
    //Metodo que se ejecuta antes del Start
    void Awake()
    {
        //Se inicializan los controles y se habilitan
        controls = new PlayerControls();
        controls.Enable();


        //Se usa la programacion funcional para simplificar la programacion
        //La variable ctx nos permite comprobar si el jugador se mueve a izq o dech.
        controls.Land.Move.performed += ctx => {
            direction = ctx.ReadValue<float>();
        };

        //Se comprueba las acciones de Jump
        controls.Land.Jump.performed += ctx => Jump();

        //Obtenemos el componente AudioSource
        jumpSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Comprobamos si el personaje esta en el suelo
        //El circulo anadido en los pies del jugador, se le aplica un radio de 0.1
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f,
                     groundLayer);

        //Se pasa el valor obtenido al controlador de animacion
        animator.SetBool("isGrounded", isGrounded);
        
        //Se usa la variable direccion para mover al jugador.
        //Si queremos que la direccion cambie, recalculamos la velocidad
        //haciendo uso de la direccion multiplicandola por el cambio de frame.
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime,
                                        playerRB.velocity.y);

        //Modificamos el parametro speed usando Animator
        //Si la direccion es 0, el personaje vuelve a la animacion de Reposo.
        animator.SetFloat("speed", Mathf.Abs(direction));

        //Comprobamos si el personaje mira a la derecha.
        //Si la direccion es negativa o positiva se da la vuelta.
        if( isFacingRigth && direction < 0 || 
            !isFacingRigth && direction > 0){
                Flip();
            }
    }

    //Metodo para darle la vuelta al personaje
    void Flip(){
        //Se cambia el valor del booleano
        isFacingRigth = !isFacingRigth;

        //Para que el personaje se de la vuelta, se
        //cambia la escala en X
        transform.localScale = new Vector2(transform.localScale.x * -1,
                                        transform.localScale.y);
    }

    //Metodo para que el personaje salte
    void Jump(){
        if(isGrounded){
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);

            //Se llama al metodo Play del AudioSource para reproducir el salto.
            jumpSound.Play();
        }  
    }

    //Metodo para detectar si ha entrado en contacto con un objeto.
    private void OnTriggerEnter2D(Collider2D other) {
        //Comprobamos si toca la moneda con su tag correspondiente
        if(other.gameObject.name == "item"){
            //Se llama al GameManager para indicar que hemos cogido la moneda.
            GameManager.instance.CollectibleGetted();
            other.gameObject.SetActive(false);
        }
        else{
            GameManager.instance.GameOver();
        }
    }
}
