using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Questionnaires;

public class NextButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

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

		for (int i = 0; i < 10; i++)
		{
			Answers [i].GetComponent<TextMesh> ().color = new Color (1, 1, 1);
			Answers [i].GetComponent<TextMesh> ().fontSize = 0;
			Answers [i].tag = "unselected";
		}

		//Questionnaire quest = new Questionnaire("test.json");

		int numberOfQuestion = GlobalQuestion.quest.Size ();

		GameObject Question = GameObject.Find("Question");

		TextMesh QuestionText = Question.GetComponent<TextMesh> ();
		TextMesh NextButton = GameObject.Find ("Next").GetComponent<TextMesh> ();
		TextMesh BackButton = GameObject.Find ("Back").GetComponent<TextMesh> ();

		if (NextButton.text == "Done")
		{
			Debug.Log ("About to export");
			// Change the 2nd argument on directory to export the Answer
			GlobalQuestion.quest.exportAnswers ("randomPerson", "../");
			Debug.Log ("export done");

			Application.Quit ();
		}


		if (GlobalQuestion.currentAnswer != -1) {
			GlobalQuestion.quest.setAnswer ("randomPerson", GlobalQuestion.currentQuestionNumber, 
				GlobalQuestion.quest.getItem (GlobalQuestion.currentQuestionNumber).getLabels () [GlobalQuestion.currentAnswer]);

			// Example output: "Question 2: Strongly Disagree"
			Debug.Log ("Question " + (GlobalQuestion.currentQuestionNumber + 1) + ": " +
				GlobalQuestion.quest.getItem (GlobalQuestion.currentQuestionNumber).getLabels () [GlobalQuestion.currentAnswer]);
		} 
		else 
		{
			GlobalQuestion.quest.setAnswer ("randomPerson", GlobalQuestion.currentQuestionNumber, 
				"No Answer");

			Debug.Log ("question " + (GlobalQuestion.currentQuestionNumber + 1) + ": " + "No Answer");
		}
			

		if (GlobalQuestion.currentQuestionNumber < (numberOfQuestion - 1)) {
			string checkQuestion = GlobalQuestion.quest.getItem (GlobalQuestion.currentQuestionNumber += 1).getDesc ();
			QuestionText.text = checkQuestion;

			int answerCount = GlobalQuestion.quest.getItem(GlobalQuestion.currentQuestionNumber).getLabels().Count;
			for (int i = 0; i < 10; i++) 
			{
				if (i < answerCount) {
					Answers [i].GetComponent<TextMesh> ().text = GlobalQuestion.quest.getItem (GlobalQuestion.currentQuestionNumber).getLabels () [i];
				} else
					Answers [i].GetComponent<TextMesh> ().text = "";
			}

			BackButton.text = "Back";
		} 
		else 
		{
			NextButton.text = "Done";
		}
			

		GlobalQuestion.currentAnswer = -1;
	}

	void OnMouseExit()
	{

	}
}