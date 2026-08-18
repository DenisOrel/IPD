
// Type: Intermech.Client.Core.SelectInObjectCompositionsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Intermech.Client.Core.Forms;
using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Форма выбора элементов состава объекта (выбор галками) или объектов
/// 
/// На данный момент создаётся для тестирования Intermech.Navigator.Controls.ObjectsCompositionNavigatorTreeView,
/// а ещё точнее - подключения к нему Intermech.Navigator.Controls.CheckableNavTreeViewHelper - Helper класса,
/// позволяющего сделать так, чтобы ноды у дерева были с возможностью простановки галок.
/// Но потенциально в будущем можно будет использовать и в мирных целях</summary>
public class SelectInObjectCompositionsForm : SelectInObjectCompositionsFormBase
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel _pnTreeView;
  protected StatusStrip _statusStrip;
  protected ToolStripStatusLabel _labelFocusedObjectType;
  protected ToolTip _toolTips;
  protected ToolStripStatusLabel _labelFocusedObjectCaption;

  /// <summary>Тип контрола c деревом выбора в структуре объекта, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного контрола, в этом случае дерево будет создано указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [NotNull]
  public static System.Type OverrideTreeViewControlClass
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return SelectInObjectCompositionsFormBase.OverrideTreeViewControlClass ?? throw new InvalidOperationException($"{SelectInObjectCompositionsFormBase.OverrideTreeViewControlClass} value must be set before this context");
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      SelectInObjectCompositionsFormBase.OverrideTreeViewControlClass = value;
    }
  }

  /// <summary>Тип контрола дерева, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного контрола, в этом случае дерево будет создано указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public static System.Type OverrideTreeViewClass
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return SelectObjectCompositionNavTreeView.OverrideTreeViewClass;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      SelectObjectCompositionNavTreeView.OverrideTreeViewClass = value;
    }
  }

  public SelectInObjectCompositionsForm() => this.InitializeComponent();

  /// <summary>Закрытый конструктор</summary>
  protected SelectInObjectCompositionsForm(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    [NotNull] IReadOnlyCollection<long> objectVersionIDs,
    bool allowChangeObjects = true)
    : this(centerOnForm, ownerServices, contextName, objectVersionIDs, (IReadOnlyCollection<int>) null, allowChangeObjects)
  {
  }

  /// <summary>Закрытый конструктор</summary>
  protected SelectInObjectCompositionsForm(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    [NotNull] IReadOnlyCollection<long> objectVersionIDs,
    [CanBeNull] IReadOnlyCollection<int> objectTypeIDs,
    bool allowChangeObjects = true)
    : base(centerOnForm, ownerServices, contextName)
  {
    SelectInObjectCompositionsForm compositionsForm = this;
    IReadOnlyCollection<int> ints = objectTypeIDs;
    this.InitializeComponent();
    this._treeViewControl.AllowChangeObjects = allowChangeObjects;
    // ISSUE: explicit non-virtual call
    this.Shown += (EventHandler) ((sender, e) => compositionsForm._treeViewControl.Init(__nonvirtual (compositionsForm.Services), objectVersionIDs, objectTypeIDs));
  }

  /// <summary>Создание и демонстрация пользователю формы выбора элементов состава</summary>
  /// <param name="contextName">Имя операции, в контексте которой был вызван диалог, для сохранения/чтения настроек в привязке к этой
  /// операции</param>
  /// <param name="objectTypeIDs">Последовательность идентификаторов типов объектов, которые должны быть доступны для выбора</param>
  /// <param name="objectVersionIDs">Последовательность идентификаторов версий объектов, в составе которых должен происходить выбор. Если
  /// не указывать, то пользователю будет предложено выбрать самостоятельно</param>
  /// <param name="allowChangeObjects">Позволять ли пользователю менять корневые объекты. В том случае, если objectVersionID не указан
  /// игнорируется, пользователь всегда сможет менять выбор</param>
  /// <returns>A DialogResult</returns>
  public static DialogResult Show(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    [CanBeNull] IReadOnlyCollection<int> objectTypeIDs,
    [CanBeNull] IReadOnlyCollection<long> objectVersionIDs = null,
    bool allowChangeObjects = true)
  {
    IReadOnlyCollection<int> objectTypeIDs1 = (IReadOnlyCollection<int>) ((object) objectTypeIDs ?? (object) Array.Empty<int>());
    IReadOnlyCollection<long> longs = objectVersionIDs ?? (IReadOnlyCollection<long>) SelectObjectCompositionNavTreeView.ShowSelectObjectsForm(contextName, objectTypeIDs1);
    if (!longs.Any<long>())
      return DialogResult.Abort;
    using (SelectInObjectCompositionsForm compositionsForm = new SelectInObjectCompositionsForm(centerOnForm, ownerServices, contextName, longs, objectTypeIDs1, objectVersionIDs == null | allowChangeObjects))
      return compositionsForm.ShowDialog();
  }

  /// <summary>Создание и демонстрация пользователю формы выбора элементов состава</summary>
  /// <param name="contextName">Имя операции, в контексте которой был вызван диалог, для сохранения/чтения настроек в привязке к этой
  /// операции</param>
  /// <param name="objectVersionIDs">Последовательность идентификаторов версий объектов, в составе которых должен происходить выбор. Если
  /// не указывать, то пользователю будет предложено выбрать самостоятельно</param>
  /// <param name="allowChangeObjects">Позволять ли пользователю менять корневые объекты. В том случае, если objectVersionID не указан
  /// игнорируется, пользователь всегда сможет менять выбор</param>
  /// <returns>A DialogResult</returns>
  public static DialogResult Show(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    [CanBeNull] IReadOnlyCollection<long> objectVersionIDs = null,
    bool allowChangeObjects = true)
  {
    return SelectInObjectCompositionsForm.Show(centerOnForm, ownerServices, contextName, (IReadOnlyCollection<int>) null, objectVersionIDs, allowChangeObjects);
  }

  /// <summary>Создание и демонстрация пользователю формы выбора элементов состава</summary>
  /// <param name="objectTypeIDs">Последовательность идентификаторов типов объектов, которые должны быть доступны для выбора</param>
  /// <param name="objectVersionIDs">Последовательность идентификаторов версий объектов, в составе которых должен происходить выбор. Если
  /// не указывать, то пользователю будет предложено выбрать самостоятельно</param>
  /// <returns>A DialogResult</returns>
  public static DialogResult Show(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] IReadOnlyCollection<int> objectTypeIDs,
    [CanBeNull] IReadOnlyCollection<long> objectVersionIDs = null)
  {
    return SelectInObjectCompositionsForm.Show(centerOnForm, ownerServices, (string) null, objectTypeIDs, objectVersionIDs);
  }

  /// <summary>Создание и демонстрация пользователю формы выбора элементов состава</summary>
  /// <param name="objectVersionIDs">Последовательность идентификаторов версий объектов, в составе которых должен происходить выбор. Если
  /// не указывать, то пользователю будет предложено выбрать самостоятельно</param>
  /// <param name="allowChangeObjects">Позволять ли пользователю менять корневые объекты. В том случае, если objectVersionID не указан
  /// игнорируется, пользователь всегда сможет менять выбор</param>
  /// <returns>A DialogResult</returns>
  public static DialogResult Show(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] IReadOnlyCollection<long> objectVersionIDs,
    bool allowChangeObjects = true)
  {
    return SelectInObjectCompositionsForm.Show(centerOnForm, ownerServices, (string) null, objectVersionIDs, allowChangeObjects);
  }

  /// <summary>Создание и демонстрация пользователю формы выбора элементов состава</summary>
  /// <param name="contextName">Имя операции, в контексте которой был вызван диалог, для сохранения/чтения настроек в привязке к этой
  /// операции</param>
  /// <param name="objectVersionID">Идентификатор версии объекта, в составе которого должен происходить выбор. Если не указывать, то
  /// пользователю будет предложено выбрать самостоятельно</param>
  /// <param name="objectTypeIDs">Последовательность идентификаторов типов объектов, которые должны быть доступны для выбора</param>
  /// <param name="allowChangeObjects">Позволять ли пользователю менять корневые объекты. В том случае, если objectVersionID не указан
  /// игнорируется, пользователь всегда сможет менять выбор</param>
  /// <returns>A DialogResult</returns>
  public static DialogResult Show(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    long objectVersionID,
    [CanBeNull] IReadOnlyCollection<int> objectTypeIDs = null,
    bool allowChangeObjects = true)
  {
    Form centerOnForm1 = centerOnForm;
    System.IServiceProvider ownerServices1 = ownerServices;
    string contextName1 = contextName;
    IReadOnlyCollection<int> objectTypeIDs1 = objectTypeIDs;
    long[] objectVersionIDs;
    if (objectVersionID != 0L)
      objectVersionIDs = new long[1]{ objectVersionID };
    else
      objectVersionIDs = (long[]) null;
    int num = allowChangeObjects ? 1 : 0;
    return SelectInObjectCompositionsForm.Show(centerOnForm1, ownerServices1, contextName1, objectTypeIDs1, (IReadOnlyCollection<long>) objectVersionIDs, num != 0);
  }

  /// <summary>Создание и демонстрация пользователю формы выбора элементов состава</summary>
  /// <param name="objectVersionID">Идентификатор версии объекта, в составе которого должен происходить выбор. Если не указывать, то
  /// пользователю будет предложено выбрать самостоятельно</param>
  /// <param name="objectTypeIDs">Последовательность идентификаторов типов объектов, которые должны быть доступны для выбора</param>
  /// <param name="allowChangeObjects">Позволять ли пользователю менять корневые объекты. В том случае, если objectVersionID не указан
  /// игнорируется, пользователь всегда сможет менять выбор</param>
  /// <returns>A DialogResult</returns>
  public static DialogResult Show(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    long objectVersionID,
    [CanBeNull] IReadOnlyCollection<int> objectTypeIDs = null,
    bool allowChangeObjects = true)
  {
    return SelectInObjectCompositionsForm.Show(centerOnForm, ownerServices, (string) null, objectVersionID, objectTypeIDs, allowChangeObjects);
  }

  /// <summary>Число отмеченных объектов (из тех, кто уже загруженны в составе)</summary>
  public int CheckedObjectsCount
  {
    [DebuggerStepThrough] get => this.TreeViewControl.CheckedObjectsCount;
  }

  /// <summary>Число отмеченных объектов (из тех, кто уже загруженны в составе), состав которых не загружен</summary>
  public int CheckedObjectsWithNotLoadedChildsCount
  {
    [DebuggerStepThrough] get => this.TreeViewControl.CheckedObjectsWithNotLoadedChildsCount;
  }

  /// <summary>Присутствуют ли в дереве присутствуют отмеченные объекты (из тех, кто уже загруженны в составе), состав которых не загружен</summary>
  public bool HasCheckedObjectsWithNotLoadedChilds
  {
    [DebuggerStepThrough] get => this.TreeViewControl.HasCheckedObjectsWithNotLoadedChilds;
  }

  /// <summary>Перечисление отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  public IEnumerable<NavigatorTreeNode> CheckedObjectNodes
  {
    [DebuggerStepThrough] get => this.TreeViewControl.CheckedObjectNodes;
  }

  /// <summary>Перечисление интерфейсов идентификаторов отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  public IEnumerable<NodeID> CheckedObjectNodeIDs
  {
    [DebuggerStepThrough] get => this.TreeViewControl.CheckedObjectNodeIDs;
  }

  /// <summary>Признак того, что в дереве отмечен хотя бы 1 объект</summary>
  public bool ObjectIsChecked
  {
    [DebuggerStepThrough] get => this.TreeViewControl.ObjectIsChecked;
  }

  /// <summary>Последовательность идентификаторов версий отмеченных в дереве объектов</summary>
  [NotNull]
  public IEnumerable<long> CheckedObjectVersionIDs
  {
    [DebuggerStepThrough] get => this.TreeViewControl.CheckedObjectVersionIDs;
  }

  /// <summary>Последовательность идентификаторов отмеченных объектов (!!! НЕ ВЕРСИЙ !!!)</summary>
  [NotNull]
  public IEnumerable<long> CheckedObjectIDs
  {
    [DebuggerStepThrough] get => this.TreeViewControl.CheckedObjectIDs;
  }

  /// <summary>Последовательность идентификаторов типов отмеченных объектов</summary>
  [NotNull]
  public IEnumerable<int> CheckedObjectTypeIDs
  {
    [DebuggerStepThrough] get => this.TreeViewControl.CheckedObjectTypeIDs;
  }

  /// <summary>Последовательность идентификаторов связей отмеченных объектов</summary>
  [NotNull]
  public IEnumerable<long> CheckedObjectPrjLinkIDs
  {
    [DebuggerStepThrough] get => this.TreeViewControl.CheckedObjectPrjLinkIDs;
  }

  /// <summary>Последовательность заголовков отмеченных в объекта</summary>
  [NotNull]
  public IEnumerable<string> CheckedObjectCaptions
  {
    [DebuggerStepThrough] get => this.TreeViewControl.CheckedObjectCaptions;
  }

  /// <summary>Загрузка свойств в словарь, который будет сохранён в FormStorage при вызове SavePropertiesToStorage</summary>
  public override void FillPropsDictionary([NotNull] Dictionary<string, object> dic)
  {
    this.TreeViewControl.FillPropsDictionary(dic);
  }

  /// <summary>Загрузка свойств из словаря, полученного из FormStorage при вызове LoadPropertiesFromStorage</summary>
  public override void ParseDictionaryFromFormStorage([NotNull] Dictionary<string, object> dic)
  {
    this.TreeViewControl.ParseDictionaryFromFormStorage(dic);
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    if (this.DialogResult != DialogResult.OK || e.Cancel)
      return;
    e.Cancel = !Warning.Show((Form) this, this.Services, this.ContextName, this.GetWarnings());
  }

  /// <summary>Изменилась фокусировка ноды</summary>
  private void TreeViewControl_TreeView_AfterFocusNode([CanBeNull] object sender, [NotNull] NavigatorTreeNodeEventArgs e)
  {
    NavigatorTreeNode node = e.Node;
    string str1 = string.Empty;
    string str2 = string.Empty;
    if (node?.NodeID is NodeID nodeId)
    {
      str1 = nodeId.Caption;
      IMSObjectType objectType = MetaDataHelper.GetObjectType(nodeId.TypeID);
      str2 = objectType != null ? objectType.ObjectName + ":" : string.Empty;
    }
    if (!string.IsNullOrEmpty(str1))
    {
      this._labelFocusedObjectCaption.Text = str1.Replace('\n', ' ').Replace('\t', ' ').Replace('\r', ' ');
      this._labelFocusedObjectCaption.ToolTipText = str1;
    }
    else
    {
      this._labelFocusedObjectCaption.Text = string.Empty;
      this._labelFocusedObjectCaption.ToolTipText = string.Empty;
    }
    if (!string.IsNullOrEmpty(str2))
    {
      this._labelFocusedObjectType.Text = str2;
      this._labelFocusedObjectType.Visible = true;
    }
    else
    {
      this._labelFocusedObjectType.Text = string.Empty;
      this._labelFocusedObjectType.Visible = false;
    }
  }

  [NotNull]
  [ItemNotNull]
  protected virtual IEnumerable<string> GetWarnings()
  {
    string checkedNotLoaded = this.TreeViewControl.WarningCheckedNotLoaded;
    if (!string.IsNullOrEmpty(checkedNotLoaded))
    {
      string checkedNotLoadedSufix = this.GetWarningCheckedNotLoadedSufix();
      yield return !string.IsNullOrEmpty(checkedNotLoadedSufix) ? $"{checkedNotLoaded} {checkedNotLoadedSufix}" : checkedNotLoaded;
    }
    string warningChecksCount = this.TreeViewControl.WarningChecksCount;
    if (!string.IsNullOrEmpty(warningChecksCount))
    {
      string checksCountSufix = this.GetWarningChecksCountSufix();
      yield return !string.IsNullOrEmpty(checksCountSufix) ? $"{warningChecksCount} {checksCountSufix}" : warningChecksCount;
    }
  }

  [CanBeNull]
  protected virtual string GetWarningCheckedNotLoadedSufix() => (string) null;

  [CanBeNull]
  protected virtual string GetWarningChecksCountSufix()
  {
    return LocalizationHolder.rm.GetString("Client.Core_1673");
  }

  /// <summary>Виртуальный метод сбора всех дочерних счётчиков блокировок возможности сохранения результата (напр. кнопка Ok в диалоге)</summary>
  [NotNull]
  [ItemNotNull]
  protected override IEnumerable<ISupportSaveLocks> GetChildSaveLocksCounters()
  {
    yield return (ISupportSaveLocks) this.TreeViewControl;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._toolTips = new ToolTip(this.components);
    this._statusStrip = new StatusStrip();
    this._labelFocusedObjectType = new ToolStripStatusLabel();
    this._labelFocusedObjectCaption = new ToolStripStatusLabel();
    this.TreeViewControl.PanelSelectButtons.SuspendLayout();
    this.TreeViewControl.TreeView.BeginInit();
    this._treeViewControl.SuspendLayout();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this._statusStrip.SuspendLayout();
    this.SuspendLayout();
    this._treeViewControl.AllowChangeObjects = true;
    this._treeViewControl.BtnSelectObjects.Location = new Point(601, 29);
    this._treeViewControl._btnSelectObjects.Location = new Point(601, 29);
    this.TreeViewControl.PanelSelectButtons.Location = new Point(0, 371);
    this.TreeViewControl.PanelSelectButtons.Size = new Size(769, 58);
    this.TreeViewControl.PanelSelectButtons.Controls.SetChildIndex((Control) this._treeViewControl._btnUncheckAll, 0);
    this.TreeViewControl.PanelSelectButtons.Controls.SetChildIndex((Control) this._treeViewControl._btnSelectObjects, 0);
    this.TreeViewControl.PanelSelectButtons.Controls.SetChildIndex((Control) this._treeViewControl._btnCheckAll, 0);
    this._treeViewControl.Size = new Size(769, 437);
    this.TreeViewControl.TreeView.BackgroundImageMode = ImageDrawMode.Tile;
    this.TreeViewControl.TreeView.BorderStyle = BorderStyle.Fixed3D;
    this.TreeViewControl.TreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.TreeViewControl.TreeView.RowEvenStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowOddStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowSelectedStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowStyle.BorderColor = SystemColors.Control;
    this.TreeViewControl.TreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.TreeViewControl.TreeView.RowStyle.BorderWidth = 1;
    this.TreeViewControl.TreeView.RowStyle.WordWrap = false;
    this.TreeViewControl.TreeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.TreeViewControl.TreeView.Size = new Size(769, 347);
    this.TreeViewControl.TreeView.ToolTipComponent = this._toolTips;
    this.TreeViewControl.TreeView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.TreeViewControl_TreeView_AfterFocusNode);
    this._pnlDialogButtons.Location = new Point(0, 437);
    this._pnlDialogButtons.Size = new Size(769, 36);
    this._bevelDialogButtons.Location = new Point(0, 495);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(769, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._panelBtns.Location = new Point(596, 0);
    this._statusStrip.GripStyle = ToolStripGripStyle.Visible;
    this._statusStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._labelFocusedObjectType,
      (ToolStripItem) this._labelFocusedObjectCaption
    });
    this._statusStrip.Location = new Point(0, 473);
    this._statusStrip.Name = "_statusStrip";
    this._statusStrip.Size = new Size(769, 22);
    this._statusStrip.TabIndex = 2;
    this._labelFocusedObjectType.BorderStyle = Border3DStyle.Sunken;
    this._labelFocusedObjectType.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this._labelFocusedObjectType.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
    this._labelFocusedObjectType.ImageAlign = ContentAlignment.MiddleLeft;
    this._labelFocusedObjectType.Margin = new Padding(6, 3, 0, 2);
    this._labelFocusedObjectType.Name = "_labelFocusedObjectType";
    this._labelFocusedObjectType.Size = new Size(0, 17);
    this._labelFocusedObjectType.TextAlign = ContentAlignment.MiddleLeft;
    this._labelFocusedObjectCaption.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this._labelFocusedObjectCaption.ImageAlign = ContentAlignment.MiddleLeft;
    this._labelFocusedObjectCaption.Name = "_labelFocusedObjectCaption";
    this._labelFocusedObjectCaption.Size = new Size(748, 17);
    this._labelFocusedObjectCaption.Spring = true;
    this._labelFocusedObjectCaption.TextAlign = ContentAlignment.MiddleLeft;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(769, 497);
    this.Controls.Add((Control) this._statusStrip);
    this.FormBorderStyle = FormBorderStyle.Sizable;
    this.MinimumSize = new Size(578, 302);
    this.Name = nameof (SelectInObjectCompositionsForm);
    this.Text = "Выбор объектов из состава";
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._statusStrip, 0);
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._treeViewControl, 0);
    this.TreeViewControl.PanelSelectButtons.ResumeLayout(false);
    this.TreeViewControl.TreeView.EndInit();
    this._treeViewControl.ResumeLayout(false);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this._statusStrip.ResumeLayout(false);
    this._statusStrip.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
