using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
public class Login : MonoBehaviour {

	private JsonData jsonvale;
	public Text mUsername;
	public InputField mPassword;
	public GameObject mScriptHolder;

	// Use this for initialization
	void Start () 
	{
		LoginPageDisplay ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*Validate which page to display*/
	private void LoginPageDisplay()
	{
		if (PlayerPrefs.GetInt ("SLIITLoggedIn") == 0) 
		{
			GameObject.Find ("ScriptHolder").GetComponent<ScreenManager> ().SetScreen ("Login");
		} 
		else 
		{
			StartCoroutine (Log ());
		}
			

	}

	/*Set player prefs for one time login*/
	public void LoginUser()
	{
		PlayerPrefs.SetString ("SLIITUID", mUsername.text);
		PlayerPrefs.SetString ("SLIITPWD", mPassword.text);
		Debug.Log ("uname "+PlayerPrefs.GetString("SLIITUID") + " pwd "+ PlayerPrefs.GetString("SLIITPWD"));
		StartCoroutine (Log ());
	}

	/*Validate user with the database*/
	private IEnumerator Log()
	{
		//TODO complete method with url
		WWWForm form = new WWWForm ();
		form.AddField ("username",PlayerPrefs.GetString("SLIITUID"));
		form.AddField ("password",PlayerPrefs.GetString("SLIITPWD"));
		string url = AppCommon.mCommonUrl + "add/login";
		WWW www = new WWW (url,form);
		yield return www;

		if (www.error == null) {

			Processjson (www.text);
			PlayerPrefs.SetInt("SLIITLoggedIn",1);
			AppCommon.mStudentID = jsonvale[0]["s_id"].ToString();
			mScriptHolder.GetComponent<ScreenManager> ().SetScreen ("Home");
		} 
		else
		{
			//TODO do something
		}
	}

	/*Clears playerprefs and reset data*/
	public void LogOut()
	{
		AppCommon.Reset();
		PlayerPrefs.SetInt("SLIITLoggedIn",0);
		PlayerPrefs.SetString ("SLIITUID", "");
		PlayerPrefs.SetString ("SLIITPWD", "");
		mScriptHolder.GetComponent<ScreenManager> ().SetScreen ("Login");
	}

	private bool Processjson(string jsonString)
	{
		jsonvale = JsonMapper.ToObject(jsonString);
		return true;
	}
}
