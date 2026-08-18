// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Role
// Assembly: Intermech.Extensions.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3168E407-FCE5-437D-846E-527B99048CAF
// Assembly location: D:\IPS\Client\Intermech.Extensions.Workflow.dll

using Intermech.Diagnostics;
using Intermech.Metadata;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Workflow;

public abstract class Role : Intermech.Metadata.Role
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemRole Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Intermech.Metadata.Role.Create<Role>(guid, true, idName);
  }

  public new abstract class Consts : Intermech.Metadata.Role.Consts
  {
  }
}
