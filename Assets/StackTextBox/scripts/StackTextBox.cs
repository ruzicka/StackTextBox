using UnityEngine;
using System.Collections.Generic;

public class StackTextBox : MonoBehaviour {

	protected List<StackText> texts = new List<StackText>();
	protected GameObject stackTextPrefab;
	protected float itemHeight = 20f;
	
	private bool restacking = false;
	private List<Vector2> restackingData = new List<Vector2>();
	private float restackingProgress = 0f;	//range 0 - 1
	private float paddingTop = 3f;
	private float paddingBottom = 3f;
	
	public Font font;
	public int fontSize = 14;


	void Start () {
		this.stackTextPrefab = Resources.Load("StackTextBox/StackText") as GameObject;
	}

	
	private float GetNextYPosition(int skipAfter = -1) {
		if (skipAfter < 0) {
			skipAfter = int.MaxValue;
		}
		float y = 0f;
		int counter = 0;
		foreach (StackText stackText in texts) {
			if (counter >= skipAfter) {
				return y - paddingTop;
			} 
			y -= stackText.height + paddingTop + paddingBottom;
			counter++;
		}
		
		return y - paddingTop;
	}


	public StackText AddText(string text, int autoHideTime = 0) {
		GameObject obj = MonoBehaviour.Instantiate(this.stackTextPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		obj.transform.SetParent(gameObject.transform, false);
		StackText stackText = obj.GetComponent<StackText>();
		stackText.transform.localPosition = new Vector3(0, GetNextYPosition(), 0);
		if (font != null) {
			stackText.font = font;
		}
		stackText.fontSize = fontSize;
		texts.Add(stackText);
		stackText.text = text;
		stackText.Show();
		stackText.AddOnHiddingDoneCallback(onTextHiddenDone);
		stackText.AddOnHiddingStartedCallback(onTextHiddenStarted);
		if (autoHideTime > 0) {
			stackText.AutoHide(autoHideTime);
		}
		if (restacking) {
			restackInit();
		}
		return stackText;
	}


	public void HideAll() {
		foreach (StackText stackText in texts) {
			stackText.Hide();
		}

		texts[0].Hide();
	}


	public void onTextHiddenDone(StackText stackText) {
		stackText.Destroy();
	}

	
	public void onTextHiddenStarted(StackText stackText) {
		texts.Remove(stackText);
		restackInit();
	}

	
	private void restackInit() {
		if (texts.Count == 0) {
			return;
		}
		
		restacking = true;
		restackingData.Clear();
		restackingProgress = 0f;
		
		int counter = 0;
		foreach(StackText text in texts) {
			restackingData.Add(new Vector2(text.transform.localPosition.y, GetNextYPosition(counter)));
			counter++;
		}
	}
	
	
	private void restack() {
		bool lastIteration = false;
		
		float step = 2f * Time.deltaTime;
		if ((restackingProgress + step) >= 1f) {
			step = 1f - restackingProgress;
			lastIteration = true;
		}

		restackingProgress += step;
		float restackingProgressSmoothed = Mathf.SmoothStep(0f, 1f, restackingProgress);
		
		int counter = 0;
		foreach(StackText text in texts) {
			Vector2 vect = restackingData[counter];
			Vector3 tr = text.transform.localPosition;
			tr.y = vect.x - ((vect.x - vect.y) * restackingProgressSmoothed);
			text.transform.localPosition = tr;
			counter++;
		}
		
		if (lastIteration) {
			restacking = false;
		}

	}
	
	void Update () {
		if (restacking) {
			restack();
		}
	
	}
	
}
