using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator door;

    private void OnCollisionEnter2D(Collision2D col)
    {
     if(col.gameObject.tag == "character") {
         door.SetBool("DoorOpen", true);
     }
    }

}
