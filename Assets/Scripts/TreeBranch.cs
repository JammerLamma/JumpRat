using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TS
{
    public class TreeBranch : MonoBehaviour
    {

        [SerializeField] public EdgeCollider2D edgeCollid;

        
        public void DestoryTreeBranch()
        {
            Destroy(gameObject);
        }

        public IEnumerator DisableBranch()
        {
            yield return new WaitForSeconds(1f);
            edgeCollid.enabled = true;
        }
    }
}