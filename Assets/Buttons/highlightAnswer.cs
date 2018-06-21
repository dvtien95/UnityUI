using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Questionnaires;

public class highlightAnswer : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver()
	{
		GetComponent<TextMesh>().color = new Color(0, 0, 1);
		GetComponent<TextMesh>().fontSize = 15;
	}

	void OnMouseDown()
	{
		GameObject Answer1 = GameObject.Find ("Answer1");
		GameObject Answer2 = GameObject.Find ("Answer2");
		GameObject Answer3 = GameObject.Find ("Answer3");
		GameObject Answer4 = GameObject.Find ("Answer4");
		GameObject Answer5 = GameObject.Find ("Answer5");
		GameObject Answer6 = GameObject.Find ("Answer6");
		GameObject Answer7 = GameObject.Find ("Answer7");
		GameObject Answer8 = GameObject.Find ("Answer8");
		GameObject Answer9 = GameObject.Find ("Answer9");
		GameObject Answer10 = GameObject.Find ("Answer10");
		GameObject[] Answers = new GameObject[]{ Answer1, Answer2, Answer3, Answer4, Answer5,
			Answer6, Answer7, Answer8, Answer9, Answer10 };

		int answerCount = GlobalQuestion.quest.getItem(GlobalQuestion.currentQuestionNumber).getLabels().Count;

		for (int i = 0; i < 10; i++) 
		{
			if (Answers [i] != gameObject) {
				Answers [i].GetComponent<TextMesh> ().color = new Color (1, 1, 1);
				Answers [i].GetComponent<TextMesh> ().fontSize = 0;
				Answers [i].tag = "unselected";
			} 
			else if (i < answerCount)
			{
				gameObject.tag = "selected";
				GlobalQuestion.currentAnswer = i;
			}
		}
	}

	void OnMouseExit()
	{
		if (gameObject.tag == "unselected" || gameObject.tag == "Untagged") {
			GetComponent<TextMesh> ().color = new Color (1, 1, 1);
			GetComponent<TextMesh> ().fontSize = 0;
		}
	}

}