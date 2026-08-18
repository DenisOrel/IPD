// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.RelationTypes
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public abstract class RelationTypes
{
  public const string EntityInstanceNameInGenitiveCase = "типа связи";
  [NotNull]
  public const int UnknownID = -1;
  [NotNull]
  public static readonly SimpleRelType Simple = SimpleRelType.Create(nameof (Simple));
  [NotNull]
  public static readonly SortedRelType Sorted = SortedRelType.Create(nameof (Sorted));
  [NotNull]
  public static readonly SystemRelationType Documentation = RelationTypes.Create("cad00154-306c-11d8-b4e9-00304f19f545", nameof (Documentation));
  [NotNull]
  public static readonly SystemRelationType DocsComposition = RelationTypes.Create("cad0057c-306c-11d8-b4e9-00304f19f545", nameof (DocsComposition));
  [NotNull]
  public static readonly SystemRelationType SP = RelationTypes.Create("cad00023-306c-11d8-b4e9-00304f19f545", nameof (SP));
  [NotNull]
  public static readonly SystemRelationType BuildingComposition = RelationTypes.Create("cad008d6-306c-11d8-b4e9-00304f19f545", nameof (BuildingComposition));
  [NotNull]
  public static readonly SystemRelationType ECO = RelationTypes.Create("cad0036b-306c-11d8-b4e9-00304f19f545", nameof (ECO));
  [NotNull]
  public static readonly AttachmentRelType WfAttachment = AttachmentRelType.Create(nameof (WfAttachment));
  [NotNull]
  public static readonly SystemRelationType TechComposition = RelationTypes.Create("cad0019f-306c-11d8-b4e9-00304f19f545", nameof (TechComposition));
  public const string EntityName = "Типы связей";
  public const string EntityInstanceName = "Тип связи";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemRelationType Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return RelationTypes.Create<RelationTypes>(guid, false, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemRelationType CreateObligatory([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return RelationTypes.Create<RelationTypes>(guid, true, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemRelationType Create<THolder>([NotNull, NotWhitespace] string guid, bool obligatory, [CallerMemberName, NotNull, NotWhitespace] string idName = "") where THolder : RelationTypes
  {
    Guid guid1 = new Guid(guid);
    return new SystemRelationType(MetaDataHelperService.Instance.GetRelationTypeID(guid1), guid1, typeof (THolder), obligatory, idName);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetIdFromGuid([NotEmpty] Guid guid, bool throwIfNotFound = true)
  {
    int relationTypeId = MetaDataHelperService.Instance.GetRelationTypeID(guid);
    return !throwIfNotFound || !Intermech.Check.RelationTypeIdIsEmpty(relationTypeId) ? relationTypeId : throw new RelationTypeNotFoundException(guid);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetIdFromGuid([NotNull, NotWhitespace] string guid, bool throwIfNotFound = true)
  {
    Guid guid1 = new Guid(guid);
    int relationTypeId = MetaDataHelperService.Instance.GetRelationTypeID(guid1);
    return !throwIfNotFound || !Intermech.Check.RelationTypeIdIsEmpty(relationTypeId) ? relationTypeId : throw new RelationTypeNotFoundException(guid1);
  }

  public abstract class Consts
  {
    public const string SimpleGuid = "cad00022-306c-11d8-b4e9-00304f19f545";
    public const string SortedGuid = "cad00151-306c-11d8-b4e9-00304f19f545";
    public const string DocumentationGuid = "cad00154-306c-11d8-b4e9-00304f19f545";
    public const string DocsCompositionGuid = "cad0057c-306c-11d8-b4e9-00304f19f545";
    public const string SpGuid = "cad00023-306c-11d8-b4e9-00304f19f545";
    public const string BuildingCompositionGuid = "cad008d6-306c-11d8-b4e9-00304f19f545";
    public const string EcoGuid = "cad0036b-306c-11d8-b4e9-00304f19f545";
    public const string AttachmentsGuid = "cad01329-306c-11d8-b4e9-00304f19f545";
    public const string TechCompositionGuid = "cad0019f-306c-11d8-b4e9-00304f19f545";
  }
}
