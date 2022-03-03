using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	[SerializeField] private TextMeshPro textScore;

	public int Score
	{
		get => _score;
		set
		{
			_score = value;
			textScore.SetText(_score.ToString());
		}
	}

	private int _score;

	private void Awake()
	{
		Instance = this;
	}
}