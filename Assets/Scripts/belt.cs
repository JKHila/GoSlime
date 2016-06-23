using UnityEngine;
using System.Collections;
using System.Timers;
class beltAttribute
{   
    private int num;
    private GameObject bt;
    public beltAttribute(int n,GameObject obj)
    {
        num = n;
        bt = obj;
    }
    public int getNum()
    {
        return num;
    }
    public GameObject getObj()
    {
        return bt;
    }
}
public class belt : MonoBehaviour {
    private Queue beltList = new Queue();
    private beltAttribute[] bta = new beltAttribute[5];
    private beltAttribute curBelt;
    private beltAttribute nextBelt;
    private bool isStart = false;

    public GameObject player;
    public GameObject[] _Belt;
    public GameObject n;
    public Transform _PlayerObjPool;
    // Use this for initialization
    public void correct()
    {
        curBelt = nextBelt;
        nextBelt = beltList.Dequeue() as beltAttribute;
    }
    public int getNextBeltNum()
    {
        return nextBelt.getNum();
    }
    public GameObject getNextBeltObj()
    {
        return nextBelt.getObj();
    }
    public int getCurBeltNum()
    {
        return curBelt.getNum();
    }
    public GameObject getCurBeltObj()
    {
        return curBelt.getObj();
    }
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            bta[i] = new beltAttribute(i, _Belt[i]);
        }
		StartCoroutine ("spawnBelt"); //코루틴 시작

    }

    // Update is called once per frame
    public bool _SpawnChk = true;
    Vector3 position = new Vector3(15f, -2.49f, 0);
    void Update()
    {
        if (isStart && !player.GetComponent<SlimeMove>().isJumping && player.transform.position.x > curBelt.getObj().transform.position.x)
        {
            player.transform.position = new Vector2(curBelt.getObj().transform.position.x, player.transform.position.y);
        }
    }
    /*GameObject m(Vector3 a)
    {
        var Set1 = Instantiate(_Belt[Random.Range(0, 4)], a, Quaternion.identity) as GameObject;
        Set1.transform.parent = _PlayerObjPool;
        Set1.transform.localScale = new Vector3(1, 1, 1);
        Set1.transform.localPosition = a;
        return Set1;
    }*/
	IEnumerator spawnBelt(){
        var start = Instantiate(bta[4].getObj(), position, Quaternion.identity) as GameObject;
        start.transform.parent = _PlayerObjPool;
        beltAttribute tpbt = new beltAttribute(bta[4].getNum(), start);
        curBelt = tpbt;
        yield return new WaitUntil(() => start.transform.position.x < 11.0f);
        isStart = true;
        while (true) {
            int r = Random.Range(0, 4);
            var set1 = Instantiate (bta[r].getObj(), position, Quaternion.identity) as GameObject;
			set1.transform.parent = _PlayerObjPool;
            tpbt = new beltAttribute(bta[r].getNum(), set1);
            beltList.Enqueue(tpbt);
            Debug.Log(beltList.Peek());
            if(nextBelt == null)
            {
                nextBelt = beltList.Dequeue() as beltAttribute;
            }
            yield return new WaitUntil(() => set1.transform.position.x < 11.0f); 
		}
	}
}
