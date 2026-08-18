
// Type: Intermech.PropertyEditors.PropertyTabAttribute4OPG
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// перекрытый атрибут c возможностью определения нескольких TabPage
/// </summary>
internal class PropertyTabAttribute4OPG : PropertyTabAttribute
{
  public PropertyTabAttribute4OPG(Type[] tabTypes)
  {
    if (tabTypes == null)
      return;
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    for (int index = 0; index < tabTypes.Length; ++index)
    {
      if (typeof (ObjectPropertyGridTab).IsAssignableFrom(tabTypes[index]))
        arrayList1.Add((object) tabTypes[index]);
    }
    for (int index = 0; index < arrayList1.Count; ++index)
      arrayList2.Add((object) PropertyTabScope.Component);
    this.InitializeArrays((Type[]) arrayList1.ToArray(typeof (Type)), (PropertyTabScope[]) arrayList2.ToArray(typeof (PropertyTabScope)));
  }
}
