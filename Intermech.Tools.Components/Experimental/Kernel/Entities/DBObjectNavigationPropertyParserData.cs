// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBObjectNavigationPropertyParserData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBObjectNavigationPropertyParserData
{
  public DBObjectNavigationPropertyParserData(ExtendedEntityPropertyInfo propertyInfo)
  {
    this.ReflectionInfo = propertyInfo;
    this.Name = propertyInfo.Name;
    this.Definition = this.ReflectionInfo.Definition;
  }

  public ExtendedEntityPropertyInfo ReflectionInfo { get; private set; }

  public string Name { get; private set; }

  public EntityPropertyDefinition Definition { get; private set; }

  public Guid DBRelationTypeGuid { get; set; }

  public bool IsRelationStart { get; set; }

  public bool IsComplex { get; set; }

  /// <summary>
  /// Возвращает или задает тип доменного объекта на другом конце связи.
  /// </summary>
  public Type InverseEntityType { get; set; }

  /// <summary>
  /// Возвращает имя парного навигационного свойства на другом конце связи.
  /// Если текущее свойство содержит начало связи, то это свойство может быть не задано.
  /// Если текущее свойство содержит окончание связи, то это свойство будет задано.
  /// </summary>
  public string InverseTypePropertyName { get; set; }

  /// <summary>
  /// Возвращает или задает тип объекта-связки для сложных связей.
  /// </summary>
  public Type ChildOccurenceType { get; set; }

  public bool IsCompleteDefinition { get; set; }
}
