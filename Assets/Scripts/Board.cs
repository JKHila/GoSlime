using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    public static float time = 100;
    public static float fevertime = 10;
    public Image hpbar;
    public Image fever;
    public Image bar;
    
    public Image scorebar;
    public Text timeText;
    public Text Score;
    public Text Combo;
    
    public static double score;
    public static double combo;
    public static bool comboswitch = false;

   
    void timer() //시간초
    {
            time -= Time.deltaTime;
            timeText.text = ((int)time).ToString();
            hpbar.fillAmount = time * 0.01f;
       
    }

    public void feverAction() { StartCoroutine(feverTimer()); }

    IEnumerator feverTimer()
    {
        fever.gameObject.SetActive(true); bar.gameObject.SetActive(true);
        fevertimer();
        yield return new WaitForSeconds(0.001f);
        
    }
    void fevertimer() //fever시간초
    {
        fevertime  -= Time.deltaTime;
        bar.fillAmount = fevertime * 0.1f;
    }
    public void GameStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1, LoadSceneMode.Single);

    }
    public void toLobby()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }
    void Update()
    {
        scorebar.fillAmount = SlimeMove.temp * 0.001f;
        if (SlimeMove.feverChk == true)
        {
            feverAction();
           
        }
        else if(SlimeMove.feverChk == false)
        {
            fevertime = 10; fever.gameObject.SetActive(false); bar.gameObject.SetActive(false);
        }


        if (combo != 0 && comboswitch == true)
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
