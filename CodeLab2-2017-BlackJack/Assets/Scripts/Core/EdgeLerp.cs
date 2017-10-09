using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdgeLerp : MonoBehaviour
{

    private Image blurredEye;
    private bool hasFaded;
    Vector3 initialScale;
    Vector3 targetScale;
    bool faded;
    public float duration;
    float timeElapsed = 0;
    public static EdgeLerp instance;

    // Use this for initialization
    void Start()
    {

        blurredEye = this.GetComponent<Image>();
        blurredEye.CrossFadeAlpha(0.75f, 0, true);
        initialScale = blurredEye.transform.localScale;
        targetScale = new Vector3(initialScale.x + 0.4f, initialScale.y + 0.4f, initialScale.z + 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!faded)
        {
            timeElapsed = Mathf.Min(duration, timeElapsed + Time.deltaTime);
            blurredEye.transform.localScale = Vector3.Lerp(initialScale, targetScale, timeElapsed / duration);
            if (timeElapsed == duration)
            {
                faded = true;
                timeElapsed = 0;
            }
        }
        else
        {
            timeElapsed = Mathf.Min(duration, timeElapsed + Time.deltaTime);
            blurredEye.transform.localScale = Vector3.Lerp(targetScale, initialScale, timeElapsed / duration);
            if (timeElapsed == duration)
            {
                faded = false;
                timeElapsed = 0;
            }
        }



    }
}
