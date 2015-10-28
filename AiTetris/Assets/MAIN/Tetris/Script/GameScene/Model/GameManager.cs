using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tetris
{
	public class GameManager : MELONS.GameObjectSingleton<GameManager>
	{
		private List<Piece> modelList = new List<Piece>();

		private static int pieceID_idx = 0;

		private enum GameState
		{
			IDLE = 0,
			SPAWN,
			DROP,
			MERGE,
		}
		private GameState currentState;

		private Piece currentPiece;

		private GameManager()
		{
			name = "GameManager";
		}

		public void Init()
		{
			Debug.Log("Tetris::GameManager Init");

			SetState(GameState.SPAWN);

			StartCoroutine("Loop");
		}

		IEnumerator Loop() 
		{
			//state Spawn -> drop -> merge -> Spawn

			while(true) 
			{
				if (GameState.IDLE == GetState()) {
					break;
				}else if (GameState.SPAWN == GetState()) {
					SpawnPiece();
					SetState(GameState.DROP);
				}else if (GameState.DROP == GetState()) {
					yield return new WaitForSeconds(Common.Config.DROP_SPEED);
					DropPiece();
				}else if (GameState.MERGE == GetState()) {
					//TODO 
					SetState(GameState.SPAWN);
					yield return null;
				}
			}
		}

		private void SetState(GameState state)
		{
			currentState = state;
		}

		private GameState GetState()
		{
			return currentState;
		}

		private void SpawnPiece()
		{
			Vector3 spawnPoint = GetSpawnPoint();

			//make piece
			Piece piece = new Piece();
			piece.Position = spawnPoint;
			piece.ID = ++pieceID_idx;
			piece.OnPositionChanged += HandlePositionChanged;
			modelList.Add(piece);

			//notify to presenter, make viewpiece
			GamePresenter.Instance.MakePieceView(piece.Position, piece.ID);

			currentPiece = piece;
		}

		private Vector3 GetSpawnPoint()
		{
			int x = Random.Range(0, Common.Config.GROUND_ROW - 1);
			int y = Common.Config.GROUND_COL;
			return new Vector3(x, y); 
		}

		private void DropPiece()
		{
			Piece piece = currentPiece;

			if (isFloor(piece) == false) {
				piece.Position += new Vector3(0, -1);
			}else{
				SetState(GameState.MERGE);
			}
		}

		private bool isFloor(Piece piece)
		{
			float y = piece.Position.y;
			if (y == 0.0f) 
				return true;
			else 
				return false;
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
		}

		private void HandlePositionChanged(object sender, PiecePositionChangedEventArgs e)
		{
			Piece piece = sender as Piece;
			if (piece != null) {
				GamePresenter.Instance.MovePieceView(piece.ID, piece.Position);
			}
		}
		
		public void PieceClicked(int ID)
		{
			DestoryPiece(ID);
		}
	}
}
