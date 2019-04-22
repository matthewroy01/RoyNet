using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoyNetConsts
{
    public static int PACKET_MAX;
}

public struct Packet
{
    public Packet(int newID)
    {
        ID = newID;
        objects = new List<object>();
    }

    public int ID;
    public List<object> objects;
}

public struct PacketRaw
{
    public int ID;
    public uint size;
    public char[] data;
}