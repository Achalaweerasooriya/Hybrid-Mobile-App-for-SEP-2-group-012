using UnityEngine;
using System.Collections;

public class EventManagerBase : MonoBehaviour {
	// Use this for initialization
	public delegate void OnEvent();
	public static event OnEvent OnHomeScreenLoaded;
	public static event OnEvent OnLectureScreenLoaded;
	public static event OnEvent OnLectureMaterialsLoaded;
	public static event OnEvent OnSpotTestLoaded;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TriggerHomeScreenLoaded()
	{
		if (OnHomeScreenLoaded != null)
			OnHomeScreenLoaded ();
	}

	public void TriggerLectureScreenLoaded()
	{
		if (OnLectureScreenLoaded != null)
			OnLectureScreenLoaded ();
	}

	public void TriggerLectureMaterialsLoaded()
	{
		if (OnLectureMaterialsLoaded != null)
			OnLectureMaterialsLoaded ();
	}

	public void TriggerSpotTestLoaded()
	{
		if (OnSpotTestLoaded != null)
			OnSpotTestLoaded ();
	}
}
