using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlimeMove : MonoBehaviour {
    private GameObject nextObj;

    public static bool isJumping;
    public float speed;
    public GameObject sp;
    public float TimeLeft = 2.0f;
    public float nextTime = 0.0f;

    public Sprite[] motion = new Sprite[10];
    public GameObject getSP()
    {
        return sp;
    }
    public void jump(GameObject nextObj) { this.nextObj = nextObj; StartCoroutine(jumpAction()); }
    public void fail() { StartCoroutine(failAction()); }
  
    IEnumerator jumpAction()
    {
        Board.comboswitch = true;
        isJumping = true;
        Board.score++;
        nextTime = Time.time + TimeLeft;
        if (Time.time < nextTime || Board.combo == 0)
        {
            Board.combo++;
            
        }
        //transform.Translate(new Vector2(nextObj.transform.position.x-3.0f,transform.position.y+1));
        for (int i=0;i<=9;i++)
        {
            sp.GetComponent<SpriteRenderer>().sprite = motion[i];
            if(i>2)
            {
                transform.position = new Vector2(nextObj.transform.position.x+0.1f, transform.position.y);
            }
            yield return new WaitForSeconds(0.02f);
        }

        isJumping = false;
     
    }    
    IEnumerator failAction()
    {
        Board.comboswitch = false;
        Board.time -= 5;
        Board.combo = 0;
        int z = 30;
        while (z<360)
        {
            sp.transform.rotation = Quaternion.Euler(0, 0, z);
            z += 30;
            yield return new WaitForSeconds(0.01f);
        }
        sp.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (Board.time == 0)
        {
            reset();
        }
    }
    private static float a;
    public bool isLevelUp= true;
    public void LevelUpAction() { StartCoroutine(LevelUp()); }
    

   public void reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage");
        Board.score = 0;
        Board.combo = 0;
        Board.time = 30;
        belt.stgN = 0; // 패턴초기화
        belt.count = 0; //패턴순서 처음으로
        beltMove._speed = -1.0f; // 속도 초기화
    }

    IEnumerator LevelUp()
    {
        isLevelUp = false;
        belt.stgN += 1; // 패턴레벨업
        belt.count = 0; //패턴순서 처음으로
        Board.time += 10; //시간업
        a = beltMove._speed + (-1.0f);
        while (sp.transform.position.x >= -2.0f)
        {
            beltMove._speed = -7.0f;
            yield return new WaitForSeconds(0.01f);
        }
        beltMove._speed = a;
        isLevelUp = true;
    }
    void OnColiisionEnter2D(Collision2D coll)
    {
        this.transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame

    
    void Update () {
        if (Time.time > nextTime)
        {
            Board.comboswitch = false;
            Board.combo=0;
        }

        if (sp.transform.position.x >= 3.0f&&isLevelUp==true)
        {
            LevelUpAction();
        }
        else if(sp.transform.position.x<= -9.7)
        {
            reset();
        }

        if(Board.time <= 0)
        {
            reset();
        }
    }


}
