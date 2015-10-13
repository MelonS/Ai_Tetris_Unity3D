using UnityEngine;

public class EnemyViewFactory : IEnemyViewFactory 
{
	public IEnemyView View { get; private set; }

	public EnemyViewFactory()
	{
		var prefab = Resources.Load<GameObject>("TestMVP/Prefabs/Enemy");
		if (prefab == null) Debug.Log("prefab IS NULL");
		else Debug.Log("prefab NOT NULL");

		var instance = UnityEngine.Object.Instantiate(prefab);
		View = instance.GetComponent<IEnemyView>();
	}
}
