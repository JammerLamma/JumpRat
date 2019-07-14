using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TS
{
    public class SQPlayer : MonoBehaviour
    {
        private GameObject go;
        private Animator anim;
        public GameObject treeBranch;
        private SpriteRenderer rend;
        [SerializeField]GameObject catGameObject;

        [SerializeField] CapsuleCollider2D capCollid;
        [SerializeField] public bool canJump;

       [SerializeField] Rigidbody2D rb;


        void Start()
        {
            capCollid = GetComponentInChildren<CapsuleCollider2D>();
            go = Camera.main.gameObject;
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            rend = GetComponent<SpriteRenderer>();

        }

        public void Respawn()
        {
            transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        }


        void Update()
        {

            if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("isLanded", false);

            }
            else
            {
                if (rb.velocity.x >= 0)
                {
                    rend.flipY = false;
                    transform.rotation = Quaternion.FromToRotation(Vector3.right, rb.velocity);
                    transform.eulerAngles -= new Vector3(0, 0, 0);
                }
                else if (rb.velocity.x <= 0)
                {
                    rend.flipY = true;
                    transform.rotation = Quaternion.FromToRotation(Vector3.right, rb.velocity);
                    transform.eulerAngles -= new Vector3(0, 0, 0);
                }
                else
                {
                    transform.rotation = Quaternion.FromToRotation(Vector3.right, rb.velocity);
                    transform.eulerAngles -= new Vector3(0, 0, 0);
                }

            }

        }

        public void PullTrigger(Collider2D c)
        {

                canJump = true;
                anim.SetBool("isLanded", true);
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                ////transform.position = collision.transform.position;
                ////animator.SetTrigger("Land");
                treeBranch = c.gameObject;
                go.GetComponent<CameraFollow>().NewYLock(treeBranch.transform.position.y);
                //body.constraints = RigidbodyConstraints2D.FreezePosition;
                //rb.velocity = Vector3.zero;
                transform.rotation = Quaternion.FromToRotation(transform.position, Vector3.up);
                capCollid.enabled = false;
               // catGameObject.GetComponent<EnemyMovement>().CycleTarget(treeBranch);
               catGameObject.GetComponent<EnemyMovement>()._SQTarget = treeBranch;
            //Debug.Log("Pill Trigger");

        }

        public void ColliderActive(bool active)
        {
            if (active)
            {
                capCollid.enabled = true;
            } else
            {
                capCollid.enabled = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject == treeBranch)
            {
                capCollid.enabled = true;
            }
        }

        public void Caught()
        {
            rb.isKinematic = true;
            Debug.Log("Caught!");
        }

        private void FaceMouse()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y
                );

            transform.right = -direction;
        }
    }

}