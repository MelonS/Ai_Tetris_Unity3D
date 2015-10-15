using System;
using UnityEngine;

namespace Tetris
{
	public interface IPieceModel 
	{
		event EventHandler<PiecePositionChangedEventArgs> OnPositionChanged;
		Vector3 Position { get; set; }
		int ID { get; set; }
	}
}