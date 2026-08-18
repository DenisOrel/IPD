// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DocumentTypesGroup
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует группу типов документов, единообразно обрабатываемых интегратором.
/// </summary>
public sealed class DocumentTypesGroup
{
  private readonly Guid groupId;
  private readonly List<LocalId<int>> docTypes;

  /// <summary>Создает объект.</summary>
  /// <param name="groupId">Идентификатор группы</param>
  public DocumentTypesGroup(Guid groupId)
  {
    this.groupId = groupId;
    this.docTypes = new List<LocalId<int>>(16 /*0x10*/);
  }

  /// <summary>Создает объект.</summary>
  /// <param name="groupId">Идентификатор группы</param>
  /// <param name="documentType">Идентификатор типа документа</param>
  public DocumentTypesGroup(Guid groupId, LocalId<int> documentType)
    : this(groupId)
  {
    this.docTypes.Add(documentType);
  }

  /// <summary>Создает объект.</summary>
  /// <param name="groupId">Идентификатор группы</param>
  /// <param name="initialValues">Начальное содержимое списка типов документов</param>
  public DocumentTypesGroup(Guid groupId, IEnumerable<LocalId<int>> initialValues)
  {
    this.groupId = groupId;
    this.docTypes = new List<LocalId<int>>(initialValues);
  }

  /// <summary>Возвращает идентификатор группы.</summary>
  public Guid Id => this.groupId;

  /// <summary>Возвращает список типов документов в группе.</summary>
  public List<LocalId<int>> DocumentTypes => this.docTypes;
}
