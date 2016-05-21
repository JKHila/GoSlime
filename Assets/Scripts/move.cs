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
		if (this.transform.position.x <= -10.0f) {
			Destroy (this.gameObject); //belt삭제
		}
        transform.Translate(_speed * Time.deltaTime, 0, 0);

    }
}
