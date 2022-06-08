using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Push.Adj;

namespace Push.ChControl
{
    public class ChController : MonoBehaviour
    {
        DynamicJoystick dJ;
        Rigidbody rb;
        float x, z;
        public float speed;
        [SerializeField] float clampedVel;
        Vector2 joystickdeger;
        AddForce adforc;

        Vector2 dashValue;
        bool dashed;
        float startClampedvel;
        [SerializeField] float dashForce;
        [SerializeField] float pushvelocity;
        public int Score;

        float timeElapsed;
        [SerializeField] float lerpDuration;
        [SerializeField] float valueToLerp;



        private void Start()
        {
            dashed = false;
            startClampedvel = clampedVel;
            adforc = GetComponent<AddForce>();
            dJ = FindObjectOfType<DynamicJoystick>();
            rb = GetComponent<Rigidbody>();

        }


        private void Update()
        {


            if (Input.GetMouseButtonUp(0))
                StartCoroutine(dash(lerpDuration));

            x = dJ.Horizontal;
            z = dJ.Vertical;

            joystickdeger = new Vector2(x, z).normalized;

            if (clampedVel > startClampedvel && !dashed)
            {
                velStabilizer(pushvelocity, startClampedvel);

            }
            else
            {
                timeElapsed = 0;
            }

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, clampedVel);


        }
        private void FixedUpdate()
        {
            move();
        }

        void move()
        {
            rb.AddForce(new Vector3(joystickdeger.x * speed, 0, joystickdeger.y * speed), ForceMode.Acceleration);


            if (joystickdeger != Vector2.zero)
                dashValue = joystickdeger;
        }

        void velStabilizer(float start, float end)
        {
            if (timeElapsed < lerpDuration)
            {
                timeElapsed += Time.deltaTime;
                valueToLerp = Mathf.Lerp(start, startClampedvel, timeElapsed / lerpDuration);
                clampedVel = valueToLerp;
            }
        }

        IEnumerator dash(float lerptime)
        {
            Score = adforc.Size_Score;
            if (Score > 25)
            {
                float temp = pushvelocity;
                clampedVel = pushvelocity;
                rb.AddForce(new Vector3(dashValue.x * dashForce, 0, dashValue.y * dashForce), ForceMode.Force);
                dashed = true;
                Score -= (Score * 25) / 100;
                adforc.Size_Score = Score;
                adforc.texter = true;
                Adjustment.adjEvent.scoretext();
                yield return new WaitForSeconds(lerpDuration);
                adforc.texter = false;
                float timer = 0;
                dashed = false;
                dashValue = Vector2.zero;
            }


        }
    }
}