using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJamStarterKit.Sidescroller
{
    public class PlayerMovement : MonoBehaviour
    {

        public float speed = 5;
        public string horizontalInput = "Horizontal", verticalInput = "Vertical";

        public MovementType movementType = MovementType.translate;

        Rigidbody2D rig;

        private int dir = 1, l_dir;


        #region jump
        BoxCollider2D collider; // our collider
        const float skinWidth = .015f;

        [Range(3, 15)]
        public int rayCount = 5; // how many rays will we work with

        float raySpacing;// minimum space between rays

        float rayLength = 0.05f; // how long are our rays

        RaycastOrigins raycastOrigins; //conors of the collider

        bool canJump;

        public float jumpforce = 200;

        public LayerMask whatIsGround;

        #endregion



        public enum MovementType
        {
            force,
            translate
        };

        // Use this for initialization
        void Start()
        {
            collider = GetComponent<BoxCollider2D>();
            rig = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

            Move();
            Jump();
            Flip();

        }


        void Move()
        {
            float x = Input.GetAxisRaw("Horizontal");

            int move = 0;

            if (x != 0)
            {
                dir = Mathf.FloorToInt(x);
                move = 1;
            }
            else
            {
                dir = move = 0;
            }


            switch (movementType)
            {
                case MovementType.force:
                    rig.AddForce(Vector2.right * speed * move * Time.deltaTime);
                    break;
                case MovementType.translate:
                    transform.Translate(Vector2.right * speed * move * Time.deltaTime);
                    break;
            }



        }

        #region jump
        public void Jump()
        {
            UpdateRaycastOrigins();
            CalculateRaySpacing();

            for (int i = 0; i < rayCount; i++)
            {
                Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * raySpacing * i, Vector2.up * rayLength, Color.red);

                Vector2 rayOrigin = raycastOrigins.bottomLeft;
                rayOrigin += Vector2.right * raySpacing * i;
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, rayLength, whatIsGround);

                if (hit)
                {
                    canJump = true;
                }
            }


            if (canJump && Input.GetButton("Jump"))
            {
                canJump = false;
                rig.AddForce(transform.up * jumpforce);
            }
        }

        void UpdateRaycastOrigins()
        {
            Bounds bounds = collider.bounds;
            bounds.Expand(skinWidth * -2);

            raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
            raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
            raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        }

        void CalculateRaySpacing()
        {
            Bounds bounds = collider.bounds;
            bounds.Expand(skinWidth * -2);

            rayCount = Mathf.Clamp(rayCount, 2, int.MaxValue);

            raySpacing = bounds.size.x / (rayCount - 1);
        }

        struct RaycastOrigins
        {
            public Vector2 topLeft, topRight;
            public Vector2 bottomLeft, bottomRight;
        }

        #endregion

        public void Flip()
        {

            if (dir != l_dir)
            {
                if (dir == -1)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }

                if (dir == 1)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }

            l_dir = dir;

        }
    }
}

