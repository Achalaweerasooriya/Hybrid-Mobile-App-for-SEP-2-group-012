using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DownloadSubjectMaterial : MonoBehaviour {
	private string mUrl;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetUrl(string url,string name)
	{
		mUrl = url;
		gameObject.transform.FindChild ("SubjectNameText").GetComponent<Text> ().text = name;
	}

	public void OpenUrl()
	{
		Application.OpenURL (mUrl);
	}
}
