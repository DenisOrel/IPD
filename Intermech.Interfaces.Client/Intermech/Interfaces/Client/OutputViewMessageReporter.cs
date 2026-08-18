// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.OutputViewMessageReporter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует адаптер для вывода многострочных сообщений в окно "Вывод".
/// </summary>
public class OutputViewMessageReporter : MessageReporterBase
{
  private IOutputView outputView;
  private readonly string categoryName;

  /// <summary>Создает объект.</summary>
  /// <param name="outputView">Сервис окна "Вывод"</param>
  /// <param name="categoryName">Имя вкладки в окне, на которую будут выводиться сообщения</param>
  /// <exception cref="T:ArgumentNullException">outputView, categoryName</exception>
  public OutputViewMessageReporter(IOutputView outputView, string categoryName)
  {
    if (outputView == null)
      throw new ArgumentNullException(nameof (outputView));
    if (categoryName == null)
      throw new ArgumentNullException(nameof (categoryName));
    this.outputView = outputView;
    this.categoryName = categoryName;
  }

  /// <summary>
  /// Выводит строку текста текущего сообщения. Вывод текста может быть отложен до момента, пока сообщение не будет завершено с помощью метода <see cref="M:EndMessage" />.
  /// </summary>
  /// <param name="text">Текст сообщения</param>
  protected override void DoWriteLine(string text)
  {
    this.outputView.WriteString(this.categoryName, text);
  }
}
