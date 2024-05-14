using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangesLevelingUp : MonoBehaviour
{
    [SerializeField] GameObject playerContainer, playerPosition;
    [SerializeField] List<GameObject> playerList = new List<GameObject>();

    public Vector3 CurrentScale = new Vector3(0.25f, 0.25f, 0);
    public float PlayerScaleIncrease = 1.1f;

    private GameObject currentPlayerAvatar;
    private int playerIndex = 0;
    private int currentLevel;

    private void Start()
    {
        currentLevel = PlayerLevel.GetPlayerLevel();
        EvolveCharacter(true);
    }

    void Update()
    {
        //When the player levels up, this code is executed
        if (currentLevel != PlayerLevel.GetPlayerLevel())
            ExecuteChanges();

        //When Player leaves the cloud, the character's position is reset to that of its container
        if (!currentPlayerAvatar.GetComponent<PlayerMovement>().GetPlayerIsOnGround())
            currentPlayerAvatar.transform.SetParent(playerContainer.transform);
    }

    private void ExecuteChanges()
    {
        //We change the Player prefab and his attributes
        EvolveCharacter(false);

        //We reset the score
        PlayerScore.ResetScore();

        //We increase maxLife and currentLife
        PlayerHealth.SetLifeNewLevel();

        //We reduce droplet cooldown time
        DropSpawner.ReduceSpawnCooldown();

        //We reduce the cooldown time of the enemy spawner
        EnemySpawner.CanDecrementCooldown();

        //We activate the camera animation
        MainCameraMovement.RunAnimation();

        //We match the level to that of the current player
        currentLevel = PlayerLevel.GetPlayerLevel();
    }

    private void EvolveCharacter(bool firstTime)
    {
        playerPosition.transform.SetParent(playerContainer.transform);
        playerPosition.transform.position = Vector3.zero;

        Destroy(currentPlayerAvatar);
        playerIndex = PlayerLevel.GetPlayerLevel() - 1;

        if (!firstTime)
            CurrentScale = new Vector3(CurrentScale.x * PlayerScaleIncrease, CurrentScale.y * PlayerScaleIncrease, 0);

        currentPlayerAvatar = Instantiate(playerList[playerIndex]);
        currentPlayerAvatar.transform.position = Vector3.zero;
        currentPlayerAvatar.transform.SetParent(playerContainer.transform);
        currentPlayerAvatar.transform.localScale = CurrentScale;

        playerPosition.transform.SetParent(currentPlayerAvatar.transform);
    }
}
