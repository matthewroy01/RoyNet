using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq.Expressions;

public class Replicator : MonoBehaviour
{
    public List<MyMemberInfo> members = new List<MyMemberInfo>();
    public List<string> classNames = new List<string>();

    Component[] comps;

    private string GetTypeAsString(MemberInfo member)
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

    public void ReflectMembers()
    {
        ClearMembers();

        comps = GetComponents(typeof(Component));

        for (int i = 0; i < comps.Length; ++i)
        {
            // exclude Replicator from the list
            if (comps[i].GetType().Name != "Replicator")
            {
                // save name of component for display purposes
                classNames.Add(comps[i].GetType().Name);

                // get type of component and save its member info
                System.Type type = comps[i].GetType();
                MemberInfo[] info = type.GetMembers();

                for (int j = 0; j < info.Length; ++j)
                {
                    // MemberInfo:                  displays the type and name of the member with a space between them (I.E. "System.single speed")
                    // MemberInfo.Name:             displays the name of the member
                    // MemberInfo.MemberType:       displays the type of member (I.E., field or method)
                    // MemberInfo.ReflectedType:    displays the type that the member belongs to (I.E., the class name)
                    // MemberInfo.DeclaringType:    the type that originally declared the member (either the class from ReflectedType or Object for more global members)
                    // MemberInfo.Module:           the source of the type, usually a DLL
                    // MemberInfo.MetadataToken:    an unique ID for metadata
                    if (info[j].DeclaringType.Name != "Object"
                        && info[j].DeclaringType.Name != "Component"
                        && info[j].DeclaringType.Name != "MonoBehaviour" // for extra things like GUILayout
                        && info[j].DeclaringType.Name != "Behaviour" // for extra things like IsActiveAndEnabled
                        && (info[j].MemberType == MemberTypes.Property
                        || info[j].MemberType == MemberTypes.Field))
                    {
                        Debug.Log(info[j].Name + "\nMemberType: " + info[j]);

                        string typeAsString = GetTypeAsString(info[j]);

                        object ptr = null;
                        switch(info[j])
                        {
                            case FieldInfo fieldInfo:
                            {
                                ptr = fieldInfo.GetValue(comps[i]);
                                break;
                            }
                            case PropertyInfo propertyInfo:
                            {
                                ptr = propertyInfo.GetValue(comps[i]);
                                break;
                            }
                        }

                        members.Add(new MyMemberInfo(ptr, info[j].Name, typeAsString, Type.GetType(typeAsString), info[j].MemberType.ToString(), classNames[i]));
                    }
                }
            }
        }

        Debug.Log("");
    }

    private void ClearMembers()
    {
        members.Clear();
        classNames.Clear();
    }
}

[System.Serializable]
public struct MyMemberInfo
{
    public MyMemberInfo(object ptr, string n, string tn, System.Type t, string tno, string o)
    {
        send = false;
        deadReckon = false;

        pointer = ptr;

        name = n;
        typeName = tn;
        type = t;
        typeNameOrigin = tno;
        owner = o;
    }

    public bool send;
    public bool deadReckon;
    public string name;
    public string typeName;
    public System.Type type;
    public string typeNameOrigin;
    public string owner;
    public object pointer;
}