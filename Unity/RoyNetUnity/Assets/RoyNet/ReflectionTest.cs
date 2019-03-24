using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

// using GetIterator(), might be helpful? https://answers.unity.com/questions/1262360/custom-inspector-for-dynamic-scriptable-objects.html

public class ReflectionTest : MonoBehaviour
{
    private System.Object ignoreObject;

    public List<MemberInfoTest> members;

    private void Awake()
    {
        members = new List<MemberInfoTest>();
        ignoreObject = this;
        LogMembers();
    }

    void Start()
    {
        /*System.Reflection.MemberInfo info = typeof(PlayerStatus);

        object[] attributes = info.GetCustomAttributes(true);

        for (int i = 0; i < attributes.Length; ++i)
        {
            Debug.Log(attributes[i]);
        }*/

        /*int i = 42;
        PlayerStatus tmp = new PlayerStatus();
        System.Type type = i.GetType();
        Debug.Log(type);
        type = tmp.GetType();
        Debug.Log(type);*/

        //PlayerStatus status = new PlayerStatus();
    }

    public string GetTypeAsString(MemberInfo member)
    {
        string result = "";
        int i = 0;
        while (member.ToString()[i] != ' ')
        {
            result += member.ToString()[i];
            ++i;
        }
        return result;
    }

    public string GetVariableName<T>(Expression<Func<T>> expr)
    {
        var body = (MemberExpression)expr.Body;
        return body.Member.Name;
    }

    public void LogMembers()
    {
        Test test = new Test();

        System.Type type = test.GetType();

        MemberInfo[] info = type.GetMembers();

        for (int i = 0; i < info.Length; ++i)
        {
            // MemberInfo:                  displays the type and name of the member with a space between them (I.E. "System.single speed")
            // MemberInfo.Name:             displays the name of the member
            // MemberInfo.MemberType:       displays the type of member (I.E., field or method)
            // MemberInfo.ReflectedType:    displays the type that the member belongs to (I.E., the class name)
            // MemberInfo.DeclaringType:    the type that originally declared the member (either the class from ReflectedType or Object for more global members)
            // MemberInfo.Module:           the source of the type, usually a DLL
            // MemberInfo.MetadataToken:    an unique ID for metadata
            if (info[i].DeclaringType.Name != "Object"
                && info[i].MemberType == MemberTypes.Field)
            {
                Debug.Log(info[i].Name + "\nMemberType: " + info[i]);

                string typeAsString = GetTypeAsString(info[i]);
                members.Add(new MemberInfoTest(info[i].Name, typeAsString, Type.GetType(typeAsString)));
            }
        }

        Debug.Log("");
    }

    public void ClearMembers()
    {
        members.Clear();
    }
}

[System.Serializable]
public class Test
{
    public int health;
    public float speed;
    public Test test;

    public int GetHealth()
    {
        return health;
    }
}

[System.Serializable]
public struct MemberInfoTest
{
    public MemberInfoTest(string n, string tn, System.Type t)
    {
        name = n;
        typeName = tn;
        type = t;
        deadReckon = false;
    }

    public string name;
    public string typeName;
    public System.Type type;
    public bool deadReckon;
}