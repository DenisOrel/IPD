// Decompiled with JetBrains decompiler
// Type: Intermech.ISessionProvider
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;

#nullable disable
namespace Intermech;

/// <summary>Класс, позволяющий объектам обращаться к сессии, независимо от того, где эти объекты находятся - на клиенте, или сервере</summary>
public interface ISessionProvider
{
  /// <summary>Обязательно должно вызываться в паре с ReleaseSession! </summary>
  [NotNull]
  IUserSession GetSession();

  /// <summary>Обязательно должно вызываться в паре с GetSession! </summary>
  bool ReleaseSession();
}
