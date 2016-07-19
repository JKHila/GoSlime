using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public static float time = 30;
    public Text timeText;
    void timer() //시간초
    {
       
            time -= Time.deltaTime;
            timeText.text = ((int)time).ToString();
       
    }
    void Update()
    {
        if(time <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage");
            time = 30;
           
        }
        else
        {
            timer();
        }
    }
}
