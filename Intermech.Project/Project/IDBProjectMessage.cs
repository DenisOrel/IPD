// Decompiled with JetBrains decompiler
// Type: Intermech.Project.IDBProjectMessage
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.Project;

/// <summary>IMProject mail notification</summary>
[DBObjectTypeHandler("cadd91f6-306c-11d8-b4e9-00304f19f545", true)]
public interface IDBProjectMessage : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
{
  /// <summary>Идентификатор задачи IMProject</summary>
  [NotEmpty]
  long TaskID { get; }

  /// <summary>Задача IMProject, по которой выпущено сообщение</summary>
  [NotNull]
  IDBProjectTask Task { get; }
}
