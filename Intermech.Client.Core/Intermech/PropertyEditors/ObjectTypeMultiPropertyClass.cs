
// Type: Intermech.PropertyEditors.ObjectTypeMultiPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

[Editor(typeof (ObjectTypeMultiEditor), typeof (UITypeEditor))]
public class ObjectTypeMultiPropertyClass
{
  private List<ObjectTypePropertyClass> objectTypePropertyClassList;

  public List<ObjectTypePropertyClass> ObjectTypePropertyClassList
  {
    get => this.objectTypePropertyClassList;
  }

  public List<int> ObjectTypeList
  {
    get
    {
      List<int> objectTypeList = new List<int>();
      if (this.objectTypePropertyClassList != null)
      {
        for (int index = 0; index < this.objectTypePropertyClassList.Count; ++index)
          objectTypeList.Add(this.objectTypePropertyClassList[index].ObjectType);
      }
      return objectTypeList;
    }
  }

  public ObjectTypeMultiPropertyClass(List<int> aObjectTypeIDs)
  {
    this.objectTypePropertyClassList = new List<ObjectTypePropertyClass>();
    for (int index = 0; index < aObjectTypeIDs.Count; ++index)
      this.objectTypePropertyClassList.Add(new ObjectTypePropertyClass(aObjectTypeIDs[index]));
  }

  public override string ToString()
  {
    string empty = string.Empty;
    for (int index = 0; index < this.objectTypePropertyClassList.Count; ++index)
    {
      empty += this.objectTypePropertyClassList[index].ToString();
      if (index >= 0 && index < this.objectTypePropertyClassList.Count - 1)
        empty += ";";
    }
    return empty;
  }
}
