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

	public void UnEnrol()
	{
		StartCoroutine (RequestUnenrol ());
	}

	private IEnumerator RequestUnenrol()
	{
		//TODO unenrol student url change
		WWWForm form = new WWWForm ();
		form.AddField ("s_id",AppCommon.mStudentID);
		form.AddField ("sid",AppCommon.mInstanceID);
		string url = AppCommon.mCommonUrl + "add/enroll";
		WWW www = new WWW (url,form);
		yield return www;
		mUnenrolMessageGameObject.SetActive (true);
		if (www.error == null) {
			mUnenrolMessage.text = "Succesfully Unenrolled from the subject " + AppCommon.mSubjectName;
			AppCommon.Reset ();
			yield return new WaitForSeconds (1.5f);
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
