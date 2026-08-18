// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.TaskDataView
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Bars;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

[ViewDescriptionProvider(typeof (TaskDataView.Description))]
public class TaskDataView : 
  TaskAttachmentsView,
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
  public TaskDataView() => this.ReadOnly = true;

  protected override PrjAttachKind PrjAttachKind => PrjAttachKind.SrcData;

  protected new class Description : TaskAttachmentsView.Description
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      ViewDescription viewDescription = base.DoGetViewDescription(selectedItems, serviceProvider);
      viewDescription.Caption = Localization.GetString("TaskData");
      return viewDescription;
    }
  }
}
