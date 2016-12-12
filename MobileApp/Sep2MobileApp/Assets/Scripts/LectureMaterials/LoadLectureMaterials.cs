using UnityEngine;
using System.Collections;
using LitJson;
public class LoadLectureMaterials : MonoBehaviour {

	private JsonData jsonvale;
	private int mNumberOfObjects;
	public GameObject mLectureMaterialPrefab;
	public GameObject mScrollviewContent;
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

	private void LoadMaterials()
	{
		StartCoroutine (Load ());

	}

	private IEnumerator Load()
	{
		//TODO change url
		string s = AppCommon.mCommonUrl + "get/getsubjects?year=";
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
					GameObject newgameobject = Instantiate (mLectureMaterialPrefab);
					newgameobject.transform.parent = mScrollviewContent.transform;
					newgameobject.GetComponent<DownloadSubjectMaterial>().SetUrl(/*ADD JSON DATA HERE*/"","");
				}
			}
		}

		yield return new WaitForSeconds (2f);

	}

	private bool Processjson(string jsonString)
	{
		jsonvale = JsonMapper.ToObject(jsonString);
		Debug.Log (jsonString);
		mNumberOfObjects = jsonvale.Count;
		return true;
	}
}
