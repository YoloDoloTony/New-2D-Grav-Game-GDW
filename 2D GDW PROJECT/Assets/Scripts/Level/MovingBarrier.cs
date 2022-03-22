using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarrier : MonoBehaviour
{
    public Transform customPivotRight;
    public Transform customPivotLeft;

    public PlayerController gravity;
    public float rotationSpeed;

    bool isVertical;
    bool isRight;

    // Update is called once per frame
    void Update()
    {
        //rotate = true;

        isVertical = GameObject.Find("Player").GetComponent<PlayerController>().GetIsVertical();
        isRight = GameObject.Find("Player").GetComponent<PlayerController>().GetIsRight();


        if (isVertical && isRight)
        {
            transform.RotateAround(customPivotRight.transform.position, -Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        if (isVertical && !isRight)
        {
            transform.RotateAround(customPivotLeft.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        //caavin gole made this no public cooler
    }
}
