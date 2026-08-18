// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IMRPAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Действие, выполняемое задачами MRP</summary>
public interface IMRPAction : IMRPContext
{
  /// <summary>Выполнить действие в рамках встроенного контекста</summary>
  void Execute();

  /// <summary>
  /// Выполнить действие в рамках указанного контекста.
  /// Если контекст не задан, будет использован встроенный.
  /// </summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие.
  /// Если значение не задано, будет использоваться встроенный контекст</param>
  void Execute(IServiceProvider context);
}
