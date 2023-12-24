using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI instance = null;
	public TextMeshProUGUI scoretext;
	int score;
	private void Start()
	{
		score = 0;
		if (instance == null)
			instance = this;
	}
	// Update is called once per frame
	public int GetScore(int score)
	{
		return this.score += score;
	}
	void Update()
    {
		scoretext.text = "È¹µæÁ¡¼ö : " + this.score.ToString();
	}
}
