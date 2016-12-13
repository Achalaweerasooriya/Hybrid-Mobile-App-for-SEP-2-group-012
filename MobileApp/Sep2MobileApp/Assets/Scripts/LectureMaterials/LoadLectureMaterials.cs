using UnityEngine;
using System.Collections;
using LitJson;
public class LoadLectureMaterials : MonoBehaviour {

	/*Pivate variables*/
	private JsonData jsonvale;
	private int mNumberOfObjects;
	private Transform[] mSearchedSubjectSet;

	/*Public variables*/
	public GameObject mLectureMaterialPrefab;
	public GameObject mScrollviewContent;
	public Transform mLecturePrefabTransform;
	public GameObject mLectureMaterialContent;


	void OnEnable()
	{
		EventManagerBase.OnLectureMaterialsLoaded += LoadMaterials;
	}

	void OnDisable()
	{
		EventManagerBase.OnLectureMaterialsLoaded -= LoadMaterials;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*Called on enable to destroy old objects and call the load method*/
	private void LoadMaterials()
	{
		mSearchedSubjectSet = mLectureMaterialContent.GetComponentsInChildren<Transform> ();

		if (mSearchedSubjectSet.Length > 0) 
		{
			foreach (Transform g in mSearchedSubjectSet) 
			{
				Destroy (g.gameObject);
			}
		}

		StartCoroutine (Load ());

	}

	/*Load lecture materials from server*/
	private IEnumerator Load()
	{
		string s = AppCommon.mCommonUrl + "get/smaterial?sid="+AppCommon.mInstanceID+"";
		WWW www = new WWW (s);
		yield return www;

		if (www.error == null) 
		{
			if (Processjson (www.text)) 
			{
				for(int i = 0; i < mNumberOfObjects; i++)
				{
					GameObject newgameobject = Instantiate (mLectureMaterialPrefab);
					newgameobject.transform.parent = mScrollviewContent.transform;
					newgameobject.transform.localScale = mLecturePrefabTransform.localScale;
					newgameobject.transform.localPosition = mLecturePrefabTransform.localPosition;
					newgameobject.GetComponent<DownloadSubjectMaterial>().SetUrl(jsonvale[i]["url"].ToString(),jsonvale[i]["mname"].ToString());
				}
			}
		}

		yield return new WaitForSeconds (2f);

	}

	/*Convert string to jsonobject*/
	private bool Processjson(string jsonString)
	{
		jsonvale = JsonMapper.ToObject(jsonString);
		mNumberOfObjects = jsonvale.Count;
		Debug.Log (jsonString);
		return true;
	}
}
