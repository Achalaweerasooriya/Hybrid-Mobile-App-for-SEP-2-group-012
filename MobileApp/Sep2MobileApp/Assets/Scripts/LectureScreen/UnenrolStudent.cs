using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UnenrolStudent : MonoBehaviour {

	public GameObject mUnenrolMessageGameObject;
	public Text mUnenrolMessage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*Calls method for student unenrolment*/
	public void UnEnrol()
	{
		StartCoroutine (RequestUnenrol ());
	}

	/*Communicate with server to unenrol student*/
	private IEnumerator RequestUnenrol()
	{
		//TODO unenrol student url change
		string url = AppCommon.mCommonUrl + "get/unenroll?s_id="+AppCommon.mStudentID;
		WWW www = new WWW (url);
		yield return www;
		mUnenrolMessageGameObject.SetActive (true);
		if (www.error == null) {
			mUnenrolMessage.text = "Succesfully Unenrolled from the subject " + AppCommon.mSubjectName;
			AppCommon.Reset ();
			GameObject.Find ("ScriptHolder").GetComponent<ScreenManager> ().SetScreen ("Home");
		}
		else 
		{
			mUnenrolMessage.text = "An error occured while unenrolling from the subject "+ AppCommon.mSubjectName+" Please try again.";
			yield return new WaitForSeconds (1.5f);
		}
		mUnenrolMessageGameObject.SetActive (false);
	}
}
