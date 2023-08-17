using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoney : Bullet
{


    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Character"))
            return;
        base.OnCollisionEnter(collision);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
            return;
        base.OnTriggerEnter(other);

        if (other.CompareTag("Activable"))
        {
            other.GetComponent<IActivable>().GetHit();
        }
    }

}
