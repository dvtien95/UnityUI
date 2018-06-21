using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Questionnaires;

public class GlobalQuestion
{
	public static int currentQuestionNumber = 0;	// The curren question number is displayed
	public static int currentAnswer = -1;	// Hold value of current Answer for a particular question
	public static Questionnaire quest = new Questionnaire("test.json"); // Only need to initialize quest class once
}

public class LoadQuestion : MonoBehaviour {

	// Use this for initialization
	void Start () {


		InitializeText ();

	}

	// Update is called once per frame
	void Update () {

	}

	// 
	void InitializeText()	{
		//Questionnaire quest = new Questionnaire("test.json");

		GameObject Title = GameObject.Find("Title");
		GameObject Question = GameObject.Find("Question");

		TextMesh Answer1 = GameObject.Find ("Answer1").GetComponent<TextMesh> ();
		TextMesh Answer2 = GameObject.Find ("Answer2").GetComponent<TextMesh> ();
		TextMesh Answer3 = GameObject.Find ("Answer3").GetComponent<TextMesh> ();
		TextMesh Answer4 = GameObject.Find ("Answer4").GetComponent<TextMesh> ();
		TextMesh Answer5 = GameObject.Find ("Answer5").GetComponent<TextMesh> ();
		TextMesh Answer6 = GameObject.Find ("Answer6").GetComponent<TextMesh> ();
		TextMesh Answer7 = GameObject.Find ("Answer7").GetComponent<TextMesh> ();
		TextMesh Answer8 = GameObject.Find ("Answer8").GetComponent<TextMesh> ();
		TextMesh Answer9 = GameObject.Find ("Answer9").GetComponent<TextMesh> ();
		TextMesh Answer10 = GameObject.Find ("Answer10").GetComponent<TextMesh> ();
		TextMesh[] Answers = new TextMesh[]{ Answer1, Answer2, Answer3, Answer4, Answer5,
			Answer6, Answer7, Answer8, Answer9, Answer10};

		TextMesh TitleText = Title.GetComponent<TextMesh> ();
		TitleText.text = GlobalQuestion.quest.getName();

		TextMesh QuestionText = Question.GetComponent<TextMesh> ();

		try 
		{
			string checkQuestion = GlobalQuestion.quest.getItem (GlobalQuestion.currentQuestionNumber).getDesc ();
			QuestionText.text = checkQuestion;

			int typeOfQuestion = 0;
			int answerCount = GlobalQuestion.quest.getItem(GlobalQuestion.currentQuestionNumber).getLabels().Count;
			// This program works for 10 answers
			if (answerCount > 10)
				answerCount = 10;
			
			//Debug.Log(answerCount);

			switch (typeOfQuestion)
			{
			case 0:
				for (int i = 0; i < answerCount; i++)
				{
					Answers[i].text = GlobalQuestion.quest.getItem(GlobalQuestion.currentQuestionNumber).getLabels()[i];
				}
				break;
			case 1:
				Answers[0].text = "True";
				Answers[1].text = "False";
				break;
			case 2:
				break;
			default:
				break;
			}
		}
		catch (System.Exception e)
		{
			System.Console.WriteLine ("Exception caught", e);
			QuestionText.text = "No question available";
		}

	}

}