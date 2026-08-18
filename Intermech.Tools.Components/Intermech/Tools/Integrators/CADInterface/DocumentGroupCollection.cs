// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentGroupCollection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Реализует коллекцию групп документов.</summary>
public class DocumentGroupCollection : Collection<DocumentGroup>
{
  /// <summary>Создает объект.</summary>
  public DocumentGroupCollection()
    : base((IList<DocumentGroup>) new List<DocumentGroup>())
  {
  }

  /// <summary>Создает объект.</summary>
  /// <param name="capacity">Начальная емкость коллекции</param>
  public DocumentGroupCollection(int capacity)
    : base((IList<DocumentGroup>) new List<DocumentGroup>(capacity))
  {
  }

  public DocumentGroup FindByDocumentType(int documentType, bool throwIfNotFound)
  {
    if (documentType == -1)
      throw new ArgumentException("Не задан идентификатор типа документа.", nameof (documentType));
    foreach (DocumentGroup byDocumentType in (IEnumerable<DocumentGroup>) this.Items)
    {
      if (byDocumentType.ContainsType(documentType))
        return byDocumentType;
    }
    if (!throwIfNotFound)
      return (DocumentGroup) null;
    throw new InvalidOperationException($"Тип объектов с идентификатором {documentType} не поддерживается интегратором.");
  }

  public DocumentGroup FindByName(string groupName, bool throwIfNotFound)
  {
    if (groupName == null)
      throw new ArgumentNullException(nameof (groupName));
    foreach (DocumentGroup byName in (IEnumerable<DocumentGroup>) this.Items)
    {
      if (byName.Name == groupName)
        return byName;
    }
    if (!throwIfNotFound)
      return (DocumentGroup) null;
    throw new InvalidOperationException($"Группа документов с именем '{groupName}' не поддерживается интегратором.");
  }

  protected override void InsertItem(int index, DocumentGroup item)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    this.CheckGroupNameIsUnique(item.Name);
    base.InsertItem(index, item);
  }

  protected override void SetItem(int index, DocumentGroup item)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    this.CheckGroupNameIsUnique(item.Name);
    base.SetItem(index, item);
  }

  private void CheckGroupNameIsUnique(string groupName)
  {
    if (CollectionUtils.Exists<DocumentGroup>((IEnumerable<DocumentGroup>) this.Items, (Predicate<DocumentGroup>) (item => item.Name == groupName)))
      throw new InvalidOperationException($"Имя группы документов '{groupName}' не является уникальным.");
  }
}
