// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ProjectObjectType
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

/// <summary>Тип объекта "Проекты IMProject"</summary>
public class ProjectObjectType : TaskObjectType
{
  /// <summary>Guid типа объекта</summary>
  public new const string TypeGuid = "cad00e91-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static ProjectObjectType Create([CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    Guid guid = new Guid("cad00e91-306c-11d8-b4e9-00304f19f545");
    return new ProjectObjectType(MetaDataHelperService.Instance.GetObjectTypeID(guid), guid, idName);
  }

  private ProjectObjectType([NotEmpty] int id, Guid guid, [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, typeof (ObjectTypes), true, idPropertyName)
  {
  }

  /// <summary>Системные атрибуты типа объекта</summary>
  public new abstract class Attributes : TaskObjectType.Attributes
  {
    /// <summary>Данные проекта</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType Data = ProjectObjectType.Attributes.Create(Intermech.Project.Attributes.ProjectData);

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static SystemAttribute4ObjectType Create([NotNull] SystemAttribute attribute)
    {
      return SystemObjectType.Attributes.Create("cad00e91-306c-11d8-b4e9-00304f19f545", attribute);
    }
  }

  /// <summary>Возможные способы формирования состава</summary>
  public new abstract class ConsistOf : TaskObjectType.ConsistOf
  {
    /// <summary>Импортированные в проект объекты</summary>
    [NotNull]
    public static ImportedObjectsRelType ImportedObjects
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => RelationTypes.ImportedObjects;
    }

    /// <summary>Ссылка на проект IMProject. Искать у задач IMProject</summary>
    [NotNull]
    public static SystemAttribute Project
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Project.Attributes.Project;
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string nestedTypeGuid)
    {
      return SystemObjectType.ConsistOf.LinkedRelation(relationType, "cad00e91-306c-11d8-b4e9-00304f19f545", nestedTypeGuid);
    }
  }

  /// <summary>Возможные способы формирования входимости</summary>
  public new abstract class UsedIn : TaskObjectType.UsedIn
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string parentTypeGuid)
    {
      return SystemObjectType.UsedIn.LinkedRelation(relationType, parentTypeGuid, "cad00e91-306c-11d8-b4e9-00304f19f545");
    }
  }
}
