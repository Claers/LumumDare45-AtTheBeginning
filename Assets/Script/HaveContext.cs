using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaveContext : MonoBehaviour
{
    public Object contextMenu;
    public GameObject contextGo;
    public GameObject canvas;
    public Game gc;

    // Start is called before the first frame update
    void Start()
    {
        contextMenu = Resources.Load("Prefab/ContextMenu");
        canvas = FindObjectOfType<Canvas>().gameObject;
        gc = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        /* DISABLED FEATURE FOR THE JAM
        if(contextGo != null)
        {
            if (contextGo.transform.GetChild(2).GetComponent<InputField>().text != "" && Input.GetButtonDown("Validate"))
            {
                gc.playerCommands.ExecCommand(contextGo.transform.GetChild(2).GetComponent<InputField>().text.ToLower(), GetComponent<Me>().nb);
            }
        }
        */
    }

    public void Select()
    {
        if(GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().SetFloat("selNormalTime", GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
            GetComponent<Animator>().SetBool("select", true);
            
        }
    }

    public void OpenContextMenu()
    {
        if(GetComponent<Me>() != null)
        {
            contextGo = Instantiate(contextMenu, canvas.transform) as GameObject;
            contextGo.GetComponent<RectTransform>().localPosition = new Vector3(contextGo.GetComponent<RectTransform>().localPosition.x + 100, contextGo.GetComponent<RectTransform>().localPosition.y - 10, contextGo.GetComponent<RectTransform>().localPosition.z);
            if (GetComponent<Me>().sound)
            {
                contextGo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Son actif";
            }
            contextGo.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { SoundButton(); });
            contextGo.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { DeleteElem(); });
        }
        else
        {
            contextGo = Instantiate(contextMenu, canvas.transform) as GameObject;
            contextGo.transform.localPosition = new Vector3(700,200,0);
            contextGo.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { DeleteElem(); });
        }
    }

    public void DeleteElem()
    {
        if (GetComponent<Me>() != null)
        {
            gc.playerCommands.Mes.Remove(gameObject);
            gc.audio.audioME.Remove(gameObject.GetComponent<AudioSource>());
            Destroy(contextGo);
            Destroy(gameObject);
        }
        else
        {
            gc.fontDeleted = true;
            Destroy(gameObject);
        }
    }

    public void SoundButton()
    {
        if (GetComponent<Me>().sound)
        {
            DeactivateSoundButton();
        }
        else
        {
            ActivateSoundButton();
        }
    }

    public void ActivateSoundButton()
    {
        GetComponent<Me>().sound = true; 
        contextGo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Son actif";
        Unselect();
    }

    public void DeactivateSoundButton()
    {
        GetComponent<Me>().sound = false;
        contextGo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Son coupé";
        Unselect();
    }

    public void Unselect()
    {
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().SetFloat("NormalTime", GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
            GetComponent<Animator>().SetBool("select", false);
        }
        Destroy(contextGo);
        
    }
}
