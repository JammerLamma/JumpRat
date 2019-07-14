using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TS
{
    public class Trajectory : MonoBehaviour
    {

        [SerializeField] GameObject clickerPos;
        public Sprite dotSprite;                                          
        public float initialDotSize;                      
        public int numberOfDots;                          
        public float dotSeparation;                        
        public float dotShift;                              
        private GameObject trajectoryDots;                  
        private GameObject ball;                          
        private Rigidbody2D ballRB;                           
        private Vector3 ballPos;                       
        private Vector3 fingerPos;                           
        private Vector3 ballFingerDiff;                             
        private Vector2 shotForce;                          
        private float x1, y1;                                    
        private bool ballIsClicked = false;                  
        private bool ballIsClicked2 = false;                      
        [SerializeField]private GameObject ballClick;
        public float shootingPowerX;
        public float shootingPowerY;
        public GameObject[] dots;                 
        private BoxCollider2D[] dotColliders;

        private Vector3 preBallFingerDiff;

        private SQPlayer sQPlayer;

        void Start()
        {
            sQPlayer = GetComponent<SQPlayer>();
            ball = gameObject;                                                 
            trajectoryDots = GameObject.Find("Trajectory Dots");                    
            ballRB = GetComponent<Rigidbody2D>();                            

            trajectoryDots.transform.localScale = new Vector3(initialDotSize, initialDotSize, trajectoryDots.transform.localScale.z);      

            for (int k = 0; k < 40; k++)
            {
                dots[k] = GameObject.Find("Dot (" + k + ")");                      
                if (dotSprite != null)
                {                                     
                    dots[k].GetComponent<SpriteRenderer>().sprite = dotSprite;        
                }
            }
            for (int k = numberOfDots; k < 40; k++)
            {                               
                GameObject.Find("Dot (" + k + ")").SetActive(false);       
            }
            trajectoryDots.SetActive(false);                                  


        }




        void Update()
        {

            if (numberOfDots > 40)
            {
                numberOfDots = 40;
            }

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);               

            if (hit.collider != null && ballIsClicked2 == false)
            {                                                         
                if (hit.collider.gameObject.name == ballClick.gameObject.name)
                {                   
                    ballIsClicked = true;                                                  
                }
                else
                {                                                                         
                    ballIsClicked = false;                                              
                }
            }
            else
            {                                                                          
                ballIsClicked = false;                                                 
            }

            if (ballIsClicked2 == true)
            {                                               
                ballIsClicked = true;                                                   
            }


            if ((ballRB.velocity.x * ballRB.velocity.x) + (ballRB.velocity.y * ballRB.velocity.y) <= 0.0085f)
            {       
                ballRB.velocity = new Vector2(0f, 0f);                                  
            }
            else
            {                                                                      
                trajectoryDots.SetActive(false);                                                           
            }


            ballPos = ball.transform.position;                                              

            if ((Input.GetKey(KeyCode.Mouse0) && ballIsClicked == true) && ((ballRB.velocity.x == 0f && ballRB.velocity.y == 0f)))
            {        										   
                ballIsClicked2 = true;                                                   

                fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);          
                fingerPos.z = 0;                                                          

                preBallFingerDiff = clickerPos.transform.position - fingerPos;
                ballFingerDiff.x = Mathf.Clamp(preBallFingerDiff.x, -1f, 1f);
                ballFingerDiff.y = Mathf.Clamp(preBallFingerDiff.y, -1.5f, 1.5f);

                shotForce = new Vector2(ballFingerDiff.x * shootingPowerX, ballFingerDiff.y * shootingPowerY);        

                if ((Mathf.Sqrt((ballFingerDiff.x * ballFingerDiff.x) + (ballFingerDiff.y * ballFingerDiff.y)) > (0.4f)))
                {            
                    trajectoryDots.SetActive(true);                               
                }
                else
                {
                    trajectoryDots.SetActive(false);                                   
                }

                for (int k = 0; k < numberOfDots; k++)
                {                                    
                    x1 = ballPos.x + shotForce.x * Time.fixedDeltaTime * (dotSeparation * k + dotShift);          
                    y1 = ballPos.y + shotForce.y * Time.fixedDeltaTime * (dotSeparation * k + dotShift) - (-Physics2D.gravity.y / 2f * Time.fixedDeltaTime * Time.fixedDeltaTime * (dotSeparation * k + dotShift) * (dotSeparation * k + dotShift));          
                    dots[k].transform.position = new Vector3(x1, y1, dots[k].transform.position.z);      
                }
            }


            if (Input.GetKeyUp(KeyCode.Mouse0))
            {                                   

                ballIsClicked2 = false;


            if (trajectoryDots.activeInHierarchy)
                {                               
                    trajectoryDots.SetActive(false);                                   
                    ballRB.velocity = new Vector2(shotForce.x, shotForce.y);          
                    if (ballRB.isKinematic == true)
                    {                               
                        ballRB.isKinematic = false;                                
                    }
                }
            }
        }


        public void collided(GameObject dot)
        {

            for (int k = 0; k < numberOfDots; k++)
            {
                if (dot.name == "Dot (" + k + ")")
                {

                    for (int i = k + 1; i < numberOfDots; i++)
                    {

                        dots[i].gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }

                }

            }
        }
        public void uncollided(GameObject dot)
        {
            for (int k = 0; k < numberOfDots; k++)
            {
                if (dot.name == "Dot (" + k + ")")
                {

                    for (int i = k - 1; i > 0; i--)
                    {

                        if (dots[i].gameObject.GetComponent<SpriteRenderer>().enabled == false)
                        {
                            Debug.Log("nigggssss");
                            return;
                        }
                    }

                    if (dots[k].gameObject.GetComponent<SpriteRenderer>().enabled == false)
                    {
                        for (int i = k; i > 0; i--)
                        {

                            dots[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;

                        }

                    }
                }

            }
        }
    }
}
