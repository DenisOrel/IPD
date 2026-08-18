// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentGroup
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует группу документов IPS для объекта настроек интегратора с CAD-системой. Разные группы документов обрабатываются интегратором по разному.
/// Способ обработки определяется интегратром по уникальному имени группы, а также флагами обработки.
/// </summary>
public sealed class DocumentGroup
{
  private readonly string name;
  private readonly string caption;
  private readonly string[] flags;
  private readonly List<GlobalId<int>> documentTypes;

  /// <summary>Создает объект.</summary>
  /// <param name="name">Уникальное имя группы документов</param>
  /// <param name="caption">Название группы</param>
  /// <param name="flags">Флаги, задающие особенности обработки группы документов</param>
  public DocumentGroup(string name, string caption, string[] flags)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (caption == null)
      throw new ArgumentNullException(nameof (caption));
    if (flags == null)
      throw new ArgumentNullException(nameof (flags));
    this.name = name;
    this.caption = caption;
    this.flags = flags;
    this.documentTypes = new List<GlobalId<int>>();
  }

  /// <summary>Возвращает уникальное имя группы.</summary>
  public string Name => this.name;

  /// <summary>Возвращает название группы.</summary>
  public string Caption => this.caption;

  /// <summary>
  /// Возвращает флаги, задающие особенности обработки группы документов.
  /// </summary>
  public string[] Flags => this.flags;

  /// <summary>
  /// Возвращает список типов документов IPS, входящих в группу.
  /// </summary>
  public List<GlobalId<int>> DocumentTypes => this.documentTypes;

  /// <summary>
  /// Проверяет, содержится ли указанный тип документа в группе.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>true - если указанный тип документа содержится в группе, false - если не содержится</returns>
  public bool ContainsType(int documentType)
  {
    return this.documentTypes.Exists((Predicate<GlobalId<int>>) (item => item.Id == documentType));
  }

  /// <summary>
  /// Возвращает список типов документов IPS, входящих в группу, в виде списка идентификаторов.
  /// </summary>
  /// <returns>Список идентификаторов типов документов</returns>
  public List<int> AsIdList()
  {
    return this.DocumentTypes.ConvertAll<int>((Converter<GlobalId<int>, int>) (item => item.Id));
  }
}
