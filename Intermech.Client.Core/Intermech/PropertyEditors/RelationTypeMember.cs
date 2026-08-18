
// Type: Intermech.PropertyEditors.RelationTypeMember
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Localization;
using System;


namespace Intermech.PropertyEditors;

/// <summary>Для назначения applicability для ObjectType</summary>
public class RelationTypeMember : IComparable
{
  public int objType;
  public int relType;
  public string descr;
  public string name;
  public RelationKinds relKind;
  public bool isReversed;
  public bool isLoaded;
  public ObjTypeApplList objTypeApplList;

  public RelationTypeMember(
    int aObjType,
    int aRelType,
    string aDescr,
    string aName,
    RelationKinds aRelKind,
    bool aIsReversed)
  {
    this.objType = aObjType;
    this.objTypeApplList = new ObjTypeApplList();
    this.relType = aRelType;
    this.descr = aDescr;
    this.name = aName;
    this.relKind = aRelKind;
    this.isReversed = aIsReversed;
  }

  public bool Load()
  {
    this.objTypeApplList.Load(this);
    this.isLoaded = true;
    return true;
  }

  public override string ToString()
  {
    string str = this.descr;
    if (this.relKind == RelationKinds.Vertical)
      str = $"{str} - {this.name}";
    return str;
  }

  public int CompareTo(object obj)
  {
    return obj is RelationTypeMember ? this.descr.CompareTo(((RelationTypeMember) obj).descr) : throw new ArgumentException(LocalizationHolder.rm.GetString(sc_2451.ssp_imclient_2452()));
  }
}
