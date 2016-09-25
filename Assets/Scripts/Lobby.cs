using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
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
        for (float i = 1f; i >= 0.5; i -= 0.01f)
        {
            Color color = new Color(255, 255, 255, i);
            start.color = color;
            if (i <= 0.51f)
            {
                StartCoroutine("Blinkout");
            }
            else
            {
                yield return new WaitForSeconds(0.0001f);
            }
            
        }
    }
    IEnumerator Blinkout()
    {
        for (float i = 0.5f; i <= 1f; i += 0.01f)
        {
            Color color = new Color(255, 255, 255, i);
            start.color = color;
            if (i >= 0.99f)
            {
                StartCoroutine("Blinkin");

            }
            else
            {
                yield return new WaitForSeconds(0.0001f);
            }

        }
    }
    
    // Update is called once per frame
    void Update () {
       // UnityEngine.SceneManagement.SceneManager.LoadScene("Stage");
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);

    }
}
