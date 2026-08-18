// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPSessionGuidHolder
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Класс, хранящий Guid серверной сессии, в рамках которой работает какая-либо очередь заданий
/// </summary>
public class MRPSessionGuidHolder
{
  /// <summary>Управление доступностью сессии</summary>
  public volatile bool Enabled = true;
  /// <summary>
  /// Guid серверной сессии, в рамках которой работает какая-либо очередь заданий
  /// </summary>
  private Guid sessionGuid;

  /// <summary>
  /// Guid серверной сессии, в рамках которой работает какая-либо очередь заданий
  /// </summary>
  public Guid SessionGuid
  {
    [DebuggerStepThrough] get => !this.Enabled ? Guid.Empty : this.sessionGuid;
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="sessionGuid">Guid серверной сессии, в рамках которой работает какая-либо очередь заданий</param>
  public MRPSessionGuidHolder(Guid sessionGuid) => this.sessionGuid = sessionGuid;
}
