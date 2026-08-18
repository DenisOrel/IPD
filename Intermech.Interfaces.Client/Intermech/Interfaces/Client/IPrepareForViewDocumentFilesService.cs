// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPrepareForViewDocumentFilesService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса для подготовки файлов документов IPS к просмотру или печати во внешнем приложении.
/// Реализация должна быть thread safe.
/// </summary>
public interface IPrepareForViewDocumentFilesService
{
  /// <summary>
  /// Готовит файл документа к просмотру или печати.
  /// Обработчик события может дописать в файл документа необходимую информацию, или
  /// проверить корректность информации, уже имеющейся в файле документа.
  /// </summary>
  /// <remarks>
  /// Событие вызывается для документов, которые находятся на шагах ЖЦ, не допускающих редактирования (на согласовании, выпущен и др.),
  /// непосредственно перед открытием файла документа во внешнем приложении.
  /// </remarks>
  event EventHandler<DocumentLocalFileEventArgs> PrepareDocumentFile;
}
