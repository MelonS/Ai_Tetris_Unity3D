using UnityEngine;
using System.Collections;

namespace Tetris
{
	public class GamePresenter : MELONS.GameObjectSingleton<GamePresenter> {

		private IGameView view;

		private GamePresenter()
		{
			name = "GamePresenter";
		}

		public void Init(IGameView view)
		{
			Debug.Log("Tetris::GamePresenter Init");

			this.view = view;

			GameManager.Instance.Init();
		}

		#region From Model
		public void MakePieceView(Vector3 pos, int ID)
		{
			view.MakePiece(pos, ID);
		}

		public void DestroyPieceView(int ID)
		{
			view.DestroyPiece(ID);
		}

		public void MovePieceView(int ID, Vector3 pos)
		{
			view.MovePiece(ID, pos);
		}
		#endregion

		#region From View
		public void PieceClicked(int ID)
		{
			GameManager.Instance.PieceClicked(ID);
		}
		#endregion
	}
}