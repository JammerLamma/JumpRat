using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TS
{
    public class EnemyChildCollider : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.tag == "TreeBranch")
            {

                //gameObject.GetComponentInParent<EnemyTest>().PullTrigger(collision);
                //// Forward to the parent (or just deal with it here).
                //// Let's say it has a script called "PlayerCollisionHelper" on it:
                //PlayerCollisionHelper parentScript = transform.parent.GetComponent<PlayerCollisionHelper>();

                //// Let it know a collision happened:
                //parentScript.CollisionFromChild(hit);
                //Debug.Log("Land on Branch");
                //Debug.Log(collision);
            }

        }
    }
}
