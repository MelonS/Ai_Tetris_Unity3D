using UnityEngine;

public class TestMVCmain : MonoBehaviour {

	void Awake() 
	{ // 기능 : 실행하면 큐브가 하나 나오고 마우스로 큐브를 클릭하면 움직인다.
		var modelFactory = new EnemyModelFactory();
		var model = modelFactory.Model;

		model.Position = new Vector3(0, 0, 0);

		var viewFactory = new EnemyViewFactory();
		var view = viewFactory.View;

		var controllerFactory = new EnemyControllerFactory(model, view);
		var controller = controllerFactory.Controller;
	}
}
