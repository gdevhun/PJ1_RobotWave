using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private Sound[] bgm = null;
    [SerializeField]
    private Sound[] sfx = null;
    [SerializeField]
    private AudioSource bgmPlayer = null;
    public List<AudioSource> sfxPlayers = new List<AudioSource>();

    private void Start()
    {
        // SFX �÷��̾� �� ���� �ʱ⿡ �����ϰ� ����Ʈ�� �߰�
        for (int i = 0; i < 60; i++)
        {
            AudioSource sfxPlayer = gameObject.AddComponent<AudioSource>();
            sfxPlayers.Add(sfxPlayer);
        }
    }

    public void PlayBGM(string bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgmName == bgm[i].name)
            {
                
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
                return;
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string sfxName,float volume = 1.0f)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfxName == sfx[i].name)
            {
                AudioSource sfxPlayer = GetAvailableSFXPlayer();
                sfxPlayer.clip = sfx[i].clip;
                sfxPlayer.volume = volume;
                sfxPlayer.Play();
                return;
            }
        }
    }

    private AudioSource GetAvailableSFXPlayer()
    {
        for (int i = 0; i < sfxPlayers.Count; i++)
        {
            if (!sfxPlayers[i].isPlaying)
            {
                return sfxPlayers[i];
            }
        }

        // ��� SFX �÷��̾ ��� ���̹Ƿ� �� �÷��̾ �����ϰ� ����Ʈ�� �߰�
        AudioSource newSFXPlayer = gameObject.AddComponent<AudioSource>();
        sfxPlayers.Add(newSFXPlayer);
        return newSFXPlayer;
    }
}