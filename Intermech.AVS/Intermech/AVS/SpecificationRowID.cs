// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecificationRowID
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS;

/// <summary>Идентификаторы строки спецификации</summary>
[Serializable]
public class SpecificationRowID
{
  private long objId = -1;
  private Guid objGuid = Guid.Empty;
  private int objType = -1;
  private long relId = -1;
  private int relType = -1;

  /// <summary>Идентификатор объекта</summary>
  public long ObjId
  {
    [DebuggerStepThrough] get => this.objId;
  }

  /// <summary>Глобальный идентификатор объекта</summary>
  public Guid ObjGuid
  {
    [DebuggerStepThrough] get => this.objGuid;
  }

  /// <summary>Тип объекта</summary>
  public int ObjType
  {
    [DebuggerStepThrough] get => this.objType;
  }

  /// <summary>Идентификатор связи</summary>
  public long RelId
  {
    [DebuggerStepThrough] get => this.relId;
  }

  /// <summary>Тип связи</summary>
  public int RelType
  {
    [DebuggerStepThrough] get => this.relType;
  }

  /// <summary>Конструктор</summary>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <param name="_objType">Тип объекта</param>
  /// <param name="relId">Идентификатор связи</param>
  /// <param name="relType">Тип связи</param>
  public SpecificationRowID(long objId, Guid objGuid, int objType, long relId, int relType)
  {
    this.objId = objId;
    this.objGuid = objGuid;
    this.objType = objType;
    this.relId = relId;
    this.relType = relType;
  }

  /// <summary>Конструктор</summary>
  /// <param name="specRow">Базовый объект из которого копируются данные</param>
  public SpecificationRowID(SpecificationRowID specRow)
  {
    this.objId = specRow.objId;
    this.objGuid = specRow.objGuid;
    this.objType = specRow.objType;
    this.relId = specRow.relId;
    this.relType = specRow.relType;
  }
}
