using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : BaseSpaceInvaders
{
		
	private const string ENEMY = "Enemy";
	private const string PLAYER = "Player";
	private const string BULLET = "Bullet";
	private const string ENEMY_BULLET = "EnemyBullet";
	private const string BULLET_NET = "BulletNet";
	private const string BUNKER = "Bunker";
	private const string SCORE_TEXT_PREFIX = "Score: ";
	private const string HIGHSCORE_TEXT_PREFIX = "Highest score: ";
	private const string PLAYER_PREFS_HIGHSCORE_KEY = "Highscore";

	[SerializeField] private GameObject enemies;
	private Result setResult;
	private Enemies enemiesScript;
	[SerializeField] private TMP_Text scoreText;
	[SerializeField]private GameObject getUserName;
    [SerializeField] private GameObject inputField;
	[SerializeField] private Text resultScoreText;
	[SerializeField] private Text nameText;
	[SerializeField] private Text highScoreText;
	private string userName;
	private static int score = 0;
	private int points = 1;
	private float totalEnemies;
	private int highScore => PlayerPrefs.GetInt(PLAYER_PREFS_HIGHSCORE_KEY,0);

	private void Start() 
	{
		enemiesScript = enemies.GetComponent<Enemies>();
		totalEnemies = enemiesScript.totalNumEnemies;
		scoreText.text = SCORE_TEXT_PREFIX + score;
	}


	public override void HandleHit( GameObject object1 , GameObject object2 )
	{
		if (object1.gameObject.tag == BULLET && object2.gameObject.tag == ENEMY)
		{	
			UpdateScore();
			Destroy(object1);
			Destroy(object2);
			totalEnemies -= 1;
			IfNoEnemies();
		} 
		else if (object1.gameObject.tag == BULLET && (object2.gameObject.tag == BUNKER || object2.gameObject.tag == BULLET_NET))
		{
			Destroy(object1);
		}
		else if (object1.gameObject.tag == ENEMY && object2.gameObject.tag == PLAYER)
		{
			Destroy(object2);
			GameOver(score);
		} 
		else if (object1.gameObject.tag == BULLET && object2.gameObject.tag == ENEMY_BULLET)
		{
			Destroy(object1);
			Destroy(object2);
		}
		else if (object1.gameObject.tag == ENEMY_BULLET && object2.gameObject.tag == PLAYER)
		{
			Destroy(object1);
			Destroy(object2);
			GameOver(score);
		}
		else if (object1.gameObject.tag == ENEMY_BULLET && (object2.gameObject.tag == BUNKER || object2.gameObject.tag == BULLET_NET))
		{
			Destroy(object1);
		}
		else if (object1.gameObject.tag == ENEMY && object2.gameObject.tag == BUNKER)
		{
			Destroy(object2);
		}
	
	}

	private void UpdateScore()
	{
		score += points;
		scoreText.text = SCORE_TEXT_PREFIX + score;
	}

	private void IfNoEnemies()
	{
		if (totalEnemies <= 0)
		{
			Enemies.IncreaseBaseSpeedPerWave();
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

    public override void GameOver(int score)
    {
        getUserName.SetActive( true );
    }

    

	public void StoreName()
    {
        userName = inputField.GetComponent<Text>().text;
		resultScoreText.GetComponent<Text>().text = score.ToString();
		nameText.GetComponent<Text>().text = userName;

		SetHighScore();
		highScoreText.GetComponent<Text>().text = HIGHSCORE_TEXT_PREFIX + highScore;

        getUserName.SetActive( false );
		resultsScreen.SetActive( true );
    }

	private void SetHighScore()
	{
  		if(score > highScore)
   		{
      		PlayerPrefs.SetInt(PLAYER_PREFS_HIGHSCORE_KEY, score);
			PlayerPrefs.Save();
		}
	}
	
}
