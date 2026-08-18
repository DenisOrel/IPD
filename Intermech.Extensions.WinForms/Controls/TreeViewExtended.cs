// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.TreeViewExtended
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Controls;

[DefaultEvent("AfterSelect")]
[Designer(typeof (ControlDesigner))]
[DefaultProperty(null)]
[Docking(DockingBehavior.Ask)]
public class TreeViewExtended : TreeViewExtended<TreeNodeExtendedBase>
{
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DisplayName("Загрузка корня дерева")]
  [Description("Событие в обработчике которого должна производится загрузка корня дерева")]
  public event TreeViewExtended.CreateRootNodesAsyncDelegate CreateRootNodes;

  [NotNull]
  [ItemNotNull]
  protected override Task<IReadOnlyCollection<TreeNodeExtendedBase>> CreateRootNodesAsync(
    [NotNull] TreeViewExtended<TreeNodeExtendedBase>.IOperationService operationService,
    System.Threading.CancellationToken cancellationToken)
  {
    Delegate[] delegateArray = this.CreateRootNodes?.GetInvocationList() ?? Array.Empty<Delegate>();
    if (delegateArray.Length == 0)
      throw new Exception("Для использования TreeViewExtended необходимо подписаться на событие CreateRootNodes");
    if (delegateArray.Length > 1)
      throw new Exception("Должна быть только одна подписка на событие CreateRootNodes");
    return (Task<IReadOnlyCollection<TreeNodeExtendedBase>>) delegateArray[0].DynamicInvoke((object) operationService, (object) cancellationToken);
  }

  public delegate Task<IReadOnlyCollection<TreeNodeExtendedBase>> CreateRootNodesAsyncDelegate(
    TreeViewExtended<TreeNodeExtendedBase>.IOperationService operationService,
    System.Threading.CancellationToken cancellationToken);
}
