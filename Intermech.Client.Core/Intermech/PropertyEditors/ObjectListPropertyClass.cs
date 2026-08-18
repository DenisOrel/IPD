
// Type: Intermech.PropertyEditors.ObjectListPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.PropertyEditors;

/// <summary>Список ObjectPropertyClass</summary>
public class ObjectListPropertyClass
{
  public List<ObjectPropertyClass> ObjectPropertyClassList { get; set; }

  public List<long> ObjectIDList
  {
    get
    {
      List<long> objectIdList = new List<long>();
      if (this.ObjectPropertyClassList != null)
      {
        for (int index = 0; index < this.ObjectPropertyClassList.Count; ++index)
        {
          if (this.ObjectPropertyClassList[index] != null)
            objectIdList.Add(this.ObjectPropertyClassList[index].ObjectID);
        }
      }
      return objectIdList;
    }
    set
    {
      this.ObjectPropertyClassList = new List<ObjectPropertyClass>();
      if (value == null)
        return;
      for (int index = 0; index < value.Count; ++index)
        this.ObjectPropertyClassList.Add(new ObjectPropertyClass(value[index]));
    }
  }

  public ObjectListPropertyClass()
  {
  }

  public ObjectListPropertyClass(List<long> aObjectIDs) => this.ObjectIDList = aObjectIDs;

  public ObjectListPropertyClass(ObjectPropertyClass[] aObjectIDs)
  {
    this.ObjectPropertyClassList = new List<ObjectPropertyClass>((IEnumerable<ObjectPropertyClass>) aObjectIDs);
  }

  public override string ToString()
  {
    string empty = string.Empty;
    for (int index = 0; index < this.ObjectPropertyClassList.Count; ++index)
    {
      empty += this.ObjectPropertyClassList[index].ToString();
      if (index >= 0 && index < this.ObjectPropertyClassList.Count - 1)
        empty += ";";
    }
    return empty;
  }
}
