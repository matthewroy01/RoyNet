using System.Collections;
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
    public List<Replicator> replicated = new List<Replicator>();

    public List<Packet> packets = new List<Packet>();

    private void Awake()
    {
        // find and store all replicators in the scene
        GetReplicators();
    }

    void Start()
    {
        // start networking
        rnStart(1);

        // start updating
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
            replicated[i].ReflectMembers();

            for (int j = 0; j < repl[i].members.Count; ++j)
            {
                repl[i].members[j].SetSend(repl[i].toSend[j]);

                // if we find a true value
                if (replicated[i].members[j].send == true)
                {
                    // create a new packet if there isn't already one with this ID
                    Packet tmp = AddNewPacket(i);

                    tmp.objects.Add(replicated[i].members[j].pointerProperty.GetValue(replicated[i].members[j].comp));
                }
            }
        }

        DebugMessage("Found " + replicated.Count + " Replicators in the scene.");
    }

    private IEnumerator NetworkUpdate()
    {
        while (doNetworking)
        {
            DebugMessage("network update");
            DebugReplicators();
            rnUpdate();

            yield return new WaitForSeconds(delayBetweenUpdates);
        }
    }

    private Packet AddNewPacket(int ID)
    {
        // see if a packet with the given ID has already been created
        for (int i = 0; i < packets.Count; ++i)
        {
            if (packets[i].ID == ID)
            {
                return packets[i];
            }
        }

        // make a new packet
        Packet tmp = new Packet(ID);
        // add it to the list
        packets.Add(tmp);

        return packets[packets.Count - 1];
    }

    private void DebugReplicators()
    {
        for (int i = 0; i < packets.Count; ++i)
        {
            for (int j = 0; j < packets[i].objects.Count; ++j)
            {
                DebugMessage(packets[i].objects[j].ToString());
            }
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