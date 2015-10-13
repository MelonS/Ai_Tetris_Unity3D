using UnityEngine;

public class EnemyControllerFactory : IEnemyControllerFactory 
{
	public IEnemyController Controller { get; private set; }

	public EnemyControllerFactory(IEnemyModel model, IEnemyView view)
	{
		if (model == null) Debug.Log("MODEL IS NULL");
		if (view == null) Debug.Log("VIEW IS NULL");

		Controller = new EnemyController(model, view);
	}

	public EnemyControllerFactory() : this(new EnemyModel(), new EnemyView())
	{
	}
}
