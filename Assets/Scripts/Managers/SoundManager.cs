using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Sprite s_musicSprite1, s_musicSprite2;
    public GameObject g_musicButton;
    private bool musicButtonIsPressed;

    public AudioSource s_upLevel, s_takeDamage, s_catchDrops, s_maxLevel, s_walk, m_mainMusic, m_introMusic;

    public static SoundManager instance;

    private void Awake()
    {
        if (SoundManager.instance == null)
            SoundManager.instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        musicButtonIsPressed = false;
    }

    public void MusicButtonIsClicked()
    {
        musicButtonIsPressed = !musicButtonIsPressed;

        if (!musicButtonIsPressed)
        {
            g_musicButton.GetComponent<Image>().sprite = s_musicSprite1;
            m_mainMusic.Play();
        }
        else
        {
            g_musicButton.GetComponent<Image>().sprite = s_musicSprite2;
            m_mainMusic.Pause();
        }
    }
}
