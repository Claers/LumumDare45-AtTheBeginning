using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCommands : MonoBehaviour
{
    public GameObject goBuffer;
    public Object MePrefab;

    public List<GameObject> Mes;
    public GameObject selectedGO;

    public Game gc;

    public bool blockCreate;

    // Start is called before the first frame update
    void Start()
    {
        MePrefab = Resources.Load("Prefab/Me");
        gc = GetComponent<Game>();
    }

    IEnumerator MoveTo(GameObject go, Vector3 pos)
    {
        while (true)
        {
            try
            {
                go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, pos, 1.5f * Time.deltaTime);
                if(V3Equal(go.transform.position, pos))
                {
                    break;
                }
            
            }
            catch (MissingReferenceException)
            {
                break;
            }
        yield return null;
        }

    }

    public void ExecCommand(string command, int MeNb)
    {
        FRCmd fr_enum = FRCmd.None;
        ENCmd en_enum = ENCmd.None;
        System.Enum.TryParse<FRCmd>(command,out fr_enum);
        if(fr_enum == FRCmd.None)
        {
            System.Enum.TryParse<ENCmd>(command, out en_enum);
        }
        if (fr_enum == FRCmd.None && en_enum == ENCmd.None)
        {

        }
        if (gc.isME)
        {

        }
        else if (gc.isEn)
        {
            if(en_enum == ENCmd.insult || fr_enum == FRCmd.insulter)
            {
                Mes[MeNb].GetComponent<Me>().Insult();
            }
        }
        else
        {
            if (fr_enum == FRCmd.insulter || en_enum == ENCmd.insult)
            {
                Mes[MeNb].GetComponent<Me>().Insult();
            }
        }
    }

    public bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.1;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 mouseToWorld = Input.mousePosition;
        mouseToWorld.z = 20;
        // UI Raycast
        PointerEventData cursor = new PointerEventData(EventSystem.current);
        cursor.position = Input.mousePosition;
        List<RaycastResult> objectsHit = new List<RaycastResult>();
        EventSystem.current.RaycastAll(cursor, objectsHit);

        if (!blockCreate)
        {
            if (Input.GetMouseButtonDown(1) && objectsHit.Count == 0)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseToWorld);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    if (selectedGO != null)
                    {
                        if (selectedGO.GetComponent<HaveContext>() != null)
                        {
                            selectedGO.GetComponent<HaveContext>().Unselect();
                        }
                    }
                    if (hit.collider.gameObject.GetComponent<HaveContext>() != null)
                    {
                        selectedGO = hit.collider.gameObject;
                        hit.collider.gameObject.GetComponent<HaveContext>().Select();
                        hit.collider.gameObject.GetComponent<HaveContext>().OpenContextMenu();
                    }
                }
            }
            if (Input.GetMouseButtonDown(0) && objectsHit.Count == 0)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseToWorld);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    if (selectedGO != null)
                    {
                        if (selectedGO.GetComponent<HaveContext>() != null)
                        {
                            selectedGO.GetComponent<HaveContext>().Unselect();
                        }
                    }
                    if (hit.collider.gameObject.GetComponent<HaveContext>() != null)
                    {
                        selectedGO = hit.collider.gameObject;
                        hit.collider.gameObject.GetComponent<HaveContext>().Select();
                    }
                }
            }

            if (goBuffer == null)
            {
                if (Input.GetButtonDown("CreateMe"))
                {
                    CreateMe();
                }
            }
            if (goBuffer != null)
            {
                goBuffer.transform.position = Camera.main.ScreenToWorldPoint(mouseToWorld);
                if (Input.GetButtonDown("Select"))
                {
                    Mes.Add(goBuffer);
                    gc.audio.audioME.Add(goBuffer.GetComponent<AudioSource>());
                    if (gc.selectLang)
                    {
                        goBuffer.GetComponent<Animator>().enabled = true;
                        goBuffer.GetComponent<BoxCollider2D>().enabled = true;
                        goBuffer.GetComponent<Me>().nb = Mes.Count;
                        if (Mes.Count == 1)
                        {
                            StartCoroutine(MoveTo(goBuffer, Vector3.zero));
                        }
                        else if (Mes.Count == 2)
                        {
                            StartCoroutine(MoveTo(goBuffer, new Vector3(-10, 0, 0)));
                        }
                        else if (Mes.Count == 3)
                        {
                            StartCoroutine(MoveTo(goBuffer, new Vector3(10, 0, 0)));
                        }
                        else if (Mes.Count == 4)
                        {
                            StartCoroutine(MoveTo(goBuffer, new Vector3(-4, 7, 0)));
                        }
                        else if (Mes.Count == 5)
                        {
                            StartCoroutine(MoveTo(goBuffer, new Vector3(4, 7, 0)));
                        }
                        else if (Mes.Count == 6)
                        {
                            StartCoroutine(MoveTo(goBuffer, new Vector3(15, 7, 0)));
                        }
                        else if (Mes.Count == 7)
                        {
                            StartCoroutine(MoveTo(goBuffer, new Vector3(-15, 7, 0)));
                        }
                        else if (Mes.Count == 8)
                        {
                            StartCoroutine(MoveTo(goBuffer, new Vector3(-15, -7, 0)));
                        }
                        else if (Mes.Count == 9)
                        {
                            StartCoroutine(MoveTo(goBuffer, new Vector3(15, -7, 0)));
                        }

                    }
                    goBuffer = null;
                }
            }
            if (selectedGO != null && Input.GetButton("Unselect"))
            {
                if (selectedGO.GetComponent<HaveContext>() != null)
                {
                    selectedGO.GetComponent<HaveContext>().Unselect();
                    selectedGO = null;
                }
            }
        }
    }

    void CreateMe()
    {
        goBuffer = Instantiate(MePrefab) as GameObject;
        goBuffer.GetComponent<Animator>().enabled = false;
    }
}

public enum FRCmd
{
    None,insulter, blague, chante, aide
}

public enum ENCmd
{
    None,insult, joke, sing, help
}