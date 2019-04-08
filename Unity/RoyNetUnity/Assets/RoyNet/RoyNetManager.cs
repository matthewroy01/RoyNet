﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// need this for plugin
using System.Runtime.InteropServices;

public class RoyNetManager : MonoBehaviour
{
    const string DLL_NAME = "RoyNetPlugin";

    [DllImport(DLL_NAME)]
    static extern int testAdd(int a, int b);

    [DllImport(DLL_NAME)]
    static extern int rnStart(int isServer);

    [DllImport(DLL_NAME)]
    static extern int rnUpdate();

    [DllImport(DLL_NAME)]
    static extern int rnStop();

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
        rnStart(1);

        // start updating
        StartCoroutine(NetworkUpdate());

        DebugMessage(testAdd(1, 2).ToString());
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
            rnUpdate();

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

    private void OnApplicationQuit()
    {
        rnStop();
    }
}

public enum Model { push, share, merge };