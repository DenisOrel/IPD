// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.GlobalType
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

[Serializable]
public sealed class GlobalType
{
  /// <summary>Гуид типа</summary>
  public Guid TypeGuid = Guid.Empty;
  /// <summary>Наименование типа</summary>
  public string TypeName = string.Empty;
  /// <summary>ID типа</summary>
  public int TypeID = -1;

  /// <summary>Конструктор</summary>
  /// <param name="guid">Гуид типа</param>
  /// <param name="category">Категория</param>
  /// <param name="session"></param>
  public GlobalType(string guid, int category, IUserSession session)
  {
    this.TypeGuid = new Guid(guid);
    switch (category)
    {
      case 3:
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.TypeGuid);
        if (attributeType == null)
          break;
        this.TypeName = attributeType.Name;
        this.TypeID = attributeType.AttributeID;
        break;
      case 4:
        IDBObjectType objectType = session.GetObjectType(this.TypeGuid, false);
        if (objectType == null)
          break;
        this.TypeName = objectType.ObjectTypeName;
        this.TypeID = objectType.ObjectType;
        break;
      case 6:
        IDBRelationType relationType = session.GetRelationType(this.TypeGuid, false);
        if (relationType == null)
          break;
        this.TypeName = relationType.Description;
        this.TypeID = relationType.RelationType;
        break;
    }
  }

  public GlobalType(int id, int category)
  {
    this.TypeID = id;
    switch (category)
    {
      case 3:
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(id);
        this.TypeName = attributeType.Name;
        this.TypeGuid = attributeType.AttributeGuid;
        break;
      case 4:
        IMSObjectType objectType = MetaDataHelper.GetObjectType(id);
        this.TypeGuid = objectType.Guid;
        this.TypeName = objectType.ObjectTypeName;
        break;
      case 6:
        IMSRelationType relationType = MetaDataHelper.GetRelationType(id);
        this.TypeName = relationType.TypeName;
        this.TypeGuid = relationType.Guid;
        break;
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="id">id типа</param>
  /// <param name="category">Категория</param>
  /// <param name="session"></param>
  public GlobalType(int id, int category, IUserSession session)
  {
    this.TypeID = id;
    switch (category)
    {
      case 3:
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.TypeID);
        if (attributeType == null)
          break;
        this.TypeName = attributeType.Name;
        this.TypeGuid = attributeType.AttributeGuid;
        break;
      case 4:
        IDBObjectType objectType = session.GetObjectType(this.TypeID, false);
        if (objectType == null)
          break;
        this.TypeName = objectType.ObjectTypeName;
        this.TypeGuid = (objectType as IDBGuid).GUID;
        break;
      case 6:
        IDBRelationType relationType = session.GetRelationType(this.TypeID, false);
        if (relationType == null)
          break;
        this.TypeName = relationType.Description;
        this.TypeGuid = (relationType as IDBGuid).GUID;
        break;
    }
  }

  public override string ToString()
  {
    return this.TypeID == -1 || this.TypeName == string.Empty ? $"{{{this.TypeGuid}}}" : this.TypeName;
  }
}
