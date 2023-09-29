using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Variable para la puntuacion
    private int score;

    //Objeto moneda
    public GameObject items;
    public static GameManager instance;

    //Objeto boton menu principal
    public GameObject mainMenuButton;

    //Objeto donde almacena la puntuacion
    public Text Score;

    //Objeto para terminar el juego.
    public GameObject EndGame;

    //Objeto para controlar el texto si el personaje gana.
    public GameObject Win;

    //Objeto jugador.
    public GameObject Player;

    void Awake()
    {
        //Se realiza la llamada a si mismo para que sepamos que solo existe un GameManager
        if (instance == null){
            instance = this;
        }
        if(instance != this){
            Destroy(this);
        }
        
        score = 0;
    }

    // Metodo para saber cuando colisionamos con el item.
    public void CollectibleGetted()
    {
        score++;
        Score.text = score.ToString();
        if(score == 3){
            GameManager.instance.Victory();
        }
    }

    // Metodo para la derrota del personaje
    public void GameOver()
    {
        //Se activa el boton Menu Principal
        mainMenuButton.SetActive(true);
        EndGame.SetActive(true);
        Player.SetActive(false);
    }

    //Metodo para volver al menu principal
    public void ToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //Metodo para la victoria del personaje.
    public void Victory(){
        mainMenuButton.SetActive(true);
        Win.SetActive(true);
        Player.SetActive(false);
    }
}
