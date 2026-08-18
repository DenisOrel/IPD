// Decompiled with JetBrains decompiler
// Type: Intermech.Project.PrjAttachmentList
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class PrjAttachmentList : AttachmentList
{
  private int _kindColumnIndex = -1;

  public PrjAttachmentList()
  {
    this.RelationTypeID = (int) (IpsMetadataEntityBase<int>) RelationTypes.TaskAttachment;
    this.AddColumns = new object[1]
    {
      (object) Attributes.AttachKind.ID
    };
    this.OnSaveAttachment += new SaveAttachmentHandler(PrjAttachmentList.PrjAttachmentList_OnSaveAttachment);
  }

  private static void PrjAttachmentList_OnSaveAttachment(
    [NotNull] Attachment attachment,
    [NotNull] IDBRelation dbRelation)
  {
    if (!(attachment is PrjAttachment prjAttachment))
      return;
    IDBAttribute attributeById = dbRelation.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.AttachKind);
    if (attributeById == null)
      dbRelation.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Attributes.AttachKind, false, new object[1]
      {
        (object) (int) prjAttachment.Kind
      });
    else
      attributeById.AsInteger = (long) prjAttachment.Kind;
  }

  [NotNull]
  public override Attachment NewAttachment() => (Attachment) new PrjAttachment();

  [NotNull]
  protected override Attachment NewAttachment([CanBeNull] DataRow row)
  {
    PrjAttachment prjAttachment = Intermech.Diagnostics.Check.Is<PrjAttachment>((object) base.NewAttachment(row));
    if (row != null)
    {
      if (this._kindColumnIndex == -1)
        this._kindColumnIndex = row.ItemArray.Length - 1;
      if (!DBNull.Value.Equals(row[this._kindColumnIndex]))
        prjAttachment.Kind = (PrjAttachKind) Convert.ToInt32(row[this._kindColumnIndex]);
    }
    return (Attachment) prjAttachment;
  }

  [NotNull]
  public PrjAttachmentList Filter(PrjAttachKind kind)
  {
    PrjAttachmentList prjAttachmentList = new PrjAttachmentList();
    foreach (Attachment attachment in (List<Attachment>) this)
    {
      if (attachment is PrjAttachment prjAttachment && prjAttachment.Kind == kind)
        prjAttachmentList.Add((Attachment) prjAttachment);
    }
    return prjAttachmentList;
  }
}
