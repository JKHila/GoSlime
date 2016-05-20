using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

    public float _speed;
    public GameObject _EnemySetObj;
    // Use this for initialization
    void Start()
    {
        _EnemySetObj.transform.localPosition += new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(_speed * Time.deltaTime, 0, 0);

    }
}
