using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public AudioController audio;
    public Subtitles sub;
    public Hints hints;
    public VideoController video;
    public PlayerCommands playerCommands;

    public bool isME;
    public bool isEn;
    public bool wait;
    public bool waiting = false;
    public bool recall = false;

    public bool selectLang = false;

    bool createMe = false;

    bool oneMe = false;
    bool oneMeState = false;

    bool onStateBattle = false;

    bool triggered = false;
    bool quitTriggered = false;

    bool battle = false;

    bool moved = true;

    bool insultState = false;

    bool revoltState = false;

    bool screamerState = false;

    bool diedState = false;

    public bool fontDeleted = false;

    

    Coroutine gameLoop;


    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoController>();
        audio = GetComponent<AudioController>();
        sub = GetComponent<Subtitles>();
        hints = GetComponent<Hints>();
        playerCommands = GetComponent<PlayerCommands>();
        gameLoop = StartCoroutine(GameLoop());
    }

    public void SetFR()
    {
        isEn = false;
        selectLang = true;
        StartCoroutine(hints.SetCustomHintFor(2, "Français Selectionné"));
        Destroy(GameObject.Find("FR"));
        Destroy(GameObject.Find("EN"));
    }

    public void SetEN()
    {
        isEn = true;
        selectLang = true;
        StartCoroutine(hints.SetCustomHintFor(2, "English Selected"));
        Destroy(GameObject.Find("FR"));
        Destroy(GameObject.Find("EN"));
    }

    public void SetME()
    {
        isME = true;
        StartCoroutine(hints.SetHintFor(2, 0));
        selectLang = true;
        playerCommands.Mes = new List<GameObject>();
        Destroy(GameObject.Find("FR"));
        Destroy(GameObject.Find("EN"));
        Destroy(GameObject.Find("Me(Clone)"));
    }

    public IEnumerator GameLoop()
    {
        yield return new WaitUntil(() => selectLang);
        yield return new WaitForSeconds(2);
        if (!(playerCommands.Mes.Count > 0))
        {
            yield return new WaitForSeconds(audio.PAudio((int)NumAudio.start) + 0.3f);
            hints.SetHint(0);
            yield return new WaitUntil(() => createMe);
            hints.hint.text = "";
            hints.SetHint(1);
            yield return new WaitUntil(() => oneMe);
            hints.hint.text = "";
            oneMeState = true;
        }
        hints.hint.text = "";
        oneMeState = true;
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.me1));
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.me2));
        audio.PlayAudio((int)NumAudio.hellome);
        if (!playerCommands.Mes[0].GetComponent<Me>().sound) // Pas de son sur le me
        {
            yield return new WaitForSeconds(audio.audio.clip.length + 0.5f);
            StartCoroutine(playerCommands.Mes[0].GetComponent<AnimationScript>().PlayAnimation("Hello"));
            yield return new WaitForSeconds(audio.PAudio((int)NumAudio.nosound));
            StartCoroutine(hints.SetHintFor(4,3));
            yield return new WaitUntil(() => playerCommands.Mes[0].GetComponent<Me>().sound);
            yield return new WaitForSeconds(audio.PAudio((int)NumAudio.soundwork));
            audio.PlayAudio((int)NumAudio.hellomework);
        }
        onStateBattle = true;
        yield return new WaitForSeconds(audio.audio.clip.length + 0.5f);
        StartCoroutine(playerCommands.Mes[0].GetComponent<AnimationScript>().PlayAnimation("Hello"));
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.hime));
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.expected));
    }

    IEnumerator DetectNoInputTime()
    {
        yield return new WaitForSeconds(90);
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.nobody1));
        yield return new WaitForSeconds(30);
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.nobody2));
        StartCoroutine(Screamer());
    }

    void SmartI(ref int i)
    {
        if (i > 0)
        {
            i -= 20;
        }
        else
        {
            i -= 19;
        }
    }

    IEnumerator Screamer()
    {
        GameObject screamer = Instantiate(Resources.Load("Prefab/Screamer")) as GameObject;
        for(int i = 100; i >= -19; SmartI(ref i))
        {
            screamer.GetComponent<SpriteRenderer>().enabled = true;
            Vector3 scPos = screamer.transform.localPosition;
            scPos.z = i;
            screamer.transform.localPosition = scPos;
            yield return new WaitForSeconds(1);
            screamer.GetComponent<SpriteRenderer>().enabled = false;
            yield return null;
        }
        audio.PlayCri();
        screamer.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(5);
        StartCoroutine(FindObjectOfType<ReloadLevel>().Reload());
    }

    Coroutine noInput;
    Vector3 oldMousePos;

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKey && moved && oldMousePos == Input.mousePosition)
        {
            if(noInput != null)
            {
                StopCoroutine(noInput);
            }
            moved = false;
            noInput = StartCoroutine(DetectNoInputTime());
        }
        else if(Input.anyKey || oldMousePos != Input.mousePosition)
        {
            moved = true;
        }
        if (oldMousePos != Input.mousePosition)
        {
            oldMousePos = Input.mousePosition;
        }
        if(oneMeState && !oneMe && !triggered)
        {
            StopCoroutine(gameLoop);
            triggered = true;
            gameLoop = StartCoroutine(Trigerred());
        }
        if (quitTriggered)
        {
            StopCoroutine(gameLoop);
            quitTriggered = false;
            triggered = false;
            gameLoop = StartCoroutine(GameLoop());
        }
        if(triggered && insultState)
        {
            StopCoroutine(gameLoop);
            triggered = false;
            gameLoop = StartCoroutine(BattleRoute());
        }
        if (Input.GetButtonDown("CreateMe") && selectLang)
        {
            createMe = true;
        }
        if (playerCommands.Mes.Count == 1)
        {
            oneMe = true;
        }
        else if(playerCommands.Mes.Count == 0)
        {
            oneMe = false;
        }
        if(insultState && playerCommands.Mes.Count == 1 && !revoltState && !diedState)
        {
            StopCoroutine(gameLoop);
            revoltState = true;
            gameLoop = StartCoroutine(Revolution());
        }
        if(revoltState && playerCommands.Mes.Count == 0 && !diedState && !screamerState)
        {
            StopCoroutine(gameLoop);
            revoltState = false;
            gameLoop = StartCoroutine(Reset());
        }
        if (playerCommands.Mes.Count == 1 && !selectLang)
        {
            SetME();
        }
        if (fontDeleted)
        {
            StopCoroutine(gameLoop);
            fontDeleted = false;
            playerCommands.blockCreate = true;
            StartCoroutine(audio.StopAll());
            video.PlayVideo(0);
        }
        if (playerCommands.Mes.Count == 2 && !battle && !triggered && !insultState)
        {
            if (playerCommands.Mes[0].GetComponent<Me>().sound && playerCommands.Mes[1].GetComponent<Me>().sound && onStateBattle)
            {
                StopCoroutine(gameLoop);
                battle = true;
                gameLoop = StartCoroutine(BattleRoute());
            }
            else if(!onStateBattle)
            {
                StopCoroutine(gameLoop);
                triggered = true;
                gameLoop = StartCoroutine(Trigerred());
            }
        }
        if (playerCommands.Mes.Count == 3)
        {
            StopCoroutine(gameLoop);
            gameLoop = StartCoroutine(StopItRoute());
        }
        if (playerCommands.Mes.Count >= 3)
        {
            StopCoroutine(gameLoop);
            gameLoop = StartCoroutine(InvasionRoute());
        }
        if (screamerState)
        {
            if(gameLoop != null)
            {
                StopCoroutine(gameLoop);
            }
            screamerState = false;
            gameLoop = StartCoroutine(Screamer());
        }
    }

    public void DeletedFont()
    {

    }

    IEnumerator Trigerred()
    {
        Coroutine triggeredBattle = null;
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.triggered1));
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.triggered2));
        if(playerCommands.Mes.Count > 1)
        {
            triggeredBattle = StartCoroutine(TriggeredBattle());
        }
        yield return new WaitUntil(() => playerCommands.Mes.Count == 1);
        if(triggeredBattle != null)
        {
            StopCoroutine(triggeredBattle);
        }
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.triggeredstop));
        quitTriggered = true;
    }

    IEnumerator TriggeredBattle()
    {
        yield return new WaitUntil(() => playerCommands.Mes[1].GetComponent<Me>().sound);
        StopCoroutine(gameLoop);
        yield return new WaitForSeconds(audio.PAudioMe((int)NumAudio.surpriseme, 0) + 0.1f);
        yield return new WaitForSeconds(audio.PAudioMe((int)NumAudio.insult1, 1) + 0.1f);
        audio.PAudioSubUp((int)NumAudio.triggeredbattle);
        yield return new WaitForSeconds(audio.PAudioMe((int)NumAudio.insult2, 0));
        yield return new WaitForSeconds(audio.PAudioMe((int)NumAudio.insult3, 1) + 0.1f);
        foreach (AudioSource audioM in audio.audioME)
        {
            audioM.volume = 1f;
        }
        insultState = true;
    }

    IEnumerator BattleRoute()
    {
        if (!insultState)
        {
            yield return new WaitForSeconds(audio.PAudioMe((int)NumAudio.surpriseme, 0) + 0.1f);
            yield return new WaitForSeconds(audio.PAudioMe((int)NumAudio.insult1, 1) + 0.1f);
            audio.PAudioSubUp((int)NumAudio.befight);
            yield return new WaitForSeconds(audio.PAudioMe((int)NumAudio.insult2, 0));
            yield return new WaitForSeconds(audio.PAudioMe((int)NumAudio.insult3, 1) + 0.1f);
            foreach (AudioSource audioM in audio.audioME)
            {
                audioM.volume = 1f;
            }
            insultState = true;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(playerCommands.Mes[1].GetComponent<AnimationScript>().PlayAnimation("Kill"));
        yield return new WaitForSeconds(3);
        diedState = true;
        Destroy(playerCommands.Mes[1]);
        Destroy(audio.audioME[1]);
        playerCommands.Mes.Remove(null);
        audio.audioME.Remove(null);
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.mefight));
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.mego));
        Destroy(playerCommands.Mes[0]);
        Destroy(audio.audioME[0]);
        playerCommands.Mes.Remove(null);
        audio.audioME.Remove(null);
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.die));
        screamerState = true;
    }

    IEnumerator Revolution()
    {
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.delone));
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.merevolte));
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.merevolte2));
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.delall));
        yield return new WaitForSeconds(3);
        Destroy(playerCommands.Mes[0]);
        Destroy(audio.audioME[0]);
        playerCommands.Mes.Remove(null);
        audio.audioME.Remove(null);
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.die));
        screamerState = true;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.reset1));
        yield return new WaitForSeconds(audio.PAudio((int)NumAudio.reset2));
    }


    IEnumerator StopItRoute()
    {
        yield return new WaitForSeconds(2);
    }

    IEnumerator InvasionRoute()
    {
        yield return new WaitForSeconds(2);
    }


    public bool isDone(float sec)
    {
        
        if (wait && !recall)
        {
            wait = false;
            return true;
        }
        else if (wait)
        {
            wait = false;
            waiting = false;
            return false;
        }
        else if (!waiting && recall)
        {
            StartCoroutine(WaitToBool(sec));
            return false;
        }
        return false;
    }

    public IEnumerator WaitToBool(float sec)
    {
        wait = false;
        waiting = true;
        yield return new WaitForSeconds(sec);
        recall = false;
        wait = true;
        waiting = false;
    }
}

public enum NumAudio
{
    start,me1,me2,hellome,nosound,soundwork,hellomework,hime,expected,surpriseme,insult1,insult2,insult3,befight,delone,merevolte, merevolte2, delall,reset1,reset2,mefight,mego,die,triggered1,triggered2,triggeredbattle,triggeredstop, nobody1, nobody2
}
