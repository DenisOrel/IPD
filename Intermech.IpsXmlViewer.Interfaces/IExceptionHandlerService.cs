// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IExceptionHandlerService
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Служба для обработки исключительных стиуаций с возможностью
/// подключения пользовательских обработчиков.
/// </summary>
public interface IExceptionHandlerService
{
  /// <summary>
  /// Вызывается при возникновении в системе необработанного исключения.
  /// Обработчики вызываются в порядке очереди до тех пор, пока кто то не установит
  /// флаг ExceptionEventArgs.Handled в true
  /// </summary>
  event ExceptionHandler HandleException;

  /// <summary>Отобразить информацию об исключении</summary>
  /// <param name="e">Исключение</param>
  void ShowException(Exception e);
}
