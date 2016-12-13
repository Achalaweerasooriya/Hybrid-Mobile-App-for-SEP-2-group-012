using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {
	public GameObject[] mScreens;
	private Stack mScreenStack = new Stack();
	private EventManagerBase mEventManagerBase;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) 
		{
			Back ();
		}
	
	}

	/*Set screen and deactivate other screens*/
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

	/*Handle back event*/
	private void Back()
	{
		string currentscreen = mScreenStack.Pop ().ToString();
		string prevscreen;

		if (!(currentscreen.Equals ("Home"))) {
			prevscreen = mScreenStack.Pop ().ToString();

			SetScreen (prevscreen);
		}
			
	}

	/*Event trigger*/
	private void TriggerScreenLoadEvents(string screenname)
	{
		if (screenname.Equals ("Home")) 
		{
			GetPublicObjects.mScriptHolder.GetComponent<EventManagerBase> ().TriggerHomeScreenLoaded ();
		}
		else if (screenname.Equals ("Lecture")) 
		{
			GetPublicObjects.mScriptHolder.GetComponent<EventManagerBase> ().TriggerLectureScreenLoaded();
		}
		else if (screenname.Equals ("Lecture Materials")) 
		{
			GetPublicObjects.mScriptHolder.GetComponent<EventManagerBase> ().TriggerLectureMaterialsLoaded ();
		}
		else if (screenname.Equals ("SpotTest")) 
		{
			GetPublicObjects.mScriptHolder.GetComponent<EventManagerBase> ().TriggerSpotTestLoaded();
		}
	}
}
