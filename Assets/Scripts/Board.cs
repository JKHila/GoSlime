using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public static float time = 30;
    public Image hpbar;
    public Text timeText;
    public Text Score;
    public Text Combo;
    public static int combo;
    public static bool comboswitch = false;
   
    public static int score;
    void timer() //시간초
    {
            time -= Time.deltaTime;
            timeText.text = ((int)time).ToString();
            hpbar.fillAmount = time * 0.01f;
       
    }
    void Update()
    {
        if(combo != 0 && comboswitch == true)
        {
           Combo.gameObject.SetActive(true);
        }
        else
        {
            Combo.gameObject.SetActive(false);
        }

       Combo.text = combo.ToString();
        Score.text = score.ToString();

        if (time > 0)
        {
            timer();
        }
    }
}
