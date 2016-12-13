using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;
public class SpotTestHandler : MonoBehaviour {

	/*Private Variables*/
	private JsonData jsonvale;
	private int mNumberOfObjects;
	private int mcurrentQ = 0;
	private string mQid;
	private string mAns1;
	private string mAns2;
	private string mAns3;
	private string mAns4;
	private string mSetAnswer;

	/*Public Variables*/
	public Text mQuestion;
	public Text mAnswers;
	// Use this for initialization
	/*void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/

	void OnEnable()
	{
		EventManagerBase.OnSpotTestLoaded += LoadSpotTest;
	}

	void OnDisable()
	{
		EventManagerBase.OnSpotTestLoaded -= LoadSpotTest;
	}

	/*Call IENumerator method to load the spot test*/
	private void LoadSpotTest()
	{
		StartCoroutine (Load ());
	}

	/*Get spot test details from server*/
	private IEnumerator Load()
	{
		WWW www = new WWW (AppCommon.mCommonUrl + "get/getlquestion?sid=" + AppCommon.mInstanceID);
		yield return www;

		if(www.error == null)
		{
			if (Processjson (www.text)) 
			{
				LoadQuestion ();
			}
		}
	}

	/*Convert string to jsonobject*/
	private bool Processjson(string jsonString)
	{
		jsonvale = JsonMapper.ToObject(jsonString);
		mNumberOfObjects = jsonvale.Count;

		return true;
	}

	/*Get the questions and display with mcq answers*/
	public void LoadQuestion()
	{
		mQid = jsonvale[mcurrentQ]["qid"].ToString ();
		mQuestion.text = jsonvale[mcurrentQ]["question"].ToString ();
		mQuestion.text = mQuestion.text.Replace("<p>","");
		mQuestion.text = mQuestion.text.Replace("</p>","");

		mAns1 = jsonvale[mcurrentQ]["answer1"].ToString ();
		mAns2 = jsonvale[mcurrentQ]["answer2"].ToString ();
		mAns3 = jsonvale[mcurrentQ]["answer3"].ToString ();
		mAns4 = jsonvale[mcurrentQ]["answer4"].ToString ();

		mAnswers.text = "1. " + mAns1 + "\n2. " + mAns2 + "\n3. " + mAns3 + "\n4. " + mAns4+"";
	}

	/*Go to next question*/
	public void NextQuestion()
	{
		StartCoroutine (SubmitTheQ ());


		if (mNumberOfObjects  > mcurrentQ) 
		{
			mcurrentQ++;
			LoadQuestion ();
		} 
		else 
		{
			gameObject.GetComponent<ScreenManager> ().SetScreen ("Lecture");
		}
	}

	/*Set the answers to variables*/
	public void SetAnswer(int val)
	{
		switch (val) 
		{
		case 0:
			mSetAnswer = mAns1;
			break;
		case 1:
			mSetAnswer = mAns2;
			break;
		case 2:
			mSetAnswer = mAns3;
			break;
		case 3:
			mSetAnswer = mAns4;
			break;
		default:
			mSetAnswer = mAns1;
			break;
			
		}
	}

	/*Submit the answer by user to the server*/
	private IEnumerator SubmitTheQ()
	{
		WWWForm form = new WWWForm ();
		form.AddField ("answer",mSetAnswer);
		form.AddField ("qid",mQid);
		form.AddField ("s_id",AppCommon.mStudentID);
		string url = AppCommon.mCommonUrl + "add/submitanswer";
		WWW www = new WWW (url,form);
		yield return www;

		if (www.error == null) 
		{
			mAns1 = null;
			mAns2 = null;
			mAns3 = null;
			mAns4 = null;
		}
	}
}
