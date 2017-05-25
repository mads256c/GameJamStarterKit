using UnityEngine;
using System.Collections;

namespace Juto.Rewind
{
    public class SimpleAI : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            //Move the object around
            if (!TimeRewind.IsReversing)
            {
                transform.Translate(Vector3.forward * 3.0f * Time.deltaTime * 1);
                transform.Rotate(Vector3.up * 200.0f * Time.deltaTime * Random.Range(-1, 1));
            }
        }
    }
}

