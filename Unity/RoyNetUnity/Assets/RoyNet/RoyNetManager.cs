using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyNetManager : MonoBehaviour
{
    public bool doNetworking;
    public bool doDebugMessages;

    [Header("Networking Model")]
    public Model model;
    public bool externalServerExe;

    [Header("Network speed")]
    public float delayBetweenUpdates;

    [Header("Objects for Replication")]
    public List<Replicator> replicated;

    void Start()
    {
        // find and store all replicators in the scene
        GetReplicators();

        // start networking
        StartCoroutine(NetworkUpdate());
    }

    private void GetReplicators()
    {
        // store replicator objects in a temporary array
        Replicator[] repl = FindObjectsOfType<Replicator>();

        // put the replicators from the array in our list
        for (int i = 0; i < repl.Length; ++i)
        {
            replicated.Add(repl[i]);
        }

        DebugMessage("Found " + replicated.Count + " Replicators in the scene.");
    }

    private IEnumerator NetworkUpdate()
    {
        while (doNetworking)
        {
            DebugMessage("network update");

            yield return new WaitForSeconds(delayBetweenUpdates);
        }
    }

    private void DebugMessage(string msg)
    {
        if (doDebugMessages)
        {
            Debug.Log(msg);
        }
    }
}

public enum Model { push, share, merge };