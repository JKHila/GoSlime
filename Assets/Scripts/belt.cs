using UnityEngine;
using System.Collections;
using System.Timers;

public class belt : MonoBehaviour {
    public GameObject[] _Belt;
    public GameObject n;
    public Transform _PlayerObjPool;
    // Use this for initialization
    void Start()
    {

 
    /*    _Belt[0].transform.localPosition += new Vector3(0, 0, 0);
        _Belt[1].transform.localPosition += new Vector3(0, 0, 0);
        _Belt[2].transform.localPosition += new Vector3(0, 0, 0);
        _Belt[3].transform.localPosition += new Vector3(0, 0, 0);*/


    }

    // Update is called once per frame
    public bool _SpawnChk = true;
    Vector3 a = new Vector3(10f, -2.49f, 0);
    void Update()
    {
       
        GameObject k = m(a);
        _SpawnChk = false;
        if (!_SpawnChk && k.transform.position.x <= 7.98f)
        {
            _SpawnChk = true;
        }

    }
    GameObject m(Vector3 a)
    {
        var Set1 = Instantiate(_Belt[Random.Range(0, 4)], a, Quaternion.identity) as GameObject;
        Set1.transform.parent = _PlayerObjPool;
        Set1.transform.localScale = new Vector3(1, 1, 1);
        Set1.transform.localPosition = a;
        return Set1;
    }
}
