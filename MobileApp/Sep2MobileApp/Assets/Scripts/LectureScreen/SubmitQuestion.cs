using UnityEngine;
using System.Collections;
using MaterialUI;
using UnityEngine.UI;
public class SubmitQuestion : MonoBehaviour {

	public Text mQuestion;
	public Text AskQDisplayMessage;
	public GameObject mQuestionDialogBox;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*Pop open input box*/
	public void AskQuestion()
	{
		mQuestionDialogBox.SetActive (true);
	}

	/*Validate question and call submitthequestion method*/
	public void SubmitStudentQuestion()
	{
		if (mQuestion.text != "") 
		{
			StartCoroutine (SubmitTheQuestion ());
		} else 
		{
			AskQDisplayMessage.text = "Please Enter a question below. You cannot leave it empty on submit";
		}
	}

	/*Close inputbox on click on x*/
	public void CloseQuestionDialog()
	{
		mQuestionDialogBox.SetActive (false);
	}

	/*Post the question to server*/
	private IEnumerator SubmitTheQuestion()
	{
		//TODO complete method with url
		WWWForm form = new WWWForm ();
		form.AddField ("question",mQuestion.text);
		form.AddField ("sid",AppCommon.mInstanceID);
		form.AddField ("s_id",AppCommon.mStudentID);
		string url = AppCommon.mCommonUrl + "add/addquestion";
		WWW www = new WWW (url,form);
		yield return www;

		if (www.error == null) {
			AskQDisplayMessage.text = "Submited";
			yield return new WaitForSeconds (0.5f);
			CloseQuestionDialog ();
		} 
		else 
		{
			AskQDisplayMessage.text = "An error occured while submitting the question. Please try again in a short time";
		}
	}
}
