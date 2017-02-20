// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;

/// <summary>
/// Explenation at: https://en.wikipedia.org/wiki/Singleton_pattern
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

	private static T _instance;

	private static object _lock = new object();
	private static bool _applicationIsQuiting = false;

	public static T Instance{
		get{
			if (_applicationIsQuiting) {
				return null;
			}

			lock(_lock) {
				if (_instance == null) {
					_instance = (T) FindObjectOfType(typeof(T));

					if ( FindObjectsOfType(typeof(T)).Length > 1 ) {
						return _instance;
					}

					if (_instance == null) {
						GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<T>();
						singleton.name = "(singleton) "+ typeof(T).ToString();
					}
				}

				return _instance;
			}
		}
	}

	public void OnDestroy(){
		_applicationIsQuiting = true;
	}
}
