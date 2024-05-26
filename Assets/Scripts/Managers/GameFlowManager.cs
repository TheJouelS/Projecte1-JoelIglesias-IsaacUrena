using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
    public KeyCode keyToStart, keyToPause, keyToRestart, keyToContinue;
    public GameObject buttonToResetGame;

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
        buttonToResetGame.GetComponent<Button>().interactable = false;
        buttonToResetGame.GetComponent<Image>().color = new Color(255f, 255f, 255f, 150f/255f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToStart) && currentGameState == EGameState.START)
            ChangeGameState(EGameState.PLAYING);

        if (Input.GetKeyDown(keyToContinue) && currentGameState == EGameState.PAUSED)
            ChangeGameState(EGameState.PLAYING);

        if (Input.GetKeyDown(keyToPause) && currentGameState == EGameState.PLAYING)
            ChangeGameState(EGameState.PAUSED);

        if(!stopExecuting)
            if (PlayerLevel.GetPlayerLevel() == PlayerLevel.GetMaxLevel() && PlayerScore.GetScore() >= PlayerLevel.GetNextGoal())
                ChangeGameState(EGameState.MAX_LEVEL_REACHED);

        if (currentGameState == EGameState.MAX_LEVEL_REACHED)
        {
            if(Input.GetKeyDown(keyToRestart))
                ChangeGameState(EGameState.RESTART);
            else if (Input.GetKeyDown(keyToContinue))
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
                PlayerMovement.gameIsPaused = true;
                CanvasManager.instance.EnableStartCanvas();
                break;
            case EGameState.PLAYING:
                PlayerMovement.gameIsPaused = false;
                CanvasManager.instance.EnableHudCanvas();
                break;
            case EGameState.PAUSED:
                Time.timeScale = 0.0f;
                CanvasManager.instance.EnablePauseCanvas();
                break;
            case EGameState.MAX_LEVEL_REACHED:
                Time.timeScale = 0.0f;
                buttonToResetGame.GetComponent<Button>().interactable = true;
                buttonToResetGame.GetComponent<Image>().color = Color.white;
                TimeManager.instance.StopCountingTotalTime();
                CanvasManager.instance.EnableFinishCanvas();
                maxLevelReached = true;
                stopExecuting = true;
                break;
            case EGameState.RESTART:
                Time.timeScale = 0.0f;
                CanvasManager.instance.DisableHudCanvas();
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
                CanvasManager.instance.DisableStartCanvas();
                break;
            case EGameState.PLAYING:
                PlayerMovement.gameIsPaused = true;
                break;
            case EGameState.PAUSED:
                Time.timeScale = 1.0f;
                CanvasManager.instance.DisablePauseCanvas();
                break;
            case EGameState.MAX_LEVEL_REACHED:
                Time.timeScale = 1.0f;
                CanvasManager.instance.DisableFinishCanvas();
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

    //Used by buttons:
    public void ChangeToPlayingState()
    {
        ChangeGameState(EGameState.PLAYING);
    }

    public void ChangeToPausedState()
    {
        ChangeGameState(EGameState.PAUSED);
    }

    public void ChangeToRestartState()
    {
        ChangeGameState(EGameState.RESTART);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
