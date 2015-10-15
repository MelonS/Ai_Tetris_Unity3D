using System;
using UnityEngine;

namespace Tetris
{
	public interface IPieceView 
	{
		event EventHandler<PieceClickedEventArgs> OnClicked;
		Vector3 Position { get; set; }
		int ID { get; set; }
	}
}