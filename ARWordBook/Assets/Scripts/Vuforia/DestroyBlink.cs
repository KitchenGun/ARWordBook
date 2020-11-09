using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlink : MonoBehaviour {

	// 시작하고 일정 시간뒤 파괴 
	void Start () {
		Destroy (gameObject, 0.8f);
	}
}
