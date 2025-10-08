using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;


public class BallMovement : MonoBehaviour
{
    private Rigidbody ball;
    public float moveForce = 0.00002f;
    // Start is called before the first frame update

    public LeapProvider leapProvider;

    private void OnEnable()
    {
        leapProvider.OnUpdateFrame += OnUpdateFrame;
    }
    private void OnDisable()
    {
        leapProvider.OnUpdateFrame -= OnUpdateFrame;
    }
    void Start()
    {
        ball = GetComponent<Rigidbody>();
    }

    void OnUpdateFrame(Frame frame)
    {
        foreach (Hand _hand in frame.Hands)
        {
            if (_hand.IsLeft)
            {
                Debug.Log("Left Hand");
                Finger indexFinger = _hand.fingers[1];
                Finger thumbFinger = _hand.fingers[0];


                Vector3 indexLeap = indexFinger.TipPosition;
                Vector3 thumbLeap = thumbFinger.TipPosition;


                Vector3 direction = indexLeap - transform.position;
                Vector3 direction_normalized = direction.normalized * moveForce;

                Vector3 force = new Vector3(direction_normalized.x, 0, direction_normalized.z);

                Debug.DrawLine(transform.position, indexLeap, Color.red);

                float distance = Vector3.Distance(indexLeap, transform.position);
                Vector3 pinchStartPosition = new Vector3();
                bool isPinching = false;

                // If the distance is small enough, we're in a pinch
                if (_hand.PinchStrength > 0.85 && distance < 10) // Adjust this threshold as needed
                {
                    if (!(_hand.IsPinching())) // Start of pinch
                    {
                        isPinching = true;
                        pinchStartPosition = transform.position - indexLeap; // Remember the initial offset
                    }

                    // Move the ball with the hand
                    Vector3 targetPosition = indexLeap + pinchStartPosition;
                    ball.transform.position = targetPosition;
                }
                else
                {
                    isPinching = false; // No longer pinching, stop following the hand
                }
            }





            if (_hand.IsRight)
            {

                Debug.Log("Left Hand");
                Finger indexFinger = _hand.fingers[1];

                Vector3 indexLeap = indexFinger.TipPosition;

                Vector3 direction = indexLeap - transform.position;
                Vector3 direction_normalized = direction.normalized * moveForce;

                Vector3 force = new Vector3(direction_normalized.x, 0, direction_normalized.z);

                Debug.DrawLine(transform.position, indexLeap, Color.red);

                ball.AddForce(force);

            }
        }
    }


    // void OnUpdateFrame(Frame frame)
    // {
    //     foreach (Hand _hand in frame.Hands)
    //     {
    //         if (_hand.IsLeft)
    //         {
    //             Debug.Log("Left Hand");
    //             Finger indexFinger = _hand.fingers[1];

    //             Vector3 indexLeap = indexFinger.TipPosition;

    //             Vector3 direction = indexLeap - transform.position;
    //             Vector3 direction_normalized = direction.normalized * moveForce;

    //             Vector3 force = new Vector3(direction_normalized.x, 0, direction_normalized.z);

    //             Debug.DrawLine(transform.position, indexLeap, Color.red);

    //             ball.AddForce(force);
    //         }
    //     }
    // }



    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 force = new Vector3(moveX, 0, moveY) * 2;

        Hand _specificPhysicsHand = Hands.Provider.GetHand(Chirality.Left);
        List<Hand> _allPhysicsHands = Hands.Provider.CurrentFixedFrame.Hands;


        ball.AddForce(force);
        
    }
}
