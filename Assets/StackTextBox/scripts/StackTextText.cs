using UnityEngine;

public class StackTextText : MonoBehaviour {


	public delegate void StackTextTextCallback();

	private StackTextTextCallback fadeOutFinishedCallbacks;
	private StackTextTextCallback fadeOutStartedCallbacks;

	public void AddFadeOutFinishedCallback (StackTextTextCallback callback) {
		fadeOutFinishedCallbacks += callback;
	}

	public void AddFadeOutStartedCallback (StackTextTextCallback callback) {
		fadeOutStartedCallbacks += callback;
	}


	public void OnFadeOutFinished () {
		if (fadeOutFinishedCallbacks != null) {
			fadeOutFinishedCallbacks();
		}
	}


	public void OnFadeOutStarted () {
		if (fadeOutStartedCallbacks != null) {
			fadeOutStartedCallbacks();
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
