using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int playerLevel = 1;
    public int maxLevel = 18;
    public int nextGoal;
    public int scoringMargin;

    public static PlayerLevel instance;

    private void Awake()
    {
        if (PlayerLevel.instance == null)
            PlayerLevel.instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LevelUp(true);
    }

    void Update()
    {
        if (playerLevel < maxLevel)
            if (PlayerScore.GetScore() >= nextGoal)
                LevelUp(false);
    }

    private void LevelUp(bool firstTime)
    {
        if (!firstTime)
            playerLevel++;

        nextGoal = playerLevel * scoringMargin; //This result serves to increase the difference in points between one level and another, increasingly greater
    }

    public static int GetNextGoal()
    {
        return instance.nextGoal;
    }

    public static int GetPlayerLevel()
    {
        return instance.playerLevel;
    }

    public static int GetMaxLevel()
    {
        return instance.maxLevel;
    }
}
