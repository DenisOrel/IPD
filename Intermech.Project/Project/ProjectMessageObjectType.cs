// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ProjectMessageObjectType
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using Intermech.Workflow;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Тип объекта "Почтовое сообщение IMProject"</summary>
public class ProjectMessageObjectType : MessageObjectType
{
  /// <summary>Guid типа объекта</summary>
  public new const string TypeGuid = "cadd91f6-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static ProjectMessageObjectType Create([CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    Guid guid = new Guid("cadd91f6-306c-11d8-b4e9-00304f19f545");
    return new ProjectMessageObjectType(MetaDataHelperService.Instance.GetObjectTypeID(guid), guid, idName);
  }

  protected internal ProjectMessageObjectType([NotEmpty] int id, Guid guid, [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, typeof (ObjectTypes), true, idPropertyName)
  {
  }

  /// <summary>Системные атрибуты типа объекта</summary>
  public new abstract class Attributes : MessageObjectType.Attributes
  {
    /// <summary>Ссылка на задачу IMProject, в контексте которой было сгенерировано сообщение</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType Task = ProjectMessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.Activity);
    /// <summary>Ответ руководителя</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType ManagerAnswer = ProjectMessageObjectType.Attributes.Create(Intermech.Project.Attributes.ManagerAnswer);

    /// <summary>Ссылка на действие, отправившее сообщение</summary>
    [NotNull]
    [Obsolete("Use Task attribute")]
    private static SystemAttribute4ObjectType Activity
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return MessageObjectType.Attributes.Activity;
      }
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static SystemAttribute4ObjectType Create([NotNull] SystemAttribute attribute)
    {
      return SystemObjectType.Attributes.Create("cadd91f6-306c-11d8-b4e9-00304f19f545", attribute);
    }
  }

  /// <summary>Возможные способы формирования состава</summary>
  public new abstract class ConsistOf : MessageObjectType.ConsistOf
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string nestedTypeGuid)
    {
      return SystemObjectType.ConsistOf.LinkedRelation(relationType, "cadd91f6-306c-11d8-b4e9-00304f19f545", nestedTypeGuid);
    }
  }

  /// <summary>Возможные способы формирования входимости</summary>
  public new abstract class UsedIn : MessageObjectType.UsedIn
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string parentTypeGuid)
    {
      return SystemObjectType.UsedIn.LinkedRelation(relationType, parentTypeGuid, "cadd91f6-306c-11d8-b4e9-00304f19f545");
    }
  }
}
