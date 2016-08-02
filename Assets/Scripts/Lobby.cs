using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Lobby : MonoBehaviour {

    public Text start;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("Blinkin");
    }

    IEnumerator Blinkin()
    {
        for (float i = 1f; i >= 0; i -= 0.1f)
        {
            Color color = new Color(255, 255, 255, i);
            start.color = color;
            if (i <= 0.1f)
            {
                StartCoroutine("Blinkout");
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
            
        }
    }
    IEnumerator Blinkout()
    {
        for (float i = 0.1f; i <= 1f; i += 0.1f)
        {
            Color color = new Color(255, 255, 255, i);
            start.color = color;
            if (i >= 0.9f)
            {
                StartCoroutine("Blinkin");

            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }

        }
    }

    // Update is called once per frame
    void Update () {
       
    }
}
