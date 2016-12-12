using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MaterialUI;
public class SearchSubjectPrefabDataLoader : MonoBehaviour {
	private string mSubjectName;
	private string mLecturerName;
	private string mTime;
	private string mLocation;
	private string mEnrolmentKey;
	private string mSessionID;

	public Text mSubject;
	public Text Lecturer;
	public Text Location;
	public Text Time;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetDetailsOnPrefab(string subject,string lecturer,string time,string location, string enrol, string sessionID)
	{
		mSubjectName = subject;
		mLecturerName = lecturer;
		mTime = time;
		mLocation = location;
		mEnrolmentKey = enrol;
		mSessionID = sessionID;

		DisplayDetailsOnPrefab ();
	}

	public void DisplayDetailsOnPrefab()
	{
		mSubject = gameObject.transform.FindChild ("SubjectNameText").gameObject.GetComponent<Text> ();
		Lecturer = gameObject.transform.FindChild ("LecturerNameValueText").gameObject.GetComponent<Text> ();
		Location = gameObject.transform.FindChild ("LocationValueText").gameObject.GetComponent<Text> ();
		Time = gameObject.transform.FindChild ("TimeValueText").gameObject.GetComponent<Text> ();

		mSubject.text = mSubjectName;
		Lecturer.text = mLecturerName;
		Location.text = mLocation;
		Time.text = mTime;
	}

	public void DisplayEnrollingDialogBox()
	{
		GetPublicObjects.mEnrolmentDialogBox.SetActive (true);
		GetPublicObjects.mScriptHolder.GetComponent<EnrollingDialogBoxHandler>().DisplayDetails(mSubjectName, mLecturerName, mLocation,mTime,mEnrolmentKey,mSessionID);
	}


}
