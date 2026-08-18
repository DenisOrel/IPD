
// Type: Intermech.Client.Core.ObjectCreator.Controls.PageArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.ObjectCreator.Controls;

/// <summary>
/// Базовый класс для аргументов передаваемых в методы шага мастера создания объектов
/// </summary>
public class PageArgs
{
  /// <summary>
  /// Если во время выполнения возникает ошибка, то содержит ошибку, иначе - null
  /// </summary>
  public Exception Error;
}
