using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyNetManager : MonoBehaviour
{
    [Header("Networking Model")]
    public Model model;
    public bool externalServerExe;

    [Header("Objects for Replication")]
    public List<Replicator> replicated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Model { push, share, merge };