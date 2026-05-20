using UnityEngine;

public class ChildClass_B : ParentClass
{
    [SerializeField] private int numbering = 2;

    void Start()
    {
        // Get
        int placeholder;
        placeholder = Fld_Pub_gObj;
        //placeholder = Fld_SFPriv_gObj;          // priv
        //placeholder = Fld_Priv_gObj;            // priv
        placeholder = Prop_Pub_gObj_PubSet;
        placeholder = Prop_Pub_gObj_PrivSet;
        placeholder = Ref_Prop_Pub_gObj_PubSet;
        placeholder = Ref_Prop_Pub_gObj_PrivSet;


        // Set
        Fld_Pub_gObj = 0;
        //Fld_SFPriv_gObj = 7;            // priv
        //Fld_Priv_gObj = 10;             // priv
        Prop_Pub_gObj_PubSet = 7;
        //Prop_Pub_gObj_PrivSet = 3;      // Priv Set
        Ref_Prop_Pub_gObj_PubSet = 2;
        //Ref_Prop_Pub_gObj_PrivSet = 6;  // Priv Set
    }


    public void SubmitBtn()
    {
        //PrivMethodUpdate();       // Priv

        PMethodUpdate(numbering, numbering, numbering, numbering, numbering, numbering, numbering);
    }
}
