using UnityEngine;

public class ParentClass : MonoBehaviour
{
    public int Fld_Pub_gObj;    // [....]
    [SerializeField] private int Fld_SFPriv_gObj;   // [....]
    private int Fld_Priv_gObj;

    public int Prop_Pub_gObj_PubSet { get; set; }
    public int Prop_Pub_gObj_PrivSet { get; private set; }

    private int Ref_Fld_Priv_gObj;
    private int Ref_Fld_Pub_gObj;
    public int Ref_Prop_Pub_gObj_PubSet { get { return Ref_Fld_Priv_gObj; } set { Ref_Fld_Priv_gObj = value; } }
    public int Ref_Prop_Pub_gObj_PrivSet { get { return Ref_Fld_Pub_gObj; } private set { Ref_Fld_Pub_gObj = value; } }
    public DB_Kalimat TestingProp { get; set; }

    private void PrivMethodUpdate(int val1, int val2, int val3, int val4, int val5, int val6, int val7)
    {
        Fld_Pub_gObj = val1;
        Fld_SFPriv_gObj = val2;
        Fld_Priv_gObj = val3;
        Prop_Pub_gObj_PubSet = val4;
        Prop_Pub_gObj_PrivSet = val5;
        Ref_Prop_Pub_gObj_PubSet = val6;
        Ref_Prop_Pub_gObj_PrivSet = val7;

        ProviderClass.instance.DoMethod(Fld_Pub_gObj, Fld_SFPriv_gObj, Fld_Priv_gObj, Prop_Pub_gObj_PubSet, Prop_Pub_gObj_PrivSet, Ref_Prop_Pub_gObj_PubSet, Ref_Prop_Pub_gObj_PrivSet);
    }

    public void PMethodUpdate(int val1, int val2, int val3, int val4, int val5, int val6, int val7)
    {
        Fld_Pub_gObj = val1;
        Fld_SFPriv_gObj = val2;
        Fld_Priv_gObj = val3;
        Prop_Pub_gObj_PubSet = val4;
        Prop_Pub_gObj_PrivSet = val5;
        Ref_Prop_Pub_gObj_PubSet = val6;
        Ref_Prop_Pub_gObj_PrivSet = val7;

        ProviderClass.instance.DoMethod(Fld_Pub_gObj, Fld_SFPriv_gObj, Fld_Priv_gObj, Prop_Pub_gObj_PubSet, Prop_Pub_gObj_PrivSet, Ref_Prop_Pub_gObj_PubSet, Ref_Prop_Pub_gObj_PrivSet);
    }

    [SerializeField] private int _val1;
    [SerializeField] private int _val2;
    [SerializeField] private int _val3;
    [SerializeField] private int _val4;
    [SerializeField] private int _val5;
    [SerializeField] private int _val6;
    [SerializeField] private int _val7;
    public void Child_Field_Test()
    {
        ProviderClass.instance.DoMethod(_val1, _val2, _val3, _val4, _val5, _val6, _val7);
    }
}
