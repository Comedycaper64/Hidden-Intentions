using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] GameObject[] availableNPCs = new GameObject[10];
    [SerializeField] bool[] availableNPC = new bool[10];
    [SerializeField] Transform[] availablePositions = new Transform[5];
    [SerializeField] bool[] availablePosition = new bool[5];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < availableNPC.Length; i++)
        {
            if (i < 5)
            {
                availablePosition[i] = true;
            }
            availableNPC[i] = true;
        }
        InstantiateNPCs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateNPCs()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(availableNPCs[GetRandomNumber(true)], availablePositions[GetRandomNumber(false)]);
        }
    }

    private int GetRandomNumber(bool NPCNumber)
    {
        if (NPCNumber)
        {
            int randomNumber = Random.Range(0, 10);
            if (availableNPC[randomNumber])
            {
                availableNPC[randomNumber] = false;
                return randomNumber;
            }
            else
            {
                return GetRandomNumber(true);
            }
        }
        else
        {
            int randomNumber = Random.Range(0, 5);
            if (availablePosition[randomNumber])
            {
                availablePosition[randomNumber] = false;
                return randomNumber;
            }
            else
            {
                return GetRandomNumber(false);
            }
        }
    }
}
