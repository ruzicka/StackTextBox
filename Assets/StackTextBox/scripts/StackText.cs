using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class StackText : MonoBehaviour {


	public delegate void StackTextCallback(StackText stackText);
	protected StackTextCallback hidingDoneCallbacks;
	protected StackTextCallback hidingStartedCallbacks;
	protected Timer timer = null;
	bool hide = false;

	public string text {
		set { GetTextTextComponent().text = value; }
		get { return GetTextTextComponent().text; }
	}
	
	public float height {
		get { return GetTextTextComponent().preferredHeight; }
	}
	
	public Font font {
		set { GetTextTextComponent().font = value; }
		get { return GetTextTextComponent().font; }
	}	
	
	public int fontSize {
		set { GetTextTextComponent().fontSize = value; }
		get { return GetTextTextComponent().fontSize; }
	}
	
	
	void Start () {
		//attach animation events callbacks
		GetStackTextTextComponent().AddFadeOutFinishedCallback(OnFadeOutFinished);
		GetStackTextTextComponent().AddFadeOutStartedCallback(OnFadeOutStarted);
	}


	private StackTextText GetStackTextTextComponent() {
		return transform.Find("Text").GetComponent<Text>().GetComponent<StackTextText>();
	}

		
	private Text GetTextTextComponent() {
		return transform.Find("Text").GetComponent<Text>();
	}

		
	private Transform GetTextComponent() {
		return transform.Find("Text");
	}

		
	// Triggered by an animation event
	public void AddOnHiddingDoneCallback (StackTextCallback callback) {
		hidingDoneCallbacks += callback;
	}


	// Triggered by an animation event
	public void AddOnHiddingStartedCallback (StackTextCallback callback) {
		hidingStartedCallbacks += callback;
	}


	public void Show() {
		Animator animator = GetTextComponent().GetComponent<Animator>();
		animator.SetBool(Animator.StringToHash("shown"), true);	
	}
	
	
	public void Hide() {
		Animator animator = GetTextComponent().GetComponent<Animator>();
		animator.SetBool(Animator.StringToHash("shown"), false);	
	}
	
	
	public void OnFadeOutFinished () {
		if (hidingDoneCallbacks != null) {
			hidingDoneCallbacks(this);
		} 
	}


	public void OnFadeOutStarted () {
		if (hidingStartedCallbacks != null) {
			hidingStartedCallbacks(this);
		}
	}
	
	
	public void AutoHide(int miliseconds) {
		if (timer != null) {
			Debug.Log("Unable autoHide text. Hidding was already initiated");
			return;
		}
		
		timer = new Timer(miliseconds);
		timer.Elapsed += new ElapsedEventHandler(OnAutoHideTimerElapsed);
		timer.Enabled = true;
	}
	
	
	private void OnAutoHideTimerElapsed(object sender, ElapsedEventArgs e) {
		timer.Enabled = false;
		Debug.Log("sdfsfd");
		hide = true; // hide can be called only in main thread :(
	}


	public void Destroy() {
		Destroy(gameObject);
	}
	
	
	// Update is called once per frame
	void Update () {
		if (hide) {
			this.Hide();
		}
	}
	
}
