
// Type: Intermech.Interfaces.CSharpScriptInvocationOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Scripting.Common;
using Intermech.Scripting.CSharp;
using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>Опции выполнения C#-сценариев.</summary>
    [Serializable]
    /// <summary>Создает объект.</summary>
    /// <param name="enableDebugInfo">Включает и выключает добавление отладочной информации в выполняемые сценарии</param>
    /// <param name="debugStream">Объект для перехвата отладочного вывода выполняемых сценариев. Параметр может быть не задан</param>
    public sealed class CSharpScriptInvocationOptions(
      bool enableDebugInfo,
      IScriptOutputStream debugStream = null) : ScriptInvocationOptions(enableDebugInfo, debugStream)
    {
      private static readonly CSharpScriptInvocationOptions withDebugInfo = new CSharpScriptInvocationOptions(true);
      private static readonly CSharpScriptInvocationOptions withOptimizations = new CSharpScriptInvocationOptions(false);

      /// <summary>
      /// Опции выполнения по умолчанию.
      /// Значение свойства совпадает с <see cref="P:Intermech.Interfaces.CSharpScriptInvocationOptions.WithDebugInfo" />.
      /// </summary>
      public static CSharpScriptInvocationOptions Default
      {
        [DebuggerStepThrough] get => CSharpScriptInvocationOptions.withDebugInfo;
      }

      /// <summary>
      /// Опции выполнения, при которых код сценария компилируется с отладочной информацией и без оптимизаций.
      /// Используется для отладки кода сценариев с помощью Visual Studio.
      /// </summary>
      public static CSharpScriptInvocationOptions WithDebugInfo
      {
        [DebuggerStepThrough] get => CSharpScriptInvocationOptions.withDebugInfo;
      }

      /// <summary>
      /// Опции выполнения, при которых код сценария компилируется без отладочной информации и с оптимизациями.
      /// Используется для достижения максимальной производительности сценария.
      /// </summary>
      public static CSharpScriptInvocationOptions WithOptimizations
      {
        [DebuggerStepThrough] get => CSharpScriptInvocationOptions.withOptimizations;
      }
    }
}
