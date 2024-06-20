using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score = 0;

    public static PlayerScore instance;

    private void Awake()
    {
        if (PlayerScore.instance == null)
            PlayerScore.instance = this;
        else
            Destroy(gameObject);
    }

    public void UpScore()
    {
        if (score < 9999999)
        {
            if (PlayerLevel.GetPlayerLevel() >= PlayerLevel.GetMaxLevel() / 2)
            {
                if (score + 2 <= PlayerLevel.GetNextGoal())
                    score += 2;
                else score++;
            }
            else
            {
                if (score < PlayerLevel.GetNextGoal())
                    score++;
            }
        }
    }

    public void ReduceScore(bool playerLifeIsAtZero, uint decrementValue = 1)
    {
        if (playerLifeIsAtZero)
        {
            score = (int) Mathf.Round(score / 2);
        }
        else
        {
            if (decrementValue <= score)
                score -= (int)decrementValue;
            else
                score = 0;
        }
    }

    public static void ResetScore()
    {
        PlayerScore.instance.score = 0;
    }

    public static int GetScore()
    {
        return PlayerScore.instance.score;
    }
}
