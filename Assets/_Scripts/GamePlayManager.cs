using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    void Start()
    {
        playerController.Setup();
    }
    void Update()
    {
        
    }
}
