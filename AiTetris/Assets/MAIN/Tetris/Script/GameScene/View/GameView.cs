using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tetris 
{
	public class GameView : IGameView {

		private List<PieceView> viewList = new List<PieceView>();

		public GameView() 
		{
			BlockObjectPool.Instance.Init();

			GamePresenter.Instance.Init(this);
		}

		public void MakePiece(Vector3 pos, int ID)
		{
			GameObject viewObj = BlockObjectPool.Instance.GetPooledObject();
			viewObj.transform.position = pos;
			viewObj.SetActive(true);
			viewObj.GetComponent<Renderer>().material.color = new Color(Random.Range(0.5f,0.8f),
				                                                        Random.Range(0.3f,0.7f), 
				                                                        Random.Range(0.2f,0.6f), 0.8f);
			
			PieceView view = viewObj.GetComponent<PieceView>();
			viewList.Add(view);
			view.Position = pos;
			view.ID = ID;
			view.OnClicked += HandleClicked;
		}

		public void DestroyPiece(int ID)
		{
			PieceView view = null;	
			foreach(PieceView v in viewList) {
				if (v.ID == ID) {
					viewList.Remove(v);
					view = v;
					break;
				}
			}

			if (view == null) Debug.LogError("ERROR");

			view.OnClicked -= HandleClicked;
			BlockObjectPool.Instance.UnusedPooledObject(view.gameObject);
		}

		private void HandleClicked(object sender, PieceClickedEventArgs e)
		{
			PieceView view = sender as PieceView;
			if (view != null) {
				int id = view.ID;

				Debug.Log("id : "+id);

				GamePresenter.Instance.PieceClicked(id);
			}
		}
	}
}