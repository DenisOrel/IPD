// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.ExceptionEventArgs
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Класс для передачи информации о исключительной ситуации и о том,
/// обработано ли это исключение подписчиком
/// </summary>
public class ExceptionEventArgs : EventArgs
{
  /// <summary>Исключение</summary>
  public Exception Exception { [DebuggerStepThrough] get; private set; }

  /// <summary>Исключение обработано подписчиком или нет.</summary>
  public bool Handled { [DebuggerStepThrough] get; set; }

  /// <summary>Создать аргументы</summary>
  /// <param name="e">Исключение</param>
  public ExceptionEventArgs(Exception e)
  {
    this.Exception = e;
    this.Handled = false;
  }
}
