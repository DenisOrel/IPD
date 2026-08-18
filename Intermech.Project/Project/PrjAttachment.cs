// Decompiled with JetBrains decompiler
// Type: Intermech.Project.PrjAttachment
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Workflow;
using System;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class PrjAttachment : Attachment
{
  public PrjAttachKind Kind;

  public override void Assign([NotNull] Attachment att)
  {
    base.Assign(att);
    if (!(att is PrjAttachment prjAttachment))
      return;
    this.Kind = prjAttachment.Kind;
  }

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return obj is PrjAttachment prjAttachment && prjAttachment.ObjectID == this.ObjectID && prjAttachment.Kind == this.Kind;
  }

  public override int GetHashCode() => (this.ObjectID, this.Kind).GetHashCode();
}
