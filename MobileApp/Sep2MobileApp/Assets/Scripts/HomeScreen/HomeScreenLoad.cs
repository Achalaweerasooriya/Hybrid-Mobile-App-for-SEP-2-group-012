using UnityEngine;
using System.Collections;
using MaterialUI;
using LitJson;
public class HomeScreenLoad : MonoBehaviour {
	public GameObject mLectureDetailPrefab;
	public GameObject mScrollviewContent;
	private float mYear;
	private float mSemester;
	private GameObject[] mSearchedSubjectSet;
	private JsonData jsonvale;
	private int mNumberOfObjects;
	// Use this for initialization
	void Start () {
		mYear = 1f;
		mSemester = 1f;
		SearchLectures ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable()
	{
		EventManagerBase.OnHomeScreenLoaded += SearchLectures;
	}

	void OnDisable()
	{
		EventManagerBase.OnHomeScreenLoaded -= SearchLectures;
	}

	public void SearchLectures()
	{
		Debug.Log ("search");
		mSearchedSubjectSet = GameObject.FindGameObjectsWithTag ("SubjectPrefab");

		foreach (GameObject g in mSearchedSubjectSet) 
		{
			Destroy (g);
		}
		StartCoroutine (LoadLectureDetailsFromServer(mYear,mSemester));
	}

	private IEnumerator LoadLectureDetailsFromServer(float year, float semester)
	{

		string s = AppCommon.mCommonUrl + "get/getsubjects?year=" + year + "&sem=" + semester;
		Debug.Log ("asdaa " + s);
		WWW www = new WWW (s);
		yield return www;

		if (www.error == null) 
		{
			Debug.Log ("No error");
			if (Processjson (www.text)) 
			{
				Debug.Log ("json count "+jsonvale.Count + " var count "+mNumberOfObjects);
				for(int i = 0; i < mNumberOfObjects; i++)
				{
					Debug.Log ("aasdfsdfasd "+jsonvale[i]["sname"].ToString());
					GameObject newgameobject = Instantiate (mLectureDetailPrefab);
					newgameobject.transform.parent = mScrollviewContent.transform;
					newgameobject.GetComponent<SearchSubjectPrefabDataLoader> ().SetDetailsOnPrefab (jsonvale[i]["sname"].ToString(),jsonvale[i]["lname"].ToString(),jsonvale[i]["time"].ToString(),jsonvale[i]["location"].ToString(),jsonvale[i]["enrollk"].ToString(),jsonvale[i]["sid"].ToString());

				}
			}
		}

		yield return new WaitForSeconds (2f);


	}

	public void OnYearValueChanged(float value)
	{
		Debug.Log ("year changed");
		mYear = value;
		SearchLectures ();
	}

	public void OnSemesterValueChanged(float value)
	{
		Debug.Log ("semester changed");
		mSemester = value;
		SearchLectures ();
	}

	private bool Processjson(string jsonString)
	{
		jsonvale = JsonMapper.ToObject(jsonString);
		Debug.Log (jsonString);
		mNumberOfObjects = jsonvale.Count;
		return true;
		//TODO
	}
}
