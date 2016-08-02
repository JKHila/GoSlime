using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlimeMove : MonoBehaviour {
    private GameObject nextObj;

    public bool isJumping;
    public float speed;
    public GameObject sp;
    public Sprite[] motion = new Sprite[10];
    public GameObject getSP()
    {
        return sp;
    }
    public void jump(GameObject nextObj) { this.nextObj = nextObj; StartCoroutine(jumpAction()); }
    public void fail() { StartCoroutine(failAction()); }
  
    IEnumerator jumpAction()
    {
        isJumping = true;
        //transform.Translate(new Vector2(nextObj.transform.position.x-3.0f,transform.position.y+1));
        for(int i=0;i<=9;i++)
        {
            sp.GetComponent<SpriteRenderer>().sprite = motion[i];
            if(i>2)
            {
                transform.position = new Vector2(nextObj.transform.position.x+0.1f, transform.position.y);
            }
            yield return new WaitForSeconds(0.02f);
        }
//        yield return new WaitForSeconds(0.01f);
        isJumping = false;
    }
    private int _hp = 100;
    public Image hpbar;
    IEnumerator failAction()
    {
        _hp -= 25;
        hpbar.fillAmount = _hp * 0.01f;
        int z = 30;
        while (z<360)
        {
            sp.transform.rotation = Quaternion.Euler(0, 0, z);
            z += 30;
            yield return new WaitForSeconds(0.01f);
        }
        sp.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (_hp == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage");
            NewBehaviourScript.time = 30;
            belt.stgN = 0; // 패턴초기화
            belt.count = 0; //패턴순서 처음으로
            beltMove._speed = -1.0f; // 속도 초기화
        }
    }
    private static float a;
    public bool isLevelUp= true;
    public void LevelUpAction() { StartCoroutine(LevelUp()); }
    IEnumerator LevelUp()
    {
        isLevelUp = false;
        belt.stgN += 1; // 패턴레벨업
        belt.count = 0; //패턴순서 처음으로
        NewBehaviourScript.time += 10; //시간업
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
        if (sp.transform.position.x >= 3.0f&&isLevelUp==true)
        {
            LevelUpAction();
        }
    }


}
