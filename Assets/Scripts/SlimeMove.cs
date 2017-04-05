using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SlimeMove : MonoBehaviour {
    private GameObject nextObj;
    public GameObject[] barArray = new GameObject[7];
    public static bool isJumping;
    public static bool feverChk = false;
    public float speed;
    public GameObject sp;
    public float TimeLeft = 2.0f;
    public float nextTime = 0.0f;
    public static bool timer = false;
    public static int levelUpCount = 0;
    public GameObject result;
    public GameObject highscore;
    public GameObject change;
    public static bool changechk = false;
    public Text score;
    public Text totalscore;
    public static float temp;
    public static float high;

    public Sprite[] motion = new Sprite[10];
    public Sprite[] failmotion = new Sprite[8];

    public Sprite[] normal_true = new Sprite[10];
    public Sprite[] normal_false = new Sprite[6];

    public Sprite[] slim_true = new Sprite[10];
    public Sprite[] slim_false = new Sprite[8];

    public AudioSource _Evolve;
    public AudioSource _moving_true;
    public AudioSource _moving_fail;
    public AudioSource _jump;
    public AudioSource _die;
    public AudioSource BGM;
    public static bool resetchk = false;
    public GameObject getSP()
    {
        return sp;
    }
    public void jump(GameObject nextObj) { this.nextObj = nextObj; StartCoroutine(jumpAction()); }
    public void fail() { StartCoroutine(failAction()); }
  
    IEnumerator jumpAction()
    {
        _jump.Play();
        Board.comboswitch = true;
        isJumping = true;
        Board.score += 1 + (Board.combo / 10)%10*0.1 + (Board.combo / 100); //스코어 계산식
        nextTime = Time.time + TimeLeft;
        if (Time.time < nextTime || Board.combo == 0)
        {
            Board.combo++;
            
        }
        //transform.Translate(new Vector2(nextObj.transform.position.x-3.0f,transform.position.y+1));

        if (temp < 100)
        {
            for (int i = 0; i <= normal_true.Length - 1; i++)
            {
                sp.GetComponent<SpriteRenderer>().sprite = normal_true[i];
                if (i > 2)
                {
                    transform.position = new Vector2(nextObj.transform.position.x + 0.1f, transform.position.y);
                }
                yield return new WaitForSeconds(0.02f);
            }
        }
        else if (temp >= 100)
        {
            for (int i = 0; i <= slim_true.Length - 1; i++)
            {
                sp.GetComponent<SpriteRenderer>().sprite = slim_true[i];
                if (i > 2)
                {        
                        transform.position = new Vector2(nextObj.transform.position.x + 0.1f, transform.position.y);                 
                }
                yield return new WaitForSeconds(0.02f);
            }
        }

        isJumping = false;
        _moving_true.Play();
    }
   
    IEnumerator failAction()
    {
        
        _moving_fail.Play();
        Board.comboswitch = false;
        Board.time -= 5;
        Board.combo = 0;


        if (temp < 100)
        {
            for (int i = 0; i <= normal_false.Length - 1; i++)
            {
                sp.GetComponent<SpriteRenderer>().sprite = normal_false[i];
               
                yield return new WaitForSeconds(0.02f);
            }
        }
        else if (temp >= 100)
        {
            for (int i = 0; i <= slim_false.Length - 1; i++)
            {
                sp.GetComponent<SpriteRenderer>().sprite = slim_false[i];
               
                yield return new WaitForSeconds(0.02f);
            }
        }

        if (Board.time <= 0)
        {
            resetAction();
            _moving_fail.Play();
        }
        
    }

    private static float temp_speed;
    public static bool isLevelUp= true;
    public void LevelUpAction() { StartCoroutine(LevelUp()); }
    public void Close_change()
    {
       
            change.SetActive(false);
        
    }
    public void resetAction() { StartCoroutine(reset()); }
   
    IEnumerator reset()
    {
        resetchk = true;
        if (high==0 || high<Board.score)
        {
            high = (float)Board.score;
            highscore.SetActive(true);
        }
        else
        {
            highscore.SetActive(false);
        }
        PlayerPrefs.SetFloat("highscore", high);
        score.text = Board.score.ToString();
        temp += (float)Board.score;
        PlayerPrefs.SetFloat("totalscore", temp);
        totalscore.text = temp.ToString();
        result.SetActive(true);
        if (temp >= 100 && changechk==false)
        {
            change.SetActive(true);
            changechk = true;
        }
        Time.timeScale = 0;
        Board.score = 0;
        feverChk = false;
        Board.combo = 0;
        Board.time = 100;
        beltMove._speed = -1.0f; // 속도 초기화
        levelUpCount = 0;
        for (int i = 6; i >= 0; i--)
        {
            barArray[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("png/speed");
        }
        _die.Play();
        yield  return new WaitForSeconds(0.0001f);
        resetchk = false;
    }


    IEnumerator LevelUp()
    {
        isLevelUp = false;       
        Board.time += 10; //시간업
        temp_speed = beltMove._speed + (-0.5f);
        while (sp.transform.position.x >= -2.0f)
        {
            beltMove._speed = -7.0f;
            yield return new WaitForSeconds(0.01f);
        }
        beltMove._speed = temp_speed;
        levelUpCount++;
        isLevelUp = true;
        if (levelUpCount >= 1 && isLevelUp == true)
         {
             if (levelUpCount == 1)
             { barArray[levelUpCount - 1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("png/speed_g"); }
             else if (levelUpCount > 1 && levelUpCount < 5)
             { barArray[levelUpCount - 1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("png/speed_y"); }
             else if (levelUpCount > 4 && levelUpCount < 8)
             { barArray[levelUpCount - 1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("png/speed_r"); }
         }
    }
    public void feverAction() { StartCoroutine(fever()); }

    IEnumerator fever()
    {
        feverChk = true;
        yield return new WaitForSeconds(10f);
        feverChk = false;
        isJumping = false;
    }

    void Start () {
        
        if (temp >= 100)
        {
            sp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("슬라임 이미지/슬림점프/slim1");
            
        }
        else if (temp <100)
        {
            sp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("슬라임 이미지/노말점프/nomal1");
        }
        
       
        if (Lobby.bgmchk==true)
        {
            BGM.Play();
        }
        else
        {
            BGM.Stop();
        }
    }

    // Update is called once per frame

    
    void Update () {
        if (Board.combo % 10 == 0 && Board.combo >= 10 && Time.time < nextTime)
        {
            feverAction();
        }
        if (Time.time > nextTime)
        {
            Board.comboswitch = false;
            Board.combo=0;
        }

        if (sp.transform.position.x >= 4.8f&&isLevelUp==true)
        {
            LevelUpAction();
        }
        else if(sp.transform.position.x<= -9.7)
        {
            resetAction();
            
            _die.Play();
        }

        if(Board.time <= 0)
        {
            resetAction();
            _die.Play();
        }

    }


}
