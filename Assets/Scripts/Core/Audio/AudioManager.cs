using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : BaseManager<AudioManager>
{
    private AudioSource BGM = null;
    private float BGMVolume = 1;

    private float SoundVolume = 1;
    private List<GameObject> soundList = new List<GameObject>();

    /// <summary>
    /// ���ű�������
    /// </summary>
    /// <param name="name">����������</param>
    public void BGMOn(string name)
    {
        if(BGM == null)
        {
            GameObject obj = new GameObject{ name = "BGM" };
            BGM = obj.AddComponent<AudioSource>();
            ResourceManager.GetInstance().LoadAsync<AudioClip>("Music/BGM/" + name, (clip) =>
            {
                BGM.clip = clip;
                BGM.loop = true;
                BGM.volume = BGMVolume;
                BGM.Play();
            });
        }
    }

    /// <summary>
    /// �ı䱳����������
    /// </summary>
    /// <param name="value">������С ��0~1֮��</param>
    public void ChangeBGMVolume(float value)
    {
        if (value >= 0 && value <= 1) 
        {
            BGMVolume = value;
        }
        
        if(BGM == null)
        {
            return;
        }

        BGM.volume = BGMVolume;
    }

    /// <summary>
    /// ֹͣ����BGM
    /// </summary>
    public void BGMOff()
    {
        if(BGM == null)
        {
            return;
        }
        BGM.Stop();
    }

    /// <summary>
    /// ��ͣBGM
    /// </summary>
    public void BGMPause()
    {
        if (BGM == null)
        {
            return;
        }
        BGM.Pause();
    }

    /// <summary>
    /// ��Ч����
    /// </summary>
    /// <param name="name">��Ч��</param>
    /// <param name="callBack">������Ч ����ֹͣ����</param>
    public void SoundPlay(string name, bool isLoop, UnityAction<GameObject> callBack = null)
    {
        ResourceManager.GetInstance().LoadAsync<AudioClip>("Music/Sound/" + name, (clip) =>
        {
            PoolManager.GetInstance().GetObj("Music/SoundPlay", (obj) =>
            {
                obj.name = "Music/SoundPlay";

                AudioSource source = obj.GetComponent<AudioSource>();
                source.clip = clip;

                if(!source.isPlaying)
                {
                    source.Play();
                }

                source.volume = SoundVolume;

                if (isLoop)
                {
                    source.loop = true;
                }

                if (callBack != null)
                {
                    callBack(obj);
                }

                MonoManager.GetInstance().StartCoroutine(source.GetComponent<SoundRecycle>().Recycle());

                soundList.Add(obj);
            });
        });
    }

    /// <summary>
    /// �ı���Ч������С
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundVolume(float value)
    {
        if(value >= 0 && value <= 1)
        {
            SoundVolume = value;
        }

        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].GetComponent<AudioSource>().volume = SoundVolume;
        } 
        
    }

    /// <summary>
    /// �ر���Ч
    /// </summary>
    /// <param name="Sound"></param>
    public void SoundOff(GameObject Sound)
    {
        if (soundList.Contains(Sound))
        {
            PoolManager.GetInstance().PushObj(Sound.name, Sound);
            soundList.Remove(Sound);
        }
    }
}
