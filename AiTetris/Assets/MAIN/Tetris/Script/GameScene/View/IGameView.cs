using UnityEngine;
using System.Collections;

namespace Tetris 
{
	public interface IGameView {
		void MakePiece(Vector3 pos, int ID);
		void DestroyPiece(int ID);

		void MovePiece(int ID, Vector3 pos);
	}
}