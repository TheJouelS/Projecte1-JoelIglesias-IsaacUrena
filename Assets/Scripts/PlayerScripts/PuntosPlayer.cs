using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosPlayer : MonoBehaviour
{
    private int score;

    private void Start()
    {
        score = 0;
    }

    public void SetScore(bool resetear)
    {
        if(resetear)
            score = 0;
        else if (score + 1 <= gameObject.GetComponent<NivelesPlayer>().GetPuntosSiguienteNivel())
        {
            score++;
            Debug.Log("Tu puntuación: " + score);
        }
    }

    public int GetScore()
    {
        return score;
    }
}
