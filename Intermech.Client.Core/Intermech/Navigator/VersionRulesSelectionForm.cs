
// Type: Intermech.Navigator.VersionRulesSelectionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>Форма "Выбрать правило подбора версий"</summary>
public class VersionRulesSelectionForm : Form
{
  /// <summary>Фильтр списка правил подбора версий</summary>
  private VersionRulesSelectFilter filter;
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService FObjectTypesIcons;
  /// <summary>ID типа объекта "Общие правила подбора версий"</summary>
  private static int commonRuleTypeID;
  /// <summary>ID типа объекта "Персональные правила подбора версий"</summary>
  private static int personalRuleTypeID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnApply;
  public TreeList listVersionRules;
  private TreeListColumn columnObjectID;
  private TreeListColumn columnCaption;
  private Button btnNew;
  private ToolTip toolTips;

  /// <summary>Конструктор</summary>
  /// <param name="Filter">Фильтр списка правил подбора версий</param>
  /// <param name="EnableMultiselect">true - разрешить выбор сразу нескольких правил подбора версий</param>
  /// <param name="Caption">Заголовок формы (пустая строка - заголовок по умолчанию)</param>
  public VersionRulesSelectionForm(
    VersionRulesSelectFilter Filter,
    bool EnableMultiselect,
    string Caption)
    : this(Filter, EnableMultiselect, Caption, Guid.Empty)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="Filter">Фильтр списка правил подбора версий</param>
  /// <param name="EnableMultiselect">true - разрешить выбор сразу нескольких правил подбора версий</param>
  /// <param name="Caption">Заголовок формы (пустая строка - заголовок по умолчанию)</param>
  /// <param name="selectedRule">Выделенное по-умолчанию правило</param>
  public VersionRulesSelectionForm(
    VersionRulesSelectFilter Filter,
    bool EnableMultiselect,
    string Caption,
    Guid selectedRule)
  {
    this.InitializeComponent();
    this.filter = Filter;
    if (EnableMultiselect)
      this.listVersionRules.BehaviorOptions |= BehaviorOptionsFlags.MultiSelect;
    else
      this.listVersionRules.BehaviorOptions &= ~BehaviorOptionsFlags.MultiSelect;
    if (VersionRulesSelectionForm.commonRuleTypeID == 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectType objectType1 = sessionKeeper.Session.GetObjectType(new Guid("cad001b4-306c-11d8-b4e9-00304f19f545"), false);
        if (objectType1 != null)
          VersionRulesSelectionForm.commonRuleTypeID = objectType1.ObjectType;
        IDBObjectType objectType2 = sessionKeeper.Session.GetObjectType(new Guid("cad001b5-306c-11d8-b4e9-00304f19f545"), false);
        if (objectType2 != null)
          VersionRulesSelectionForm.personalRuleTypeID = objectType2.ObjectType;
      }
    }
    this.FObjectTypesIcons = Statics.IconSrv;
    this.listVersionRules.SelectImageList = this.FObjectTypesIcons != null ? this.FObjectTypesIcons.ImageList : (ImageList) null;
    this.Text = Caption == string.Empty ? VersionRulesSelectionForm.VersionRulesSelectionFormConsts.FormCaption : Caption;
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 50, workingArea.Height / 100 * 60);
    int width1 = workingArea.Width;
    Size size = this.Size;
    int width2 = size.Width;
    int x = (width1 - width2) / 2;
    int height1 = workingArea.Height;
    size = this.Size;
    int height2 = size.Height;
    int y = (height1 - height2) / 2;
    this.Location = new Point(x, y);
    this.FillRulesList(selectedRule);
    this.UpdateControls();
  }

  /// <summary>Убрать за собой мусор</summary>
  /// <param name="disposing">true, если управляемые ресурсы должны быть освобождены</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Список выделенных правил подбора версий</summary>
  public VersionsRule[] SelectedRules
  {
    get
    {
      if (this.listVersionRules.Selection.Count == 0)
        return (VersionsRule[]) null;
      List<VersionsRule> versionsRuleList = new List<VersionsRule>(this.listVersionRules.Selection.Count);
      for (int index = 0; index < this.listVersionRules.Selection.Count; ++index)
      {
        if (this.listVersionRules.Selection[index].Tag is VersionsRule tag)
          versionsRuleList.Add(tag);
      }
      return versionsRuleList.ToArray();
    }
  }

  /// <summary>Список выделенных правил подбора версий</summary>
  public long[] SelectedItems
  {
    get
    {
      if (this.listVersionRules.Selection.Count == 0)
        return (long[]) null;
      List<long> longList = new List<long>(this.listVersionRules.Selection.Count);
      for (int index = 0; index < this.listVersionRules.Selection.Count; ++index)
      {
        if (this.listVersionRules.Selection[index].Tag is VersionsRule tag)
          longList.Add(tag.RuleObjectID);
      }
      return longList.ToArray();
    }
  }

  /// <summary>
  /// Вызвать форму "Выберите правило подбора версий" (стандартный заголовок, разрешено выбирать только одно правило)
  /// </summary>
  /// <param name="Filter">Фильтр списка правил подбора версий</param>
  /// <returns>Результат вызова формы - массив [F_OBJECT_ID] выбранных правил подбора версий или null</returns>
  [STAThread]
  public static long[] Execute(VersionRulesSelectFilter Filter)
  {
    return VersionRulesSelectionForm.Execute(Filter, false, string.Empty);
  }

  /// <summary>
  /// Вызвать форму "Выберите правило подбора версий" (стандартный заголовок)
  /// </summary>
  /// <param name="Filter">Фильтр списка правил подбора версий</param>
  /// <param name="EnableMultiselect">true - разрешить выбор сразу нескольких правил подбора версий</param>
  /// <returns>Результат вызова формы - массив [F_OBJECT_ID] выбранных правил подбора версий или null</returns>
  [STAThread]
  public static long[] Execute(VersionRulesSelectFilter Filter, bool EnableMultiselect)
  {
    return VersionRulesSelectionForm.Execute(Filter, EnableMultiselect, string.Empty);
  }

  /// <summary>Вызвать форму "Выберите правило подбора версий"</summary>
  /// <param name="Filter">Фильтр списка правил подбора версий</param>
  /// <param name="EnableMultiselect">true - разрешить выбор сразу нескольких правил подбора версий</param>
  /// <param name="Caption">Заголовок формы (пустая строка - заголовок по умолчанию)</param>
  /// <returns>Результат вызова формы - массив [F_OBJECT_ID] выбранных правил подбора версий или null</returns>
  [STAThread]
  public static long[] Execute(
    VersionRulesSelectFilter Filter,
    bool EnableMultiselect,
    string Caption)
  {
    using (VersionRulesSelectionForm rulesSelectionForm = new VersionRulesSelectionForm(Filter, EnableMultiselect, Caption))
      return rulesSelectionForm.ShowDialog() != DialogResult.OK ? (long[]) null : rulesSelectionForm.SelectedItems;
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void VersionRulesSelectionForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void VersionRulesSelectionForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Установить статус всех контролов формы</summary>
  internal void UpdateControls()
  {
    this.btnApply.Enabled = this.listVersionRules.Selection.Count > 0;
    this.btnCancel.Enabled = true;
  }

  /// <summary>Проходит ли правило под указанный фильтр</summary>
  /// <param name="rule">Фильтруемое правило</param>
  /// <param name="filter">Фильтр списка правил подбора версий</param>
  /// <returns>true, если правило не противоречит фильтру</returns>
  private bool RulePassFilter(VersionsRule rule, VersionRulesSelectFilter filter)
  {
    return rule != null && (filter == VersionRulesSelectFilter.vrfNone || ((filter & VersionRulesSelectFilter.vrfExcludeCommonRules) != VersionRulesSelectFilter.vrfExcludeCommonRules || rule.RuleObjectType != VersionRulesSelectionForm.commonRuleTypeID) && ((filter & VersionRulesSelectFilter.vrfExcludePersonalRules) != VersionRulesSelectFilter.vrfExcludePersonalRules || rule.RuleObjectType != VersionRulesSelectionForm.personalRuleTypeID) && ((filter & VersionRulesSelectFilter.vrfExcludeStaticRules) != VersionRulesSelectFilter.vrfExcludeStaticRules || rule.HasVariableValues()) && ((filter & VersionRulesSelectFilter.vrfExcludeSystemRules) != VersionRulesSelectFilter.vrfExcludeSystemRules || rule.CurrentRuleType == VersionsRuleType.vrtStandardRule) && ((filter & VersionRulesSelectFilter.vrfExcludeVariableRules) != VersionRulesSelectFilter.vrfExcludeVariableRules || !rule.HasVariableValues()) && ((filter & VersionRulesSelectFilter.vrfExcludeAllVersionsRule) != VersionRulesSelectFilter.vrfExcludeAllVersionsRule || !(rule.RuleObjectGuid == "cad001e3-306c-11d8-b4e9-00304f19f545")));
  }

  /// <summary>Добавить в дерево очередное правило подбора версий</summary>
  /// <returns>Добавленный элемент или null</returns>
  private TreeListNode AddTreeItem(VersionsRule rule)
  {
    if (rule == null)
      return (TreeListNode) null;
    if (!this.RulePassFilter(rule, this.filter))
      return (TreeListNode) null;
    int num = this.FObjectTypesIcons.IndexOf(4, rule.RuleObjectType);
    TreeListNode treeListNode = this.listVersionRules.AppendNode((object) new object[2]
    {
      (object) rule.RuleObjectID,
      (object) rule.RuleObjectCaption
    }, (TreeListNode) null);
    treeListNode.ImageIndex = num;
    treeListNode.SelectImageIndex = num;
    treeListNode.Tag = (object) rule;
    return treeListNode;
  }

  /// <summary>
  /// Подготовить список правил подбора версий согласно текущему фильтру
  /// </summary>
  private void FillRulesList() => this.FillRulesList(Guid.Empty);

  /// <summary>
  /// Подготовить список правил подбора версий согласно текущему фильтру
  /// </summary>
  /// <param name="selectedRule">Выделить правило с таким гуидом, если не надо ничего выделять - Guid.Empty или юзать FillRulesList() без параметров</param>
  private void FillRulesList(Guid selectedRule)
  {
    TreeListNode node = (TreeListNode) null;
    try
    {
      this.listVersionRules.BeginUpdate();
      this.listVersionRules.ClearNodes();
      if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService) || customService.Count == 0)
        return;
      for (int Index = 0; Index < customService.Count; ++Index)
      {
        VersionsRule rule = customService[Index];
        TreeListNode treeListNode = this.AddTreeItem(rule);
        if (selectedRule != Guid.Empty && rule.RuleObjectGuid.Equals(selectedRule.ToString()))
          node = treeListNode;
      }
    }
    finally
    {
      this.listVersionRules.EndUpdate();
      if (node != null)
        this.listVersionRules.SetFocusedNode(node);
      this.UpdateControls();
    }
  }

  /// <summary>Изменился сфокусированный узел в дереве</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoFocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>Двойной клик в списке</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoDoubleClick(object sender, EventArgs e)
  {
    this.UpdateControls();
    if (this.listVersionRules.Selection.Count <= 0)
      return;
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>Создать новое правило подбора версий</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoCreateRule(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service) || service.CreateObjectByTypeDialog(new Guid("cad001b4-306c-11d8-b4e9-00304f19f545")) < 0L)
      return;
    this.FillRulesList();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VersionRulesSelectionForm));
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.listVersionRules = new TreeList();
    this.columnObjectID = new TreeListColumn();
    this.columnCaption = new TreeListColumn();
    this.btnNew = new Button();
    this.toolTips = new ToolTip(this.components);
    this.listVersionRules.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    componentResourceManager.ApplyResources((object) this.listVersionRules, "listVersionRules");
    this.listVersionRules.Columns.AddRange(new TreeListColumn[2]
    {
      this.columnObjectID,
      this.columnCaption
    });
    this.listVersionRules.Name = "listVersionRules";
    this.listVersionRules.Styles.AddReplace("SelectedRow", (object) new ViewStyle("SelectedRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this.listVersionRules.Styles.AddReplace("FocusedRow", (object) new ViewStyle("FocusedRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this.listVersionRules.Styles.AddReplace("OddRow", (object) new ViewStyle("OddRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.None, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LightGreen, SystemColors.WindowText));
    this.listVersionRules.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this.listVersionRules.Styles.AddReplace("HideSelectionRow", (object) new ViewStyle("HideSelectionRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this.listVersionRules.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.DoFocusedNodeChanged);
    this.listVersionRules.DoubleClick += new EventHandler(this.DoDoubleClick);
    componentResourceManager.ApplyResources((object) this.columnObjectID, "columnObjectID");
    this.columnObjectID.Name = "columnObjectID";
    componentResourceManager.ApplyResources((object) this.columnCaption, "columnCaption");
    this.columnCaption.Name = "columnCaption";
    componentResourceManager.ApplyResources((object) this.btnNew, "btnNew");
    this.btnNew.Cursor = Cursors.Default;
    this.btnNew.Name = "btnNew";
    this.toolTips.SetToolTip((Control) this.btnNew, componentResourceManager.GetString("btnNew.ToolTip"));
    this.btnNew.Click += new EventHandler(this.DoCreateRule);
    this.AcceptButton = (IButtonControl) this.btnApply;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.btnNew);
    this.Controls.Add((Control) this.listVersionRules);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnApply);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (VersionRulesSelectionForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Load += new EventHandler(this.VersionRulesSelectionForm_Load);
    this.FormClosed += new FormClosedEventHandler(this.VersionRulesSelectionForm_FormClosed);
    this.listVersionRules.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Свалка констант для формы VersionRulesSelectionForm</summary>
  internal static class VersionRulesSelectionFormConsts
  {
    /// <summary>Заголовок формы - "Выберите правило подбора версий"</summary>
    internal static readonly string FormCaption = LocalizationHolder.rm.GetString("Client.Core_832");
  }
}
