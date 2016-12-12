using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MaterialUI;
public class EnrollingDialogBoxHandler : MonoBehaviour {

	public Text mEnrolmentKeyValue;
	public Text mMessage;
	public GameObject mEnrolmentKeyInputField;
	public GameObject mOkButton;
	public GameObject mEnrolmentDialogBox;
	private string mSubjectName;
	private string mLecturerName;
	private string mTime;
	private string mLocation;
	private string mSubjectEnrolmentKey;
	private string mSessionID;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DisplayDetails(string SubjectName, string LecturerName, string Location,string lTime,string enrol, string sessionID)
	{
		mSubjectName = SubjectName;
		mLecturerName = LecturerName;
		mTime = lTime;
		mLocation = Location;
		mSubjectEnrolmentKey = enrol;
		mSessionID = sessionID;
		mMessage.text = "You are about to Enroll to "+SubjectName+" of "+LecturerName+" on "+Location+" at "+lTime+"\n Please Enter the Enrolment Key below and press OK";
		mEnrolmentKeyInputField.SetActive (true);
		mOkButton.SetActive (true);
	}

	public void VerifyEnrolment()
	{
		string key = mEnrolmentKeyValue.text;
		mMessage.text = "Verifying Enrolment Key...... Please Wait....";
		mEnrolmentKeyInputField.SetActive (false);
		mOkButton.SetActive (false);

		StartCoroutine (ValidateEnrolmentKey(key));
	}

	private IEnumerator ValidateEnrolmentKey(string key)
	{
		

		SetDetailsOfAppCommon ();
		if (key.Equals(mSubjectEnrolmentKey)) {
			WWWForm form = new WWWForm ();
			form.AddField ("s_id",AppCommon.mStudentID);
			form.AddField ("sid",AppCommon.mInstanceID);
			string url = AppCommon.mCommonUrl + "add/enroll";
			WWW www = new WWW (url,form);
			yield return www;

			if (www.error == null) {

				mEnrolmentKeyInputField.GetComponent<InputField> ().text = "";
				mEnrolmentDialogBox.SetActive (false);
				GetPublicObjects.mScriptHolder.GetComponent<ScreenManager> ().SetScreen ("Lecture");
				GetPublicObjects.mScriptHolder.GetComponent<DisplayDetails> ().SetDemo ();
			}
			else 
			{
				mMessage.text = "Sorry! the key you entered is not right. Please try again";
			}

		} 
		else 
		{
			mMessage.text = "Sorry! the key you entered is not right. Please try again";
			mEnrolmentKeyInputField.SetActive (true);
			mOkButton.SetActive (true);
		}
		yield return new WaitForSeconds (0.5f);
	}

	private void SetDetailsOfAppCommon()
	{
		AppCommon.mLecturerName = mLecturerName;
		AppCommon.mLocation = mLocation;
		AppCommon.mSubjectName = mSubjectName;
		AppCommon.mTime = mTime;
		AppCommon.mInstanceID = mSessionID;
	}

	public void CloseDialogBox()
	{
		mEnrolmentDialogBox.SetActive (false);
	}
}
