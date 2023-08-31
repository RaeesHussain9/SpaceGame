using UnityEngine;

// --------------------------------------------------------------------------------------------------------
// 
// Base class for Space Invaders game.
//
// Please derive from this class to implement your game.
// 
// --------------------------------------------------------------------------------------------------------

public abstract class BaseSpaceInvaders : MonoSingleton<BaseSpaceInvaders>, ISpaceInvaders
{

	public GameObject resultsScreen;
	protected override void Awake()
	{
		base.Awake();
	}

	void Start()
	{

	}

	public abstract void HandleHit( GameObject object1 , GameObject object2 );

	public abstract void GameOver( int score );

}
