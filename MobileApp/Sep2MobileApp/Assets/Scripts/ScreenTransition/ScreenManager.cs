using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {
	public GameObject[] mScreens;
	private Stack mScreenStack = new Stack();
	private EventManagerBase mEventManagerBase;
	// Use this for initialization
	void Start () {
		mEventManagerBase = GetPublicObjects.mScriptHolder.GetComponent<EventManagerBase> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) 
		{
			Back ();
		}
	
	}

	//CHECK
	public void SetScreen(string NxtScreenname)
	{
		foreach (GameObject g in mScreens) 
		{
			ScreenProperties sp = g.GetComponent<ScreenProperties> ();
			if (sp.GetScreenName ().Equals (NxtScreenname)) 
			{
				g.SetActive (true);
				g.transform.FindChild ("ScreenSpace").gameObject.SetActive(true);
				mScreenStack.Push (NxtScreenname);
			} 
			else 
			{
				g.SetActive (false);
				g.transform.FindChild ("ScreenSpace").gameObject.SetActive(false);
			}
		}
		TriggerScreenLoadEvents (NxtScreenname);
	}

	private void Back()
	{
		string currentscreen = mScreenStack.Pop ().ToString();
		string prevscreen;

		if (!(currentscreen.Equals ("Home"))) {
			prevscreen = mScreenStack.Pop ().ToString();

			SetScreen (prevscreen);
		}
			
	}

	private void TriggerScreenLoadEvents(string screenname)
	{
		if (screenname.Equals ("Home")) 
		{
			mEventManagerBase.TriggerHomeScreenLoaded ();
		}
		else if (screenname.Equals ("Lecture")) 
		{
			mEventManagerBase.TriggerLectureScreenLoaded();
		}
	}
}
