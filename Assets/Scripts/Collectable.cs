using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TS
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] Animator anim;
        [SerializeField] GameObject vfx;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Trajectory>())
            {

                anim.SetTrigger("isCollected");
                GameObject newVFX = Instantiate(vfx, transform.position, transform.rotation);
                Destroy(gameObject, 2f);
                Destroy(newVFX, 2f);
            }
        }
    }
}
