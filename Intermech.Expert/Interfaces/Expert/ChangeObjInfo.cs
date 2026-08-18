// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.ChangeObjInfo
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>
/// Информация об изменениях объектов в структуре комплекта
/// </summary>
[Serializable]
public class ChangeObjInfo : ChangeInfo
{
  /// <summary>Конструктор</summary>
  /// <param name="id"></param>
  /// <param name="typeID"></param>
  public ChangeObjInfo(long id, int typeID)
    : this(id, typeID, DocOperType.Created)
  {
    this._elemType = AttributableElements.Object;
  }

  /// <summary>Конструктор</summary>
  /// <param name="id"></param>
  /// <param name="typeID"></param>
  /// <param name="operType"></param>
  public ChangeObjInfo(long id, int typeID, DocOperType operType)
    : base(id, typeID, operType)
  {
    this._elemType = AttributableElements.Object;
  }
}
