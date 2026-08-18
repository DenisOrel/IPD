// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMViewerPublishItem
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Files;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Описывает объект IMViewer в паре с исходным документом.
/// Объекты этого типа используются при извлечение файлов объектов IMViewer на локальный диск
/// для открытия в приложении-просмотрщике.
/// </summary>
/// <remarks>Реализация является immutable и thread safe.</remarks>
[Serializable]
public class IMViewerPublishItem
{
  /// <summary>Создает объект.</summary>
  /// <param name="sourceDocument">Исходный документ</param>
  /// <param name="sidecarObject">Объект IMViewer</param>
  /// <param name="sidecarContentStatus">Статус содержимого объекта IMViewer по отношению к исходному документу</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="sourceDocument" /> содержит null</exception>
  public IMViewerPublishItem(
    DBObjectState sourceDocument,
    DBObjectState sidecarObject = null,
    ObjectContentStatus sidecarContentStatus = ObjectContentStatus.NotSet)
  {
    this.SourceDocument = sourceDocument != null ? sourceDocument : throw new ArgumentNullException(nameof (sourceDocument));
    this.SidecarObject = sidecarObject;
    this.SidecarContentStatus = sidecarContentStatus;
  }

  /// <summary>Возвращает исходный документ.</summary>
  public DBObjectState SourceDocument { get; }

  /// <summary>
  /// Возвращает объект IMViewer.
  /// Если объект IMViewer еще не был сгенерирован для исходного документа,
  /// то значение свойства будет равно null.
  /// </summary>
  public DBObjectState SidecarObject { get; }

  /// <summary>
  /// Возвращает статус содержимого объекта IMViewer по отношению к исходному документу.
  /// Если объект IMViewer еще не был сгенерирован для исходного документа,
  /// то значение свойства будет равно <see cref="F:Intermech.ObjectContentStatus.NotSet" />.
  /// </summary>
  public ObjectContentStatus SidecarContentStatus { get; }
}
