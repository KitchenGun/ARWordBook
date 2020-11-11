using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour {

	[SerializeField]
	GameObject blink;
	[SerializeField]
	Transform blinktrans;

	public void TakeAShot()
	{
		StartCoroutine ("CaptureIt");
	}
	//캡쳐 enumerator
	private IEnumerator CaptureIt()
	{
		GameObject blinkCopy=Instantiate(blink, blinktrans.transform.position, Quaternion.identity) as GameObject;
		string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
		string fileName = "Screenshot" + timeStamp + ".png";
		string pathToSave = fileName;
		blinkCopy.transform.localScale = new Vector3(blinkCopy.transform.localScale.x, 0, blinkCopy.transform.localScale.z);
		ScreenCapture.CaptureScreenshot(pathToSave);
		yield return new WaitForEndOfFrame();
		GameObject arCam = GameObject.Find("ARCamera");
		yield return new WaitForSeconds(0.5f);
		arCam.GetComponent<PhoneCamera>().btnClick();
<<<<<<< HEAD
		Instantiate (blink, blinktrans.transform.position, Quaternion.identity);
=======
		//씬 이동
		Manager.instance.CallSelectWordScene();
>>>>>>> 924990696d687d021309950e1a2344662bcf9ce1
	}

}
