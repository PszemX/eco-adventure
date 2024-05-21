using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public CharacterController characterController;
    public GameObject player;
    public bool isFacingRight;


    private void Update()
    {
        isFacingRight = characterController.isFacingRight;
    }
    private void FixedUpdate()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();
        float rotationZ = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) + 80f;
        if (isFacingRight)
        {
            if (rotationZ >= 0 && rotationZ <= 180) transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
        else
        {
            if ((rotationZ > 180 && rotationZ < 270) || (rotationZ < 0 && rotationZ > -100)) transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
    }
}
