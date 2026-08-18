// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Attributes
// Assembly: Intermech.Extensions.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3168E407-FCE5-437D-846E-527B99048CAF
// Assembly location: D:\IPS\Client\Intermech.Extensions.Workflow.dll

using Intermech.Diagnostics;
using Intermech.Metadata;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Workflow;

public abstract class Attributes : Intermech.Metadata.Attributes
{
  [NotNull]
  public static readonly SystemAttribute Activity = Attributes.Create("cad002c8-306c-11d8-b4e9-00304f19f545", nameof (Activity));
  [NotNull]
  public static readonly SystemAttribute ProcessPriority = Attributes.Create("cad002d1-306c-11d8-b4e9-00304f19f545", nameof (ProcessPriority));
  [NotNull]
  public static readonly SystemAttribute ProcessSubject = Attributes.Create("cad002d6-306c-11d8-b4e9-00304f19f545", nameof (ProcessSubject));
  [NotNull]
  public static readonly SystemAttribute Sender = Attributes.Create("cad002c9-306c-11d8-b4e9-00304f19f545", nameof (Sender));
  [NotNull]
  public static readonly SystemAttribute Recipient = Attributes.Create("cad002ca-306c-11d8-b4e9-00304f19f545", nameof (Recipient));
  [NotNull]
  public static readonly SystemAttribute ActivityStatus = Attributes.Create("cad002cd-306c-11d8-b4e9-00304f19f545", nameof (ActivityStatus));
  [NotNull]
  public static readonly SystemAttribute PrototypeProcess = Attributes.Create("cad00362-306c-11d8-b4e9-00304f19f545", nameof (PrototypeProcess));
  [NotNull]
  public static readonly SystemAttribute RemoteProcessStatus = Attributes.Create("cadd94c6-306c-11d8-b4e9-00304f19f545", nameof (RemoteProcessStatus));
  [NotNull]
  public static readonly SystemAttribute MessageText = Attributes.Create("cad002d2-306c-11d8-b4e9-00304f19f545", nameof (MessageText));
  [NotNull]
  public static readonly SystemAttribute RecipientStatus = Attributes.Create("cad0035f-306c-11d8-b4e9-00304f19f545", nameof (RecipientStatus));
  [NotNull]
  public static readonly SystemAttribute SenderStatus = Attributes.Create("cad00365-306c-11d8-b4e9-00304f19f545", nameof (SenderStatus));
  [NotNull]
  public static readonly SystemAttribute Started = Attributes.Create("cad002cb-306c-11d8-b4e9-00304f19f545", nameof (Started));
  [NotNull]
  public static readonly SystemAttribute Finished = Attributes.Create("cad002cc-306c-11d8-b4e9-00304f19f545", nameof (Finished));
  [NotNull]
  public static readonly SystemAttribute DueDate = Attributes.Create("cad0132d-306c-11d8-b4e9-00304f19f545", nameof (DueDate));
  [NotNull]
  public static readonly SystemAttribute PublicationNecessary = Attributes.Create("cadd95f6-306c-11d8-b4e9-00304f19f545", nameof (PublicationNecessary));

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemAttribute Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Intermech.Metadata.Attributes.Create<Attributes>(guid, true, idName);
  }

  public new abstract class Consts : Intermech.Metadata.Attributes.Consts
  {
    public const string ActivityGuid = "cad002c8-306c-11d8-b4e9-00304f19f545";
    public const string ProcessPriorityGuid = "cad002d1-306c-11d8-b4e9-00304f19f545";
    public const string ProcessSubjectGuid = "cad002d6-306c-11d8-b4e9-00304f19f545";
    public const string SenderGuid = "cad002c9-306c-11d8-b4e9-00304f19f545";
    public const string RecipientGuid = "cad002ca-306c-11d8-b4e9-00304f19f545";
    public const string ActivityStatusGuid = "cad002cd-306c-11d8-b4e9-00304f19f545";
    public const string PrototypeProcessGuid = "cad00362-306c-11d8-b4e9-00304f19f545";
    public const string RemoteProcessStatusGuid = "cadd94c6-306c-11d8-b4e9-00304f19f545";
    public const string MessageTextGuid = "cad002d2-306c-11d8-b4e9-00304f19f545";
    public const string RecipientStatusGuid = "cad0035f-306c-11d8-b4e9-00304f19f545";
    public const string SenderStatusGuid = "cad00365-306c-11d8-b4e9-00304f19f545";
    public const string StartedGuid = "cad002cb-306c-11d8-b4e9-00304f19f545";
    public const string FinishedGuid = "cad002cc-306c-11d8-b4e9-00304f19f545";
    public const string DueDateGuid = "cad0132d-306c-11d8-b4e9-00304f19f545";
    public const string PublicationNecessaryGuid = "cadd95f6-306c-11d8-b4e9-00304f19f545";
  }
}
