using UnityEngine;

public class Enemies : MonoBehaviour
{
    private const float ROW_SPACING = 10f;
    private const float COL_SPACING = 15f;
    private const float MAX_BASE_SPEED = 300f;
    
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyBullets;
    [SerializeField] private Transform enemies;
    [SerializeField] private float moveSpeed;
    [SerializeField] private static float baseSpeed = 40f;
    [SerializeField] private float speedIncrement = 60f;
    [SerializeField] private int rows;
    [SerializeField] private int cols;
    private Vector3 moveDirection = Vector3.right;
    private float flipOffset = 8f;
    public float totalNumEnemies => (float)rows * (float)cols;
    public float currentNumEnemies => this.transform.childCount;
    private float percentAlive => currentNumEnemies / totalNumEnemies;
    private float percentDead => 1 - percentAlive;
    private float shootStartTime = 2f;
    private float shootRate = 1f;

    private void Awake() 
    {
        rows = Random.Range(1,5);
        cols = Random.Range(1,7);
        for (int i = 0; i < this.rows; i++)
        {
            float width = 2f * (cols -1);
            float height = 2f * (rows -1);
            Vector2 centerPos = new Vector2(-width/2, -height/2);
            Vector3 rowPos = new Vector3(centerPos.x , centerPos.y + (i * ROW_SPACING), 0f);
            for (int j = 0; j < this.cols; j++)
            {
                Vector3 zPos = new Vector3(0f ,2f, -6f);
                GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation * Quaternion.Euler (90f, 0f, -180f), enemies);
                Vector3 pos  = rowPos;
                pos.x += j * COL_SPACING;
                enemy.transform.localPosition = pos;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating( nameof( EnemyShoot ), shootStartTime, shootRate );
    }

    private void Update()
    {
        transform.Translate( moveDirection * moveSpeed * Time.deltaTime, Space.World );

        Vector3 leftCameraEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightCameraEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach ( Transform enemy in this.transform )
        {
            if ( moveDirection == Vector3.right && enemy.position.x >= ( rightCameraEdge.x - flipOffset ) )
            {
                FlipAndDescend();

            } else if ( moveDirection == Vector3.left && enemy.position.x <= (leftCameraEdge.x + flipOffset ))
            {
                FlipAndDescend();
            }
        }

        IncreaseEnemySpeed();
    }

    private void EnemyShoot() 
    {   
        if (currentNumEnemies == 0)
        {
            CancelInvoke();
        } 
        else 
        {
            int randomIndex = ( int )Random.Range( 0, currentNumEnemies - 1 );
            
            Transform enemy2 = this.transform.GetChild( randomIndex );
            
            Instantiate( enemyBulletPrefab, enemy2.transform.position, transform.rotation * Quaternion.Euler (270f, 180f, -180f), enemyBullets );
        }
        
    }

    private void FlipAndDescend()
    {
        moveDirection.x *= -1f;
        transform.Translate( Vector3.down * 5f, Space.World );
    }

    private void IncreaseEnemySpeed()
    {
        moveSpeed = percentDead *  speedIncrement + baseSpeed;
    }

    public static void IncreaseBaseSpeedPerWave() 
    {
        if (baseSpeed < MAX_BASE_SPEED)
        {
            baseSpeed *= 1.50f;
        }
    }
}
