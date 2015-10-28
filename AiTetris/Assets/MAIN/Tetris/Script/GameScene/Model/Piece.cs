using System;
using UnityEngine;

namespace Tetris
{
	public class Piece
	{
		private Vector3 _position;
		public event EventHandler<PiecePositionChangedEventArgs> OnPositionChanged = (sender, e) => {};
		
		public Vector3 Position
		{
			get { return _position; }
			set {
				if (_position != value) {
					_position = value;
					
					var eventArgs = new PiecePositionChangedEventArgs();
					OnPositionChanged(this, eventArgs);
				}
			}
		}
		
		private int _id;
		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}
	}
}
