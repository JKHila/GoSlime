using UnityEngine;
using System.Collections;

public class ButtonDowned : MonoBehaviour {
    public SlimeMove slime;
    public GameObject[] btn;
    public belt bt;
    private int pressedBtn;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
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

                if (pressedBtn == curBeltNum)
                {
                    slime.jump(bt.getNextBeltObj());
                    bt.correct();
                }
                else
                    slime.fail();
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
