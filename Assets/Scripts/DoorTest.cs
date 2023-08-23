using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    public float DoorUpAmount;
    private bool PlayerCloseEnough;
    public GameObject text;
    private Vector3 MoveTo;
    private Vector3 CurrentPos;
    public float MoveSpeed;
    private bool IsMoving;
    public Transform PlayerTransform;
    public float TextDis;
    private bool TextEnabled = true;
    public float DoorCloseTime;

    void Update()
    {
        if(Vector3.Distance(PlayerTransform.position, transform.position) < TextDis && TextEnabled)
        {
            text.SetActive(true);
            PlayerCloseEnough = true;
        }
        else
        {
            text.SetActive(false);    
            PlayerCloseEnough = false;  
        }


        if(PlayerCloseEnough && Input.GetKeyDown(KeyCode.E))
        {    
            StartCoroutine(DoorMove(DoorCloseTime));
        }
    }

    void FixedUpdate()
    {
        if(IsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, MoveTo, MoveSpeed * Time.deltaTime);
        }
        if(transform.position == MoveTo)
        {
            IsMoving = false;
        }
    }

    IEnumerator DoorMove(float WaitTime)
    {
        MoveTo = new Vector3(transform.position.x, transform.position.y + DoorUpAmount, 0);
        IsMoving = true;
        TextEnabled = false;
        yield return new WaitForSeconds(WaitTime);
        MoveTo = new Vector3(transform.position.x, transform.position.y - DoorUpAmount, 0);
        IsMoving = true;
        TextEnabled = true;
    }

}
