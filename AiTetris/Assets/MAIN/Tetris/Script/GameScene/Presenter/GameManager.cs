using UnityEngine;
using System.Collections;

namespace Tetris
{
	public class GameManager : MELONS.GameObjectSingleton<GameManager>
	{
		private GameManager()
		{
			name = "GameManager";
		}

		public void Init()
		{
			Debug.Log("Tetris::GameManager Init");

			BlockObjectPool.Instance.Init();


			for (int i = 0; i < Common.Config.GROUND_ROW; ++i)
			{
				for (int j = 0; j < Common.Config.GROUND_COL; ++j)
				{
					var obj = BlockObjectPool.Instance.GetPooledObject();
					obj.transform.position = new Vector3(i,j,0);
					obj.SetActive(true);
					obj.GetComponent<Renderer>().material.color = new Color(Random.Range(0.5f,0.8f),
					                                                        Random.Range(0.3f,0.7f), 
					                                                        Random.Range(0.2f,0.6f), 0.8f);
				}
			}
		}
	}
}
