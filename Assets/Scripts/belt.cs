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
    private beltAttribute checkBelt;
    private bool isStart = false;

    public GameObject player;
    public GameObject[] _Belt;

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
    
    void Update()
    {
        if (isStart && !player.GetComponent<SlimeMove>().isJumping && player.transform.position.x > curBelt.getObj().transform.position.x) //시작o & 점프x & 슬라임위치가 현재벨트보다 더 앞에있을때
        {
            player.transform.position = new Vector2(curBelt.getObj().transform.position.x+0.1f, player.transform.position.y); //플레이어의 위치를 현재벨트에 맞춤
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
    Vector3 position = new Vector3(9.5f, -2.52f, 0);
    private int[,] stage = new int[,] { 
        { 0, 1, 2, 3, 0, 1, 3, 2, 0, 2, 1, 3, 0, 2, 3, 1, 0, 3, 2, 1, 0, 3, 1, 2 },
        { 0, 2, 2, 0, 0, 1, 1, 0, 0, 3, 3, 0, 2, 0, 0, 2, 2, 1, 1, 2, 2, 3, 3, 2 }
    };
    public static int stgN = 0;
    public static int count = 0;
    IEnumerator spawnBelt(){
        var start = Instantiate(bta[4].getObj(), position, Quaternion.identity) as GameObject;
        start.transform.parent = _PlayerObjPool;//클론을 벨트안에 넣어 보기 편하게
        beltAttribute tpbt = new beltAttribute(bta[4].getNum(), start); // 검은타일 저장
        curBelt = tpbt; // 설정
        yield return new WaitUntil(() => start.transform.position.x <= 8.589f); // 타일길이맞춤
        isStart = true;//시작
        while (true) {
            var set1 = Instantiate (bta[stage[stgN, count]].getObj(), position, Quaternion.identity) as GameObject; //패턴에서 패널을뽑아 초기위치에 배치하고 회전은 없음
			set1.transform.parent = _PlayerObjPool;// 마찬가지로 벨트안에 넣어 보기 편하게
            tpbt = new beltAttribute(bta[stage[stgN, count]].getNum(), set1); // 뽑은타일 저장
            beltList.Enqueue(tpbt); // 타일을 큐에 넣음
           // Debug.Log(beltList.Peek());
            if (nextBelt == null) // 다음벨트가없으면
            {
                nextBelt = beltList.Dequeue() as beltAttribute; //큐에 저장된 타일을 다음타일로 설정
            }

            count++;
            if (count == 24)
            {
                count = 0;
            }
            yield return new WaitUntil(() => set1.transform.position.x <= 8.589f); //타일길이맞춤
		}
	}
}
