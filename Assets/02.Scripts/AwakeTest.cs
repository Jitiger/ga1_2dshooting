using System;
using UnityEngine;

public class AwakeTest : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log($"[Awake] {gameObject}");
    }

    private void Start()
    {
        Debug.Log($"[Awake] {gameObject}");
    }

    private void Update()
    {
        Debug.Log($"[Awake] {gameObject}");
    }
}