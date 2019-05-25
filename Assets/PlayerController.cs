using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float CharacterSpeed = 1;

    public float CharacterRotationSpeed = 1;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        Vector2 currentInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        bool hasInput = currentInput.sqrMagnitude > 0.0001f;

        animator.SetBool("isRunning", hasInput);

        if (!hasInput)
        {
            return;
        }


        Vector3 desiredForwardVector = new Vector3(currentInput.x, 0, currentInput.y);

        GetComponent<CharacterController>().SimpleMove(desiredForwardVector * CharacterSpeed);

        Vector3 desiredRotationDelta = Quaternion.FromToRotation(transform.forward, desiredForwardVector).eulerAngles;

        float desiredRotationDeltaYaw = desiredRotationDelta.y;

        Mathf.MoveTowardsAngle(transform.eulerAngles.y, Quaternion.FromToRotation(Vector3.forward, desiredForwardVector).eulerAngles.y, CharacterRotationSpeed * Time.deltaTime);


        transform.rotation =
            Quaternion.Euler(0, Mathf.MoveTowardsAngle(transform.eulerAngles.y, Quaternion.FromToRotation(Vector3.forward, desiredForwardVector).eulerAngles.y, CharacterRotationSpeed * Time.deltaTime), 0);
    }
}
