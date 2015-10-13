using UnityEngine;

public class TestMVPmain : MonoBehaviour {

	void Awake() 
	{ // 기능 : 실행하면 큐브가 하나 나오고 마우스로 큐브를 클릭하면 움직인다.
		GameManager.Instance.Init();
	}
}
