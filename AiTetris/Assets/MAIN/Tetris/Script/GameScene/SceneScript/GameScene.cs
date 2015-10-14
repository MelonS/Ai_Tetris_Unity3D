using UnityEngine;

public class GameScene : MonoBehaviour 
{	
	void Start () 
	{
		Tetris.GameManager.Instance.Init();		
	}
}
