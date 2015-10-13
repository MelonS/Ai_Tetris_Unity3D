using System;  
using System.Reflection;  
using UnityEngine;

namespace MELONS
{
	public class GameObjectSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{   
		private const string ROOT_NAME = "Singleton Objects Root";
		private static object syncobj_ = new object();   
		private static volatile T instance_ = null;  
		
		public static T Instance
		{  
			get  
			{  
				if (instance_ == null && isApplicationQuit == false)  
				{  
					CreateInstance();  
				}  
				
				return instance_;  
			}  
		}  
		
		private static void CreateInstance()  
		{   
			lock (syncobj_)  
			{  
				if (instance_ == null)  
				{  
					GameObject root = GameObject.Find( ROOT_NAME );
					if( root == null )
					{
						root = new GameObject( ROOT_NAME );
						DontDestroyOnLoad( root );
					}
					
					Type t = typeof(T);  
					
					// Ensure there are no public constructors...  
					ConstructorInfo[] ctors = t.GetConstructors();  
					if (ctors.Length > 0)  
					{  
						throw new InvalidOperationException(String.Format("{0} has at least one accesible ctor making it impossible to enforce singleton behaviour", t.Name));  
					}  
					
					GameObject obj = new GameObject();
					obj.name = typeof(T).ToString();
					obj.transform.SetParent( root.transform, false );
					instance_ = obj.AddComponent(typeof(T)) as T;
				}  
				
				if (instance_ == null)
					Debug.Log("Fail to "+typeof(T).Name+" Instance");
			}      
		} 
		
		public void Release()
		{
			if (instance_) {
				Destroy(instance_);
				instance_ = null;
			}
		}  
		
		private static bool isApplicationQuit = false;
		private void OnApplicationQuit()
		{
			isApplicationQuit = true;
		}
	}  
}
