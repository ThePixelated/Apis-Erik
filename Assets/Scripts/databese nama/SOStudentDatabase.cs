using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SOStudentDatabase", menuName = "Scriptable Objects/SOStudentDatabase")]
public class SOStudentDatabase : ScriptableObject
{
    public List<StudentData> students =
        new List<StudentData>();
}