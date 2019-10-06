using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReloadLevel : MonoBehaviour
{
    GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Reload());
    }

    public IEnumerator Reload()
    {
        if (game != null)
        {
            Destroy(game);
            List<GameObject> allObjects = FindObjectsOfType<GameObject>().ToList();
            allObjects.Remove(gameObject);
            foreach (GameObject go in allObjects)
            {
                Destroy(go);
            }
        }
        game = Instantiate(Resources.Load("Prefab/Game")) as GameObject;
        foreach(Transform child in game.transform)
        {
            child.transform.parent = null;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Reload"))
        {
            print("reloading");
            StartCoroutine(Reload());
        }
    }
}
