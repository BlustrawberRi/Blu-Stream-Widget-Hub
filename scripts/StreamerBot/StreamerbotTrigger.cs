
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

    public string DataToString(Dictionary data, string[] path)
    {
        Dictionary next = data;
        string value = "";
        for (int i = 0; i < path.Length; i++)
        {
            try
            {
                if (i == path.Length - 1)
                {
                    value = next.GetValueOrDefault(path[i]).AsString();
                    break;
                }
                next = next.GetValueOrDefault(path[i]).AsGodotDictionary();
            }
            catch (ArgumentNullException e)
            {
                GD.PrintErr("Could not translate streamer bot event data to object: " + e);
                return null;
            }
        }
        return value;
    }
    public float? DataToFloat(Dictionary data, string[] path)
    {
        Dictionary next = data;
        float? value = null;
        for (int i = 0; i < path.Length; i++)
        {
            try
            {
                if (i == path.Length - 1)
                {
                    value = (float)next.GetValueOrDefault(path[i]).AsDouble();
                    break;
                }
                next = next.GetValueOrDefault(path[i]).AsGodotDictionary();
            }
            catch (ArgumentNullException e)
            {
                GD.PrintErr("Could not translate streamer bot event data to object: " + e);
                return null;
            }
        }
        return value;
    }

    public int? DataToInt(Dictionary data, string[] path)
    {
        Dictionary next = data;
        int? value = null;
        for (int i = 0; i < path.Length; i++)
        {
            try
            {
                if (i == path.Length - 1)
                {
                    value = next.GetValueOrDefault(path[i]).AsInt32();
                    break;
                }
                next = next.GetValueOrDefault(path[i]).AsGodotDictionary();
            }
            catch (ArgumentNullException e)
            {
                GD.PrintErr("Could not translate streamer bot event data to object: " + e);
                return null;
            }
        }
        return value;
    }

    public bool? DataToBool(Dictionary data, string[] path)
    {
        Dictionary next = data;
        bool? value = null;
        for (int i = 0; i < path.Length; i++)
        {
            try
            {
                if (i == path.Length - 1)
                {
                    value = next.GetValueOrDefault(path[i]).AsBool();
                    break;
                }
                next = next.GetValueOrDefault(path[i]).AsGodotDictionary();
            }
            catch (ArgumentNullException e)
            {
                GD.PrintErr("Could not translate streamer bot event data to object: " + e);
                return null;
            }
        }
        return value;
    }
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

