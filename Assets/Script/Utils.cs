using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{

}

public class EnumNamedListAttribute : PropertyAttribute
{
    public string[] names;
    public EnumNamedListAttribute(System.Type names_enum_type)
    {
        this.names = System.Enum.GetNames(names_enum_type);
    }
}
