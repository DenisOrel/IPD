// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SessionPoolVars
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.ControlFlow;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Динамические переменные, управляющие работой пула сессий.
/// </summary>
public static class SessionPoolVars
{
  private const int defaultControlFlowId = 0;
  private static readonly DynamicVariable<int> controlFlowId = new DynamicVariable<int>("SessionPool.ControlFlowId", 0);
  private static int controlFlowIdGen = 0;

  /// <summary>
  /// <para>
  /// Идентификатор логического подпотока управления в пределах текущего потока (thread). Он используется для
  /// корректного выделения сессий из пула, если требуется несколько независимых сессий в пределах одного
  /// потока (thread): подпотоки управления с разными идентификаторами получат разные сессии из пула.</para>
  /// <para>
  /// Переменная ControlFlowId используется в тех случаях, когда у одного потока (thread) имеется несколько точек входа.
  /// Например, использование метода Control.Invoke создает новую точку входа и логический подпоток управления в UI-thread,
  /// так как в этом случае UI-thread используется для выполнения фрагмента кода из другого потока (thread).</para>
  /// </summary>
  public static DynamicVariable<int> ControlFlowId
  {
    [DebuggerStepThrough] get => SessionPoolVars.controlFlowId;
  }

  /// <summary>
  /// Создает и возвращает идентификатор для нового потока управления в пределах текущего thread.
  /// Метод используется совместно с переменной <see cref="P:Intermech.Interfaces.Client.SessionPoolVars.ControlFlowId" />.
  /// </summary>
  /// <returns>Идентификатор потока управления</returns>
  public static int CreateControlFlowId()
  {
    int controlFlowId = Interlocked.Increment(ref SessionPoolVars.controlFlowIdGen);
    if (controlFlowId == 0)
      controlFlowId = Interlocked.Increment(ref SessionPoolVars.controlFlowIdGen);
    return controlFlowId;
  }
}
