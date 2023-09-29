using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Carga la escena del juego
    public void PlayGame()
    {
        SceneManager.LoadScene("Juego");
    }

    // Metodo para salir de la aplicacion
    public void QuitGame()
    {
        Application.Quit();
    }

}
