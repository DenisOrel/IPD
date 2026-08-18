// Decompiled with JetBrains decompiler
// Type: Intermech.Project.PhysicalQuantity
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Metadata;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Системные физические величины IPS.Project</summary>
public abstract class PhysicalQuantity : Intermech.Metadata.PhysicalQuantity
{
  /// <summary>Время</summary>
  [NotNull]
  public static readonly SystemPhysicalQuantity Time = PhysicalQuantity.Create("cad002e0-306c-11d8-b4e9-00304f19f545", nameof (Time));

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemPhysicalQuantity Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Intermech.Metadata.PhysicalQuantity.Create<PhysicalQuantity>(guid, true, idName);
  }

  /// <summary>Guid-ы и идентификаторы системных физических величин IPS.Project (строковое представление Guid-ов)</summary>
  public new abstract class Consts : Intermech.Metadata.PhysicalQuantity.Consts
  {
    /// <summary>Время</summary>
    public const string TimeGuid = "cad002e0-306c-11d8-b4e9-00304f19f545";
  }
}
