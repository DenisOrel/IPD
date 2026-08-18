// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityParserData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal abstract class DBEntityParserData
{
  private DBEntityKind entityKind;
  private Type entityType;
  private ExtendedEntityTypeInfo reflectionInfo;

  protected DBEntityParserData(DBEntityKind entityKind, Type entityType)
  {
    if (entityType == (Type) null)
      throw new ArgumentNullException(nameof (entityType));
    this.entityKind = entityKind;
    this.entityType = entityType;
    this.reflectionInfo = new ExtendedEntityTypeInfo(entityType);
  }

  public DBEntityKind EntityKind
  {
    [DebuggerStepThrough] get => this.entityKind;
  }

  public Type EntityType
  {
    [DebuggerStepThrough] get => this.entityType;
  }

  public ExtendedEntityTypeInfo ReflectionInfo
  {
    [DebuggerStepThrough] get => this.reflectionInfo;
  }

  /// <summary>
  /// Возвращает или задает список свойств типа, пригодных для отображения в базу данных.
  /// </summary>
  public List<ExtendedEntityPropertyInfo> MappablePropertiesParserData { get; set; }

  public Dictionary<string, DataPropertyParserData> DataPropertiesParserData { get; set; }

  public Dictionary<string, DataPropertyDescriptor> DataPropertiesDescriptors { get; set; }
}
