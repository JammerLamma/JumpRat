using UnityEngine;
using System.Collections;

namespace TS
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform tosser;
        public Transform boundryRight;
        public Transform boundryLeft;
        [SerializeField]private float minYLock;

        void Update()
        {

            Vector3 newPos = transform.position;
            //newPos.y = Mathf.Clamp(tosser.position.y +1f, minYLock,  tosser.position.y + 20f);
            newPos.y = tosser.position.y;
            newPos.x = Mathf.Clamp(newPos.x, boundryLeft.position.x, boundryRight.position.x);
            transform.position = newPos;
        }

        public void NewYLock(float value)
        {
            if (minYLock < value)
            {
                minYLock = value +1f;
            }
        }
    }
}
