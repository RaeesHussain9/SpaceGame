using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 70f;

    private void Update() 
    {   
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime, Space.World);
    }
}
