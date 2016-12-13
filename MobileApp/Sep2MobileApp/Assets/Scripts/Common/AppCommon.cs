using UnityEngine;
using System.Collections;

public class AppCommon : MonoBehaviour {

	public static string mCommonUrl = "http://192.168.1.2:3000/"; //"http://192.168.1.4:3000/" //http://pc:3000/
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

	/*Reset global data*/
	public static void Reset()
	{
		mSubjectName = "";
		mLecturerName = "";
		mTime = "";
		mLocation = "";
		mInstanceID = "";
	}
}
