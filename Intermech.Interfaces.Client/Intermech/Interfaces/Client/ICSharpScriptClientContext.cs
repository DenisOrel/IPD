// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICSharpScriptClientContext
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Объект контекста для клиентских C#-сценариев, выполняемых в изолированном окружении.
/// Через этот объект сценарии могут обращаться к API основного приложения.
/// </summary>
/// <remarks>
/// Сервисы основного приложения доступны в виде свойств контекста.
/// </remarks>
public interface ICSharpScriptClientContext : ICSharpScriptContext
{
  [Obsolete("Вместо обращения к этому свойству в ScriptContext следует объявить и использовать аналогичное свойство в классе Script", true)]
  IOutputView OutputView { get; }

  [Obsolete("Вместо обращения к этому свойству в ScriptContext следует объявить и использовать аналогичное свойство в классе Script", true)]
  IAuthFilesService AuthFilesService { get; }
}
