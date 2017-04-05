using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Lobby : MonoBehaviour {

    public Image start;
    public AudioSource BGM;
    public AudioClip file;
    public GameObject setting;
    public GameObject change;
    public GameObject vol;
    public Image slime;
    public static bool bgmchk = true;

    // Use this for initialization
    void Start()
    {
        SlimeMove.temp = PlayerPrefs.GetFloat("totalscore");
        SlimeMove.high = PlayerPrefs.GetFloat("highscore");
        

        if (Lobby.bgmchk == true)
        {
            vol.GetComponent<Image>().sprite = Resources.Load<Sprite>("png/SoundOn");
            BGM.Play();
        }
        else
        {
            vol.GetComponent<Image>().sprite = Resources.Load<Sprite>("png/SoundOff");
            BGM.Stop();
        }

        StartCoroutine("Blinkin");
    }
    public void setting_UI()
    {
        setting.SetActive(true);
    }
    public void change_UI()
    {
        change.SetActive(true);
    }
    public void Close_UI()
    {
        change.SetActive(false);
        setting.SetActive(false);
    }
    public void volume()
    {
        if(vol.GetComponent<Image>().sprite == Resources.Load<Sprite>("png/SoundOn"))
        {
            vol.GetComponent<Image>().sprite = Resources.Load<Sprite>("png/SoundOff");
            bgmchk = false;
            this.BGM.Stop();
        }
        else
        {
            vol.GetComponent<Image>().sprite = Resources.Load<Sprite>("png/SoundOn");
            bgmchk = true;
            this.BGM.Play();           
        }
       
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            SlimeMove.temp = 0;
            SlimeMove.high = 0;
            SlimeMove.changechk = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SlimeMove.temp = 99;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SlimeMove.temp = 101;
        }

        if (SlimeMove.temp >= 100)
        {
            slime.GetComponent<Image>().sprite = Resources.Load<Sprite>("슬라임 이미지/슬림점프/slim1");
        }
        else if (SlimeMove.temp < 100)
        {
            
            slime.GetComponent<Image>().sprite = Resources.Load<Sprite>("슬라임 이미지/노말점프/nomal1");

        }
    }
   
    public void GameStart()
    {
        
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
