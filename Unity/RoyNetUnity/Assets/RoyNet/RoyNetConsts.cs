using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameMessages
{
    ID_TEST_TRANSFORM = 137,
};

public static class RoyNetConsts
{
    public static int PACKET_MAX;
}

public struct Msg_TestTransform
{
    public Msg_TestTransform(int id, Vector3 pos, Vector3 rot)
    {
        px = pos.x;
        py = pos.y;
        pz = pos.z;

        rx = rot.x;
        ry = rot.y;
        rz = rot.z;

        ID = id;
    }

    int ID;
    float px, py, pz;
    float rx, ry, rz;
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