// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ImportedObjectsRelType
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Тип связи "Импортированные в проект объекты"</summary>
public sealed class ImportedObjectsRelType : SystemRelationType
{
  /// <summary>Guid типа связи</summary>
  public const string TypeGuid = "cadd95aa-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static ImportedObjectsRelType Create([CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    Guid guid = new Guid("cadd95aa-306c-11d8-b4e9-00304f19f545");
    return new ImportedObjectsRelType(MetaDataHelperService.Instance.GetRelationTypeID(guid), guid, idName);
  }

  private ImportedObjectsRelType([NotEmpty] int id, Guid guid, [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, typeof (RelationTypes), true, idPropertyName)
  {
  }

  /// <summary>Системные атрибуты типа связи</summary>
  public new abstract class Attributes : SystemRelationType.Attributes
  {
    /// <summary>Идентификатор итерации в которой сохранён состав импортированного объекта на момент импорта</summary>
    [NotNull]
    public static readonly SystemAttribute4RelationType IterationID = ImportedObjectsRelType.Attributes.Create(Intermech.Metadata.Attributes.IterationID);
    /// <summary>Ссылка на объект, импортированный в проект</summary>
    [NotNull]
    public static readonly SystemAttribute4RelationType ImportedObject = ImportedObjectsRelType.Attributes.Create(Intermech.Project.Attributes.ImportedObject);
    /// <summary>Данные импорта (XML?)</summary>
    [NotNull]
    public static readonly SystemAttribute4RelationType Data = ImportedObjectsRelType.Attributes.Create(Intermech.Metadata.Attributes.Data);
    /// <summary>Дата последней синхронизации состава</summary>
    [NotNull]
    public static readonly SystemAttribute4RelationType LastSyncDate = ImportedObjectsRelType.Attributes.Create(Intermech.Project.Attributes.LastSyncDate);
    /// <summary>Шаблон для новых задач</summary>
    [NotNull]
    public static readonly SystemAttribute4RelationType Prototype = ImportedObjectsRelType.Attributes.Create(Intermech.Project.Attributes.Prototype);
    /// <summary>Сценарий инициализации задач</summary>
    [NotNull]
    public static readonly SystemAttribute4RelationType InitScript = ImportedObjectsRelType.Attributes.Create(Intermech.Project.Attributes.InitScript);

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static SystemAttribute4RelationType Create([NotNull] SystemAttribute attribute)
    {
      return SystemRelationType.Attributes.Create("cadd95aa-306c-11d8-b4e9-00304f19f545", attribute);
    }
  }
}
