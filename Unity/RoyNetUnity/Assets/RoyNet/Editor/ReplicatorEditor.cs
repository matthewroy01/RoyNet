using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;

[CustomEditor(typeof(Replicator))]
public class ReplicatorEditor : Editor
{
    private System.Type[] deadReckonTypes = { typeof(float), typeof(Vector2), typeof(Vector3), typeof(Vector4) };
    bool[] foldouts;

    private List<string> componentNames;

    private void Start()
    {
        componentNames = new List<string>();
    }

    public override void OnInspectorGUI()
    {
        Replicator targ = (Replicator)target;

        EditorGUILayout.LabelField("ID: " + targ.gameObject.name);

        if (GUILayout.Button("Get Members"))
        {
            targ.ReflectMembers();
            foldouts = new bool[targ.classNames.Count];
        }

        // loop through all components
        for(int i = 0; i < targ.classNames.Count; ++i)
        {
            foldouts[i] = EditorGUILayout.Foldout(foldouts[i], targ.classNames[i]);

            // if the foldout is active
            if (foldouts[i])
            {
                // loop through all of the members
                for (int j = 0; j < targ.members.Count; ++j)
                {
                    MyMemberInfo tmp = targ.members[j];

                    // check if that member matched the current foldout
                    if (targ.members[j].owner == targ.classNames[i])
                    {
                        // set send bool
                        tmp.send = EditorGUILayout.Toggle(tmp.name, tmp.send);

                        // set dead reckon bool if the type is dead reckonable
                        if (CheckDeadReckonType(tmp.type))
                        {
                            tmp.deadReckon = EditorGUILayout.Toggle("dead reckon?", tmp.deadReckon);
                        }

                        // add a space for neatness
                        EditorGUILayout.Space();
                    }

                    // editing structs this way only creates a copy so we have to set the changes back into the original
                    targ.members[j] = tmp;
                }
            }
        }

        Repaint();
    }

    private bool CheckDeadReckonType(System.Type type)
    {
        for (int i = 0; i < deadReckonTypes.Length; ++i)
        {
            if (type == deadReckonTypes[i])
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckDeadReckonType(string type)
    {
        for (int i = 0; i < deadReckonTypes.Length; ++i)
        {
            if (Type.GetType(type) == deadReckonTypes[i])
            {
                return true;
            }
        }
        return false;
    }
}
