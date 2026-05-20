using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SO_QuestionData", menuName = "Scriptable Objects/SO_QuestionData")]
public class SO_QuestionData : ScriptableObject
{
   public List<QuestionData> questions; 
}
