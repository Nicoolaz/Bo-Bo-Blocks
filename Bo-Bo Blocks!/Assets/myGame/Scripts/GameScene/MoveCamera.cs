using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
    public float distance;//相机距离目标物体默认距离大小  

    public float xSpeed = 250.0f;//X（左右旋转速度）  


    float X = 0.0f;
    float Y = 0.0f;
    float StartDis;

    float XChange;//旋转灵敏度  

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        distance = Vector3.Distance(transform.position, Vector3.zero);
        X = angles.y;
        Y = angles.x;
    }

    void Update()
    {
        if (Input.touchCount >0)
        {
            X += Input.GetTouch(0).deltaPosition.x * xSpeed * Time.deltaTime / 5;//+和-决定从左向右旋转还是从右向左转  
            //Y += Input.GetTouch(0).deltaPosition.y * ySpeed * Time.deltaTime / 60;  //+和-决定从上向下旋转还是从下而上转  
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (Input.GetTouch(0).deltaPosition.x > 5)
                {
                    XChange = 5;
                }
                else if (Input.GetTouch(0).deltaPosition.x < -5)
                {
                    XChange = -5;
                }
                else
                {
                    XChange = Input.GetTouch(0).deltaPosition.x;
                }
            }
        }

        X += XChange;
        if (XChange > 0)
        {
            XChange -= Time.deltaTime * 5;
        }
        else if (XChange < 0)
        {
            XChange += Time.deltaTime * 5;
        }


    }

    void LateUpdate()
    {

        transform.rotation = Quaternion.Euler(Y, X, 0);
        transform.position = transform.rotation * new Vector3(0, 0, -distance) + Vector3.zero;
    }
}
