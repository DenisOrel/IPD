
// Type: Intermech.Navigator.SelectionView.SelFormResult
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Navigator.SelectionView;

/// <summary>Class describing dialog result</summary>
public class SelFormResult
{
  public string shortName;
  public string longName;
  public int ID;
  public string GUID;

  public SelFormResult()
  {
  }

  public SelFormResult(string sName, string lName, int ID, string GUID)
  {
    this.shortName = sName;
    this.longName = lName;
    this.ID = ID;
    this.GUID = GUID;
  }

  public SelFormResult(IUserSession ius, bool attr, string GUID)
  {
    this.GUID = GUID;
    if (attr)
    {
      IDBAttributeType attributeType = ius.GetAttributeType(new Guid(GUID), false);
      if (attributeType == null)
        return;
      this.shortName = attributeType.ShortName;
      this.longName = attributeType.Name;
      this.ID = attributeType.AttributeID;
    }
    else
    {
      IDBObjectType objectType = ius.GetObjectType(new Guid(GUID), false);
      if (objectType == null)
        return;
      this.shortName = objectType.ObjectTypeShortName;
      this.longName = objectType.ObjectTypeName;
      this.ID = objectType.ObjectType;
    }
  }
}
