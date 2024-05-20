using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public KeyCode keyToStart, keyToPause, keyToRestart;
    private EGameState currentGameState = EGameState.START;
    private bool maxLevelReached = false, stopExecuting = false;
    private enum EGameState
    {
        START,
        PLAYING,
        PAUSED,
        MAX_LEVEL_REACHED,
        RESTART
    }

    private void Start()
    {
        EnterCurrentState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToStart) && (currentGameState == EGameState.START || currentGameState == EGameState.PAUSED))
            ChangeGameState(EGameState.PLAYING);

        if (Input.GetKeyDown(keyToPause) && currentGameState == EGameState.PLAYING)
            ChangeGameState(EGameState.PAUSED);

        if(!stopExecuting)
            if (PlayerLevel.GetPlayerLevel() == PlayerLevel.GetMaxLevel() && PlayerScore.GetScore() == PlayerLevel.GetNextGoal())
                ChangeGameState(EGameState.MAX_LEVEL_REACHED);

        if (currentGameState == EGameState.MAX_LEVEL_REACHED)
        {
            if(Input.GetKeyDown(keyToRestart))
                ChangeGameState(EGameState.RESTART);
            else if (Input.GetKeyDown(keyToStart))
                ChangeGameState(EGameState.PLAYING);
        }

        if (currentGameState == EGameState.PAUSED && maxLevelReached)
        {
            if (Input.GetKeyDown(keyToRestart))
                ChangeGameState(EGameState.RESTART);
        }
    }

    private void ChangeGameState(EGameState newState)
    {
        if (currentGameState == newState)
            return;
        Debug.Log(newState.ToString());
        ExitCurrentState();
        currentGameState = newState;
        EnterCurrentState();
    }

    private void EnterCurrentState()
    {
        switch (currentGameState)
        {
            case EGameState.START:
                Time.timeScale = 0.0f;
                break;
            case EGameState.PLAYING:
                break;
            case EGameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            case EGameState.MAX_LEVEL_REACHED:
                //PANTALLA FINAL CON RESULTADOS Y TIEMPO DE PARTIDA (UI/MENÚ) --> CANVAS (para la D3) / Lo mismo para es estado de PAUSE, pero con menos elementos en pantalla
                Time.timeScale = 0.0f;
                maxLevelReached = true;
                stopExecuting = true;
                break;
            case EGameState.RESTART:
                Time.timeScale = 0.0f;
                PlayerScore.ResetScore();
                ResetGame();
                break;
        }
    }

    private void ExitCurrentState()
    {
        switch (currentGameState)
        {
            case EGameState.START:
                Time.timeScale = 1.0f;
                break;
            case EGameState.PLAYING:
                break;
            case EGameState.PAUSED:
                Time.timeScale = 1.0f;
                break;
            case EGameState.MAX_LEVEL_REACHED:
                Time.timeScale = 1.0f;
                break;
            case EGameState.RESTART:
                Time.timeScale = 1.0f;
                break;
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
