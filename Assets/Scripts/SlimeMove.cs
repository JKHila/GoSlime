using UnityEngine;
using System.Collections;

public class SlimeMove : MonoBehaviour {
    private GameObject nextObj;

    public bool isJumping;
    public float speed;
    public GameObject sp;
    public void jump(GameObject nextObj) { this.nextObj = nextObj; StartCoroutine(jumpAction()); }
    public void fail() { StartCoroutine(failAction()); }
    IEnumerator jumpAction()
    {
        isJumping = true;
        //transform.Translate(new Vector2(nextObj.transform.position.x-3.0f,transform.position.y+1));
        transform.position= new Vector2(nextObj.transform.position.x-0.4f, transform.position.y);
        yield return new WaitForSeconds(0.1f);
        isJumping = false;
    }
    IEnumerator failAction()
    {
        int z = 30;
        while (z<360)
        {
            sp.transform.rotation = Quaternion.Euler(0, 0, z);
            z += 30;
            yield return new WaitForSeconds(0.01f);
        }
        sp.transform.rotation = Quaternion.Euler(0, 0, 0);
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
	
	}
}
