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
		string fileName = "Screenshot" + ".png";
		string pathToSave = fileName; 
		ScreenCapture.CaptureScreenshot(pathToSave);
		yield return new WaitForEndOfFrame();
		GameObject blinkCopy = Instantiate(blink, blinktrans.transform.position, Quaternion.identity) as GameObject;
		GameObject arCam = GameObject.Find("ARCamera");
		yield return new WaitForSeconds(0.5f);
		arCam.GetComponent<PhoneCamera>().btnClick();
		yield return new WaitForSeconds(8f);
		//씬 이동
		Manager.instance.CallSelectWordScene();
	}

}
