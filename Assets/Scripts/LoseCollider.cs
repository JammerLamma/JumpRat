using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TS
{
    public class LoseCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Trajectory>())
            {


            }
        }
    }
}
