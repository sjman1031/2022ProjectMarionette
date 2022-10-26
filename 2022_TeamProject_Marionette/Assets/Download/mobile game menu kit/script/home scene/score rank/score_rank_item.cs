using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class score_rank_item : MonoBehaviour {

	public TextMeshProUGUI rank_number_text;
	public TextMeshProUGUI name_text;
	public TextMeshProUGUI score_text;

	public void Update_me(int rank, string name, int score)
	{
		rank_number_text.text = rank.ToString();
		name_text.text = name;
		score_text.text = score.ToString("N0");
	}
}
