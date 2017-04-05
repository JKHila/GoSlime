using UnityEngine;
using System.Collections;

public class ButtonDowned : MonoBehaviour {
    public SlimeMove slime;
    public GameObject[] btn;
    public belt bt;
    private int pressedBtn;
    private SpriteRenderer sr;
    public BoxCollider2D temp;

    public void LockAction() { StartCoroutine(Lock()); }

    IEnumerator Lock()
    {
        temp = btn[0].GetComponent<BoxCollider2D>();
        temp.size = new Vector2(0, 0);
        temp = btn[1].GetComponent<BoxCollider2D>();
        temp.size = new Vector2(0, 0);
        temp = btn[2].GetComponent<BoxCollider2D>();
        temp.size = new Vector2(0, 0);
        temp = btn[3].GetComponent<BoxCollider2D>();
        temp.size = new Vector2(0, 0);
        beltMove._speed = -3.0f;
        yield return new WaitForSeconds(3f);
        beltMove._speed = -1.0f;
        temp = btn[0].GetComponent<BoxCollider2D>();
        temp.size = new Vector2(1, 1);
        temp = btn[1].GetComponent<BoxCollider2D>();
        temp.size = new Vector2(1, 1);
        temp = btn[2].GetComponent<BoxCollider2D>();
        temp.size = new Vector2(1, 1);
        temp = btn[3].GetComponent<BoxCollider2D>();
        temp.size = new Vector2(1, 1);

    }

    // Use this for initialization
    void Start () {
        LockAction();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale==0)
        {
            btn[0].GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
            btn[1].GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
            btn[2].GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
            btn[3].GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
        }
       

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                string tmp = hit.transform.gameObject.name;
                Color press = new Color(150, 150, 150);

                switch (tmp)
                {
                    case "Button_Red": pressedBtn = 0; sr = btn[0].GetComponent<SpriteRenderer>(); sr.color = new Color(150, 150, 150,255);  break;
                    case "Button_Green": pressedBtn = 1; sr = btn[1].GetComponent<SpriteRenderer>(); sr.color = new Color(150, 150, 150, 255); break;
                    case "Button_Blue":pressedBtn = 2; sr = btn[2].GetComponent<SpriteRenderer>(); sr.color = new Color(150, 150, 150, 255); break;
                    case "Button_Yellow": pressedBtn = 3; sr = btn[3].GetComponent<SpriteRenderer>(); sr.color = new Color(150, 150, 150, 255); break;
                }
                int curBeltNum = bt.getNextBeltNum();

                if (pressedBtn == curBeltNum && tmp.Contains("Button") && SlimeMove.feverChk == false)
                {
                    
                    slime.jump(bt.getNextBeltObj());
                    bt.correct();
                }
                else if (SlimeMove.feverChk == true)
                {
                    slime.jump(bt.getNextBeltObj());
                    bt.correct();
                 
                }
                else if(pressedBtn != curBeltNum && !tmp.Contains("Button") && SlimeMove.feverChk == false)
                {

                }
                else if (pressedBtn != curBeltNum && tmp.Contains("Button") && SlimeMove.feverChk == false)
                {
                    slime.fail();
                }
                  
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Color up = new Color(255, 255, 255);
            foreach (GameObject tp in btn)
            {
               // tp.GetComponent<SpriteRenderer>().color = up;
            }
            
        }
    }
}
