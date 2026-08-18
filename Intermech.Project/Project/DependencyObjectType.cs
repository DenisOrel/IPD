// Decompiled with JetBrains decompiler
// Type: Intermech.Project.DependencyObjectType
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

/// <summary>Тип объекта "Взаимосвязи IMProject"</summary>
public class DependencyObjectType : SystemObjectType
{
  /// <summary>Guid типа объекта</summary>
  public const string TypeGuid = "cad00e9a-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static DependencyObjectType Create([CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    Guid guid = new Guid("cad00e9a-306c-11d8-b4e9-00304f19f545");
    return new DependencyObjectType(MetaDataHelperService.Instance.GetObjectTypeID(guid), guid, idName);
  }

  protected internal DependencyObjectType([NotEmpty] int id, Guid guid, [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, typeof (ObjectTypes), true, idPropertyName)
  {
  }

  /// <summary>Системные атрибуты типа объекта</summary>
  public new abstract class Attributes : SystemObjectType.Attributes
  {
    /// <summary>Исходит из задачи</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType FromTask = DependencyObjectType.Attributes.Create(Intermech.Project.Attributes.FromTask);
    /// <summary>Входит в задачу</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType ToTask = DependencyObjectType.Attributes.Create(Intermech.Project.Attributes.ToTask);
    /// <summary>Тип зависимости</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType Type = DependencyObjectType.Attributes.Create(Intermech.Project.Attributes.DependencyType);
    /// <summary>Запаздывание</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType Lag = DependencyObjectType.Attributes.Create(Intermech.Project.Attributes.Lag);

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static SystemAttribute4ObjectType Create([NotNull] SystemAttribute attribute)
    {
      return SystemObjectType.Attributes.Create("cad00e9a-306c-11d8-b4e9-00304f19f545", attribute);
    }
  }

  /// <summary>Возможные способы формирования состава</summary>
  public new abstract class ConsistOf : SystemObjectType.ConsistOf
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string nestedTypeGuid)
    {
      return SystemObjectType.ConsistOf.LinkedRelation(relationType, "cad00e9a-306c-11d8-b4e9-00304f19f545", nestedTypeGuid);
    }
  }

  /// <summary>Возможные способы формирования входимости</summary>
  public new abstract class UsedIn : SystemObjectType.UsedIn
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string parentTypeGuid)
    {
      return SystemObjectType.UsedIn.LinkedRelation(relationType, parentTypeGuid, "cad00e9a-306c-11d8-b4e9-00304f19f545");
    }
  }
}
