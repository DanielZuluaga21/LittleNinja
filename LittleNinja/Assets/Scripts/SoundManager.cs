using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Instancia publica para tener el control de SoundManager
    public static SoundManager instance;

    //Atributo para el componente AudioSource del SoundManager
    private AudioSource source;
    void Start()
    {
        //Se inicializa la instancia de la clase
        instance = this;

        //Se obtiene el componente AudioSource
        source = GetComponent<AudioSource>();
    }

    // Metodo para reproducir el AudioClip que se pase como atributo
    public void PlaySound()
    {
        source.Play(); 
    }
}
