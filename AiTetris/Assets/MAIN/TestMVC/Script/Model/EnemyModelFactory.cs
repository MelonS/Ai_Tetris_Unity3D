using UnityEngine;

public class EnemyModelFactory : IEnemyModelFactory 
{
	public IEnemyModel Model { get; private set; }

	public EnemyModelFactory()
	{
		Model = new EnemyModel();
	}
}
