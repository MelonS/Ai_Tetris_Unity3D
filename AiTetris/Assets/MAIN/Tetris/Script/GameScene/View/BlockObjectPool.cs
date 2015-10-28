using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tetris
{
	public class BlockObjectPool : MELONS.GameObjectSingleton<BlockObjectPool> 
	{
		public int pooledAmount = Common.Config.GROUND_COL * Common.Config.GROUND_ROW;
		public bool willGrow = true;

		private GameObject blockPrefab;
		private List<GameObject> pooledObjects = new List<GameObject>();

		private BlockObjectPool()
		{
			name = "BlockObjectPool";
		}

		public void Init()
		{
			blockPrefab = Resources.Load<GameObject>(Common.Path.Prefabs.BLOCK);
			if (blockPrefab == null) Debug.LogError("Not Loading BLOCK PREFAB...");

			for (int i = 0; i < pooledAmount; ++i)
			{
				GameObject obj = (GameObject)Instantiate(blockPrefab);
				obj.SetActive(false);
				pooledObjects.Add(obj);
			}
		}

		public GameObject GetPooledObject()
		{
			for (int i = 0; i < pooledObjects.Count; ++i)
			{
				if (!pooledObjects[i].activeInHierarchy)
				{
					return pooledObjects[i];
				}
			}

			if (willGrow)
			{
				GameObject obj = (GameObject)Instantiate(blockPrefab);
				obj.SetActive(false);
				pooledObjects.Add(obj);
				return obj;
			}

			return null;
		}

		public void UnusedPooledObject(GameObject obj)
		{
			obj.SetActive(false);
		}
	}
}