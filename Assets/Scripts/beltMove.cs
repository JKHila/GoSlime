using UnityEngine;
using System.Collections;

public class beltMove : MonoBehaviour {

    private float _speed = -4.0f;
    public GameObject _EnemySetObj;
    // Use this for initialization
    void Start()
    {
        _EnemySetObj.transform.localPosition += new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
		if (this.transform.position.x <= -15.0f) {
			Destroy (this.gameObject); //belt삭제
		}
        transform.Translate(_speed * Time.deltaTime, 0, 0);

    }
}
