using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bullets;
    [SerializeField] private float shootCoolDown = 0.5f;
    [SerializeField] private float shootTimer;
    [SerializeField] private float moveSpeed = 55f;
    

    private void Update()
    {
        Vector2 inputVector = new Vector2(0,0);

        if(Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        if(Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }

        transform.position += (Vector3)inputVector * moveSpeed * Time.deltaTime;

        shootTimer += Time.deltaTime;

        if (shootTimer > shootCoolDown && Input.GetKeyDown(KeyCode.Space))
        {
            shootTimer = 0f;
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation, bullets);
    }
}
