using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Canvas c_hud, c_start, c_finish, c_pause;

    public List<Sprite> l_greySpritePlayer = new List<Sprite>();
    public List<Sprite> l_colorSpritePlayer = new List<Sprite>();
    public float timeToUpScoreBar, exponentValueToUpBar;

    public List<Sprite> l_greySpriteSkill = new List<Sprite>();
    public List<Sprite> l_colorSpriteSkill = new List<Sprite>();
    public string animationSkillTag_1 = "iniciateAnim";

    private bool playerIsLevelingUp = false, playerIsAttacking = false;
    private float timerBarLevel = 0f, timerRadiusSkills = 0f;
    private int lastBiggestScore = 0;

    public static CanvasManager instance;

    private void Awake()
    {
        if (CanvasManager.instance == null)
            CanvasManager.instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        EnableStartCanvas();
        DisableHudCanvas();
        DisablePauseCanvas();
        DisableFinishCanvas();

        c_hud.transform.GetChild(0).GetComponent<Image>().sprite = l_greySpritePlayer[PlayerLevel.GetPlayerLevel() - 1];
        c_hud.transform.GetChild(1).GetComponent<Image>().sprite = l_colorSpritePlayer[PlayerLevel.GetPlayerLevel() - 1];
        c_hud.transform.GetChild(1).GetComponent<Image>().fillAmount = 0f;
        c_hud.transform.GetChild(8).GetComponent<Image>().fillAmount = 1f;

        lastBiggestScore = PlayerScore.GetScore();
    }

    void Update()
    {
        if (lastBiggestScore < PlayerScore.GetScore())
        {
            QuadraticMovementBar();

            if (c_hud.transform.GetChild(1).GetComponent<Image>().fillAmount >= (float)PlayerScore.GetScore() / (float)PlayerLevel.GetNextGoal())
            {
                timerBarLevel = 0f;
                lastBiggestScore = PlayerScore.GetScore();
            }
        }

        if (lastBiggestScore > PlayerScore.GetScore())
        {
            QuadraticMovementBar();

            if (c_hud.transform.GetChild(1).GetComponent<Image>().fillAmount <= (float)PlayerScore.GetScore() / (float)PlayerLevel.GetNextGoal())
            {
                timerBarLevel = 0f;
                lastBiggestScore = PlayerScore.GetScore();
            }
        }

        if (playerIsLevelingUp)
        {
            if (PlayerLevel.GetPlayerLevel() != PlayerLevel.GetMaxLevel())
            {
                c_hud.transform.GetChild(0).GetComponent<Image>().sprite = l_greySpritePlayer[PlayerLevel.GetPlayerLevel() - 1];
                c_hud.transform.GetChild(1).GetComponent<Image>().sprite = l_colorSpritePlayer[PlayerLevel.GetPlayerLevel() - 1];
                c_hud.transform.GetChild(1).GetComponent<Image>().fillAmount = 0f;
                playerIsLevelingUp = false;
            }
        }

        if (PlayerLevel.GetPlayerLevel() == PlayerLevel.GetMaxLevel() && PlayerScore.GetScore() > PlayerLevel.GetNextGoal())
            c_hud.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Puntos: " + PlayerScore.GetScore() + "/ -";
        else
            c_hud.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Puntos: " + PlayerScore.GetScore() + "/" + PlayerLevel.GetNextGoal();

        c_hud.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Resistencia: " + PlayerHealth.GetCurrentLife() + "/" + PlayerHealth.GetMaxLife();
        c_hud.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Nivel " + PlayerLevel.GetPlayerLevel();

        if (playerIsAttacking)
        {
            timerRadiusSkills += Time.deltaTime;
            float ratio = Mathf.Min(1f, (float)timerRadiusSkills / PlayerAttack.copy_timeBetweenHit_Attack1);
            float bar_increasePosition = 1;
            c_hud.transform.GetChild(8).GetComponent<Image>().fillAmount = Mathf.Lerp(0f, bar_increasePosition,  ratio);

            if (c_hud.transform.GetChild(8).GetComponent<Image>().fillAmount >= 1f)
            {
                c_hud.transform.GetChild(8).GetComponent<Animator>().SetTrigger(animationSkillTag_1);
                playerIsAttacking = false;
                timerRadiusSkills = 0f;
            }
        }
    }

    private void QuadraticMovementBar()
    {
        timerBarLevel += Time.deltaTime;
        float ratio = Mathf.Min(1f, (float)timerBarLevel / timeToUpScoreBar);
        float bar_initialPosition = c_hud.transform.GetChild(1).GetComponent<Image>().fillAmount;
        float bar_increasePosition = (float)PlayerScore.GetScore() / (float)PlayerLevel.GetNextGoal() - bar_initialPosition;
        c_hud.transform.GetChild(1).GetComponent<Image>().fillAmount = bar_increasePosition * Mathf.Pow(ratio, exponentValueToUpBar) + bar_initialPosition;
    }

    public void PlayerIsLevelingUp()
    {
        playerIsLevelingUp = true;
    }

    public void IsUsingAttack(int numOfAttack)
    {
        switch (numOfAttack)
        {
            case 1:
                c_hud.transform.GetChild(8).GetComponent<Image>().fillAmount = 0;
                playerIsAttacking = true;
                break;
        }
    }

    public void EnableStartCanvas() {c_start.enabled = true;}

    public void DisableStartCanvas() {c_start.enabled = false;}

    public void EnableHudCanvas() {c_hud.enabled = true;}

    public void DisableHudCanvas() {c_hud.enabled = false;}

    public void EnablePauseCanvas() 
    { 
        c_pause.enabled = true;
        c_pause.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Nivel: " + PlayerLevel.GetPlayerLevel();
        c_pause.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Puntos: " + PlayerScore.GetScore();
    }

    public void DisablePauseCanvas() { c_pause.enabled = false; }

    public void EnableFinishCanvas() 
    { 
        c_finish.enabled = true;
        c_finish.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Tiempo total de la partida: " + TimeManager.instance.GetTotalTime().ToString("F2") + " Minutos";
    }

    public void DisableFinishCanvas() { c_finish.enabled = false; }
}
