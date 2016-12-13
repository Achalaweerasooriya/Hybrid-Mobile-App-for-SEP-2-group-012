using UnityEngine;
using System.Collections;
using MaterialUI;
using UnityEngine.UI;
using LitJson;
public class HomeScreenLoad : MonoBehaviour {

	/*Public Variables */
	public GameObject mLectureDetailPrefab;
	public GameObject mScrollviewContent;
	public Transform mSubjectPrefabTransform;
	public Text mDebug;
	public GameObject mLoadSubjectsContent;

	/*Private Variables */
	private float mYear = 1f;
	private float mSemester = 1f;
	private Transform[] mSearchedSubjectSet;
	private JsonData jsonvale;
	private int mNumberOfObjects;
	private bool mIsDeleting = false;

	// Use this for initialization
	void Start () {
		//SearchLectures ();
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

	/*Called on event trigger. Destroys the old objects and calls LoadLectureDetailsFromServer*/
	public void SearchLectures()
	{
			mDebug.text = "Search Lectures";
			mSearchedSubjectSet = mLoadSubjectsContent.GetComponentsInChildren<Transform> ();
			mDebug.text += "\n Search gameobjects";

			if (mSearchedSubjectSet.Length > 0) 
			{
				foreach (Transform g in mSearchedSubjectSet) 
				{
					mDebug.text += "\n inside for ech";
					Destroy (g.gameObject);
					mDebug.text += "\n destroy";
				}
			}
		StartCoroutine (LoadLectureDetailsFromServer(mYear,mSemester));
		mDebug.text += "\n load lecs";
	}

	/*Calls the rest API for lectures at current time*/
	private IEnumerator LoadLectureDetailsFromServer(float year, float semester)
	{

		string s = AppCommon.mCommonUrl + "get/getsubjects?year=" + year + "&sem=" + semester;
		WWW www = new WWW (s);
		yield return www;

		if (www.error == null) 
		{
			if (Processjson (www.text)) 
			{
				for(int i = 0; i < mNumberOfObjects; i++)
				{
					GameObject newgameobject = Instantiate (mLectureDetailPrefab);
					newgameobject.transform.parent = mScrollviewContent.transform;
					newgameobject.transform.localScale = mSubjectPrefabTransform.localScale;
					newgameobject.transform.localPosition = mSubjectPrefabTransform.localPosition;
					newgameobject.GetComponent<SearchSubjectPrefabDataLoader> ().SetDetailsOnPrefab (jsonvale[i]["sname"].ToString(),jsonvale[i]["lname"].ToString(),jsonvale[i]["time"].ToString(),jsonvale[i]["location"].ToString(),jsonvale[i]["enrollk"].ToString(),jsonvale[i]["sid"].ToString());

				}
			}
		}

		yield return new WaitForSeconds (2f);


	}

	/*Dynamic value change function for dropdown for year selection*/
	public void OnYearValueChanged(int value)
	{
		mYear =(float) (value + 1f);
		mIsDeleting = true;
		SearchLectures ();
	}

	/*Dynamic value change function for dropdown for semester selection*/
	public void OnSemesterValueChanged(int value)
	{
		mSemester = (float) (value+1f);
		SearchLectures ();
	}

	/*Convert json from string to jsonobject*/
	private bool Processjson(string jsonString)
	{
		jsonvale = JsonMapper.ToObject(jsonString);
		Debug.Log (jsonString);
		mNumberOfObjects = jsonvale.Count;
		return true;
	}
}
