using UnityEngine;

public interface ISpaceInvaders 
{
	void HandleHit( GameObject object1 , GameObject object2 );
	void GameOver( int score );
}
