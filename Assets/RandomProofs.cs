using System;
using UnityEngine;


public class RandomProofs : MonoBehaviour
{

    [SerializeField] private int numberOfProofs = 10;
    [SerializeField] private GameObject[] proofs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Shuffle(proofs);
        for (int i = 0; i < numberOfProofs; i++)
        {
            proofs[i].SetActive(true);
        }
    }

    // Easiest, but not thread safe
    private static System.Random random = new System.Random();

    private static void Shuffle<T>(T[] array)
    {
        if (array is null)
            throw new ArgumentNullException(nameof(array));

        for (int i = 0; i < array.Length - 1; ++i)
        {
            int r = random.Next(i, array.Length);
            (array[r], array[i]) = (array[i], array[r]);
        }
    }
}
