
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Please Name <EventSource><EventType>
/// </summary>
public abstract class StreamerbotTrigger
{

    public StreamerbotTrigger(Dictionary data)
    {
        CreateDataObject(data);
    }

    public abstract void CreateDataObject(Dictionary data);
}

/* =========== COMMAND DATA STRUCTURE ====================
==========================================================
    {
        "data": { ...
        },
        "event": {
            "source": " ",
            "type": " "
        },
        "timeStamp": "2025-05-03T19:39:19.7509626+02:00"
    }*/

