// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.SelectObjectsForImportControl
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

public class SelectObjectsForImportControl : 
  SelectObjectsForImportControlBase,
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
  /// <summary>Тип контрола дерева, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного контрола, в этом случае дерево будет создано указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public new static System.Type OverrideTreeViewClass
  {
    [DebuggerStepThrough] get => SelectObjectCompositionNavTreeView.OverrideTreeViewClass;
    [DebuggerStepThrough] set
    {
      SelectObjectCompositionNavTreeView.OverrideTreeViewClass = !(value != (System.Type) null) || !(value != typeof (ImportObjectsNavTree)) || value.IsSubclassOf(typeof (ImportObjectsNavTree)) ? value : throw new Exception($"Tree class must be {typeof (ImportObjectsNavTree).FullName} or it`s child class");
    }
  }
}
