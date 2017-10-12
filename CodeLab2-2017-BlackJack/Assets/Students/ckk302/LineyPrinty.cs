using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineyPrinty : MonoBehaviour
{

    private int count = 5;

    public TextMesh title;
    public TextMesh line1;
    public TextMesh line2;
    public TextMesh line3;
    public TextMesh line4;

    public string[] titles =
    {
        "my cool ass poem",
        "shelley: a poem",
        "my dank ass poem",
        "my sick ass poem",
        "wow what a hot mess",
        "an ode to eggs",
        "dogs are very pure but i am trash",
        "i call it bold and brash",
        "beautiful like pandas in the sky",
        "why",
        "(shelley voice) did you miss me?",
        "egg tarts",
        "creepy crepes",
        "butts: a poem",
        "change!",
        "cool beans: the musical",
        "haty catchy",
        "whiteboard marker: a poem",
        "my first experience with kombucha",
        "tiger tiger: a shitty club"
    };

    public string[] lineys = {
        "why is there so much work",
        "i wish for death to claim me",
        "i'm just a baby",
        "cha cha cha",
        "i'm an egg",
        "i want to buy new clothes",
        "retail therapy is a blessing and a curse",
        "i read a scary story at 2am and now i regret it",
        "what am i going to do in the future",
        "did i remember to renew my passport",
        "what should i make for dinner",
        "i hope my dogs are doing well",
        "i'm hungry",
        "i'm sleepy",
        "did i switch off the oven before i left the flat",
        "why is life so hard",
        "did i forget my keys at home",
        "the rain in spain falls mainly on the plain",
        "say i'm ke ai",
        "shine bright like a panda",
        "a man killed a cow",
        "it was the best of times",
        "it was the worst of times",
        "i want bubble tea",
        "food?"
    };

    private static ShuffleBag<string> wordyjumbly;
    private static ShuffleBag<string> titlejumbly;

    protected virtual void AddLines()
    {
        for (int i = 0; i < 24; i++)
        {
            wordyjumbly.Add(lineys[i]);
        }
    }

    protected virtual void AddTitles()
    {
        for (int i = 0; i < 19; i++)
        {
            titlejumbly.Add(titles[i]);
        }
    }

    private string PickWords()
    {
        string lineToPrint = wordyjumbly.Next();
        return lineToPrint;
    }

    private string PickTitle()
    {
        string printThis = titlejumbly.Next();
        return printThis;
    }

    void OnMouseDown()
    {
        if (count == 5)
        {
            title.text = PickTitle();
            count -= 1;
        } else if (count == 4)
        {
            line1.text = PickWords();
            count -= 1;
        }
        else if (count == 3)
        {
            line2.text = PickWords();
            count -= 1;
        }
        else if (count == 2)
        {
            line3.text = PickWords();
            count -= 1;
        }
        else if (count == 1)
        {
            line4.text = PickWords();
            count -= 1;
        }
        else if (count == 0)
        {
            title.text = " ";
            line1.text = " ";
            line2.text = " ";
            line3.text = " ";
            line4.text = " ";
            count = 5;
        }
        Debug.Log("click");
    }

    // Use this for initialization
    void Start()
    {
        wordyjumbly = new ShuffleBag<string>();
        titlejumbly = new ShuffleBag<string>();
        AddLines();
        AddTitles();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
