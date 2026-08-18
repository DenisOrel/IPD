// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.NodeInfo
// Assembly: Intermech.Interfaces.MaterialsHandbook, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C75FAC17-15DB-4F73-814B-B278FC9C1B73
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MaterialsHandbook;

/// <summary>
/// 
/// </summary>
[Serializable]
public class NodeInfo
{
  public int ID = -1;
  public Guid Guid = Guid.Empty;
  public string Caption = string.Empty;

  /// <summary>Конструктор.</summary>
  /// <param name="g">Глобальный идентификатор узла</param>
  /// <param name="id">Идентификатор узла</param>
  /// <param name="caption">Наименование узла</param>
  public NodeInfo(Guid g, int id, string caption)
  {
    this.Guid = g;
    this.ID = id;
    this.Caption = caption;
  }
}
