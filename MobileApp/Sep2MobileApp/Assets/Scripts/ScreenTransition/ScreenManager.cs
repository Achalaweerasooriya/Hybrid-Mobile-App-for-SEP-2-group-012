using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetScreen(string screenname)
	{
		GameObject NewScreen = GameObject.Find (screenname);
		NewScreen.transform.FindChild ("ScreenSpace").gameObject.SetActive(true);
	}
}
