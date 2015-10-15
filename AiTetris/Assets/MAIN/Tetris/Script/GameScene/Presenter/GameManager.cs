using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tetris
{
	public class GameManager : MELONS.GameObjectSingleton<GameManager>
	{
		private List<IPieceModel> modelList = new List<IPieceModel>();
		private List<IPieceView> viewList = new List<IPieceView>();

		private static int pieceID_idx = 0;

		private bool[,] spawnPointMap = new bool[Common.Config.GROUND_ROW,Common.Config.GROUND_COL];

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
					spawnPointMap[i,j] = false;
				}
			}

			StartCoroutine("Loop");
		}

		IEnumerator Loop() 
		{
			while(true) 
			{
				yield return new WaitForSeconds(0.5f);

				SpawnPiece();
			}
		}

		private void SpawnPiece()
		{
			Vector3 spawnPoint = GetSpawnPoint();
			if (spawnPoint == new Vector3(-1.0f, -1.0f, -1.0f))
			    return;

			//make model
			var model = new PieceModel();
			model.Position = spawnPoint;
			model.ID = ++pieceID_idx;
			modelList.Add(model);

			//make view
			GameObject viewObj = BlockObjectPool.Instance.GetPooledObject();
			viewObj.transform.position = model.Position;
			viewObj.SetActive(true);
			viewObj.GetComponent<Renderer>().material.color = new Color(Random.Range(0.5f,0.8f),
				                                                        Random.Range(0.3f,0.7f), 
				                                                        Random.Range(0.2f,0.6f), 0.8f);

			var view = viewObj.GetComponent<IPieceView>();
			viewList.Add(view);

			view.Position = model.Position;
			view.ID = model.ID;
			view.OnClicked += HandleClicked;
		}

		private Vector3 GetSpawnPoint()
		{
			for (int i = 0; i < 10; ++i)
			{
				int x = Random.Range(0, Common.Config.GROUND_ROW);
				int y = Random.Range(0, Common.Config.GROUND_COL);

				if (spawnPointMap[x,y] == false) {
					spawnPointMap[x,y] = true;
					return new Vector3(x, y, 0);
				}
			}

			for (int i = 0; i < Common.Config.GROUND_ROW; ++i)
			{
				for (int j = 0; j < Common.Config.GROUND_COL; ++j)
				{
					if (spawnPointMap[i,j] == false) {
						spawnPointMap[i,j] = true;
						return new Vector3(i, j, 0);
					}
				}
			}

			return new Vector3(-1.0f, -1.0f, -1.0f);
		}

		private void ResetSpawnPoint(Vector3 point)
		{
			int x = (int)point.x;
			int y = (int)point.y;

			spawnPointMap[x,y] = false;
		}

		private void DestoryPiece(int ID)
		{
			Debug.Log("DestoryPiece : "+ ID);

			IPieceModel imodel = null;
			IPieceView iview = null;

			//model remove
			foreach(IPieceModel m in modelList) {
				if (m.ID == ID) {
					modelList.Remove(m);
					imodel = m;
					break;
				}
			}

			//view remove
			foreach(IPieceView v in viewList) {
				if (v.ID == ID) {
					viewList.Remove(v);
					iview = v;
					break;
				}
			}

			if (imodel == null || iview == null) Debug.LogError("ERROR GameManager::DestroyPiece");

			PieceView view = iview as PieceView;
			if (view == null) Debug.LogError("ERROR");

			view.OnClicked -= HandleClicked;
			view.gameObject.SetActive(false);

			//ResetSpawnPoint
			ResetSpawnPoint(view.Position);
		}

		private void HandlePositionChanged(object sender, PiecePositionChangedEventArgs e)
		{

		}
		
		private void HandleClicked(object sender, PieceClickedEventArgs e)
		{
			IPieceView view = sender as IPieceView;
			if (view != null) {
				int id = view.ID;
				Debug.Log("id : "+id);
				DestoryPiece(id);
			}
		}
	}
}
