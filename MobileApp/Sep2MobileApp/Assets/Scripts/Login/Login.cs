using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Login : MonoBehaviour {


	public Text mUsername;
	public Text mPassword;
	private bool mIsLogged = false;
	public 
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void LoginPageDisplay()
	{
		if (PlayerPrefs.GetInt ("LoggedIn") == 0) 
		{
			GameObject.Find ("ScriptHolder").GetComponent<ScreenManager> ().SetScreen ("Login");
		} 
		else 
		{
			StartCoroutine (Log ());
		}
			

	}


	public void LoginUser()
	{
		PlayerPrefs.SetString ("UID", mUsername.text);
		PlayerPrefs.SetString ("PWD", mUsername.text);
		StartCoroutine (Log ());
	}

	private IEnumerator Log()
	{
		//TODO complete method with url
		WWWForm form = new WWWForm ();
		form.AddField ("s_id",PlayerPrefs.GetString("UID"));
		form.AddField ("sid",PlayerPrefs.GetString("PWD"));
		string url = AppCommon.mCommonUrl + "add/enroll";
		WWW www = new WWW (url,form);
		yield return www;

		if (www.error == null) {
			PlayerPrefs.SetInt("LoggedIn",1);
			yield return new WaitForSeconds (0.5f);
			GameObject.Find ("ScriptHolder").GetComponent<ScreenManager> ().SetScreen ("Home");
		} 
		else 
		{
			//TODO do something
		}
	}

	public void LogOut()
	{
		AppCommon.Reset();
		PlayerPrefs.SetInt("LoggedIn",0);
		PlayerPrefs.SetString ("UID", "");
		PlayerPrefs.SetString ("PWD", "");
	}
}
