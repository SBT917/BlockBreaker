using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class AudioData
{
    public string name;
    public AudioClip clip;
    [Range(0, 1)] public float volume = 1;
}

public class AudioManager : Singleton<AudioManager> //シングルトン継承
{
    private AudioSource audioSource; //AudioSource
    [SerializeField]private List<AudioData> SeDatas; //SEのデータリスト

    protected override void Awake()
    {
        TryGetComponent(out audioSource);
        base.Awake();
    }

    //指定した名前のSEを再生
    public void PlaySE(string name)
    {
        AudioData data = SeDatas.Find(se => se.name == name);
        audioSource.volume = data.volume;
        audioSource.PlayOneShot(data.clip);
    }
}
