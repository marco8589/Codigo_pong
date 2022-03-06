using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bola : MonoBehaviour
{
    //Velocidad
    public float velocidad = 30.0f;
    private int Tcantidadgoles = 0;
    //Contadores de goles
    public int golesIzquierda = 0;
    public int golesDerecha = 0;
    //Cajas de texto de los contadores
    public Text ContadorIzquierda;
    public Text ContadorDerecha;
    public Text Ganador1;
    public Text Ganador2;
    public Text Descripcionreinicio;


    //Audio Source
    AudioSource fuenteDeAudio;
    //Clips de audio
    public AudioClip audioGol, audioRaqueta, audioRebote, audioFin;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
        //Pongo los contadores a 0
        ContadorIzquierda.text = golesIzquierda.ToString();
        ContadorDerecha.text = golesDerecha.ToString();
        //Letrero del ganador
        Ganador1.text = "";
        Ganador2.text = "";
        Descripcionreinicio.text = "";

        //Recupero el componente audio source;
        fuenteDeAudio = GetComponent<AudioSource>();


    }

    void Update()
    {
       // velocidad = velocidad + 0.1f;
    }


    //Se ejecuta al colisionar
    void OnCollisionEnter2D(Collision2D micolision)
    {
        //Si choca con la raqueta izquierda
        if (micolision.gameObject.name == "RaquetaIzquierda")
        {
            //Valor de x
            int x = 1;
            //Valor de y
            int y = direccionY(transform.position,
            micolision.transform.position);
            //Calculo dirección
            Vector2 direccion = new Vector2(x, y);
            //Aplico velocidad
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            //Reproduzco el sonido de la raqueta
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();

        }

        //Si choca con la raqueta derecha
        if (micolision.gameObject.name == "RaquetaDerecha")
        {
            //Valor de x
            int x = -1;
            //Valor de y
            int y = direccionY(transform.position,
            micolision.transform.position);
            //Calculo dirección (normalizada para que de 1 o -1)
            Vector2 direccion = new Vector2(x, y);
            //Aplico velocidad
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            //Reproduzco el sonido de la raqueta
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();

        }

        //Para el sonido del rebote
        if (micolision.gameObject.name == "Arriba" ||
        micolision.gameObject.name == "Abajo")
        {
            //Reproduzco el sonido del rebote
            fuenteDeAudio.clip = audioRebote;
            fuenteDeAudio.Play();

        }
    }

    //Direccion Y
    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if (posicionBola.y > posicionRaqueta.y)
        {
            return 1;
        }
        else if (posicionBola.y < posicionRaqueta.y)
        {
            return -1;
        }
        else
        {
            return 0;
        }



    }

    //Reinicio la posición de la bola
    public void reiniciarBola(string direccion)
    {
        //Posición 0 de la bola
        transform.position = Vector2.zero;
        //Vector2.zero es lo mismo que new Vector2(0,0);
        //Velocidad inicial de la bola
        //velocidad = 30.0f;

        //Velocidad y dirección
        if (direccion == "Derecha")
        {
            //Incremento goles al de la derecha
            golesDerecha++;

            //Lo escribo en el marcador
            ContadorDerecha.text = golesDerecha.ToString();

            if (golesDerecha == 5)
            {
                Ganador2.text = "Ganador!";
                //poner la bola estatica
                GetComponent<Rigidbody2D>().velocity = Vector2.right * 0;
                //titulo de reinicio
                Descripcionreinicio.text = "Click Para Reiniciar";
                //Reproduzco el sonido de la raqueta
                fuenteDeAudio.clip = audioFin;
                fuenteDeAudio.Play();

                return;

            }


            //Calculo total de la cantidad de goles
            Tcantidadgoles = Tcantidadgoles + golesDerecha;


            //velocidad de la bola en funcion de la cantidad de goles
            velocidad = velocidad+ Tcantidadgoles;



            //Reinicio la bola
                GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
            //Vector2.right es lo mismo que new Vector2(1,0)
        }
        else if (direccion == "Izquierda")
        {
            //Incremento goles al de la izquierda
            golesIzquierda++;

            //Lo escribo en el marcador
            ContadorIzquierda.text = golesIzquierda.ToString();

            if (golesIzquierda == 5)
            {
                Ganador1.text = "Ganador!";
                //poner la bola estatica
                GetComponent<Rigidbody2D>().velocity = Vector2.left * 0;
                //titulo de reinicio
                Descripcionreinicio.text = "Click Para Reiniciar";
                //Reproduzco el sonido de la raqueta
                fuenteDeAudio.clip = audioFin;
                fuenteDeAudio.Play();
                return;
            }

            //Calculo total de la cantidad de goles
            Tcantidadgoles = Tcantidadgoles + golesIzquierda;



            //velocidad de la bola en funcion de la cantidad de goles
            velocidad = velocidad + Tcantidadgoles;


            //Reinicio la bola
            GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
            //Vector2.right es lo mismo que new Vector2(-1,0)
        }

        //Reproduzco el sonido del gol
        fuenteDeAudio.clip = audioGol;
        fuenteDeAudio.Play();
    }

}