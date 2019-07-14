using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TS
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rb;
        [SerializeField] SpriteRenderer rend;
        [SerializeField] private Vector3 startPos, endPos, midPos;
        [SerializeField] private Vector3[] positions = new Vector3[20];
        private int numOfPoints = 20;
        [SerializeField]private int current = 0;
        private float WPradius = 0.1f;
        private float speed = 1f;

        [SerializeField] GameObject targetBranch;
        [SerializeField] GameObject sqPlayer;

        [SerializeField]private float timeTillMove;
        [SerializeField] bool canMove;

        [SerializeField]private GameObject sqTarget;
        public GameObject _SQTarget
        {
            get { return sqTarget; }
            set { sqTarget = value; }
        }

        private void Start()
        {
            sqPlayer = FindObjectOfType<SQPlayer>().gameObject;
            rb = GetComponent<Rigidbody2D>();
            rend = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {

            if (!canMove && Vector3.Distance(sqTarget.transform.position, transform.position) > 1f)
            {
                PlayerChase();
            }


            if (canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, positions[current], Time.deltaTime * speed);

                if (transform.position == positions[current])
                {
                    current++;

                    if (current >= positions.Length - 1)
                    {
                        timeTillMove = 0;
                        canMove = false;

                    }
                }
            }
        }


        private void PlayerChase()
        {
            if (Vector3.Distance(sqPlayer.transform.position, transform.position) > 3f && !canMove)
            {
                DrawQuadraticCurve(sqTarget);
                    timeTillMove = 0;
                canMove = true;
            }
            else
            {
                if (timeTillMove < 1)
                {
                    canMove = false;
                    timeTillMove += Time.deltaTime * 1;
                }
                else
                {
                    DrawQuadraticCurve(sqTarget);
                    canMove = true;
                }
            }
        }

        private Vector3 CalculateQuadraticBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;
            return p;
        }

        private void DrawQuadraticCurve(GameObject target)
        {
            current = 0;
            Debug.Log("Draw curve");
            startPos = transform.position;
            midPos.y = GetMidPoint(target).y;
            midPos.x = GetMidPoint(target).x;
            endPos = target.transform.position;
            for (int i = 1; i < numOfPoints + 1; i++)
            {
                float t = i / (float)numOfPoints;
                positions[i - 1] = CalculateQuadraticBezier(t, startPos, midPos, endPos);
            }
        }

        private Vector3 GetMidPoint(GameObject target)
        {
            float midX = (transform.position.x + target.transform.position.x) / 2;
            float midY = target.transform.position.y + 1f;

            return new Vector3(midX, midY, 0);



        }
    }
}
