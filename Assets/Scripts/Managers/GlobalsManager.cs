using UnityEngine;
using System.Collections;

public class GlobalsManager : MonoBehaviour {

    public static GlobalsManager instance;

	void Awake()
    {
        instance = this;
    }

}
