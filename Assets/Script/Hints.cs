using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour
{
    public Game gc;
    public Text hint;

    public List<string> hintFR;
    public List<string> hintEN;

    // Start is called before the first frame update
    void Start()
    {
        gc = GetComponent<Game>();
        hintFR.Add("Appuyez sur C pour créer quelque chose");
        hintEN.Add("Push C for create something");
        hintFR.Add("Appuyez sur Clic Gauche pour me poser");
        hintEN.Add("Push Left Click to put me down");
        hintFR.Add("Appuyez sur Clic Droit pour selectionner quelque chose. Appuyez sur Echap pour deselectionner;");
        hintEN.Add("Push Left Click to put me down");
        hintFR.Add("Appuyez sur Clic Gauche pour ouvrir le menu contextuel. Appuyez sur Echap pour le fermer");
        hintEN.Add("Push Left Click to put me down");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCustomHint(string text)
    {
        if (gc.isEn)
        {
            hint.text = text;
        }
        else
        {
            hint.text = text;
        }
    }

    public IEnumerator SetCustomHintFor(float sec, string text)
    {
        SetCustomHint(text);
        yield return new WaitForSeconds(sec);
        hint.text = "";
    }


    public void SetHint(int nb)
    {
        if (gc.isME)
        {
            hint.text = new StringBuilder().Insert(0, "ME", Random.Range(3,5)).ToString();
        }
        else if (gc.isEn)
        {
            hint.text = hintEN[nb];
        }
        else
        {
            hint.text = hintFR[nb];
        }
    }

    public IEnumerator SetHintFor(float sec, int nb)
    {
        SetHint(nb);
        yield return new WaitForSeconds(sec);
        hint.text = "";
    }

    public IEnumerator SetHintWhile(bool isTrue, int nb)
    {
        SetHint(nb);
        yield return new WaitUntil(() => isTrue);
        hint.text = "";
        yield return null;
    }
}


