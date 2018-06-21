using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;


namespace Questionnaires
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Item
	{
		[JsonProperty(PropertyName = "Item_Description")]
		private string desc;            //Item description (Question, Statement, etc)
		[JsonProperty(PropertyName = "Item_Point_Scale")]
		private int pointScale;         //Item point-scale
		[JsonProperty(PropertyName = "Item_Labels")]
		private List<string> labels;    //Item point-scale labels (Answers)

		public Item()
		{ }

		/**
         * Value constructor for Item object type.
         * Input: string desc, int pointScale, List<string> labels\n
         * Output: None\n
         * Result: Initialize new Item object
         */
		public Item(string desc, int pointScale, List<string> labels)
		{
			this.desc = desc;
			this.pointScale = pointScale;
			this.labels = labels;
		}

		//Getters
		/**
         * Accessor that returns the Item description.
         * Input: None\n
         * Output: string desc\n
         * Result: returns string value desc
         */
		public string getDesc()
		{
			return desc;
		}

		/**
         * Accessor that returns the Item point scale.   
         * Input: None\n
         * Output: int pointScale\n
         * Result: returns int value pointScale
         */
		public int getPointScale()
		{
			return pointScale;
		}

		/**
         * Accessor that returns the Item labels.
         * Note: The label list is copied as an IList (readonly) before being returned, meaning the labels list cannot be modified directly, but data can still be read\n
         * \n
         * Examples:\n
         * \n
         * This will work...\n
         * TestItem.getLabels()[1]; // this returns the label at index 1\n
         * \n
         * This will not work!\n
         * TestItem.getLabels().Add("This will throw an error!");\n
         * \n
         * Input: None\n
         * Output: IList<string> readOnly\n
         * Result: returns readOnly copy of string list labels
         */
		public IList<string> getLabels()
		{
			IList<string> readOnly = labels.AsReadOnly();
			return readOnly;
		}

		//Setters
		/**
         * Mutator that allows you to set the Item description.
         * Input: string desc\n
         * Output: None\n
         * Result: Item desc value changed to specfied input desc
         */
		public void setDesc(string desc)
		{
			this.desc = desc;
		}

		/**
         * Mutator that allows you to set the Item point scale.
         * Input: int pointScale\n
         * Output: None\n
         * Result: Item pointScale value changed to specified input pointScale
         */
		public void setPointScale(int pointScale)
		{
			this.pointScale = pointScale;
		}

		/**
         * Mutator that allows you to set the Item labels.
         * Input: string list labels\n
         * Output: None\n
         * Result: Item labels value changed to specified input labels
         */
		public void setLabels(List<string> labels)
		{
			this.labels = labels;
		}


		//Additional functions

		/**
         * Add label to the label list.
         * Inserts a new string label at the end of the list of labels\n
         * \n
         * Input: string label\n
         * Output: None\n
         * Result: new string label inserted at the end of string list labels
         */
		public void addLabel(string label)
		{
			labels.Add(label);
		}

		/**
        * Delete label from the label list.
        * Deletes the string label specified, if the label exists in the list\n
        * \n
        * Input: string label\n
        * Output: None\n
        * Result: Specified label removed from list if exists, otherwise silently fails
        */
		public void deleteLabel(string label)
		{
			labels.Remove(label);
		}

		/**
        * Delete label from the label list at specified index.
        * Deletes the string label specified, if the label exists in the list\n
        * \n
        * Input: int index\n
        * Output: None\n
        * Result: Specified label removed from list if exists, note that if the index is out of range, an exception will occur
        */
		public void deleteLabel(int index)
		{
			labels.RemoveAt(index);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class Questionnaire
	{       
		[JsonProperty(PropertyName = "Questionnaire_Name")]
		private string name;
		[JsonProperty(PropertyName = "Questionnaire_Items")]
		private List<Item> items = new List<Item>();
		// A dictionary that holds the name of the people and their answers
		private Dictionary<string, List<string>> answerList = new Dictionary<string, List<string>>();


		//Bare constructor
		public Questionnaire()
		{ }

		/**
        * Value constructor for Item object type.
        * This constructor can be used if you have an existing Item list to insert into the Questionnaire object\n
        * \n
        * Input: string name, List<Item> items\n
        * Output: None\n
        * Result: Initialize new Questionnaire object
        */
		public Questionnaire(string name, List<Item> items)
		{
			this.name = name;
			this.items = items;
		}


		/**
        * Reads from input .json file, Special constructor for Questionnaire objects.
        * This constructor is designed to build a Questionnaire object
        * from serialized Json data. It takes a string filename and reads
        * in all data found within the file as a string. Assuming the
        * data contained in the file is serialized Json data matching the
        * Questionnaire stucture, then a new Questionnaire object will be
        * initialized containing the data from the input file\n
        * \n
        * Input: string file\n
        * Output: None\n
        * Result: Initialize new Questionnaire object
        */
		public Questionnaire(string file) 
		{
			string readIn = File.ReadAllText(file);

			Questionnaire q = JsonConvert.DeserializeObject<Questionnaire>(readIn);

			this.name = q.name;
			this.items = q.items;
		}


		//Getters
		/**
        * Accessor that returns the Questionnaire name.
        * Input: None\n
        * Output: string name\n
        * Result: returns string value name
        */
		public string getName()
		{
			return name;
		}

		/**
        * Accessor that returns the Questionnaire ItemList.
        * Note: The Item list is copied to a readOnly variable and returned as such.
        * \n
        * Examples:\n
        * \n
        * This will work...\n
        * TestQuestionnaire.getItemList()[1].getDesc(); // this returns the description of the item at index 1\n
        * \n
        * This will throw an error...\n
        * TestQuestionnaire.getItemList().Add(TestItem);\n
        * \n
        * Input: None\n
        * Output: IList<Item> readOnly\n
        * Result: returns IList<Item> copy of object list value items
        */
		public IList<Item> getItemList()
		{
			IList<Item> readOnly = items.AsReadOnly();
			return readOnly;
		}

		/**
        * Accessor that returns a Questionnaire Item.
        * Note: Similarly to getItemList(), the Item object is copied before being returned, which prevents the Item from being modified via this function\n
        * \n
        * Examples:\n
        * \n
        * This will work...\n
        * TestQuestionnaire.getItem(1).getDesc(); // this returns the description of the item at index 1\n
        * \n
        * This won't work...\n
        * TestQuestionnaire.getItem(0).addLabel("This won't throw an error, but you won't actually be adding a label to that item!");\n
        * \n
        * Input: int index\n
        * Output: Item readOnly\n
        * Result: returns copy of Item Object at specified index
        */
		public Item getItem(int index)
		{
			Item readOnly = new Item(items[index].getDesc(), items[index].getPointScale(), new List<string>(items[index].getLabels()));
			return readOnly;
		}

		//Setters
		/**
        * Mutator that allows you to set the Questionnaire name.
        * Input: string name\n
        * Output: None\n
        * Result: Questionnaire name value changed to specified input name
        */
		public void setName(string name)
		{
			this.name = name;
		}


		//Additional functions

		/**
        * Add Item to the Item list.
        * Input: Item object\n
        * Output: None\n
        * Result: Specified item added to Questionnaire instance Item list
        */
		public void addItem(Item item)
		{
			items.Add(item);
		}

		/**
        * Delete specified Item from the Item list via it's description.
        * Input: string itemDesc\n
        * Output: None\n
        * Result: Specified item removed from Questionnaire instance Item list
        */
		public void deleteItem(string itemDesc)
		{
			for (int i = 0; i < items.Count; i++)
				if (items[i].getDesc() == itemDesc)
					items.RemoveAt(i);
		}

		/**
        * Delete specified Item from the Item list at specified index.
        * Input: int index\n
        * Output: None\n
        * Result: Specified item removed from Questionnaire instance Item list, note that if the index is out of range, an exception will occur
        */
		public void deleteItem(int index)
		{
			items.RemoveAt(index);
		}

		/**
        * Accessor that returns the number of items contained in the Questionnaire.
        * Input: None\n
        * Output: int size\n
        * Result: size of Item List returned
        */
		public int Size()
		{
			return items.Count;
		}

		//Getters
		/**
        * Accessor that returns the list of answer of a particular person.
        * Input: name of a person\n
        * Output: List<string>\n
        * Result: returns list of string (of the answers to all question).
        */
		public List<string> getAnswerList(string name)
		{
			return answerList[name];
		}

		//Getters
		/**
        * Accessor that returns the answer of a question of a person.
        * Input: name of a person, int index\n
        * Output: string name\n
        * Result: returns string (of the answer to a provided question).
        */
		public string getAnswerNumber(string name, int index)
		{
			return answerList[name][index];
		}

		//Setters
		/**
        * Mutator that allows you to set the Questionnaire name.
        * Input: string name, int index, string answer\n
        * Output: None\n
        * Result: Add answer to the corresponding question
        */
		public void setAnswer(string name, int index, string answer)
		{
			if (answerList.ContainsKey (name)) 
			{
				answerList [name] [index] = answer;
			}
			else
			{
				List<string> tmpList = new List<string> (items.Count);
				for (int i = 0; i < items.Count; i++) 
				{
					tmpList.Add ("No Answer");
				}
				tmpList [index] = answer;
				answerList.Add (name, tmpList);
			}
		}

		//Serialize object to Json format
		/**
         * Convert a Questionnaire object into a Json string.
         * This function converts the calling Questionnaire object to
         * a Json string and returns the string value. Json string is
         * formatted using indentation for readability sake\n
         * \n
         * Input: None\n
         * Output: string JsonFormattedObject\n
         * Result: returns string value of serialized Questionnaire object
         */
		public string toJson()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}


		//Output the answers to csv
		/** 
         * Outputs the users answers list to {Current_Time}_{Questionnaire_Name}_{User_Name}.csv, if the user exists.
         * This function outputs a csv file matching the following format...\n
         * \n
         * Item_Number, Item, Answer\n
         * \n
         * Input: string userName, OPTIONAL: string output_directory\n
         * Output: None\n
         * Result: csv file created if user is found, otherwise outputs error message\n
         * \n
         * Note: the output_directory string is merely appended to the front of the output file, so failing to close 
         * off the string with a trailing slash ("/") will append additional text to the filename\n
         * \n
         * Examples:\n
         * //Assume the current Questionnaire object is named TestQuestionnaire and it is 12:00\n
		* exportAnswers("John Doe", "../../"); //This will output 12.00_TestQuestionnaire_John Doe.csv two directories above the current working directory\n
		* exportAnswers("Me", "../..");  //This will output ..12.00_TestQuestionnaire_Me.csv one directory above the current working directory\n
		* exportAnswers("You", "C:/Users/You/Desktop/"); //This will output 12.00_TestQuestionnaire_You.csv to C:/Users/You/Desktop/\n
		* exportAnswers("YouToo", "C:/Users/You/Desktop"); //This will output Desktop12.00_TestQuestionnaire_YouToo.csv to C:/Users/YouToo/
		*/
		public void exportAnswers(string userName, string output_directory = "")
		{
			if (answerList.ContainsKey(userName))
			{
				var csv = new StringBuilder();

				for (int i = 0; i < items.Count; i++)
				{
					string entry = string.Format("{0},{1},{2}", i + 1, items[i].getDesc(), answerList[userName][i]);
					csv.AppendLine(entry);
				}

				File.WriteAllText(string.Format(output_directory + "{0}_{1}_{2}.csv", DateTime.Now.ToString("hh.mm.ss"), this.name, userName), csv.ToString());
			}
			else
				Console.Error.WriteLine("Error! UserName does not exist in " + this.name + " answerList!\nNo file written...");
		}

	}
}