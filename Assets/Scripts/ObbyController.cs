using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObbyController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpHeight = 5f;
    public Transform cameraTransform;

    public GameObject player;
    private bool isGrounded;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        // �������� �� �����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // �������� ����������� �� ������
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // ������� ������� �� ��� Y
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // �������� ������������ ������
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            // ������� � ����������� ��������
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);

            // ���������� ���� ��� ��������
            rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);
        }

        // ������
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), rb.velocity.z);
        }
    }
}
