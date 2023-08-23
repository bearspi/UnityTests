using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Thanks Nade from https://www.youtube.com/@NadeOnRust for this code


    private void FixedUpdate()
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            difference.Normalize();

            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //After this is written by me
            if(Mathf.Abs(rotationZ) < 70f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
            }
            else if(rotationZ > 0f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 70f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -70f);
            }

        }
}
