using UnityEngine;

public class ProviderClass : MonoBehaviour
{
    public static ProviderClass instance;

    private void Awake()
    {
        instance = this;
    }

    public void DoMethod(int val1, int val2, int val3, int val4, int val5, int val6, int val7)
    {
        Debug.LogWarning("Val1: " + val1 + "\n" + "Val2: " + val2 + "\n" + "Val3: " + val3 + "\n" + "Val4: " + val4 + "\n" + "Val5: " + val5 + "\n" + "Val6: " + val6 + "\n" + "Val7: " + val7);
    }
}
