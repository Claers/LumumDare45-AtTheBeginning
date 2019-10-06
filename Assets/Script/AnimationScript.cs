using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Animator>().SetFloat("selNormalTime", GetComponent<Animator>().GetFloat("selNormalTime") + (1f * Time.fixedDeltaTime));
        GetComponent<Animator>().SetFloat("NormalTime", GetComponent<Animator>().GetFloat("NormalTime") + (1f * Time.fixedDeltaTime));
    }

    public IEnumerator PlayAnimation(string animName)
    {
        GetComponent<Animator>().SetBool(animName, true);
        GetComponent<Animator>().SetFloat("selNormalTime", 0);
        GetComponent<Animator>().SetFloat("NormalTime", 0);
        yield return new WaitUntil(() => (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length < GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime) && (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(animName) || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(animName+"Sel")));
        GetComponent<Animator>().SetBool(animName, false);
    }
}
