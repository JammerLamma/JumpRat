using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TS
{
    public class ChildSQ : MonoBehaviour
    {

        public void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.tag == "TreeBranch")
            {

                gameObject.GetComponentInParent<SQPlayer>().PullTrigger(collision);
            }
            if (collision.gameObject.tag == "Bottom")
            {
                gameObject.GetComponentInParent<SQPlayer>().Respawn();
            }

        }

    }
}

