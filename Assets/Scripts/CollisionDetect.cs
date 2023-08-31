using UnityEngine;

public class CollisionDetect : MonoBehaviour 
{
	void OnTriggerEnter( Collider other ) 
	{
		if (GameManager.Instance != null) 
		{
			GameManager.Instance.HandleHit( this.gameObject , other.gameObject );
		}
	}
}
