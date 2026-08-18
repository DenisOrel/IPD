// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ImportObjectsFormAdv
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Infralution.Controls;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.Forms;
using Intermech.Common;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Snapshots;
using Intermech.PropertyEditors;
using Intermech.UI;
using Intermech.Workflow;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Форма импорта структуры объекта в структуру задач</summary>
public class ImportObjectsFormAdv : 
  ImportObjectsFormAdvBase,
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
  [NotNull]
  protected ImportObjectSettings _Settings;
  [NotNull]
  protected ImportObjectSettingsBase _SelectedSpecialSettingsForObjType = (ImportObjectSettingsBase) new ImportObjectSettings();
  private readonly string _chooseProtoText = string.Empty;
  [NotNull]
  protected HashSet<NavigatorTreeNode> _SubProjectNodes = new HashSet<NavigatorTreeNode>();
  [NotNull]
  protected HashSet<int> _DisallowedTypes = new HashSet<int>();
  private bool _scriptsComboWasLoaded;
  private bool _finalScriptsComboWasLoaded;
  private MenuButtonItem _commandAsSubProject;
  private EditTaskForm _editTaskForm;
  private Task _oldTask;
  [CanBeNull]
  protected Intermech.Project.Project _Prototype;
  protected int _TaskIndex;
  /// TODO: Хвост, оставленный Лембиевским, надо избавится от него, держать сессию в поле нехорошо
  [CanBeNull]
  protected SessionKeeper _ImportSessionKeeper;
  protected int _LastLevel = 100;
  [CanBeNull]
  protected Task _LastTask;
  protected NavigatorTreeNode _LastNode;
  [NotNull]
  protected Dictionary<object, Task> _AddedTasks = new Dictionary<object, Task>();
  /// <summary>Объект состава, который сейчас обрабатывается (из него берутся значения атрибутов и т.д.)</summary>
  [CanBeNull]
  protected IDBObject _CurrentObject;
  /// <summary>Массив идентификаторов версий импортированных ранее в проект объектов</summary>
  [NotNull]
  protected long[] _ProjectAddedObjectIDs = Array.Empty<long>();
  /// <summary>HashSet для устранения дублей объектов в проекте. Инициализируется списком идентификаторов уже импортированных в проект
  /// объектов, а в дальнейшем туда добавляются импортированные в рамка текущей сессии объекты. Перед импортом каждого объекта
  /// производится проверка, не импортировался ли объект в проект ранее (нет ли его в этом списке)</summary>
  [NotNull]
  protected HashSet<long> _AddedObjectIDs = new HashSet<long>();
  protected List<(Task Task, long ObjectID, int ObjectTypeID, long PrjLinkID)> CreatedTasks;
  protected Guid CurrentRootImportedObjectGuid;
  protected int CurrentProjectIdentLevel;
  protected Stack<ImportObjectsFormAdv.SubProjectObjectImportInfo> _SubProjectObjectImportInfoStack;
  private int _updateControlsEnabledStatusCounter;
  private bool _treeViewControlEnabled = true;
  private IContainer components;

  /// <summary>Тип контрола выбора объектов из структуры, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного формы, в этом случае контрол будет создан указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public static System.Type OverrideSelectObjectsInCompositionControlType
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return ImportObjectsFormAdvBase.OverrideSelectObjectsInCompositionControlType;
    }
    [DebuggerStepThrough] set
    {
      ImportObjectsFormAdvBase.OverrideSelectObjectsInCompositionControlType = !(value != (System.Type) null) || !(value != typeof (SelectObjectsForImportControl)) || value.IsSubclassOf(typeof (SelectObjectsForImportControl)) ? value : throw new Exception($"Tree class must be {typeof (SelectObjectsForImportControl).FullName} or it`s child class");
    }
  }

  protected ImportObjectsFormAdv()
  {
    this._Settings = this.CreateEmptySettings();
    this.InitializeComponent();
  }

  public ImportObjectsFormAdv([NotNull] System.IServiceProvider ownerServices, [NotNull] string contextName)
    : base(ownerServices, contextName)
  {
    this._Settings = this.CreateEmptySettings();
    this.InitializeComponent();
    this._ProjectAddedObjectIDs = this.Project.Tasks.Select<Task, long>((System.Func<Task, long>) (task => task.ImportedObjectVersionID)).Where<long>((System.Func<long, bool>) (importedObjectVersionId => importedObjectVersionId != 0L)).ToArray<long>();
    this._chooseProtoText = this.ButtonPrototype.Text;
    this.TreeView.BeforePaintText = new BeforePaintTextEventHandler(this.BeforePaintNodeText);
  }

  protected DialogResult StandardShowDialog() => base.ShowDialog();

  public new DialogResult ShowDialog() => this.ShowDialog((IReadOnlyCollection<long>) null);

  [NotNull]
  protected virtual ImportObjectSettings _CreateEmptySettings() => new ImportObjectSettings();

  [NotNull]
  protected virtual ImportObjectSettings CreateEmptySettings()
  {
    ImportObjectSettings emptySettings = this._CreateEmptySettings();
    emptySettings.AfterPrototypeChanged += new Action<IDBObject>(this._settings_AfterPrototypeChanged);
    emptySettings.AfterInitTaskScriptChanged += new Action<IDBObject>(this.AfterInitTaskScriptChanged);
    emptySettings.AfterImportAsSubTasksChanged += new ImportObjectSettingsBase.AfterValueChangedDelegate<bool>(this._settings_AfterImportAsSubTasksChanged);
    emptySettings.AfterImportRootObjectsChanged += new ImportObjectSettingsBase.AfterValueChangedDelegate<bool>(this._settings_AfterImportRootObjectsChanged);
    emptySettings.AfterLimitMaxLevelsChanged += new Action(this._settings_AfterLimitMaxLevelsChanged);
    emptySettings.AfterInitTaskParamsChanged += new Action<Task>(this._settings_AfterInitTaskParamsChanged);
    return emptySettings;
  }

  public DialogResult ShowDialog([CanBeNull] IReadOnlyCollection<long> objectVersionIDs)
  {
    this._SelectedSpecialSettingsForObjType = (ImportObjectSettingsBase) this._Settings;
    (this.ComboBoxObjTypes.Items[this.ComboBoxObjTypes.SelectedIndex] as IDComboItem).Data = (object) this._Settings;
    this.AddService<ImportObjectSettings>(this._Settings);
    List<IMSObjectType> childObjectTypes = MetaDataHelperService.Instance.GetApplicabilityChildObjectTypes((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project, (int) (IpsMetadataEntityBase<int>) Intermech.Project.RelationTypes.ImportedObjects);
    List<int> objectTypeIDsThatCanBeImportToProject = childObjectTypes.Select<IMSObjectType, int>((System.Func<IMSObjectType, int>) (imsObjectType => imsObjectType.ObjectTypeID)).ToList<int>(childObjectTypes.Count);
    DialogResult dialogResult;
    IReadOnlyCollection<long> selectedObjectVersions;
    do
    {
      dialogResult = DialogResult.OK;
      selectedObjectVersions = objectVersionIDs == null ? (IReadOnlyCollection<long>) SelectObjectCompositionNavTreeView.ShowSelectObjectsForm(this.ContextName, (IReadOnlyCollection<int>) objectTypeIDsThatCanBeImportToProject) : objectVersionIDs;
      if (selectedObjectVersions.IsNullOrEmpty<long>())
        return DialogResult.Abort;
      selectedObjectVersions = (IReadOnlyCollection<long>) selectedObjectVersions.Select<long, long>((System.Func<long, long>) (selectedObjectVersion => Math.Abs(selectedObjectVersion))).ToList<long>();
      if (objectVersionIDs == null)
      {
        List<long> denyObjectVersionIDs = selectedObjectVersions.Intersect<long>(this.Project.ImportedObjects.Select<ImportedObject, long>((System.Func<ImportedObject, long>) (importedObject => importedObject.ObjectVersionID))).ToList<long>();
        if (denyObjectVersionIDs.Count > 0)
        {
          dialogResult = selectedObjectVersions.Count != 1 ? (denyObjectVersionIDs.Count != 1 ? MessageFuncs.SayError(string.Format(Localization.GetString("ManyObjectAlreadyImported"), (object) string.Concat(denyObjectVersionIDs.Select<long, string>((System.Func<long, string>) (denyObjectVersionId => $"\t {Session.Invoke<QuickObjectInfo>((Session.SessionHandler<QuickObjectInfo>) (session => session.GetObjectInfo(denyObjectVersionId))).Caption} \r\n")))), MessageBoxButtons.YesNoCancel) : MessageFuncs.SayError(string.Format(Localization.GetString("ObjectWithCaptionAlreadyImported"), (object) Session.Invoke<QuickObjectInfo>((Session.SessionHandler<QuickObjectInfo>) (session => session.GetObjectInfo(denyObjectVersionIDs[0]))).Caption), MessageBoxButtons.YesNoCancel)) : MessageFuncs.SayError(Localization.GetString("ObjectAlreadyImported"), MessageBoxButtons.YesNo);
          switch (dialogResult)
          {
            case DialogResult.Cancel:
              return DialogResult.Abort;
            case DialogResult.Yes:
              dialogResult = DialogResult.Retry;
              break;
          }
        }
        selectedObjectVersions = (IReadOnlyCollection<long>) selectedObjectVersions.Except<long>((IEnumerable<long>) denyObjectVersionIDs).ToList<long>();
      }
    }
    while (dialogResult == DialogResult.Retry);
    if (selectedObjectVersions.IsNullOrEmpty<long>())
      return DialogResult.Abort;
    this.Shown += (EventHandler) ((sender, e) => this.InitTreeView(selectedObjectVersions, objectTypeIDsThatCanBeImportToProject));
    return base.ShowDialog();
  }

  protected virtual bool SerializeImportSettings() => true;

  /// <summary>Загрузка свойств в словарь, который будет сохранён в FormStorage при вызове SavePropertiesToStorage</summary>
  public override void FillPropsDictionary([NotNull] Dictionary<string, object> dic)
  {
    this.TreeViewControl.FillPropsDictionary(dic);
    if (!this.SerializeImportSettings())
      return;
    this._Settings.SaveToDictionary(dic, true);
  }

  /// <summary>Загрузка свойств из словаря, полученного из FormStorage при вызове LoadPropertiesFromStorage</summary>
  public override void ParseDictionaryFromFormStorage([NotNull] Dictionary<string, object> dic)
  {
    this.TreeViewControl.ParseDictionaryFromFormStorage(dic);
    if (!this.SerializeImportSettings())
      return;
    this._Settings.LoadFormDictionary(dic);
    this.UpdateUI_fromSettings();
  }

  protected override void OnShown([NotNull] EventArgs e)
  {
    base.OnShown(e);
    if (this.InDesignMode)
      return;
    this.TreeView.Focus();
    this.LoadObjectTypesSpecialSettingsCombo();
    this.LoadTasksScriptsCombo();
    this.LoadFinalScriptsCombo();
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    if (this.DialogResult != DialogResult.OK || e.Cancel)
      return;
    e.Cancel = !Warning.Show((Form) this, this.Services, this.ContextName, this.GetWarnings());
    if (e.Cancel)
      return;
    List<int> childrenIdRecursive = MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) wfFunx.GetApplicableAttachmentTypes((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task, (int) (IpsMetadataEntityBase<int>) Intermech.Project.RelationTypes.TaskAttachment));
    string empty = string.Empty;
    this._DisallowedTypes.Clear();
    this.CheckForDisallowedTypes((IEnumerable<NavigatorTreeNode>) this.TreeViewControl.RootObjectNavigatorTreeNodes, childrenIdRecursive, ref empty);
    if (!(empty != string.Empty))
      return;
    e.Cancel = MessageBox.Show(string.Format(Strings.NotAllObjectsCouldBeSrcData, (object) empty), string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._commandAsSubProject != null)
      {
        this._commandAsSubProject.Dispose();
        this._commandAsSubProject = (MenuButtonItem) null;
      }
      this._Settings.AfterPrototypeChanged -= new Action<IDBObject>(this._settings_AfterPrototypeChanged);
      this._Settings.AfterInitTaskScriptChanged -= new Action<IDBObject>(this.AfterInitTaskScriptChanged);
      this._Settings.AfterImportAsSubTasksChanged -= new ImportObjectSettingsBase.AfterValueChangedDelegate<bool>(this._settings_AfterImportAsSubTasksChanged);
      this._Settings.AfterImportRootObjectsChanged -= new ImportObjectSettingsBase.AfterValueChangedDelegate<bool>(this._settings_AfterImportRootObjectsChanged);
      this._Settings.AfterLimitMaxLevelsChanged -= new Action(this._settings_AfterLimitMaxLevelsChanged);
      this._Settings.AfterInitTaskParamsChanged -= new Action<Task>(this._settings_AfterInitTaskParamsChanged);
    }
    base.Dispose(disposing);
  }

  private void ImportObjectsFormAdv_TreeViewControl_TreeView_FocusRowChanged(
    [CanBeNull] object sender,
    [NotNull] EventArgs e)
  {
    this.UpdateCheckBoxAsSubProjectState();
  }

  private void ImportObjectsFormAdv_TreeViewControl_TreeView_CheckStateChanged(
    [CanBeNull] object sender,
    [NotNull] NodeEventArgs e)
  {
    if (!e.Node.Equals((object) this.TreeView.FocusedNode))
      return;
    this.UpdateCheckBoxAsSubProjectState();
  }

  private void UpdateCheckBoxAsSubProjectState() => this.UpdateControlsEnabledStatus();

  private void _checkBoxAsProject_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.TreeView.FocusedTreeNode == null)
      return;
    if (this.TreeView.FocusedTreeNode.ShowCheckState && this.TreeView.FocusedTreeNode.CheckState != CheckState.Unchecked && this.CheckBoxAsProject.Checked)
    {
      if (this._SubProjectNodes.Contains(this.TreeView.FocusedNode))
        return;
      this._SubProjectNodes.Add(this.TreeView.FocusedTreeNode);
      this.TreeView.Invalidate();
    }
    else
    {
      if (!this._SubProjectNodes.Contains(this.TreeView.FocusedNode))
        return;
      this._SubProjectNodes.Remove(this.TreeView.FocusedTreeNode);
      this.TreeView.Invalidate();
    }
  }

  private void ImportObjectsFormAdv_TreeViewControl_TreeView_BuildTree([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._SubProjectNodes = new HashSet<NavigatorTreeNode>();
    this.TreeView.Invalidate();
  }

  private void _okButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._Settings.DefaultSettings.InitTaskParams == null && (this._Settings.DefaultSettings.PrototypeObjectVersionID != 0L || this._Settings.ObjTypesWithSpecialTypes.Any<int>((System.Func<int, bool>) (objTypeId =>
    {
      ImportObjectSettingsBase objectSettingsBase = this._Settings.SettingsForObjType[objTypeId];
      return objectSettingsBase.PrototypeObjectVersionID != 0L && objectSettingsBase.InitTaskParams == null;
    }))))
    {
      List<int> objTypesWithInitTaskParams = this._Settings.ObjTypesWithSpecialTypes.Where<int>((System.Func<int, bool>) (objTypeId => this._Settings.SettingsForObjType[objTypeId].InitTaskParams != null)).ToList<int>();
      using (IEnumerator<int> enumerator = this.TreeViewControl.CheckedObjectNodes.Where<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (node =>
      {
        if (!node.HasChildren || !node.Full || !node.ShowCheckState)
          return false;
        return !this.TreeViewControl.MaxCheckableObjectLevel.HasValue || this.TreeView.GetObjectNodeLevel(node) <= this.TreeViewControl.MaxCheckableObjectLevel.Value - 1;
      })).Select<NavigatorTreeNode, int>((System.Func<NavigatorTreeNode, int>) (node => (node.NodeID as NodeID).ObjectTypeID)).Distinct<int>().ToList<int>().Where<int>((System.Func<int, bool>) (objectTypeId => !objTypesWithInitTaskParams.Contains(objectTypeId) && !objTypesWithInitTaskParams.Any<int>((System.Func<int, bool>) (objTypeWithInitTaskParams => MetaDataHelperService.Instance.IsObjectTypeChildOf(objectTypeId, objTypeWithInitTaskParams))))).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          int current = enumerator.Current;
          int num = (int) MessageFuncs.SayError(string.Format(Localization.GetString("SubTaskTemplateNotAssigned"), (object) MetaDataHelperService.Instance.GetObjectTypeName(current)), MessageBoxButtons.OK);
          return;
        }
      }
    }
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  protected virtual void InitTreeView(
    [NotNull] IReadOnlyCollection<long> selectedObjectVersions,
    [NotNull] List<int> objectTypeIDsThatCanBeImportToProject)
  {
    this._treeViewControl.Init(this.Services, selectedObjectVersions, (IReadOnlyCollection<int>) objectTypeIDsThatCanBeImportToProject);
  }

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

  private void CheckForDisallowedTypes(
    [NotNull, ItemNotNull] IEnumerable<NavigatorTreeNode> nodes,
    [NotNull] List<int> typeIDs,
    [CanBeNull] ref string disTypes)
  {
    foreach (NavigatorTreeNode node in nodes)
    {
      if (node.CheckState != CheckState.Unchecked || !node.ShowCheckState)
      {
        if (node.NodeID is NodeID nodeId)
        {
          int typeId = nodeId.TypeID;
          if (!typeIDs.Contains(typeId))
          {
            if (this._DisallowedTypes.Contains(typeId))
              break;
            if (disTypes != string.Empty)
              disTypes += ", ";
            disTypes = $"{disTypes}\"{MetaDataHelperService.Instance.GetObjectTypeFullName(typeId)}\"";
            this._DisallowedTypes.Add(typeId);
            break;
          }
        }
        if (node.Children != null && node.Children.Count > 0)
          this.CheckForDisallowedTypes((IEnumerable<NavigatorTreeNode>) node.Children, typeIDs, ref disTypes);
      }
    }
  }

  [CanBeNull]
  protected virtual string GetWarningCheckedNotLoadedSufix() => (string) null;

  [NotNull]
  protected virtual string GetWarningChecksCountSufix()
  {
    return Localization.GetString("ImportTooMuchObjects");
  }

  /// <summary>Дополнительная проверка (кроме IsReadOnly и блокировки сохранения - _saveLocker.IsLocked), должна ли быть включена кнопка OK</summary>
  /// <returns>true если кнопка может быть включена</returns>
  protected override bool OkButtonCanBeEnabled()
  {
    return base.OkButtonCanBeEnabled() && this.TreeViewControl.FirstPaintWasCalled && this.TreeViewControl.ObjectIsChecked;
  }

  private void BeforePaintNodeText([NotNull] NavigatorTreeNode node, [NotNull] ref Style style)
  {
    if (this._SubProjectNodes.Contains(node) && node.ShowCheckState && node.CheckState != CheckState.Unchecked)
    {
      if (style.Font.Style == FontStyle.Bold)
        return;
      style.Font = new Font(style.Font, FontStyle.Bold);
    }
    else
    {
      if (style.Font.Style != FontStyle.Bold)
        return;
      style.ResetFont();
    }
  }

  /// <summary>Обновление UI настроек из данных</summary>
  protected void UpdateUI_fromSettings()
  {
    this.CheckBoxAsSubTask.Checked = this.CheckBoxAsSubTask.Enabled && this._Settings.ImportAsSubTasks;
    this.CheckBoxImportRoot.Checked = this._Settings.ImportRootObjects;
    this.CheckBoxMaxLevels.Checked = this._Settings.LimitMaxLevels;
    this.EditMaxLevels.Value = (Decimal) this._Settings.LimitMaxLevelsCount;
    this.CheckBoxCopySummaries.Checked = this._Settings.CopySummaries;
    this.CheckBoxLinear.Checked = this._Settings.LinearImport;
    this.CheckBoxCreateIteration.Checked = this._Settings.CreateIteration;
    this.EditIterationName.Text = this._Settings.IterationName;
    if (this._Settings.InitTaskParams != null)
      this._Settings.FireAfterInitTaskParamsChanged(this._Settings.InitTaskParams);
    if (this._Settings.PrototypeObjectVersionID != 0L)
      this._Settings.FireAfterPrototypeChanged();
    if (this._Settings.InitTaskScriptID == 0L)
      return;
    this._Settings.FireAfterInitTaskScriptChanged();
  }

  /// <summary>Импортировать корневой элемент</summary>
  private void _checkBoxImportRoot_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._Settings.ImportRootObjects != this.CheckBoxImportRoot.Checked)
      this._Settings.ImportRootObjects = this.CheckBoxImportRoot.Checked;
    this.EditMaxLevels.Minimum = (Decimal) (this._Settings.ImportRootObjects ? 1 : 2);
  }

  /// <summary>Импортировать как подзадачи</summary>
  private void _checkBoxAsSubTask_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._Settings.ImportAsSubTasks == this.CheckBoxAsSubTask.Checked || !this.CheckBoxAsSubTask.Enabled)
      return;
    this._Settings.ImportAsSubTasks = this.CheckBoxAsSubTask.Checked;
  }

  /// <summary>Ограничить ли глубину импорта</summary>
  private void _maxLevelsCheckBox_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._Settings.LimitMaxLevels != this.CheckBoxMaxLevels.Checked)
      this._Settings.LimitMaxLevels = this.CheckBoxMaxLevels.Checked;
    this.UpdateControlsEnabledStatus();
  }

  /// <summary>Число импортируемых уровней структуры (если глубина импорта ограничена)</summary>
  private void _editMaxLevels_ValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._Settings.LimitMaxLevelsCount != (int) this.EditMaxLevels.Value)
      this._Settings.LimitMaxLevelsCount = (int) this.EditMaxLevels.Value;
    if (this.ActiveControl != this.EditMaxLevels)
      return;
    this.TreeView.Focus();
  }

  /// <summary>Создавать вложенные копии суммарных задач</summary>
  private void _checkBoxCopySummaries_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._Settings.CopySummaries == this.CheckBoxCopySummaries.Checked)
      return;
    this._Settings.CopySummaries = this.CheckBoxCopySummaries.Checked;
  }

  /// <summary>Создавать задачи на одном уровне, игнорируя иерархию</summary>
  private void _checkBoxLinear_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._Settings.LinearImport == this.CheckBoxLinear.Checked)
      return;
    this._Settings.LinearImport = this.CheckBoxLinear.Checked;
  }

  /// <summary>Создавать итерации импортируемых объектов</summary>
  private void _checkBoxCreateIteration_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateControlsEnabledStatus();
    if (this._Settings.CreateIteration == this.CheckBoxCreateIteration.Checked)
      return;
    this._Settings.CreateIteration = this.CheckBoxCreateIteration.Checked;
    if (!this.EditIterationName.Enabled)
      return;
    this.EditIterationName.Focus();
  }

  private void _settings_AfterImportAsSubTasksChanged(bool oldValue)
  {
    if (this.CheckBoxAsSubTask.Checked == this._Settings.ImportAsSubTasks || !this.CheckBoxAsSubTask.Enabled)
      return;
    this.CheckBoxAsSubTask.Checked = this._Settings.ImportAsSubTasks;
  }

  private void _settings_AfterImportRootObjectsChanged(bool oldValue)
  {
    this.TreeViewControl.RootObjectsAreCheckable = this._Settings.ImportRootObjects;
  }

  private void _settings_AfterLimitMaxLevelsChanged()
  {
    this.TreeViewControl.MaxCheckableObjectLevel = this._Settings.LimitMaxLevels ? new int?(this._Settings.LimitMaxLevelsCount) : new int?();
  }

  private void LoadTasksScriptsCombo()
  {
    if (!this._scriptsComboWasLoaded)
    {
      if (this.ComboScript.Items.Count == 2)
        this.ComboScript.Items.RemoveAt(1);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable dataTable = sessionKeeper.Session.GetObjectCollection(Intermech.Project.ObjectTypes.ScriptInitTaskAfterImport.ID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) -2,
          (object) -50
        }, new object[1]{ (object) -50 }, new SortOrders[1]
        {
          SortOrders.ASC
        }));
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            string Name = row[1]?.ToString();
            if (Name != null && Name.Trim() != string.Empty)
              this.ComboScript.Items.Add((object) new IDInfo(Math.Abs(Convert.ToInt64(row[0])), Name));
          }
        }
      }
      this._scriptsComboWasLoaded = true;
    }
    this.SelectActualInitTaskScriptIdInComboBox();
  }

  private void SelectActualInitTaskScriptIdInComboBox()
  {
    if (this._SelectedSpecialSettingsForObjType.InitTaskScriptID != 0L)
    {
      IDInfo idInfo = this.ComboScript.Items.OfType<IDInfo>().FirstOrDefault<IDInfo>((System.Func<IDInfo, bool>) (info => info.ID == this._SelectedSpecialSettingsForObjType.InitTaskScriptID));
      if (idInfo != null)
        this.ComboScript.SelectedItem = (object) idInfo;
      else
        this.ComboScript.SelectedIndex = 0;
    }
    else
      this.ComboScript.SelectedIndex = 0;
  }

  private void ComboScript_DropDown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.LoadTasksScriptsCombo();
  }

  /// <summary>Скрипт инициализации</summary>
  private void ComboScript_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._SelectedSpecialSettingsForObjType.InitTaskScriptID = this.ComboScript.SelectedItem is IDInfo selectedItem ? selectedItem.ID : 0L;
  }

  private void AfterInitTaskScriptChanged([CanBeNull] IDBObject iDbObject)
  {
    int num1 = 0;
    if (iDbObject != null)
    {
      long newVersionId = iDbObject.ObjectID;
      if (!this._scriptsComboWasLoaded)
      {
        if (this.ComboScript.Items.Count == 1)
          this.ComboScript.Items.Add((object) new IDInfo(newVersionId, iDbObject.Caption));
        else if (((IDInfo) this.ComboScript.Items[1]).ID != newVersionId)
          this.ComboScript.Items[1] = (object) new IDInfo(newVersionId, iDbObject.Caption);
        num1 = this.ComboScript.Items.Count == 2 ? 1 : 0;
      }
      else
      {
        int num2 = this.ComboScript.Items.OfType<IDInfo>().IndexOfFirst<IDInfo>((Predicate<IDInfo>) (idInfo => idInfo.ID == newVersionId)) + 1;
        num1 = num2 >= 0 ? num2 : 0;
      }
    }
    if (this.ComboScript.SelectedIndex == num1)
      return;
    this.ComboScript.SelectedIndex = num1;
  }

  private void LoadFinalScriptsCombo()
  {
    if (!this._finalScriptsComboWasLoaded)
    {
      if (this.ComboFinalScript.Items.Count == 2)
        this.ComboFinalScript.Items.RemoveAt(1);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable dataTable = sessionKeeper.Session.GetObjectCollection((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.ScriptInitAfterImportTasks).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) -2,
          (object) -50
        }, new object[1]{ (object) -50 }, new SortOrders[1]
        {
          SortOrders.ASC
        }));
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            string Name = row[1]?.ToString();
            if (Name != null && Name.Trim() != string.Empty)
              this.ComboFinalScript.Items.Add((object) new IDInfo(Math.Abs(Convert.ToInt64(row[0])), Name));
          }
        }
      }
      this._finalScriptsComboWasLoaded = true;
    }
    this.SelectActualFinalScriptIdInComboBox();
  }

  private void SelectActualFinalScriptIdInComboBox()
  {
    if (this._Settings.FinalScriptID != 0L)
    {
      IDInfo idInfo = this.ComboFinalScript.Items.OfType<IDInfo>().FirstOrDefault<IDInfo>((System.Func<IDInfo, bool>) (info => info.ID == this._Settings.FinalScriptID));
      if (idInfo != null)
        this.ComboFinalScript.SelectedItem = (object) idInfo;
      else
        this.ComboFinalScript.SelectedIndex = 0;
    }
    else
      this.ComboFinalScript.SelectedIndex = 0;
  }

  private void ComboFinalScript_DropDown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.LoadFinalScriptsCombo();
  }

  /// <summary>Скрипт инициализации</summary>
  private void ComboFinalScript_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._Settings.FinalScriptID = this.ComboFinalScript.SelectedItem is IDInfo selectedItem ? selectedItem.ID : 0L;
  }

  private void AfterFinalScriptChanged([CanBeNull] IDBObject iDbObject)
  {
    int num1 = 0;
    if (iDbObject != null)
    {
      long newVersionId = iDbObject.ObjectID;
      if (!this._scriptsComboWasLoaded)
      {
        if (this.ComboFinalScript.Items.Count == 1)
          this.ComboFinalScript.Items.Add((object) new IDInfo(newVersionId, iDbObject.Caption));
        else if (((IDInfo) this.ComboFinalScript.Items[1]).ID != newVersionId)
          this.ComboFinalScript.Items[1] = (object) new IDInfo(newVersionId, iDbObject.Caption);
        num1 = this.ComboFinalScript.Items.Count == 2 ? 1 : 0;
      }
      else
      {
        int num2 = this.ComboFinalScript.Items.OfType<IDInfo>().IndexOfFirst<IDInfo>((Predicate<IDInfo>) (idInfo => idInfo.ID == newVersionId)) + 1;
        num1 = num2 >= 0 ? num2 : 0;
      }
    }
    if (this.ComboFinalScript.SelectedIndex == num1)
      return;
    this.ComboFinalScript.SelectedIndex = num1;
  }

  private void ImportObjectsFormAdv_TreeViewControl_OnContextPopupInit(
    [CanBeNull] object sender,
    [NotNull] ContextMenuBarItem menu)
  {
    this._commandAsSubProject = this.TreeViewControl.AddCommandToMenu(menu, Strings.ImportAsSubproject, "AsSubproject", true);
    this._commandAsSubProject.Index = 0;
    menu.Items[1].BeginGroup = true;
  }

  private void ImportObjectsFormAdv_TreeViewControl_OnContextPopupRefresh(
    [CanBeNull] object sender,
    [NotNull] ContextMenuBarItem menu,
    [NotNull] NavigatorTreeNode selectedNode)
  {
    this._commandAsSubProject.Enabled = this.CheckBoxAsProject.Enabled;
    this._commandAsSubProject.Checked = this.CheckBoxAsProject.Checked;
  }

  private void ImportObjectsFormAdv_TreeViewControl_OnTranslateContextPopupCommand(
    [CanBeNull] object sender,
    [NotNull, NotWhitespace] string commandName)
  {
    if (!(commandName == "AsSubproject") || !this._commandAsSubProject.Enabled)
      return;
    this.CheckBoxAsProject.Checked = !this.CheckBoxAsProject.Checked;
  }

  private bool ChoosePrototype()
  {
    IReadOnlyList<IDBObjectID> source = SelectDialog.Objects((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project, Localization.GetString("ChooseProject"), operationName: this.ContextName + "/Prototype");
    if (source == null || source.Count <= 0)
      return false;
    this._SelectedSpecialSettingsForObjType.PrototypeObjectVersionID = source.First<IDBObjectID>().Value;
    return true;
  }

  private void _settings_AfterPrototypeChanged([CanBeNull] IDBObject obj)
  {
    this.CheckBoxProto.Checked = this._SelectedSpecialSettingsForObjType.PrototypeObjectVersionID != 0L;
    this.ButtonPrototype.Text = obj?.Caption ?? this._chooseProtoText;
    this.CheckBoxInitTaskSettings.Text = obj == null ? Localization.GetString("UsePrototypeForTask") : Localization.GetString("UsePrototypeForSubprojects");
  }

  private void _buttonPrototype_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.ChoosePrototype();
  }

  /// <summary>Создавать задачи по прототипу</summary>
  private void _checkBoxProto_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if ((!this.CheckBoxProto.Checked || this._SelectedSpecialSettingsForObjType.PrototypeObjectVersionID != 0L || this.ChoosePrototype()) && (this.CheckBoxProto.Checked || this._SelectedSpecialSettingsForObjType.PrototypeObjectVersionID == 0L))
      return;
    this._SelectedSpecialSettingsForObjType.PrototypeObjectVersionID = 0L;
  }

  private void _settings_AfterInitTaskParamsChanged([NotNull] Task obj)
  {
    this.CheckBoxInitTaskSettings.Checked = this._SelectedSpecialSettingsForObjType.InitTaskParams != null;
  }

  private void _checkBoxInitTaskSettings_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.CheckBoxInitTaskSettings.Checked == (this._SelectedSpecialSettingsForObjType.InitTaskParams != null))
      return;
    if (this.CheckBoxInitTaskSettings.Checked)
    {
      this.EditInitTaskSettings();
      if (this._SelectedSpecialSettingsForObjType.InitTaskParams != null)
        return;
      this.CheckBoxInitTaskSettings.Checked = false;
    }
    else
    {
      this._oldTask = this._oldTask ?? this._SelectedSpecialSettingsForObjType.InitTaskParams;
      this._Settings.InitTaskParams = (Task) null;
    }
  }

  private void InitTaskSettings_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.EditInitTaskSettings();
  }

  /// <summary>Изменить параметры, используемые как шаблон для всех новых задач</summary>
  private void EditInitTaskSettings()
  {
    IMSAttributeType attributeType = MetaDataHelperService.Instance.GetAttributeType(-50);
    Task task = this._SelectedSpecialSettingsForObjType.InitTaskParams ?? new Task($"%{attributeType.Name}%");
    task._SessionProvider = ClientSessionProvider2.Provider;
    this._editTaskForm = new EditTaskForm();
    if (!this._editTaskForm.EditTask(task, false))
      return;
    if (this._SelectedSpecialSettingsForObjType.InitTaskParams == null)
      this._SelectedSpecialSettingsForObjType.InitTaskParams = task;
    else
      this._SelectedSpecialSettingsForObjType.FireAfterInitTaskParamsChanged(task);
  }

  /// <summary>Заполнение комбобокса типов объектов, которым заданы специальные настройки</summary>
  private void LoadObjectTypesSpecialSettingsCombo()
  {
    foreach (int typesWithSpecialType in (IEnumerable<int>) this._Settings.ObjTypesWithSpecialTypes)
      this.RegisterObjTypeSpecialSettingsInComboBox(typesWithSpecialType);
  }

  /// <summary>Добавить в комбобокс тип объекта</summary>
  private void RegisterObjTypeSpecialSettingsInComboBox(int objectTypeId)
  {
    IMSObjectType objectType = MetaDataHelperService.Instance.GetObjectType(objectTypeId);
    if (objectType == null || this._ObjTypesIconsService == null)
      return;
    this.ComboBoxObjTypes.Items.Add((object) new IDComboItem(objectType.ObjectTypeName, (long) objectTypeId, this._ObjTypesIconsService.IndexOf(4, objectTypeId))
    {
      Data = (object) this._Settings.SettingsForObjType[objectTypeId]
    });
  }

  /// <summary>Кнопка "Добавить тип объекта" в специальных настройках для типов объектов</summary>
  private void _btnAddObjType_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Типы объектов", typeof (ObjectTypeFolder), false);
    selectorForm.StartPosition = FormStartPosition.CenterScreen;
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList == null || selectorForm.IDList.Count <= 0)
      return;
    int focusObjType = -1;
    foreach (int objTypeId in selectorForm.IDList.OfType<int>().Except<int>((IEnumerable<int>) this._Settings.ObjTypesWithSpecialTypes).ToList<int>(selectorForm.IDList.Count))
    {
      this.AddObjTypeSpecialSettings(objTypeId);
      focusObjType = objTypeId;
    }
    if (focusObjType == -1)
      focusObjType = Convert.ToInt32(selectorForm.IDList[selectorForm.IDList.Count - 1]);
    if (focusObjType == -1)
      return;
    this.FocusObjType(focusObjType);
  }

  /// <summary>Выбрать в специальных настройках для типов объектов тип объекта с переданным идентификатором</summary>
  private void FocusObjType(int focusObjType)
  {
    int num = this.ComboBoxObjTypes.Items.OfType<IDComboItem>().IndexOfFirst<IDComboItem>((Predicate<IDComboItem>) (item => item.ID == (long) focusObjType));
    if (num == -1)
      num = 0;
    this.ComboBoxObjTypes.SelectedIndex = num;
  }

  /// <summary>Пользователь создал новые настройки</summary>
  private void AddObjTypeSpecialSettings(int objTypeId)
  {
    this._Settings.AddNewSpecialSettings(objTypeId);
    this.RegisterObjTypeSpecialSettingsInComboBox(objTypeId);
  }

  /// <summary>Выбранный тип объекта в комбобоксе специальных настроек для типов объектов</summary>
  protected int SelectedObjectType
  {
    get
    {
      return (int) ((IDComboItem) this.ComboBoxObjTypes.Items[this.ComboBoxObjTypes.SelectedIndex]).ID;
    }
  }

  /// <summary>Был выбран другой тип объекта</summary>
  private void _comboBoxObjTypes_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._SelectedSpecialSettingsForObjType = (ImportObjectSettingsBase) ((IDComboItem) this.ComboBoxObjTypes.Items[this.ComboBoxObjTypes.SelectedIndex]).Data;
    this.CheckBoxInitTaskSettings.Checked = this._SelectedSpecialSettingsForObjType.InitTaskParams != null;
    this.CheckBoxProto.Checked = this._SelectedSpecialSettingsForObjType.PrototypeObjectVersionID != 0L;
    QuickObjectInfo prototypeObject = this._SelectedSpecialSettingsForObjType.PrototypeObject;
    this.ButtonPrototype.Text = !prototypeObject.Empty ? prototypeObject.Caption : this._chooseProtoText;
    this.SelectActualInitTaskScriptIdInComboBox();
    this.SelectActualFinalScriptIdInComboBox();
    this._editTaskForm = (EditTaskForm) null;
    this._oldTask = (Task) null;
    this.BtnDelObjType.Enabled = this.ComboBoxObjTypes.SelectedIndex > 0;
  }

  /// <summary>Кнопка "удалить тип объекта"</summary>
  private void _btnDelObjType_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    int selectedObjectType = this.SelectedObjectType;
    if (MessageFuncs.Confirm(string.Format(Localization.GetString("DeleteSettingsForObjectType"), (object) MetaDataHelperService.Instance.GetObjectTypeName(selectedObjectType))) != DialogResult.OK)
      return;
    int selectedIndex = this.ComboBoxObjTypes.SelectedIndex;
    this._Settings.DeleteSpecialSettings(selectedObjectType);
    this.ComboBoxObjTypes.Items.RemoveAt(this.ComboBoxObjTypes.SelectedIndex);
    this.ComboBoxObjTypes.SelectedIndex = selectedIndex < this.ComboBoxObjTypes.Items.Count ? selectedIndex : this.ComboBoxObjTypes.Items.Count - 1;
  }

  private void ImportObjectsFormAdv_Load([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._TaskIndex = this.ProjectView != null ? Math.Max(0, this.ProjectView.SelectedIndex) : -1;
    if (this.ProjectView == null || this.ProjectView.HasSelected)
      return;
    this.CheckBoxAsSubTask.Enabled = false;
  }

  /// <summary>Импорт задач</summary>
  public void ImportTasks()
  {
    if (this._SelectedSpecialSettingsForObjType.PrototypeObjectVersionID != 0L)
    {
      this._Prototype = new Intermech.Project.Project();
      this._Prototype.AssignProperties((Task) this.Project);
      this._Prototype.Load(this._SelectedSpecialSettingsForObjType.PrototypeObjectVersionID, new bool?(false));
    }
    this.ProjectView.GridView.CancelEdit();
    this.Project.BeginUpdate();
    using (this._ImportSessionKeeper = new SessionKeeper())
    {
      try
      {
        int level = 0;
        if (this.ProjectView.HasSelected && this._TaskIndex >= 0 && this._TaskIndex < this.Project.Tasks.Count)
        {
          level = this.Project.Tasks[this._TaskIndex].IndentLevel;
          if (this._Settings.ImportAsSubTasks)
            ++level;
        }
        this._SubProjectObjectImportInfoStack = (Stack<ImportObjectsFormAdv.SubProjectObjectImportInfo>) null;
        this._AddedObjectIDs.Clear();
        this._AddedObjectIDs.AddRange<long>((IEnumerable<long>) this._ProjectAddedObjectIDs);
        this.CreatedTasks = (List<(Task, long, int, long)>) null;
        foreach (NavigatorTreeNode navigatorTreeNode in (IEnumerable<NavigatorTreeNode>) this.TreeViewControl.RootObjectNavigatorTreeNodes)
        {
          if (navigatorTreeNode._checkState != CheckState.Unchecked || !this._Settings.ImportRootObjects)
          {
            this.CreatedTasks = new List<(Task, long, int, long)>();
            long num = Math.Abs((navigatorTreeNode.NodeID as NodeID).ObjectID);
            this.CurrentRootImportedObjectGuid = this._ImportSessionKeeper.Session.GetObjectInfo(num).VersionGuid;
            this.CurrentProjectIdentLevel = level;
            long objectIteration = this._Settings.CreateIteration ? this.CreateObjectIteration(navigatorTreeNode) : 0L;
            this.Project.AddImportedObjectInfo(num, objectIteration, this._Settings);
            this.AddChecked(navigatorTreeNode, level, true);
            if (this._SubProjectObjectImportInfoStack != null)
              this._SubProjectObjectImportInfoStack.Clear();
          }
        }
        if (this.CreatedTasks == null || this.CreatedTasks.Count <= 0 || this._SelectedSpecialSettingsForObjType.FinalScriptID == 0L)
          return;
        MiscFunx.ExecScript(this._ImportSessionKeeper.Session, this._SelectedSpecialSettingsForObjType.FinalScriptID, (object) this.Project, (object) this.CreatedTasks);
      }
      finally
      {
        this.Project.EndUpdate();
        this.Project.PropertiesChanged();
        this.Project.DebugClearCache();
      }
    }
  }

  /// <summary>Проверка, что мы ещё не перешли на уровень вложенности, выходящий за ограничение в настройках (если оно установлено)</summary>
  protected bool CheckMaxLevel([NotNull] NavigatorTreeNode node)
  {
    return !this._Settings.LimitMaxLevels || this.TreeView.GetObjectNodeLevel(node) <= this._Settings.LimitMaxLevelsCount;
  }

  private void AddChecked([NotNull] NavigatorTreeNode node, int level, bool addChildren)
  {
    if (node.CheckState == CheckState.Unchecked && (this.TreeView.GetObjectNodeLevel(node) != 1 || this._Settings.ImportRootObjects) || !this.CheckMaxLevel(node))
      return;
    if (this._Settings.ImportRootObjects || this.TreeView.GetObjectNodeLevel(node) > 1)
    {
      long objectId = (node.NodeID as NodeID).ObjectID;
      if (this._AddedObjectIDs.Contains(objectId))
        return;
      int typeId = (node.NodeID as NodeID).TypeID;
      if (this._DisallowedTypes.Contains(typeId))
        return;
      this._AddedObjectIDs.Add(objectId);
      this._SelectedSpecialSettingsForObjType = this._Settings.SettingsForObjType[typeId];
      if (this.AddTask(node, level) == null)
        return;
      if (!this._Settings.LinearImport)
        ++level;
    }
    if (!addChildren)
      return;
    foreach (NavigatorTreeNode objectChild in (IEnumerable<NavigatorTreeNode>) node.GetObjectChilds())
      this.AddChecked(objectChild, level, true);
  }

  [CanBeNull]
  private Task AddTask([NotNull] NavigatorTreeNode node, int level, bool creatingSummary = false)
  {
    bool flag = this._Prototype != null && ((!node.HasChildren || !node.Full ? 1 : (node.GetObjectChilds().All<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (childNode => !childNode.ShowCheckState || childNode.CheckState == CheckState.Unchecked)) ? 1 : 0)) | (creatingSummary ? 1 : 0)) != 0;
    if (this._SubProjectNodes.Contains(node))
      flag = false;
    if (!flag)
      return this._addTask(node, level, this._SelectedSpecialSettingsForObjType.InitTaskParams, creatingSummary);
    try
    {
      Task task1 = (Task) null;
      foreach (Task task2 in (System.Collections.ObjectModel.Collection<Task>) this._Prototype.Tasks)
      {
        if (!task2.IsProjectSummaryTask)
        {
          Task task3 = this._addTask(node, level + task2.IndentLevel, task2, creatingSummary);
          if (this._SelectedSpecialSettingsForObjType.InitTaskParams != null && this._SelectedSpecialSettingsForObjType.InitTaskParams.Start != DateTime.MinValue && task3.Start != DateTime.MinValue)
            task3.Start = this._SelectedSpecialSettingsForObjType.InitTaskParams.Start + (task2.Start - this._Prototype.Start);
          this._LastTask = (Task) null;
          this._LastNode = (NavigatorTreeNode) null;
          if (task1 == null)
            task1 = task3;
        }
      }
      if (!creatingSummary && this._Settings.CopySummaries)
      {
        this._LastTask = task1;
        this._LastNode = node;
        this._LastLevel = level;
      }
      foreach (Task task4 in (System.Collections.ObjectModel.Collection<Task>) this._Prototype.Tasks)
      {
        if (task4.Dependencies.Count > 0)
        {
          Task addedByPrototype1 = this.FindAddedByPrototype(task4);
          if (addedByPrototype1 != null)
          {
            foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) task4.Dependencies)
            {
              Task addedByPrototype2 = this.FindAddedByPrototype(dependency.DependentOfTask);
              if (addedByPrototype2 != null)
                new Dependency(addedByPrototype2, dependency.DependencyType).Task = addedByPrototype1;
            }
          }
        }
      }
      return task1;
    }
    finally
    {
      this._AddedTasks.Clear();
    }
  }

  [CanBeNull]
  protected Task FindAddedByPrototype([NotNull] Task proto)
  {
    Task addedByPrototype;
    this._AddedTasks.TryGetValue((object) proto, out addedByPrototype);
    return addedByPrototype;
  }

  [NotNull]
  protected string GetMacroValue([NotNull] string name)
  {
    if (this._CurrentObject != null)
    {
      object[] valuesByName = this._CurrentObject.GetValuesByName(name, false);
      if (valuesByName != null && valuesByName.Length != 0)
        return valuesByName[0]?.ToString() ?? string.Empty;
    }
    return string.Empty;
  }

  [CanBeNull]
  protected Task _addTask([NotNull] NavigatorTreeNode node, int level, [CanBeNull] Task proto, bool creatingSummary = false)
  {
    NodeID nodeId = node.NodeID as NodeID;
    if (!creatingSummary && this._LastNode != null && level > this._LastLevel)
      this.AddTask(this._LastNode, level, true);
    string name = nodeId?.Caption ?? string.Empty;
    this._CurrentObject = (IDBObject) null;
    if (proto != null)
    {
      if (nodeId != null)
        this._CurrentObject = this._ImportSessionKeeper.Session.GetObject(nodeId.ObjectID, false);
      name = StringFuncs.ReplaceMacros(proto.Name, new StringFuncs.GetMacroValueDelegate(this.GetMacroValue));
    }
    Task task = this._addTask(node, name, proto, level, this._SubProjectNodes.Contains(node));
    task.Tag = (object) proto;
    if (proto != null)
      this._AddedTasks[task.Tag] = task;
    if (nodeId != null)
    {
      PrjAttachment prjAttachment = new PrjAttachment();
      prjAttachment.ObjectID = nodeId.ObjectID;
      prjAttachment.ID = nodeId.ID;
      prjAttachment.TypeID = nodeId.ObjectTypeID;
      prjAttachment.Kind = PrjAttachKind.SrcData;
      if (!task.Attachments.Contains((Attachment) prjAttachment))
        task.Attachments.Add((Attachment) prjAttachment);
    }
    if (this._SelectedSpecialSettingsForObjType.InitTaskScriptID != 0L)
    {
      if (this._CurrentObject == null && nodeId != null)
        this._CurrentObject = this._ImportSessionKeeper.Session.GetObject(nodeId.ObjectID, false);
      object[] objArray = new object[2]
      {
        (object) task,
        (object) this._CurrentObject
      };
      MiscFunx.ExecScript(this._ImportSessionKeeper.Session, this._SelectedSpecialSettingsForObjType.InitTaskScriptID, objArray);
      if (task != objArray[0] && objArray[0] == null)
      {
        this.Project.Tasks.Remove(task);
        --this._TaskIndex;
        return (Task) null;
      }
    }
    if (this._Settings.CopySummaries)
    {
      this._LastTask = task;
      this._LastNode = node;
      this._LastLevel = level;
    }
    return task;
  }

  [NotNull]
  private Task _addTask(
    [NotNull] NavigatorTreeNode node,
    [NotNull] string name,
    [CanBeNull] Task proto,
    int level,
    bool isProject = false)
  {
    NodeID nodeId = (NodeID) node.NodeID;
    this.Project.Tasks.ResetBindings();
    if (this._SubProjectObjectImportInfoStack != null && level <= this.CurrentProjectIdentLevel && this._SubProjectObjectImportInfoStack.Count > 0)
    {
      ImportObjectsFormAdv.SubProjectObjectImportInfo objectImportInfo = this._SubProjectObjectImportInfoStack.Pop();
      this.CurrentRootImportedObjectGuid = objectImportInfo.RootImportedObjectGuid;
      this.CurrentProjectIdentLevel = objectImportInfo.ProjectIdentLevel;
    }
    Task task;
    if (isProject)
    {
      Intermech.Project.Project project = new Intermech.Project.Project(name);
      long objectIteration = this._Settings.CreateIteration ? this.CreateObjectIteration(node) : 0L;
      ImportObjectSettings importSettings = (ImportObjectSettings) this._Settings.Clone();
      importSettings.LimitMaxLevelsCount = Math.Max(1, this._Settings.LimitMaxLevelsCount - this.TreeView.GetObjectNodeLevel(node));
      importSettings.ImportRootObjects = false;
      this._SubProjectObjectImportInfoStack = this._SubProjectObjectImportInfoStack ?? new Stack<ImportObjectsFormAdv.SubProjectObjectImportInfo>();
      this._SubProjectObjectImportInfoStack.Push(new ImportObjectsFormAdv.SubProjectObjectImportInfo(this.CurrentRootImportedObjectGuid, this.CurrentProjectIdentLevel));
      this.CurrentRootImportedObjectGuid = this._ImportSessionKeeper.Session.GetObjectInfo(nodeId.ObjectID).VersionGuid;
      this.CurrentProjectIdentLevel = level;
      project.AddImportedObjectInfo(nodeId.ObjectID, objectIteration, importSettings);
      task = (Task) project;
    }
    else
    {
      ImportObjectsFormAdv.ClonedTask clonedTask = new ImportObjectsFormAdv.ClonedTask(name);
      if (proto != null)
        clonedTask.LoadFrom(proto);
      task = (Task) clonedTask;
    }
    if (this.ProjectView != null && this.ProjectView.HasSelected)
    {
      if (this._TaskIndex >= this.Project.Tasks.Count)
        this.Project.Tasks.Add(task);
      else
        this.Project.Tasks.Insert(this._TaskIndex + 1, task);
    }
    else
      this.Project.Tasks.Insert(this._TaskIndex, task);
    ++this._TaskIndex;
    if (name != null)
      task.Name = name;
    task.IndentLevel = level;
    if (proto != null && proto.IndentLevel == -1 && proto.Assignments.Count > 0)
      task.Assignments.AddRange(proto.Assignments.Select<Assignment, Assignment>((System.Func<Assignment, Assignment>) (assignment => new Assignment(assignment.Resource, assignment.Units, assignment.MaxUnits))));
    task.LinkWithImportedObject(this.CurrentRootImportedObjectGuid, nodeId.ObjectID, nodeId.RelGuid);
    this.CreatedTasks.Add((task, nodeId.ObjectID, nodeId.ObjectTypeID, nodeId.PrjLinkID));
    return task;
  }

  /// <summary>Создание итерации, сохранение настроек импорта</summary>
  /// <returns>Идентификатор версии созданного объекта типа "Настройки импорта"</returns>
  protected long CreateObjectIteration([NotNull] NavigatorTreeNode node)
  {
    return new SnapshotMasterModel((IDBTypedObjectID) (DBTypedObjectID) (NodeID) node.NodeID).CreateNewSnapshot(node.EnumerationWithChilds().Select<NavigatorTreeNode, long>((System.Func<NavigatorTreeNode, long>) (childNode => childNode.NodeID.GetObjVerID(false))).Where<long>((System.Func<long, bool>) (objID => objID != 0L)).ToList<long>(), this._Settings.IterationName.Trim());
  }

  /// <summary>Виртуальный метод сбора всех дочерних счётчиков блокировок возможности сохранения результата (напр. кнопка Ok в диалоге)</summary>
  [ItemNotNull]
  protected override IEnumerable<ISupportSaveLocks> GetChildSaveLocksCounters()
  {
    yield return (ISupportSaveLocks) this.TreeViewControl;
  }

  public void UpdateControlsEnabledStatus()
  {
    if (this._updateControlsEnabledStatusCounter != 0)
      return;
    ++this._updateControlsEnabledStatusCounter;
    try
    {
      this.PanelRight.GetAllChilds().ExceptTypes<Control>((ICollection<System.Type>) new System.Type[4]
      {
        typeof (Label),
        typeof (Bevel),
        typeof (Panel),
        typeof (GroupBox)
      }).Except<Control>((IEnumerable<Control>) new Control[4]
      {
        (Control) this.CheckBoxAsProject,
        (Control) this.EditMaxLevels,
        (Control) this.EditIterationName,
        (Control) this.ComboBoxObjTypes
      }).InvokeForAll<Control>((Action<Control>) (control => control.Enabled = this._treeViewControlEnabled));
      this.ComboBoxObjTypes.Enabled = this._treeViewControlEnabled;
      this.ComboBoxObjTypes.BackColor = this.ComboBoxObjTypes.Enabled ? SystemColors.Window : SystemColors.Control;
      this.CheckBoxAsProject.Enabled = this.TreeView.FocusedTreeNode != null && this.TreeView.FocusedTreeNode.ShowCheckState && this.TreeView.FocusedTreeNode.CheckState != CheckState.Unchecked && this._SubProjectNodes.Contains(this.TreeView.FocusedNode);
      this.EditMaxLevels.Enabled = this._treeViewControlEnabled && this.CheckBoxMaxLevels.Checked;
      this.EditMaxLevels.BackColor = this.EditMaxLevels.Enabled ? SystemColors.Window : SystemColors.Control;
      this.EditIterationName.Enabled = this._treeViewControlEnabled && this.CheckBoxCreateIteration.Checked;
      this.EditIterationName.BackColor = this.EditIterationName.Enabled ? SystemColors.Window : SystemColors.Control;
      this.LabelIterationName.Enabled = this._treeViewControlEnabled && this.EditIterationName.Enabled;
    }
    finally
    {
      --this._updateControlsEnabledStatusCounter;
    }
  }

  private void ImportObjectsFormAdv_TreeViewControl_OnControlsEnabled([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._treeViewControlEnabled = true;
    this.UpdateControlsEnabledStatus();
  }

  private void ImportObjectsFormAdv_TreeViewControl_OnControlsDisabled([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._treeViewControlEnabled = false;
    this.UpdateControlsEnabledStatus();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImportObjectsFormAdv));
    this.TreeViewControl.PanelSelectButtons.SuspendLayout();
    this._treeViewControl.SuspendLayout();
    this._panelTreeCaption.SuspendLayout();
    this._panelRight.SuspendLayout();
    this._groupBoxSettings.SuspendLayout();
    this._editMaxLevels.BeginInit();
    this._panelRightDown.SuspendLayout();
    this._panel1.SuspendLayout();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this.TreeViewControl.TreeView.BeginInit();
    this.SuspendLayout();
    this._treeViewControl.AllowChangeObjects = true;
    this.TreeViewControl.BtnClearSorting.AutoToggle = AutoToggleType.Single;
    this.TreeViewControl.BtnClearSorting.CommandName = "btCancelSort";
    this.TreeViewControl.BtnClearSorting.ImageIndex = 9;
    this.TreeViewControl.BtnClearSorting.ToolTipText = "Режим ручной сортировки";
    this._treeViewControl.BtnSelectObjects.Anchor = AnchorStyles.Top | AnchorStyles.Left;
    this._treeViewControl.BtnSelectObjects.Location = new Point(173, 6);
    this.TreeViewControl.BtnSetupSorting.CommandName = "btSetupSorting";
    this.TreeViewControl.BtnSetupSorting.ImageIndex = 10;
    this.TreeViewControl.BtnSetupSorting.ToolTipText = "Выполнить настройку ручной сортировки";
    this.TreeViewControl.ImagesToolbar.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ImportObjectsFormAdv.TreeViewControl.ImagesToolbar.ImageStream");
    this.TreeViewControl.ImagesToolbar.TransparentColor = Color.Transparent;
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(0, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(1, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(2, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(3, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(4, "ручная_сортировка.png");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(5, "настройка_ручной_сортировки.png");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(6, "SettingsIcons");
    this.TreeViewControl.LabelSpace.BeginGroup = true;
    this.TreeViewControl.LabelSpace.CommandName = "labelSpace";
    this.TreeViewControl.LabelSpace.Enabled = false;
    this.TreeViewControl.LabelSpace.Stretch = true;
    this.TreeViewControl.LabelSpace.Text = " ";
    this.TreeViewControl.LabelSpace.ToolTipText = " ";
    this.TreeViewControl.PanelSelectButtons.Location = new Point(0, 474);
    this.TreeViewControl.PanelSelectButtons.Controls.SetChildIndex((Control) this._treeViewControl._btnUncheckAll, 0);
    this.TreeViewControl.PanelSelectButtons.Controls.SetChildIndex((Control) this._treeViewControl._btnCheckAll, 0);
    this.TreeViewControl.TreeToolbar.FlipLastItem = true;
    this.TreeViewControl.TreeToolbar.FullMenus = true;
    this.TreeViewControl.TreeToolbar.Guid = new Guid("3fb71a02-4b93-44ea-84a6-db6e9ca5869f");
    this.TreeViewControl.TreeToolbar.Hidden = false;
    this.TreeViewControl.TreeToolbar.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.TreeViewControl.BtnClearSorting,
      (ToolbarItemBase) this.TreeViewControl.BtnSetupSorting,
      (ToolbarItemBase) this.TreeViewControl.LabelSpace
    });
    this.TreeViewControl.TreeToolbar.Location = new Point(0, 0);
    this.TreeViewControl.TreeToolbar.Name = "_tbTreePanel";
    this.TreeViewControl.TreeToolbar.Size = new Size(562, 24);
    this.TreeViewControl.TreeToolbar.TabIndex = 8;
    this.TreeViewControl.TreeToolbar.Text = "";
    this._treeViewControl.OnContextPopupInit += new SelectObjectCompositionNavTreeView.PopupEvent(this.ImportObjectsFormAdv_TreeViewControl_OnContextPopupInit);
    this._treeViewControl.OnContextPopupRefresh += new SelectObjectCompositionNavTreeView.RefreshContextPopupEvent(this.ImportObjectsFormAdv_TreeViewControl_OnContextPopupRefresh);
    this._treeViewControl.OnTranslateContextPopupCommand += new SelectObjectCompositionNavTreeView.TranslateContextPopupCommandEvent(this.ImportObjectsFormAdv_TreeViewControl_OnTranslateContextPopupCommand);
    this._treeViewControl.OnControlsEnabled += new EventHandler(this.ImportObjectsFormAdv_TreeViewControl_OnControlsEnabled);
    this._treeViewControl.OnControlsDisabled += new EventHandler(this.ImportObjectsFormAdv_TreeViewControl_OnControlsDisabled);
    this._panelTreeCaption.Size = new Size(568, 28);
    this._panelRight.Location = new Point(568, 0);
    this._checkBoxAsProject.CheckedChanged += new EventHandler(this._checkBoxAsProject_CheckedChanged);
    this._checkBoxImportRoot.CheckedChanged += new EventHandler(this._checkBoxImportRoot_CheckedChanged);
    this._comboScript.DropDown += new EventHandler(this.ComboScript_DropDown);
    this._comboScript.SelectedIndexChanged += new EventHandler(this.ComboScript_SelectedIndexChanged);
    this._buttonPrototype.Click += new EventHandler(this._buttonPrototype_Click);
    this._checkBoxProto.CheckedChanged += new EventHandler(this._checkBoxProto_CheckedChanged);
    this._editMaxLevels.ValueChanged += new EventHandler(this._editMaxLevels_ValueChanged);
    this._checkBoxCopySummaries.CheckedChanged += new EventHandler(this._checkBoxCopySummaries_CheckedChanged);
    this._checkBoxCreateIteration.CheckedChanged += new EventHandler(this._checkBoxCreateIteration_CheckedChanged);
    this._checkBoxLinear.CheckedChanged += new EventHandler(this._checkBoxLinear_CheckedChanged);
    this._checkBoxMaxLevels.CheckedChanged += new EventHandler(this._maxLevelsCheckBox_CheckedChanged);
    this._panel1.Size = new Size(568, 549);
    this._initTaskSettings.Click += new EventHandler(this.InitTaskSettings_Click);
    this._checkBoxInitTaskSettings.CheckedChanged += new EventHandler(this._checkBoxInitTaskSettings_CheckedChanged);
    this._comboBoxObjTypes.SelectedIndexChanged += new EventHandler(this._comboBoxObjTypes_SelectedIndexChanged);
    this._bevelObjTypes.Style = BevelStyle.Lowered;
    this._btnAddObjType.Click += new EventHandler(this._btnAddObjType_Click);
    this._btnDelObjType.Click += new EventHandler(this._btnDelObjType_Click);
    this._checkBoxAsSubTask.CheckedChanged += new EventHandler(this._checkBoxAsSubTask_CheckedChanged);
    this._comboFinalScript.DropDown += new EventHandler(this.ComboFinalScript_DropDown);
    this._comboFinalScript.SelectedIndexChanged += new EventHandler(this.ComboFinalScript_SelectedIndexChanged);
    this.bevel1.Style = BevelStyle.Lowered;
    this._pnlDialogButtons.Size = new Size(915, 36);
    this._okButton.Click += new EventHandler(this._okButton_Click);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(915, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._panelBtns.Location = new Point(742, 0);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.ClientSize = new Size(915, 637);
    this.Name = nameof (ImportObjectsFormAdv);
    this.TreeViewControl.TreeView.BackgroundImageMode = ImageDrawMode.Tile;
    this.TreeViewControl.TreeView.BorderStyle = BorderStyle.Fixed3D;
    this.TreeViewControl.TreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.TreeViewControl.TreeView.RootDbObjectVersionIDs = (IReadOnlyList<long>) componentResourceManager.GetObject("_treeViewControl.TreeView.RootDbObjectVersionIDs");
    this.TreeViewControl.TreeView.RowEvenStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowOddStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowSelectedStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowStyle.BorderColor = SystemColors.Control;
    this.TreeViewControl.TreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.TreeViewControl.TreeView.RowStyle.BorderWidth = 1;
    this.TreeViewControl.TreeView.RowStyle.WordWrap = false;
    this.TreeViewControl.TreeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.TreeViewControl.TreeView.Size = new Size(562, 450);
    this.TreeViewControl.TreeView.BuildTree += new EventHandler(this.ImportObjectsFormAdv_TreeViewControl_TreeView_BuildTree);
    this.TreeViewControl.TreeView.CheckStateChanged += new EventHandler<NodeEventArgs>(this.ImportObjectsFormAdv_TreeViewControl_TreeView_CheckStateChanged);
    this.TreeViewControl.TreeView.FocusRowChanged += new EventHandler(this.ImportObjectsFormAdv_TreeViewControl_TreeView_FocusRowChanged);
    this.Load += new EventHandler(this.ImportObjectsFormAdv_Load);
    this.TreeViewControl.PanelSelectButtons.ResumeLayout(false);
    this.TreeViewControl.PanelSelectButtons.PerformLayout();
    this._treeViewControl.ResumeLayout(false);
    this._panelTreeCaption.ResumeLayout(false);
    this._panelTreeCaption.PerformLayout();
    this._panelRight.ResumeLayout(false);
    this._groupBoxSettings.ResumeLayout(false);
    this._groupBoxSettings.PerformLayout();
    this._editMaxLevels.EndInit();
    this._panelRightDown.ResumeLayout(false);
    this._panel1.ResumeLayout(false);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this.TreeViewControl.TreeView.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  protected readonly struct SubProjectObjectImportInfo(
    Guid rootImportedObjectGuid,
    int projectIdentLevel)
  {
    public Guid RootImportedObjectGuid { get; } = rootImportedObjectGuid;

    public int ProjectIdentLevel { get; } = projectIdentLevel;
  }

  [Serializable]
  public class ClonedTask([NotNull] string name) : Task(name)
  {
    public void LoadFrom([NotNull] Task proto)
    {
      if (proto.ObjectID == 0L)
      {
        this.Name = proto.Name;
        this.Notes = proto.Notes;
        this.Duration = proto.Duration;
        this.DurationString = proto.DurationString;
        this.VerifySchemeID = proto.VerifySchemeID;
        this.UseActualScheme = proto.UseActualScheme;
        this.Assignments._Modified = true;
        this._UseBulkData = false;
        this.Attachments.Assign((AttachmentList) proto.Attachments);
        this.ClearCache();
      }
      else
      {
        IDBObject dbObject = proto.GetObject();
        this._Partial = true;
        try
        {
          long objectId = this._ObjectID;
          this._SessionProvider = ClientSessionProvider2.Provider;
          this._UseBulkData = false;
          this.Load(dbObject, new bool?(false));
          this.HackObjectID = objectId;
          this.Assignments._Modified = true;
        }
        finally
        {
          this._Partial = false;
          proto.ReleaseObject();
        }
        this.ClearCache();
      }
    }
  }
}
