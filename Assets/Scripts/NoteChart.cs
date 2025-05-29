using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class NoteData
{
    public float time;
    public int lane;
}
[Serializable]
public class NoteChart
{
    public List<NoteData> notes;
}