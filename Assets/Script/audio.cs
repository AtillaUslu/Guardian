using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class audio : MonoBehaviour
{
    public Text Text;
    public Slider slider;

    private void Start()
    {
        LoadAudio();
    }
    public void SetAudio(float value)
    {
        AudioListener.volume = value;
        Text.text = ((int)(value * 100)).ToString();
        SaveAudio();
    }
    public void SaveAudio()
    {
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
    }
    private void LoadAudio()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            slider.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            slider.value = PlayerPrefs.GetFloat("volume");
        }


    }

}