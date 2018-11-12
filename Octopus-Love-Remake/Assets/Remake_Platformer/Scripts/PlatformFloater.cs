//randomly move the platform in x position like a ping pong
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFloater : MonoBehaviour {

    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private Vector3 endPosition;
    private float speed = 1.0F;

    void Start()
    {
        speed = Random.Range(0.5f, 2.0f);
        startPosition = this.transform.position;
        endPosition = new Vector3(Random.Range(0 , 6.0f), this.transform.position.y, this.transform.position.z);
    }

    private void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1.0f);
        this.transform.position = Vector3.Lerp(startPosition, endPosition, pingPong);
    }
}
