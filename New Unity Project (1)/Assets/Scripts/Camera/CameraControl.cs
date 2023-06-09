using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.2f;

    public Transform m_target;

    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;

    public float m_Size;


    private void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        m_DesiredPosition = m_target.position;
        transform.position = Vector3.SmoothDamp(transform.position,
       m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get scroll value
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //Check if scroll value is not 0{
        if(scroll != 0)
        {
            Camera.main.orthographicSize -= scroll * 2;
        }
        //modify the orthographic size to the scroll value}


    }
}
