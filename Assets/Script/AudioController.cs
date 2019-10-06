using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [EnumNamedList(typeof(NumAudio))]
    public List<AudioClip> enAudio;
    [EnumNamedList(typeof(NumAudio))]
    public List<AudioClip> frAudio;
    public List<AudioClip> MEAudio;
    public AudioSource audio;
    public List<AudioSource> audioME;
    public Subtitles subtitles;
    Coroutine sub;
    public AudioClip cri;

    public Game gc;

    // Start is called before the first frame update
    void Start()
    {
        subtitles = GetComponent<Subtitles>();
        gc = GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCri()
    {
        audio.clip = cri;
        audio.Play(0);
    }
    public void PlayAudioUpSub(int audioNB)
    {
        foreach (AudioSource audioM in audioME)
        {
            audioM.volume = 0.05f;
        }
        audio.Stop();
        if (gc.isME)
        {
            audio.clip = MEAudio[Random.Range(0, MEAudio.Count)];
        }
        else if (gc.isEn)
        {
            audio.clip = enAudio[audioNB];
        }
        else
        {
            audio.clip = frAudio[audioNB];
        }
        audio.Play(0);
        StartCoroutine(subtitles.SetSubtitleUpFor(audio.clip.length, audioNB));
    }

    public float PAudioSubUp(int nb)
    {
        PlayAudioUpSub(nb);
        return (audio.clip.length + 0.5f);
    }

    public void PlayAudio(int audioNB)
    {
        if(sub != null)
        {
            StopCoroutine(sub);
        }
        audio.Stop();
        if (gc.isME)
        {
            audio.clip = MEAudio[Random.Range(0, MEAudio.Count)];
        }
        else if (gc.isEn)
        {
            audio.clip = enAudio[audioNB];
        }
        else
        {
            audio.clip = frAudio[audioNB];
        }
        audio.Play(0);
        sub = StartCoroutine(subtitles.SetSubtitleFor(audio.clip.length, audioNB));
    }

    public float PAudio(int nb)
    {
        PlayAudio(nb);
        return (audio.clip.length + 0.5f);
    }

    public void PlayAudioMe(int audioNB, int me)
    {
        audioME[me].Stop();
        if (gc.isME)
        {
            audioME[me].clip = MEAudio[Random.Range(0, MEAudio.Count)];
        }
        else if (gc.isEn)
        {
            audioME[me].clip = enAudio[audioNB];
        }
        else
        {
            audioME[me].clip = frAudio[audioNB];
        }
        audioME[me].Play(0);
        StartCoroutine(subtitles.SetSubtitleFor(audioME[me].clip.length, audioNB));
    }

    public float PAudioMe(int nb, int me)
    {
        PlayAudioMe(nb, me);
        return (audioME[me].clip.length + 0.5f);
    }

    public IEnumerator StopAll()
    {
        audio.Stop();
        foreach(AudioSource audiom in audioME)
        {
            audiom.Stop();
            yield return null;
        }
        yield return null;
    }
}
