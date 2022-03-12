using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
        }
        PlaySound("BackgroundMusic");
    }
    public List<AudioSource> audioList;
    public void PlaySound(string _audioName)
    {
        AudioSource audio = audioList.Where(audio => audio.name == _audioName).FirstOrDefault();
        if(audio==null)
        {
            Debug.Log("Please check the name again");
        }
        else
        {
            audio.Play();
        }
    }
}
