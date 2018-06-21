using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Questionnaires;

public class highlightButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver()
	{
		GetComponent<TextMesh>().color = new Color(0, 1, 0);
		GetComponent<TextMesh>().fontSize = 18;
	}

	void OnMouseExit()
	{
		GetComponent<TextMesh>().color = new Color(1, 1, 1);
		GetComponent<TextMesh>().fontSize = 0;
	}
}
