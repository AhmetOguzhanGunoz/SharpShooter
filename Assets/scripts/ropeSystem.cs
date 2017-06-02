using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ropeSystem : MonoBehaviour {

    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;

    public LayerMask mask;

    public float distance = 10f;

    public LineRenderer line;

    bool swing = false;

    public Animator anim;

	// Use this for initialization
	void Start () 
    {
        joint = GetComponent<DistanceJoint2D>();

        joint.enabled = false;
        line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (CrossPlatformInputManager.GetButtonDown("CatchButton"))
        {
            
            Touch touch = Input.touches[0];

            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {


                targetPos = Camera.main.ScreenToWorldPoint(touch.position);
                targetPos.z = 0;


                hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);


                if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {

                    joint.enabled = true;

                    joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();

                    joint.distance = 2f;

                    line.enabled = true;

                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, hit.point);

                    if (!swing)
                    {
                        anim.SetBool("swing", true);
                        swing = true;
                    }


                }
            }

        }

        if (CrossPlatformInputManager.GetButtonUp("CatchButton"))
        {
            anim.SetBool("swing", false);
            joint.enabled = false;
            line.enabled = false;
            swing = false;
        }

        if (CrossPlatformInputManager.GetButton("CatchButton"))
        {

            line.SetPosition(0, transform.position);

        }
	
	}
}
