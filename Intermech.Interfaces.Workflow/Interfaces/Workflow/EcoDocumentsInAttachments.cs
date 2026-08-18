// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.EcoDocumentsInAttachments
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

[Serializable]
public class EcoDocumentsInAttachments
{
  /// <summary>Идентификатор объекта Eco</summary>
  public long EcoObjectID { get; set; }

  /// <summary>
  /// Словарь с требуемым типом данных и уровнем продвижения на который его запросили перевести
  /// </summary>
  public Dictionary<int, int> TypeToLCLevel { get; set; }

  /// <summary>
  /// Словарь с требуемым типом данных и Шагом ЖЦ на который его запросили перевести
  /// </summary>
  public Dictionary<int, int> TypeToLCStep { get; set; }

  public EcoDocumentsInAttachments()
  {
    this.TypeToLCLevel = new Dictionary<int, int>();
    this.TypeToLCStep = new Dictionary<int, int>();
  }
}
