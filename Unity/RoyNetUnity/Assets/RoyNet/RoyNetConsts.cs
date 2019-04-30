using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class RoyNetConsts
{
    public static int PACKET_MAX;
}

public struct Packet
{
    public Packet(int newID)
    {
        ID = newID;
        objects = new List<Tuple<object, System.Type>>();
    }

    public int ID;
    public List<Tuple<object, System.Type>> objects;
}

public struct PacketRaw
{
    public int ID;
    public uint size;
    public char[] data;
}