// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ResourceRelType
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

/// <summary>Тип связи "Ресурс проекта IMProject"</summary>
public sealed class ResourceRelType : SystemRelationType
{
  /// <summary>Guid типа связи</summary>
  public const string TypeGuid = "cad00e9d-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static ResourceRelType Create([CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    Guid guid = new Guid("cad00e9d-306c-11d8-b4e9-00304f19f545");
    return new ResourceRelType(MetaDataHelperService.Instance.GetRelationTypeID(guid), guid, idName);
  }

  private ResourceRelType([NotEmpty] int id, Guid guid, [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, typeof (RelationTypes), true, idPropertyName)
  {
  }

  /// <summary>Системные атрибуты типа связи</summary>
  public new abstract class Attributes : SystemRelationType.Attributes
  {
    /// <summary>Число единиц ресурса</summary>
    [NotNull]
    public static readonly SystemAttribute4RelationType Units = ResourceRelType.Attributes.Create(Intermech.Project.Attributes.ResourceUnits);
    /// <summary>Признак руководителя</summary>
    [NotNull]
    public static readonly SystemAttribute4RelationType IsChief = ResourceRelType.Attributes.Create(Intermech.Project.Attributes.ResourceIsChief);

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static SystemAttribute4RelationType Create([NotNull] SystemAttribute attribute)
    {
      return SystemRelationType.Attributes.Create("cad00e9d-306c-11d8-b4e9-00304f19f545", attribute);
    }
  }
}
