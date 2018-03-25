using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamGame
{
    public class SpringBounce : MonoBehaviour
    {

        [SerializeField] bool m_Grounded;
        public Rigidbody2D rb;
        [SerializeField] float dropSpeed = 0;
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        [SerializeField] private LayerMask m_WhatIsGround;
        public float timeFromGrounded = 0;



        // Use this for initialization
        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        private void Awake()
        {
            m_GroundCheck = transform.Find("GroundCheck");
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            m_Grounded = false;
            dropSpeed = rb.velocity.y;
            GroundChecker();




        }

        private void GroundChecker()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
                else m_Grounded = false;
            }

            if (m_Grounded)
            {
                timeFromGrounded = 0;
            }
            if (!m_Grounded)
            {
                timeFromGrounded++;
            }
        }
    }
}
