using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
	public Text resultName;
	public Text resultScore;

	public void SetScore( string name, int score )
	{
		resultName.GetComponent<Text>().text = name;
		resultScore.GetComponent<Text>().text = score.ToString();
	}
}
