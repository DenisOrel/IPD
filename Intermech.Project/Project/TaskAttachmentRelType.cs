// Decompiled with JetBrains decompiler
// Type: Intermech.Project.TaskAttachmentRelType
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

/// <summary>Тип связи "Вложение IMProject"</summary>
public sealed class TaskAttachmentRelType : SystemRelationType
{
  /// <summary>Guid типа связи</summary>
  public const string TypeGuid = "cadd9384-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static TaskAttachmentRelType Create([CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    Guid guid = new Guid("cadd9384-306c-11d8-b4e9-00304f19f545");
    return new TaskAttachmentRelType(MetaDataHelperService.Instance.GetRelationTypeID(guid), guid, idName);
  }

  private TaskAttachmentRelType([NotEmpty] int id, Guid guid, [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, typeof (RelationTypes), true, idPropertyName)
  {
  }

  /// <summary>Системные атрибуты типа связи</summary>
  public new abstract class Attributes : SystemRelationType.Attributes
  {
    /// <summary>Вид вложения IMProject</summary>
    [NotNull]
    public static readonly SystemAttribute4RelationType Kind = TaskAttachmentRelType.Attributes.Create(Intermech.Project.Attributes.AttachKind);

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static SystemAttribute4RelationType Create([NotNull] SystemAttribute attribute)
    {
      return SystemRelationType.Attributes.Create("cadd9384-306c-11d8-b4e9-00304f19f545", attribute);
    }
  }
}
