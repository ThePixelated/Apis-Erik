using System;
using System.Collections.Generic;

[Serializable]
public class StudentData
{
    public string studentName;

    // history nilai
    public List<int> scores =
        new List<int>();
}