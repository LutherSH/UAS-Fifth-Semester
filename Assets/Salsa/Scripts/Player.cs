using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    public float moveSpeed = 5f; // Kecepatan gerakan
    public float gravity = -9.81f; // Nilai gravitasi
    private Vector3 velocity; // Kecepatan saat ini

    void Start()
    {
        // Ambil komponen Character Controller saat permainan dimulai
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Periksa apakah pemain berada di tanah
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Pastikan pemain tetap di tanah
        }

        // Dapatkan input dari tombol keyboard (WASD atau panah)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Hitung arah gerakan
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        // Gerakkan pemain berdasarkan input dan kecepatan
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Terapkan gravitasi
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}