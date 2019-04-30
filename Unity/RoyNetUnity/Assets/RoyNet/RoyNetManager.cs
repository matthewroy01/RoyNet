using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

// need this for plugin
using System.Runtime.InteropServices;

public class RoyNetManager : MonoBehaviour
{
    const string DLL_NAME = "RoyNetPlugin";

    [DllImport(DLL_NAME)]
    static extern int testAdd(int a, int b);

    [DllImport(DLL_NAME)]
    static extern int sendTestTransform(Msg_TestTransform msg);

    [DllImport(DLL_NAME)]
    static extern int rnStart(int isServer);

    [DllImport(DLL_NAME)]
    static extern int rnUpdate(Msg_TestTransform data);

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

        PacketToCharArray();
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

                    tmp.objects.Add(new Tuple<object, Type>(replicated[i].members[j].pointerProperty.GetValue(replicated[i].members[j].comp), (replicated[i].members[j].type)));
                    Debug.Log("");
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

            for (int i = 0; i < replicated.Count; ++i)
            {
                Msg_TestTransform test = new Msg_TestTransform((int)GameMessages.ID_TEST_TRANSFORM, replicated[i].transform.position, replicated[i].transform.eulerAngles);
                rnUpdate(test);
            }

            DebugMessage("Position is: " + packets[0].objects[0].ToString());

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

    private char[] PacketToCharArray()//Packet pack)
    {
        float test1 = 0.1f;

        char[] testArray = new char[256];
        testArray[0] = System.Convert.ToChar(test1);

        int index = 0;
        float test2 = (float)testArray[index];

        Debug.Log(test2);

        return testArray;
    }

    private PacketRaw PacketToPacketRaw(Packet pack)
    {
        PacketRaw tmp = new PacketRaw();

        // set ID
        tmp.ID = pack.ID;

        // set size
        tmp.size = (uint)System.Runtime.InteropServices.Marshal.SizeOf(pack);

        // set data by converting Packet data to a char array
        byte[] bytes = ObjectToByteArray(pack.objects);
        System.Convert.ToBase64CharArray(bytes, 0, bytes.Length, tmp.data, 0);

        return tmp;
    }

    public static byte[] ObjectToByteArray(object obj)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
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