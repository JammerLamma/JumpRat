using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TS
{
    public class CatStart : MonoBehaviour
    {
        [SerializeField] GameObject catPrefab;

        void Start()
        {
            Instantiate(catPrefab, transform.position, transform.rotation);
        }

    }
}