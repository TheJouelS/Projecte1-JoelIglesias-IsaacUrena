using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NivelesPlayer : MonoBehaviour
{
    private int nivelJugador = 1;
    private int nivelMaximo = 18;
    private int puntosParaSiguienteNivel;

    [SerializeField] List<Sprite> sprite = new List<Sprite>();

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite[0];
    }

    void Update()
    {
        puntosParaSiguienteNivel = nivelJugador * 2; //Multiplico por 10 porque el resultado será el número de puntos para subir al siguiente nivel

        if (nivelJugador <= nivelMaximo)
            SubirNivel();
    }

    private void SubirNivel()
    {
        float incrementoNivel = 1.1f;

        if (gameObject.GetComponent<PuntosPlayer>().GetScore() == puntosParaSiguienteNivel)
        {
            //Aumenta el nivel
            nivelJugador++;

            //Cambia el sprite de la jarra
            for (int i = 1; i < sprite.Count; i++)
                if (i == nivelJugador - 1)
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprite[i];

            //Aumenta el tamaño
            gameObject.transform.localScale = new Vector3(transform.localScale.x * incrementoNivel, transform.localScale.y * incrementoNivel, 0);

            //Aumenta la velocidad de gotas del Spawner
            SpawnerGotas.SetSpawnCooldown();

            //Reseteamos los puntos para el nuevo nivel
            gameObject.GetComponent<PuntosPlayer>().SetScore(true);

            //Activamos el movimineto de la cámara
            MainCameraMovimiento.levelUp = true;


            Debug.Log("Tu nivel es: " + nivelJugador);
            Debug.Log("Puntos para siguiente nivel: " + nivelJugador * 2);
        }
    }

    public int GetPuntosSiguienteNivel()
    {
        return puntosParaSiguienteNivel;
    }
}
