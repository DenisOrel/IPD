// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SessionPoolThreadKey
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Класс ключей для идентификации потоков, к которым привязаны пользовательские сессии.
/// </summary>
public sealed class SessionPoolThreadKey : 
  IEquatable<SessionPoolThreadKey>,
  IComparable<SessionPoolThreadKey>,
  IComparable
{
  private readonly int threadId;
  private readonly int controlFlowId;

  /// <summary>Создает объект.</summary>
  /// <param name="threadId">Идентификатор потока (thread)</param>
  /// <param name="controlFlowId">Идентификатор логического подпотока управления внутри потока (thread)</param>
  public SessionPoolThreadKey(int threadId, int controlFlowId)
  {
    this.threadId = threadId;
    this.controlFlowId = controlFlowId;
  }

  /// <summary>Возвращает идентификатор потока (thread).</summary>
  public int ThreadId
  {
    [DebuggerStepThrough] get => this.threadId;
  }

  /// <summary>
  /// Идентификатор логического подпотока управления внутри потока (thread)
  /// Подробности - в описании <see cref="P:Intermech.Interfaces.Client.SessionPoolVars.ControlFlowId" />.
  /// </summary>
  public int ControlFlowId
  {
    [DebuggerStepThrough] get => this.controlFlowId;
  }

  public int CompareTo(SessionPoolThreadKey other)
  {
    int num = other != null ? this.threadId.CompareTo(other.threadId) : throw new ArgumentNullException(nameof (other));
    if (num == 0)
      num = this.controlFlowId.CompareTo(other.controlFlowId);
    return num;
  }

  public int CompareTo(object obj)
  {
    return obj is SessionPoolThreadKey other ? this.CompareTo(other) : throw new ArgumentException("Invalid argument type", nameof (obj));
  }

  public bool Equals(SessionPoolThreadKey other)
  {
    return other != null && this.threadId == other.threadId && this.controlFlowId == other.controlFlowId;
  }

  public override bool Equals(object obj)
  {
    return !(obj is SessionPoolThreadKey other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode()
  {
    return this.threadId ^ (this.controlFlowId & (int) ushort.MaxValue) << 16 /*0x10*/;
  }
}
