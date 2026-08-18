
// Type: Intermech.Client.Core.SelectInObjectCompositionsFormBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Windows.Forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Базовый класс для формы выбора элементов состава объекта (выбор галками) или объектов.
/// Сделан для того, чтобы можно было переопределить класс создаваемого контрола для выбора объектов из структуры</summary>
public class SelectInObjectCompositionsFormBase : 
  IpsBaseDialog,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IContextAware,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2
{
  /// <summary>Тип контрола c деревом выбора в структуре объекта, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного контрола, в этом случае дерево будет создано указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public static System.Type OverrideTreeViewControlClass;
  [NotNull]
  protected SelectObjectCompositionNavTreeView _treeViewControl;

  private void Init()
  {
    this.SuspendLayout();
    this.CreateTreeViewControl();
    this.ResumeLayout(false);
  }

  public SelectInObjectCompositionsFormBase()
    : base((Form) null, contextName: string.Empty)
  {
    this.Init();
  }

  /// <summary>Закрытый конструктор</summary>
  protected SelectInObjectCompositionsFormBase(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices = null,
    [CanBeNull] string contextName = null)
    : base(centerOnForm, ownerServices, contextName)
  {
    this.Init();
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected virtual System.Type DefaultNavigatorTreeViewControlClass
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return typeof (SelectObjectCompositionNavTreeView);
    }
  }

  protected virtual void CreateTreeViewControl()
  {
    System.Type viewControlClass = SelectInObjectCompositionsFormBase.OverrideTreeViewControlClass;
    if ((object) viewControlClass == null)
      viewControlClass = this.DefaultNavigatorTreeViewControlClass;
    this._treeViewControl = (SelectObjectCompositionNavTreeView) Activator.CreateInstance(viewControlClass);
    SelectInObjectCompositionsFormBase.OverrideTreeViewControlClass = (System.Type) null;
    this._treeViewControl.PanelSelectButtons.SuspendLayout();
    this._treeViewControl.TreeView.BeginInit();
    this._treeViewControl.SuspendLayout();
    this._treeViewControl.BtnSelectObjects.Location = new Point(623, 29);
    this._treeViewControl.Dock = DockStyle.Fill;
    this._treeViewControl.Location = new Point(0, 0);
    this._treeViewControl.MinimumSize = new Size(562, 204);
    this._treeViewControl.Name = "_treeViewControl";
    this._treeViewControl.PanelSelectButtons.Dock = DockStyle.Bottom;
    this._treeViewControl.PanelSelectButtons.Location = new Point(0, 349);
    this._treeViewControl.PanelSelectButtons.Name = "PanelSelectButtons";
    this._treeViewControl.PanelSelectButtons.Size = new Size(791, 58);
    this._treeViewControl.PanelSelectButtons.TabIndex = 12;
    this._treeViewControl.Size = new Size(791, 415);
    this._treeViewControl.TabIndex = 0;
    this._treeViewControl.TreePagesReadOnly = true;
    this._treeViewControl.TreeView.AllowDrop = true;
    this._treeViewControl.TreeView.AllowMultiSelect = false;
    this._treeViewControl.TreeView.AllowUserPinnedColumns = false;
    this._treeViewControl.TreeView.BackgroundAutoLoadComposition = AdvNavigatorTreeView.NullAbleBoolDefault.True;
    this._treeViewControl.TreeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
    this._treeViewControl.TreeView.DisableCheckedOutColumn = true;
    this._treeViewControl.TreeView.Dock = DockStyle.Fill;
    this._treeViewControl.TreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._treeViewControl.TreeView.LineStyle = LineStyle.Dot;
    this._treeViewControl.TreeView.Location = new Point(0, 24);
    this._treeViewControl.TreeView.Name = "TreeView";
    this._treeViewControl.TreeView.RowEvenStyle.WordWrap = false;
    this._treeViewControl.TreeView.RowOddStyle.WordWrap = false;
    this._treeViewControl.TreeView.RowSelectedStyle.WordWrap = false;
    this._treeViewControl.TreeView.RowStyle.BorderColor = SystemColors.Control;
    this._treeViewControl.TreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this._treeViewControl.TreeView.RowStyle.BorderWidth = 1;
    this._treeViewControl.TreeView.RowStyle.WordWrap = false;
    this._treeViewControl.TreeView.SelectBeforeEdit = true;
    this._treeViewControl.TreeView.ShowRootRow = false;
    this._treeViewControl.TreeView.Size = new Size(791, 383);
    this._treeViewControl.TreeView.SuppressErrorMessages = true;
    this._treeViewControl.TreeView.TabIndex = 0;
    this.Controls.Add((Control) this._treeViewControl);
    this._treeViewControl.PanelSelectButtons.ResumeLayout(false);
    this._treeViewControl.TreeView.EndInit();
    this._treeViewControl.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Контрол с деревом навигатора</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public SelectObjectCompositionNavTreeView TreeViewControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._treeViewControl;
    }
  }
}
