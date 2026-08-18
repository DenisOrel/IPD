// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.StandaloneView.StandaloneViewData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Signs.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.StandaloneView;

/// <summary>
/// Контейнер для всех собранных сведений о файле документа для режима автономного просмотра файла. Эти сведения должны быть
/// записаны в файл документа перед просмотром. Они включают в себя подписи документа, контрольную сумму файла,
/// значения некоторых атрибутов документа и др. Назначение записывамых сведений - обеспечить полноценный просмотр и
/// получение твердой копии файла документа без необходимости иметь доступ к самому документу в базе IPS.
/// </summary>
public class StandaloneViewData
{
  private ICollection<SignParams> objectSigns;
  private Tuple<string, string> fileChecksum;
  private ICollection<Tuple<string, string>> objectAttributes;

  /// <summary>Очищает контейнер.</summary>
  public void Clear()
  {
    this.ObjectSigns = (ICollection<SignParams>) null;
    this.FileChecksum = (Tuple<string, string>) null;
    this.ObjectAttributes = (ICollection<Tuple<string, string>>) null;
  }

  /// <summary>
  /// Возвращает признак, что контейнер пуст, т.е. в нем отсутствуют какие-либо сведения для внедрения в файл документа.
  /// </summary>
  public bool IsEmpty
  {
    get => this.IsObjectSignsEmpty && this.IsFileChecksumEmpty && this.IsObjectAttributesEmpty;
  }

  /// <summary>
  /// Возвращает или задает коллекцию подписей документа, которые должны быть внедрены в файл документа.
  /// Значение свойства может быть не задано.
  /// </summary>
  public ICollection<SignParams> ObjectSigns
  {
    [DebuggerStepThrough] get => this.objectSigns;
    [DebuggerStepThrough] set => this.objectSigns = value;
  }

  /// <summary>
  /// Возвращает признак, что не заданы подписи документа, которые должны быть внедрены в файл документа.
  /// </summary>
  public bool IsObjectSignsEmpty => this.ObjectSigns == null || this.ObjectSigns.Count == 0;

  /// <summary>
  /// Возвращает или задает контрольную сумму файла документа. Значение свойства содержит имя параметра документа для записи контрольной суммы, а также ее значение.
  /// Значение свойства может быть не задано.
  /// </summary>
  public Tuple<string, string> FileChecksum
  {
    [DebuggerStepThrough] get => this.fileChecksum;
    [DebuggerStepThrough] set => this.fileChecksum = value;
  }

  /// <summary>
  /// Возвращает признак, что не задана контрольная сумма файла, которая должна быть внедрена в файл документа.
  /// </summary>
  public bool IsFileChecksumEmpty
  {
    get => this.FileChecksum == null || string.IsNullOrEmpty(this.FileChecksum.Item1);
  }

  /// <summary>
  /// Возвращает или задает значения атрибутов документа, которые должны быть внедрены в файл документа.
  /// Значение свойства может быть не задано.
  /// </summary>
  public ICollection<Tuple<string, string>> ObjectAttributes
  {
    [DebuggerStepThrough] get => this.objectAttributes;
    [DebuggerStepThrough] set => this.objectAttributes = value;
  }

  /// <summary>
  /// Возвращает признак, что не заданы атрибуты документа, которые должны быть внедрены в файл документа.
  /// </summary>
  public bool IsObjectAttributesEmpty
  {
    get => this.ObjectAttributes == null || this.ObjectAttributes.Count == 0;
  }
}
