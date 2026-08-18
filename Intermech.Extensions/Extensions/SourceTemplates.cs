// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SourceTemplates
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.Extensions;

[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class SourceTemplates
{
  [SourceTemplate]
  public static void CallOnDispose([NotNull] this Action action)
  {
    using (Helper.CallOnDispose(action))
      ;
  }

  [SourceTemplate]
  public static void Arg<T>(this T argument)
  {
  }

  [SourceTemplate]
  public static void ArgNotNullNotEmpty(this string str)
  {
  }

  [SourceTemplate]
  public static void NotNullNotEmpty(this string str)
  {
  }

  [SourceTemplate]
  public static void ArgNotNullNotWhitespace(this string str)
  {
  }

  [SourceTemplate]
  public static void NotNullNotWhitespace(this string str)
  {
  }

  [SourceTemplate]
  public static void ArgNotEmpty<T>(this T val) where T : struct
  {
  }

  [SourceTemplate]
  public static void NotEmpty<T>(this T val) where T : struct
  {
  }

  [SourceTemplate]
  public static void ArgGuidNotEmpty(this Guid val)
  {
  }

  [SourceTemplate]
  public static void GuidNotEmpty(this Guid val)
  {
  }

  [SourceTemplate]
  public static void InRange<TEnum>(this TEnum val) where TEnum : struct, Enum
  {
  }

  [SourceTemplate]
  public static void IgnoreOperationCancelled([NotNull] this Action action)
  {
    Helper.IgnoreOperationCancelled(action);
  }

  [SourceTemplate]
  [Macro(Target = "continueOnCapturedContext", Expression = "constant(\"true\"))")]
  public static void IgnoreOperationCancelledAsync([NotNull] this Task task)
  {
  }

  [SourceTemplate]
  [Macro(Target = "result", Expression = "suggestVariableName()")]
  [Macro(Target = "continueOnCapturedContext", Expression = "constant(\"true\"))")]
  public static void IgnoreOperationCancelledAsyncParam<T>([NotNull] this Task<T> task)
  {
  }

  [SourceTemplate]
  [Macro(Target = "continueOnCapturedContext", Expression = "constant(\"true\"))")]
  public static void IgnoreOperationCancelledAsync([NotNull] this Func<Task> taskConstructor)
  {
  }

  [SourceTemplate]
  [Macro(Target = "result", Expression = "suggestVariableName()")]
  [Macro(Target = "continueOnCapturedContext", Expression = "constant(\"true\"))")]
  public static void IgnoreOperationCancelledAsyncParam<T>([NotNull] this Func<Task<T>> taskConstructor)
  {
  }

  [SourceTemplate]
  [Macro(Target = "operationCancelledException", Expression = "suggestVariableName()")]
  public static void CatchOperationCancelled([NotNull] this Action action)
  {
  }

  [SourceTemplate]
  [Macro(Target = "operationCancelledException", Expression = "suggestVariableName()")]
  [Macro(Target = "continueOnCapturedContext", Expression = "constant(\"true\"))")]
  public static void CatchOperationCancelledAsync([NotNull] this Task task)
  {
  }

  [SourceTemplate]
  [Macro(Target = "result", Expression = "suggestVariableName()")]
  [Macro(Target = "operationCancelledException", Expression = "suggestVariableName()")]
  [Macro(Target = "continueOnCapturedContext", Expression = "constant(\"true\"))")]
  public static void CatchOperationCancelledAsyncParam<T>([NotNull] this Task<T> task)
  {
  }
}
