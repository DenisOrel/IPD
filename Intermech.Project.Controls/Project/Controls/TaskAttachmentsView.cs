// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.TaskAttachmentsView
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Workflow;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

[ViewDescriptionProvider(typeof (TaskAttachmentsView.Description))]
public class TaskAttachmentsView : 
  BaseAttachmentsView,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IAdvancedView,
  IView,
  IEmbeddedViews,
  IViewData,
  ICommandTarget,
  ISelectedItemsHost,
  INodeView,
  IIOSource,
  IReportView,
  INavigatorContextSearch,
  ISelectedItemsText
{
  [CanBeNull]
  private bool? _inDesignMode;

  /// <summary>Перечисление всех родителей данного контрола</summary>
  /// <param name="includeThis">Включая данный контрол или нет</param>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private IEnumerable<System.Windows.Forms.Control> GetParents(bool includeThis = false)
  {
    System.Windows.Forms.Control control = includeThis ? (System.Windows.Forms.Control) this : this.Parent;
    while (true)
    {
      System.Windows.Forms.Control control1 = control;
      if ((control1 != null ? (!control1.IsDisposed ? 1 : 0) : 0) != 0)
      {
        yield return control;
        control = control.Parent;
      }
      else
        break;
    }
  }

  /// <summary>Немного более точная проверка в DesignMode мы или нет. Работает и в конструкторе (обычный DesignMode - не работает)</summary>
  protected bool InDesignMode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (this._inDesignMode ?? (this._inDesignMode = new bool?(this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.GetParents(true).Any<System.Windows.Forms.Control>((Func<System.Windows.Forms.Control, bool>) (ctrl =>
      {
        ISite site = ctrl.Site;
        return site != null && site.DesignMode;
      }))))).Value;
    }
  }

  public TaskAttachmentsView()
  {
    if (this.InDesignMode)
      return;
    this.RelationTypeID = (int) (IpsMetadataEntityBase<int>) Intermech.Project.RelationTypes.TaskAttachment;
    this.Conditions = new ConditionStructure[1]
    {
      new ConditionStructure((int) (IpsMetadataEntityBase<int>) Intermech.Project.Attributes.AttachKind, RelationalOperators.Equal, (object) (int) this.PrjAttachKind, LogicalOperators.AND, 0, false)
    };
    this.OnSaveAttachment += new SaveAttachmentHandler(this.TaskAttachmentsView_OnSaveAttachment);
  }

  private void TaskAttachmentsView_OnSaveAttachment([NotNull] Attachment att, [NotNull] IDBRelation rel)
  {
    IDBAttribute attributeById = rel.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Project.Attributes.AttachKind);
    if (attributeById == null)
      rel.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Intermech.Project.Attributes.AttachKind, false, new object[1]
      {
        (object) (int) this.PrjAttachKind
      });
    else
      attributeById.AsInteger = (long) this.PrjAttachKind;
  }

  public override int ObjectType => (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task;

  protected override void AdjustObject([CanBeNull] ref IDBObject obj, ref bool readOnly)
  {
    if (obj != null && obj.TypeID == (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.ProjectMessage)
    {
      long objectID = 0;
      IDBAttribute attributeById = obj.GetAttributeByID(Intermech.Workflow.Attributes.Activity.ID);
      if (attributeById != null)
        objectID = attributeById.AsInteger;
      obj = objectID != 0L ? obj.Session.GetObject(objectID) : (IDBObject) null;
    }
    else
      base.AdjustObject(ref obj, ref readOnly);
  }

  protected virtual PrjAttachKind PrjAttachKind => PrjAttachKind.Result;

  protected override void SaveChanges()
  {
    if (this._attachments != null)
    {
      foreach (PrjAttachment prjAttachment in this.Attachments.OfType<PrjAttachment>())
        prjAttachment.Kind = this.PrjAttachKind;
    }
    base.SaveChanges();
  }

  [NotNull]
  protected override AttachmentList NewAttachmentList() => (AttachmentList) new PrjAttachmentList();

  protected class Description : BaseAttachmentsView.BaseAttachmentsViewDescriptionProvider
  {
    [NotNull]
    public override ViewDescription DoGetViewDescription(
      [NotNull] ISelectedItems selectedItems,
      [CanBeNull] System.IServiceProvider serviceProvider)
    {
      return base.DoGetViewDescription(selectedItems, serviceProvider);
    }
  }
}
