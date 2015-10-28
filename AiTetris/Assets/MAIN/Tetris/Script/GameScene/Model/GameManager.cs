using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tetris
{
	public class GameManager : MELONS.GameObjectSingleton<GameManager>
	{
		private List<Piece> modelList = new List<Piece>();

		private static int pieceID_idx = 0;

		private bool[,] spawnPointMap = new bool[Common.Config.GROUND_ROW,Common.Config.GROUND_COL];

		private GameManager()
		{
			name = "GameManager";
		}

		public void Init()
		{
			Debug.Log("Tetris::GameManager Init");

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

			//make piece
			Piece piece = new Piece();
			piece.Position = spawnPoint;
			piece.ID = ++pieceID_idx;
			piece.OnPositionChanged += HandlePositionChanged;
			modelList.Add(piece);

			//notify to presenter, make viewpiece
			GamePresenter.Instance.MakePieceView(piece.Position, piece.ID);
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

			Piece piece = null;

			//remove piece
			foreach(Piece p in modelList) {
				if (p.ID == ID) {
					modelList.Remove(p);
					piece = p;
					break;
				}
			}

			if (piece == null) Debug.LogError("ERROR GameManager::DestroyPiece");

			//notify to presenter, remove viewpiece
			GamePresenter.Instance.DestroyPieceView(ID);

			//ResetSpawnPoint
			piece.OnPositionChanged -= HandlePositionChanged;
			ResetSpawnPoint(piece.Position);
		}

		private void HandlePositionChanged(object sender, PiecePositionChangedEventArgs e)
		{

		}
		
		public void PieceClicked(int ID)
		{
			DestoryPiece(ID);
		}
	}
}
