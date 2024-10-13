using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraAnchor : MonoBehaviour
{
	public enum AnchorType
	{
		BottomLeft,
		BottomCenter,
		BottomRight,
		MiddleLeft,
		MiddleCenter,
		MiddleRight,
		TopLeft,
		TopCenter,
		TopRight,
	};
	public AnchorType anchorType;
	public Vector3 anchorOffset;
	Vector3 Kanchor;
	private Camera cam;
	float camvl;
	//new Vector3(-2.56500006f, -1.6027627f, 0f)
	//Vector3 Generate;
	//public Transform Object;
	//private Vector3 setPosition;

	IEnumerator updateAnchorRoutine; //Coroutine handle so we don't start it if it's already running

	// Use this for initialization
	void Start()
	{
		
		switch (anchorType)
		{
			case AnchorType.BottomLeft:
				Kanchor = ViewportHandler.Instance.BottomLeft;
				break;
			case AnchorType.BottomCenter:
				Kanchor = ViewportHandler.Instance.BottomCenter;
				break;
			case AnchorType.BottomRight:
				Kanchor = ViewportHandler.Instance.BottomRight;
				break;
			case AnchorType.MiddleLeft:
				Kanchor = ViewportHandler.Instance.MiddleLeft;
				break;
			case AnchorType.MiddleCenter:
				Kanchor = ViewportHandler.Instance.MiddleCenter;
				break;
			case AnchorType.MiddleRight:
				Kanchor = ViewportHandler.Instance.MiddleRight;
				break;
			case AnchorType.TopLeft:
				Kanchor = ViewportHandler.Instance.TopLeft;
				break;
			case AnchorType.TopCenter:
				Kanchor = ViewportHandler.Instance.TopCenter;
				break;
			case AnchorType.TopRight:
				Kanchor = ViewportHandler.Instance.TopRight;
				break;
		}
		cam = Camera.main;
		camvl = cam.orthographicSize;
		updateAnchorRoutine = UpdateAnchorAsync();
		StartCoroutine(updateAnchorRoutine);
		//Generate = transform.position;
		//setPosition = Object.position;
		
	}

	/// <summary>
	/// Coroutine to update the anchor only once ViewportHandler.Instance is not null.
	/// </summary>
	IEnumerator UpdateAnchorAsync()
	{
		uint cameraWaitCycles = 0;
		while (ViewportHandler.Instance == null)
		{
			++cameraWaitCycles;
			yield return new WaitForEndOfFrame();
		}
		if (cameraWaitCycles > 0)
		{
			print(string.Format("CameraAnchor found ViewportHandler instance after waiting {0} frame(s). You might want to check that ViewportHandler has an earlie execution order.", cameraWaitCycles));
		}
		UpdateAnchor();
		updateAnchorRoutine = null;
	}

	void UpdateAnchor()
	{
		switch (anchorType)
		{
			case AnchorType.BottomLeft:
				SetAnchor(ViewportHandler.Instance.BottomLeft);
				break;
			case AnchorType.BottomCenter:
				SetAnchor(ViewportHandler.Instance.BottomCenter);
				break;
			case AnchorType.BottomRight:
				SetAnchor(ViewportHandler.Instance.BottomRight);
				break;
			case AnchorType.MiddleLeft:
				SetAnchor(ViewportHandler.Instance.MiddleLeft);
				break;
			case AnchorType.MiddleCenter:
				SetAnchor(ViewportHandler.Instance.MiddleCenter);
				break;
			case AnchorType.MiddleRight:
				SetAnchor(ViewportHandler.Instance.MiddleRight);
				break;
			case AnchorType.TopLeft:
				SetAnchor(ViewportHandler.Instance.TopLeft);
				break;
			case AnchorType.TopCenter:
				SetAnchor(ViewportHandler.Instance.TopCenter);
				break;
			case AnchorType.TopRight:
				SetAnchor(ViewportHandler.Instance.TopRight);
				break;
		}
	}

	void SetAnchor(Vector3 anchor)
	{

		
		//Vector3 newPos = transform.position - anchor;
		// scale with camera size
		if (camvl != cam.orthographicSize)
		{
			//Debug.Log("Gn" + (anchor) + Kanchor);
			//Debug.Log("Gnms");

			float height = Camera.main.orthographicSize * 2;
			//float width = height * Screen.width / Screen.height;
			Debug.Log(Vector3.one * height / 4f);
			gameObject.transform.localScale = Vector3.one * height / 3.2f;
			camvl = cam.orthographicSize;

		}

		//Debug.Log("Gnf" + (anchor) + Kanchor);
		if (!anchor.Equals(Kanchor))
		{
			Debug.Log("Gnm"+ camvl+ cam.orthographicSize);
			transform.position = transform.position+(anchor-Kanchor);
			Kanchor = anchor;


		}
		
		/*else
        {
			transform.position = this.transform.position;
        }*/
	}

#if UNITY_EDITOR
	// Update is called once per frame
	void Update()
	{
		
		if (updateAnchorRoutine == null)
		{
			updateAnchorRoutine = UpdateAnchorAsync();
			StartCoroutine(updateAnchorRoutine);
		}
		
	}
#endif
}
