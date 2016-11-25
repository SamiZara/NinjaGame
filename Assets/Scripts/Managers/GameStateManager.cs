using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    public static GameStateManager Instance;
    public GameObject player;

    void Awake()
    {
        Instance = this;
    }
}
