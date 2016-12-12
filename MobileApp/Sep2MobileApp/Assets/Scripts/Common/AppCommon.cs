using UnityEngine;
using System.Collections;

public class AppCommon : MonoBehaviour {

	public static string mCommonUrl = "http://pc:3000/";
	public static string mSubjectName;
	public static string mLecturerName;
	public static string mTime;
	public static string mLocation;
	public static string mInstanceID;
	public static string mStudentID = "4017148";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static void Reset()
	{
		mSubjectName = "";
		mLecturerName = "";
		mTime = "";
		mLocation = "";
		mInstanceID = "";
	}
}
