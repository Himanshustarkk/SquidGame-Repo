using UnityEngine;
using UnityEngine.UI;

public class SoundToggleButton : MonoBehaviour
{
    public Button soundButton;
    public Image buttonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Start()
    {
        //UpdateButtonSprite();
       // soundButton.onClick.AddListener(ToggleSound);
    }

    public void ToggleSound()
    {
        SoundManager.instance.ToggleMute();
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
       // bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        bool MuteSprite= SoundManager.instance.isMuted;
        Debug.Log(MuteSprite + "This is is Muted Value");
        buttonImage.sprite = MuteSprite ? soundOffSprite : soundOnSprite;

    }
}
