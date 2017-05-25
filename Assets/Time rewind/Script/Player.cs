using UnityEngine;
using System.Collections;

namespace Juto.Rewind
{
    public class Player : MonoBehaviour
    {

        void Update()
        {

            //Basic movement
            if (!TimeRewind.IsReversing)
            {
                transform.Translate(Vector3.forward * 3.0f * Time.deltaTime * Input.GetAxis("Vertical"));
                transform.Rotate(Vector3.up * 200.0f * Time.deltaTime * Input.GetAxis("Horizontal"));
            }


            //If we move, disable
            if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
            {
                TimeRewind.SetReversing(false);
            }


            //Enable/disable
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TimeRewind.ToggleReversing();
            }
        }
    }
}
