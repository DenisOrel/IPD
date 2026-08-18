// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.SelectObjectsForSyncWithCompositionControlBase
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>База для контрола выбора объектов для синхронизации задач проекта с составом объекта</summary>
public class SelectObjectsForSyncWithCompositionControlBase : 
  SelectObjectsForImportControl,
  ITreeNodesFactory,
  IDBObjectsSource,
  ITreeListColumns,
  ICommandTarget,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  /// <summary>Тип дерева навигатора по-умолчанию</summary>
  protected override System.Type DefaultNavigatorTreeViewClass
  {
    [DebuggerStepThrough] get => typeof (SelectObjectsForSyncWithCompositionNavTreeView);
  }

  /// <summary>UI: Дерево состава объекта</summary>
  [NotNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public SelectObjectsForSyncWithCompositionNavTreeView TreeView
  {
    [DebuggerStepThrough] get => (SelectObjectsForSyncWithCompositionNavTreeView) base.TreeView;
  }
}
