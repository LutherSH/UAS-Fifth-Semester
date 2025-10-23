using Unity.VisualScripting;
using UnityEngine;

public class ArrowBehave : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public int damageAmount = 10;
    private bool damaged;

    void Start()
    {
        Destroy(gameObject, lifeTime);
        damaged = false;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        {
            if (other.collider.CompareTag("Player") && !damaged)
            {
                damaged = true;
                PlayerBehaviour player = other.collider.GetComponent<PlayerBehaviour>();
                if (player != null)
                {
                    player.PlayerTakeDmg(damageAmount);
                    Debug.Log("Player took " + damageAmount + " damage. Current health: " +
                    GameManager.gameManager._playerHealth.Health + "/" +
                    GameManager.gameManager._playerHealth.MaxHealth);
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }
}