// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ObjectTypes
// Assembly: Intermech.Extensions.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3168E407-FCE5-437D-846E-527B99048CAF
// Assembly location: D:\IPS\Client\Intermech.Extensions.Workflow.dll

using Intermech.Diagnostics;
using Intermech.Metadata;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Workflow;

public abstract class ObjectTypes : Intermech.Metadata.ObjectTypes
{
  [NotNull]
  public static readonly SystemObjectType Process = ObjectTypes.Create("cad002ad-306c-11d8-b4e9-00304f19f545", nameof (Process));
  [NotNull]
  public static readonly SystemObjectType Activity = ObjectTypes.Create("cad002af-306c-11d8-b4e9-00304f19f545", nameof (Activity));
  [NotNull]
  public static readonly SystemObjectType Message = (SystemObjectType) MessageObjectType.Create(nameof (Message));

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemObjectType Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Intermech.Metadata.ObjectTypes.Create<ObjectTypes>(guid, true, idName);
  }

  public new abstract class Consts : Intermech.Metadata.ObjectTypes.Consts
  {
    public const string ProcessGuid = "cad002ad-306c-11d8-b4e9-00304f19f545";
    public const string ActivityGuid = "cad002af-306c-11d8-b4e9-00304f19f545";
    public const string MessageGuid = "cad002bd-306c-11d8-b4e9-00304f19f545";
  }
}
