// Decompiled with JetBrains decompiler
// Type: Intermech.Project.SystemObject
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Metadata;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Системные объекты IPS.Project</summary>
public abstract class SystemObject : Intermech.Metadata.SystemObject
{
  /// <summary>Шаблон отчета загрузки исполнителей 2</summary>
  [NotNull]
  public static readonly SystemObjectDescriptor AssignmentsReportTemplate2ID = SystemObject.Create("caf0940f-306c-11d8-b4e9-00304f19f545", nameof (AssignmentsReportTemplate2ID));

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemObjectDescriptor Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Intermech.Metadata.SystemObject.Create<SystemObject>(guid, true, idName);
  }

  /// <summary>Guid-ы и идентификаторы системных объектов IPS (строковое представление Guid-ов)</summary>
  public new abstract class Consts : Intermech.Metadata.SystemObject.Consts
  {
    /// <summary>Шаблон отчета загрузки исполнителей 2</summary>
    public const string AssignmentsReportTemplate2IDGuid = "caf0940f-306c-11d8-b4e9-00304f19f545";
  }
}
