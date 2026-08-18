// Decompiled with JetBrains decompiler
// Type: Intermech.Project.RelationTypes
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Metadata;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Системные типы связей IPS.Project</summary>
public abstract class RelationTypes : Intermech.Metadata.RelationTypes
{
  /// <summary>Состав задачи</summary>
  [NotNull]
  public static readonly TaskCompositionRelType TaskComposition = TaskCompositionRelType.Create(nameof (TaskComposition));
  /// <summary>Вложение IMProject</summary>
  [NotNull]
  public static readonly TaskAttachmentRelType TaskAttachment = TaskAttachmentRelType.Create(nameof (TaskAttachment));
  /// <summary>Связь с задействованными в проекте ресурсами</summary>
  [NotNull]
  public static readonly ResourceRelType Resources = ResourceRelType.Create(nameof (Resources));
  /// <summary>Импортированные в проект объекты</summary>
  [NotNull]
  public static readonly ImportedObjectsRelType ImportedObjects = ImportedObjectsRelType.Create(nameof (ImportedObjects));

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemRelationType Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Intermech.Metadata.RelationTypes.Create<RelationTypes>(guid, true, idName);
  }

  /// <summary>Guid-ы и идентификаторы системных типов связей IPS (строковое представление Guid-ов)</summary>
  public new abstract class Consts : Intermech.Metadata.RelationTypes.Consts
  {
    /// <summary>Состав задачи</summary>
    public const string TaskCompositionGuid = "cad00e93-306c-11d8-b4e9-00304f19f545";
    /// <summary>Вложение IMProject</summary>
    public const string TaskAttachmentGuid = "cadd9384-306c-11d8-b4e9-00304f19f545";
    /// <summary>Связь с задействованными в проекте ресурсами</summary>
    public const string ResourcesGuid = "cad00e9d-306c-11d8-b4e9-00304f19f545";
    /// <summary>Импортированные в проект объекты</summary>
    public const string ImportedObjectsInProjectGuid = "cadd95aa-306c-11d8-b4e9-00304f19f545";
  }
}
