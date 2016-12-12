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

	public void AskQuestion()
	{
		mQuestionDialogBox.SetActive (true);
	}

	public void SubmitStudentQuestion()
	{
		Debug.Log ("quetsion is =" + mQuestion.text);
		if (mQuestion.text != "") 
		{
			StartCoroutine (SubmitTheQuestion ());
		} else 
		{
			AskQDisplayMessage.text = "Please Enter a question below. You cannot leave it empty on submit";
		}
	}

	public void CloseQuestionDialog()
	{
		mQuestionDialogBox.SetActive (false);

		//Application.OpenURL ("<p>ada<span style='color:rgb(230,0,0);'>sasasd</span></p>");
	}

	private IEnumerator SubmitTheQuestion()
	{
		//TODO complete method with url
		WWWForm form = new WWWForm ();
		form.AddField ("s_id",AppCommon.mInstanceID);
		form.AddField ("sid",mQuestion.text);
		string url = AppCommon.mCommonUrl + "add/enroll";
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
