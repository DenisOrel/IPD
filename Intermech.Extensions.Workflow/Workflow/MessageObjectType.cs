// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.MessageObjectType
// Assembly: Intermech.Extensions.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3168E407-FCE5-437D-846E-527B99048CAF
// Assembly location: D:\IPS\Client\Intermech.Extensions.Workflow.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Workflow;

public class MessageObjectType : SystemObjectType
{
  public const string TypeGuid = "cad002bd-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static MessageObjectType Create([CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    Guid guid = new Guid("cad002bd-306c-11d8-b4e9-00304f19f545");
    return new MessageObjectType(MetaDataHelperService.Instance.GetObjectTypeID(guid), guid, idName);
  }

  protected internal MessageObjectType([NotEmpty] int id, Guid guid, [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, typeof (ObjectTypes), true, idPropertyName)
  {
  }

  protected internal MessageObjectType(
    [NotEmpty] int id,
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, holderType, obligatory, idPropertyName)
  {
  }

  public new abstract class Attributes : SystemObjectType.Attributes
  {
    [NotNull]
    public static readonly SystemAttribute4ObjectType Process = MessageObjectType.Attributes.Create(Intermech.Metadata.Attributes.Process);
    [NotNull]
    public static readonly SystemAttribute4ObjectType Activity = MessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.Activity);
    [NotNull]
    public static readonly SystemAttribute4ObjectType Sender = MessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.Sender);
    [NotNull]
    public static readonly SystemAttribute4ObjectType Recipient = MessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.Recipient);
    [NotNull]
    public static readonly SystemAttribute4ObjectType Subject = MessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.ProcessSubject);
    [NotNull]
    public static readonly SystemAttribute4ObjectType Priority = MessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.ProcessPriority);
    [NotNull]
    public static readonly SystemAttribute4ObjectType MessageText = MessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.MessageText);
    [NotNull]
    public static readonly SystemAttribute4ObjectType SenderStatus = MessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.SenderStatus);
    [NotNull]
    public static readonly SystemAttribute4ObjectType RecipientStatus = MessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.RecipientStatus);
    [NotNull]
    public static readonly SystemAttribute4ObjectType WasSentToRecipients = MessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.Started);
    [NotNull]
    public static readonly SystemAttribute4ObjectType WasSentFurther = MessageObjectType.Attributes.Create(Intermech.Workflow.Attributes.Finished);

    [NotNull]
    public static SystemAttribute4ObjectType Start
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return MessageObjectType.Attributes.WasSentToRecipients;
      }
    }

    [NotNull]
    public static SystemAttribute4ObjectType Completed
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return MessageObjectType.Attributes.WasSentFurther;
      }
    }

    [NotNull]
    public static SystemAttribute4ObjectType Finished
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return MessageObjectType.Attributes.WasSentFurther;
      }
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static SystemAttribute4ObjectType Create([NotNull] SystemAttribute attribute)
    {
      return SystemObjectType.Attributes.Create("cad002bd-306c-11d8-b4e9-00304f19f545", attribute);
    }
  }

  public new abstract class ConsistOf : SystemObjectType.ConsistOf
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string nestedTypeGuid)
    {
      return SystemObjectType.ConsistOf.LinkedRelation(relationType, "cad002bd-306c-11d8-b4e9-00304f19f545", nestedTypeGuid);
    }
  }

  public new abstract class UsedIn : SystemObjectType.UsedIn
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string parentTypeGuid)
    {
      return SystemObjectType.UsedIn.LinkedRelation(relationType, parentTypeGuid, "cad002bd-306c-11d8-b4e9-00304f19f545");
    }
  }
}
