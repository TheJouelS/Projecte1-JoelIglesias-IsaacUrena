using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Sprite s_musicSprite1, s_musicSprite2;
    public GameObject g_musicButton;
    private bool musicButtonIsPressed;

    private void Start()
    {
        musicButtonIsPressed = false;
    }

    public void MusicButtonIsClicked()
    {
        musicButtonIsPressed = !musicButtonIsPressed;

        if (!musicButtonIsPressed)
            g_musicButton.GetComponent<Image>().sprite = s_musicSprite1; //PONER MÚSICA DE PASO
        else
            g_musicButton.GetComponent<Image>().sprite = s_musicSprite2;
    }
}
