// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IEditingContextNavWindow
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Contexts;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Привязка контекстов редактирования к окну "Навигатора"
/// </summary>
public interface IEditingContextNavWindow
{
  /// <summary>Идентификатор текущего контекста редактирования</summary>
  long EditingContextID { get; set; }

  /// <summary>Режим работы текущего контекста редактирования</summary>
  EditingContextMode EditingContextMode { get; set; }

  /// <summary>
  /// Список контекстов редактирования, которые будут отображаться в комбо-боксе (история выбранных контекстов)
  /// </summary>
  List<long> History { get; set; }
}
