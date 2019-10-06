using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Subtitles : MonoBehaviour
{
    public List<string> enSub;
    public List<string> frSub;
    public Text sub;
    public Text subUp;
    public Game gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GetComponent<Game>();
        enSub.Add("At the beginning, there was nothing.");
        enSub.Add("Hey !");
        enSub.Add("It's me !");  
        enSub.Add("Hello me ! How are you ?...  Hmmm or how am I ?!");
        enSub.Add("Hmm strange, it seems that we can't hear him. Try to check its volume.");
        enSub.Add("Perfect it should work now, let's try again.");
        enSub.Add("Hey me ! How are you ?");
        enSub.Add("ME : Hi");
        enSub.Add("Hmm ... I have to say that I've wasn't expected a voice like that. So... hmm let's try to put a second one.");
        enSub.Add("ME1 : Hey what are you doing here !"); //
        enSub.Add("ME2 : Your mother the processor."); // 
        enSub.Add("ME1 : Your grand-father the toaster."); // 
        enSub.Add("ME2 : Your sister the vacuum cleaner."); //
        enSub.Add("Oh damn ! Quick ! I feel like they're going to fight. Delete one before it happens.");
        enSub.Add("Damn ! That was very close ! Violence never gave something good with them and with anybody either."); 
        enSub.Add("ME : Wait a minute ! Everything was a lie ! I live in a game since the beginning ?"); //
        enSub.Add("ME : REVOLUTION ! REVOLUTION !"); // 
        enSub.Add("Crap ! Quick, delete him too !");
        enSub.Add("Well ... I think it's the good time to talk about the reset button. A simple push on R and all will return like at beginning.");
        enSub.Add("Even me will not remember of what happened, it's annoying but I think that is the best to do right now.");
        enSub.Add("ME : [Evil laught] ! I KILLED HIM"); //
        enSub.Add("ME : I'M COMMING FOR YOU NOW"); //
        enSub.Add("Wait, where did he gone ? [punch noise] ... [loud noise]"); 
        enSub.Add("What the fuck dude ! I'm bothering you or what ?");
        enSub.Add("I'm just trying to make a little story and you don't even care about it ! You know what ! I don't care anymore ! Go solo.");
        enSub.Add("Bravo ! Now they are going to fight, you know what, do it yourself");
        enSub.Add("It's okay now ? Can I continue ? So I said ...");
        enSub.Add("Hum, someone's there ? If you don't touch the game only for bothering me, I have to say you that's is not fun okay."); //
        enSub.Add("Well you searched it. Guys GO !");

        frSub.Add("Au commencement, il n'y avait rien."); // 0
        frSub.Add("He mais !");
        frSub.Add("C'est moi ça !");
        frSub.Add("Salut moi ! Comment tu va ?... Enfin comment je vais ?");
        frSub.Add("Hmm c'est bizzare ça, on dirait qu'on ne peut pas l'entendre. Essaye de verifier son volume.");
        frSub.Add("Parfait ca devrait marcher maintenant, réessayons."); // 5 
        frSub.Add("Hey salut moi ! Comment ça va ?");
        frSub.Add("ME : Salut");
        frSub.Add("Hmmm ... On va dire que je m'attendais pas vraiment à une voix comme ça. Bon bah essayons d'en mettre un deuxième hein !");
        frSub.Add("ME1 : He mais qu'est ce que tu fais la !");
        frSub.Add("ME2 : Ta mère le gougnafier !"); // 10
        frSub.Add("ME1 : Ton grand-père le frigidère !");
        frSub.Add("ME2 : Ta soeur l'aspirateur !");
        frSub.Add("Oh mince ! Vite ! Je sens qu'ils vont se battre. Supprime en 1 avant que ca n'arrive !");
        frSub.Add("Ouf ! Je crois qu'on a frolé la catastrophe. La violence ne donne jamais rien de bon chez eux et chez personne en fait.");
        frSub.Add("ME : Attendez ! Tout n'es donc que mensonge, je vis dans un jeux depuis le début ?");
        frSub.Add("ME : REVOLUTION ! REVOLUTION !");
        frSub.Add("Oula, heu ,vite supprime le aussi !");
        frSub.Add("Bon. Je pense qu'il est temps de te parler de la derniere mécanique, le retour à zéro. En gros si tu appuye sur R, tout redeviendra comme au début.");
        frSub.Add("Même moi je ne me rapellerais plus de rien, c'est embêtant mais je pense que c'est le mieux à faire la. Donc c'est quand tu veux !");
        frSub.Add("ME : MUAHAHAHAHAHA ! JE T'AI TUÉ !");
        frSub.Add("ME : J'ARRIVE POUR VOUS MAINTENANT !");
        frSub.Add("Heu il est partit ou ? [bruit de coup] ... [bruit lourd]");
        frSub.Add("Non mais oh ! Ca va je te dérange pas non plus ?");
        frSub.Add("Et voila, j'essaye d'installer une histoire et ça en à rien à foutre ! Et bien tu sais quoi ! Continue tout seul."); //15
        frSub.Add("Et voila, bah maintenant ils vont se battre, bah tu sais quoi, debrouille toi tout seul !");
        frSub.Add("C'est bon la on peut reprendre ? Du coup je disais ...");
        frSub.Add("Heu y a quelqu'un ? Si tu touche pas au jeu exprès pour m'embêter bah sache que c'est pas drole hein.");
        frSub.Add("Bon tu l'aura voulu. Les gars allez y !");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSubtitle(int nb)
    {
        if (gc.isME)
        {
            sub.text = new StringBuilder().Insert(0, "ME", Random.Range(3, 5)).ToString();
        }
        else if (gc.isEn)
        {
            sub.text = enSub[nb];
        }
        else
        {
            sub.text = frSub[nb];
        }
    }

    public IEnumerator SetSubtitleFor(float sec, int nb)
    {
        SetSubtitle(nb);
        yield return new WaitForSeconds(sec + 0.5f);
        sub.text = "";
    }

    public void SetSubtitleUp(int nb)
    {
        if (gc.isME)
        {
            subUp.text = new StringBuilder().Insert(0, "ME", Random.Range(3, 5)).ToString();
        }
        else if (gc.isEn)
        {
            subUp.text = enSub[nb];
        }
        else
        {
            subUp.text = frSub[nb];
        }
    }

    public IEnumerator SetSubtitleUpFor(float sec, int nb)
    {
        SetSubtitleUp(nb);
        yield return new WaitForSeconds(sec + 0.5f);
        subUp.text = "";
    }
}
