
// Type: Intermech.Navigator.Controls.SelectObjectCompositionNavTreeView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.Forms;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.UI.Winforms;
using Intermech.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Дерево выбора из состава объектов (ну или одного)
/// Чекбоксы, логика проверки и подсчёта и всё прочее, относящееся к выбору</summary>
public class SelectObjectCompositionNavTreeView : 
  ObjectCompositionsNavTreeView,
  ITreeListColumns,
  ICommandTarget,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable,
  ITreeNodesFactory,
  IDBObjectsSource
{
  [NotNull]
  public static readonly string SettingsIconKey = "SettingsIcons";
  /// <summary>Оболочки для сериализации кнопок</summary>
  [NotNull]
  private readonly ControlDesignTimeSerializationWrapper _btnSelectObjectsWrapper;
  [NotNull]
  private readonly ControlDesignTimeSerializationWrapper _btnCheckAllWrapper;
  [NotNull]
  private readonly ControlDesignTimeSerializationWrapper _btnUnCheckAllWrapper;
  /// <summary>Какой-то сервис, обеспечивающий работу выпадающих меню</summary>
  [CanBeNull]
  private readonly IPopupMenuHost _popupHost;
  /// <summary>типы, выбранные при помощи команды "Отметки по типам"
  /// null означает режим выделения всех типов объектов</summary>
  [CanBeNull]
  private List<int> _selectedTypesCheck;
  [CanBeNull]
  private List<int> _selectedTypesUncheck;
  /// <summary>Форма обработки простановки прогресса</summary>
  [CanBeNull]
  private ChecksProgressForm _checksProgressForm;
  /// <summary>Максимальный уровень вложенности объектов, который может быть отмечен</summary>
  private int? _maxCheckableObjectLevel;
  /// <summary>Можно ли ставить отметки на корневых объектах</summary>
  private bool _rootObjectsAreCheckable = true;
  [CanBeNull]
  private ContextMenuBarItem _btnCheckAll_DropDownMenu;
  [CanBeNull]
  private MenuButtonItem _btnCheckAll_CheckAll;
  [CanBeNull]
  private MenuButtonItem _btnCheckAll_CheckLevels;
  [CanBeNull]
  private MenuButtonItem _btnCheckAll_CheckObjTypes;
  [CanBeNull]
  private MenuButtonItem _btnCheckAll_ExpandSelected;
  [CanBeNull]
  private MenuButtonItem _btnCheckAll_LoadCheckedComposition;
  [CanBeNull]
  private ContextMenuBarItem _btnUncheckAll_DropDownMenu;
  [CanBeNull]
  private MenuButtonItem _btnUncheckAll_UncheckAll;
  [CanBeNull]
  private MenuButtonItem _btnUncheckAll_UncheckLevels;
  [CanBeNull]
  private MenuButtonItem _btnUncheckAll_UncheckObjTypes;
  [CanBeNull]
  private ContextMenuBarItem _treeView_PopupMenu;
  [CanBeNull]
  private MenuButtonItem _treeView_CheckAll;
  [CanBeNull]
  private MenuButtonItem _treeView_CheckLevels;
  [CanBeNull]
  private MenuButtonItem _treeView_CheckObjTypes;
  [CanBeNull]
  private MenuButtonItem _treeView_UncheckAll;
  [CanBeNull]
  private MenuButtonItem _treeView_UncheckLevels;
  [CanBeNull]
  private MenuButtonItem _treeView_UncheckObjTypes;
  [CanBeNull]
  private MenuButtonItem _treeView_ExpandSelected;
  [CanBeNull]
  private MenuButtonItem _treeView_LoadCheckedComposition;
  /// <summary>Настройки</summary>
  [NotNull]
  protected SelectObjectCompositionSettings _SelectObjectSettings;
  [CanBeNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_287")]
  public SelectObjectCompositionNavTreeView.CreateDefaultSettingsDelegate CreateDefaultSettings;
  /// <summary>Кнопка настроек контрола в тулбаре над деревом</summary>
  [CanBeNull]
  private ButtonItem _btnSettings;
  [CanBeNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_288")]
  public SelectObjectCompositionNavTreeView.CreateSettingsFormDelegate CreateSettingsForm;
  /// <summary>Счётчик блокировок обновления статус бара</summary>
  private int _updateCountStatusesLocksCounter;
  private bool _setCheckedPacketProgressLocked;
  private const string SetCheckedPacketContextName = "SetCheckedPacket";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  public SplitButton _btnUncheckAll;
  public SplitButton _btnCheckAll;
  protected Panel _pnlSelectButtons;
  public Button _btnSelectObjects;
  private ToolStripMenuItem _menuItemLoadCheckNodesComposition;
  public ToolStripDropDownButton _buttonCheckNotLoadedCount;
  public ToolStripStatusLabel _buttonChecksCount;
  public ToolStripStatusLabel _labelSpace;
  public StatusStrip _statusStrip;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  public SplitButton _BtnUncheckAll
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnUncheckAll.CheckInitializedIn<SplitButton>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  public SplitButton _BtnCheckAll
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnCheckAll.CheckInitializedIn<SplitButton>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal Panel PnlSelectButtons
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._pnlSelectButtons.CheckInitializedIn<Panel>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  public Button _BtnSelectObjects
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnSelectObjects.CheckInitializedIn<Button>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal ToolStripMenuItem MenuItemLoadCheckNodesComposition
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._menuItemLoadCheckNodesComposition.CheckInitializedIn<ToolStripMenuItem>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal ToolStripDropDownButton ButtonCheckNotLoadedCount
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCheckNotLoadedCount.CheckInitializedIn<ToolStripDropDownButton>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal ToolStripStatusLabel ButtonChecksCount
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonChecksCount.CheckInitializedIn<ToolStripStatusLabel>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal StatusStrip StatusStrip
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._statusStrip.CheckInitializedIn<StatusStrip>((object) this);
    }
  }

  /// <summary>Тип контрола дерева, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного контрола, в этом случае дерево будет создано указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public new static System.Type OverrideTreeViewClass
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return NavigatorTreeViewWithObjectTypeFiltration.OverrideTreeViewClass;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      ObjectCompositionsNavTreeView.OverrideTreeViewClass = value;
    }
  }

  /// <summary>Показать диалог выбора объектов</summary>
  /// <param name="contextName">Имя операции, служит для идентификации настроек окна выбора</param>
  /// <param name="objectTypeIDs">Перечисление идентификаторов типов объектов, доступных для выбора. Если null - доступны все объекты</param>
  /// <returns>Коллекция идентификаторов выбранных объектов</returns>
  [NotNull]
  [ItemNotEmpty]
  public static IReadOnlyList<long> ShowSelectObjectsForm(
    [CanBeNull] string contextName = null,
    [CanBeNull] IReadOnlyCollection<int> objectTypeIDs = null)
  {
    string caption = LocalizationHolder.rm.GetString("Client.Core_1633");
    string operationName = contextName;
    IReadOnlyList<IDBObjectID> source = SelectDialog.Objects(objectTypeIDs, caption, operationName: operationName);
    return (source != null ? source.MapListReadOnly<IDBObjectID, long>((Func<IDBObjectID, long>) (iObjectID => iObjectID.Value)) : (IReadOnlyList<long>) null) ?? (IReadOnlyList<long>) Array.Empty<long>();
  }

  /// <summary>Событие, информирующее о том, что список идентификаторов выбранных версий объектов изменился, например пользователем были
  /// выбраны другие версии объектов</summary>
  private event SelectionChangedEventHandler RootObjectVersionsListChanged;

  /// <summary>Default constructor</summary>
  public SelectObjectCompositionNavTreeView()
  {
    this.InitializeComponent();
    this._SelectObjectSettings = this.CreateDefaultSelectObjectCompositionSettings();
    this.CreateSettingsButton();
    this._btnSelectObjectsWrapper = new ControlDesignTimeSerializationWrapper((Control) this._BtnSelectObjects);
    this._btnCheckAllWrapper = new ControlDesignTimeSerializationWrapper((Control) this._BtnCheckAll);
    this._btnUnCheckAllWrapper = new ControlDesignTimeSerializationWrapper((Control) this._BtnUncheckAll);
    this.ViewsTree.SendToBack();
    if (this.InDesignMode)
      return;
    this.TreeView.AllowCheckParentWithoutChildren = true;
    this.TreeView.BeforeSetCheckState = new BeforeSetCheckStateEventHandler(this.BeforeSetCheckState);
    this._popupHost = ApplicationServices.Container.GetService<IPopupMenuHost>(false);
  }

  /// <summary>Инициализировать сервисы</summary>
  /// <param name="ownerServices"></param>
  public override void InitializeServices(System.IServiceProvider ownerServices)
  {
    base.InitializeServices(ownerServices);
    this._servicesTree.AddService<ITreeNodesFactory>((ITreeNodesFactory) this);
  }

  /// <summary>Деинициализировать сервисы</summary>
  protected override void DisposeServices()
  {
    if (this._servicesTree != null)
      this._servicesTree.RemoveService<ITreeNodesFactory>();
    base.DisposeServices();
  }

  /// <summary>Инициализация словаря свойств User Control-а перед загрузкой свойств из </summary>
  public override void ParseDictionaryFromFormStorage([NotNull] Dictionary<string, object> dic)
  {
    base.ParseDictionaryFromFormStorage(dic);
    object obj;
    if (dic.TryGetValue("SelTypesCheck", out obj))
    {
      string[] source = obj.ToString().Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries);
      if (source.Length != 0)
        this._selectedTypesCheck = ((IEnumerable<string>) source).Select<string, int>((Func<string, int>) (s => Convert.ToInt32(s))).ToList<int>(source.Length);
    }
    if (dic.TryGetValue("SelTypesUncheck", out obj))
    {
      string[] source = obj.ToString().Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries);
      if (source.Length != 0)
        this._selectedTypesUncheck = ((IEnumerable<string>) source).Select<string, int>((Func<string, int>) (s => Convert.ToInt32(s))).ToList<int>(source.Length);
    }
    if (dic.TryGetValue("Columns", out obj))
    {
      string s = obj.ToString();
      if (!string.IsNullOrEmpty(s))
      {
        byte[] buffer = Convert.FromBase64String(s);
        if (buffer.Length != 0)
        {
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          using (MemoryStream serializationStream = new MemoryStream(buffer))
          {
            try
            {
              if (binaryFormatter.Deserialize((Stream) serializationStream) is NodeColumnCollection nodeColumnCollection)
              {
                if (nodeColumnCollection.Count > 0)
                  this.TreeView.SetColumns(nodeColumnCollection);
              }
            }
            finally
            {
              serializationStream.Close();
            }
          }
        }
      }
    }
    SelectObjectCompositionSettings compositionSettings = this.CreateDefaultSelectObjectCompositionSettings();
    compositionSettings.LoadFromDictionary(dic);
    this.SelectObjectSettings = compositionSettings;
  }

  /// <summary>Загрузка свойств в словарь, который будет сохранён в FormStorage при вызове SavePropertiesToStorage</summary>
  public override void FillPropsDictionary([NotNull] Dictionary<string, object> dic)
  {
    base.FillPropsDictionary(dic);
    dic.Add("SelTypesCheck", this._selectedTypesCheck != null ? (object) string.Join<int>(",", (IEnumerable<int>) this._selectedTypesCheck) : (object) "");
    dic.Add("SelTypesUncheck", this._selectedTypesUncheck != null ? (object) string.Join<int>(",", (IEnumerable<int>) this._selectedTypesUncheck) : (object) "");
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    string base64String;
    using (MemoryStream serializationStream = new MemoryStream())
    {
      try
      {
        binaryFormatter.Serialize((Stream) serializationStream, (object) this.TreeView.ReflectTreeColumsChanges());
        serializationStream.Position = 0L;
        base64String = Convert.ToBase64String(serializationStream.ToArray());
      }
      finally
      {
        serializationStream.Close();
      }
    }
    if (!string.IsNullOrEmpty(base64String))
      dic.Add("Columns", (object) base64String);
    this._SelectObjectSettings.SaveToDictionary(dic);
  }

  /// <summary>Панель с кнопками</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public Panel PanelSelectButtons
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._pnlSelectButtons.CheckInitializedIn<Panel>((object) this);
    }
  }

  /// <summary>Кнопка "Выбрать другие объекты"</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public ControlDesignTimeSerializationWrapper BtnSelectObjects
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnSelectObjectsWrapper;
    }
  }

  /// <summary>Кнопка "Отметить все"</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public ControlDesignTimeSerializationWrapper BtnCheckAll
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnCheckAllWrapper;
    }
  }

  /// <summary>Кнопка "Снять все отметки"</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public ControlDesignTimeSerializationWrapper BtnUnCheckAll
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnUnCheckAllWrapper;
    }
  }

  /// <summary>Позволяли пользователю выбрать корневые объекты</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(true)]
  public bool AllowChangeObjects
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._BtnSelectObjects.CheckInitializedIn<Button>((object) this).Visible;
    }
    set
    {
      if (this._BtnSelectObjects.Visible == value)
        return;
      this._BtnSelectObjects.Visible = value;
    }
  }

  /// <summary>Количество корневых отмеченных объектов</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int RootObjectCheckedCount
  {
    get
    {
      int result = 0;
      NavigatorTreeView treeView = this._treeView;
      if (treeView != null)
        treeView.InvokeForAllClosestToRootTreeNodes((Func<NavigatorTreeNode, bool>) (treeNode => treeNode.CheckState != 0), (Action<NavigatorTreeNode>) (treeNode => ++result));
      return result;
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string WarningCheckedNotLoaded
  {
    get
    {
      return this.ButtonCheckNotLoadedCount.Visible && this.ButtonCheckNotLoadedCount.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText ? this.ButtonCheckNotLoadedCount.Text + "." : (string) null;
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string WarningChecksCount
  {
    get
    {
      return this.ButtonChecksCount.Visible && this.ButtonChecksCount.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText ? this.GetCheckedCountString().Append(".").ToString() : (string) null;
    }
  }

  protected virtual bool GetIsCheckVisible([NotNull] NavigatorTreeNode node)
  {
    if (this.TreeView != null)
    {
      if (this._maxCheckableObjectLevel.HasValue)
      {
        int objectNodeLevel = this.TreeView.GetObjectNodeLevel(node);
        int? checkableObjectLevel = this._maxCheckableObjectLevel;
        int valueOrDefault = checkableObjectLevel.GetValueOrDefault();
        if (!(objectNodeLevel <= valueOrDefault & checkableObjectLevel.HasValue))
          goto label_6;
      }
      return this._rootObjectsAreCheckable || !this.TreeView.IsNodeRootObject(node);
    }
label_6:
    return false;
  }

  public void RefreshNodeCheckVisible([NotNull] NavigatorTreeNode node)
  {
    bool isCheckVisible = this.GetIsCheckVisible(node);
    if (node.ShowCheckState == isCheckVisible)
      return;
    node.ShowCheckState = isCheckVisible;
    if (!isCheckVisible)
      return;
    this.RestoreCheckStatus(node);
  }

  private void RefreshTreeCheckBoxes(int maxLevel = 0)
  {
    if (maxLevel == 0)
      maxLevel = this._maxCheckableObjectLevel ?? int.MaxValue;
    NavigatorTreeView treeView = this._treeView;
    if (treeView != null)
      treeView.InvokeForTreeNodes((Func<NavigatorTreeNode, bool>) (node => node.NodeID.IsObjectCategory()), (Func<NavigatorTreeNode, bool>) (node => !node.NodeID.IsObjectCategory() || this.TreeView.GetObjectNodeLevel(node) <= maxLevel), new Action<NavigatorTreeNode>(this.RefreshNodeCheckVisible), (Action<NavigatorTreeNode>) (node =>
      {
        int? checkableObjectLevel;
        if (this._maxCheckableObjectLevel.HasValue)
        {
          int objectNodeLevel = this.TreeView.GetObjectNodeLevel(node);
          checkableObjectLevel = this._maxCheckableObjectLevel;
          int valueOrDefault = checkableObjectLevel.GetValueOrDefault();
          if (objectNodeLevel == valueOrDefault & checkableObjectLevel.HasValue && node.CheckState == CheckState.Indeterminate)
          {
            node.SetCheckState(CheckState.Checked, true, false, false);
            return;
          }
        }
        if (this._maxCheckableObjectLevel.HasValue)
        {
          int objectNodeLevel = this.TreeView.GetObjectNodeLevel(node);
          checkableObjectLevel = this._maxCheckableObjectLevel;
          int valueOrDefault = checkableObjectLevel.GetValueOrDefault();
          if (!(objectNodeLevel < valueOrDefault & checkableObjectLevel.HasValue))
            return;
        }
        if (node.CheckState != CheckState.Checked || !node.HasChildren || node.Full)
          return;
        node.SetCheckState(CheckState.Indeterminate, true, false, false);
      }));
    this.UpdateCountStatuses();
    this._treeView?.Invalidate();
  }

  /// <summary>Максимальный уровень вложенности в структуре объектов, который может быть выбран</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int? MaxCheckableObjectLevel
  {
    [DebuggerStepThrough] get => this._maxCheckableObjectLevel;
    set
    {
      int? checkableObjectLevel = this._maxCheckableObjectLevel;
      int? nullable1 = value;
      if (checkableObjectLevel.GetValueOrDefault() == nullable1.GetValueOrDefault() & checkableObjectLevel.HasValue == nullable1.HasValue)
        return;
      int? nullable2 = this._maxCheckableObjectLevel;
      int val1 = nullable2 ?? int.MaxValue;
      nullable2 = value;
      int val2 = nullable2 ?? int.MaxValue;
      int maxLevel = Math.Max(val1, val2);
      this._maxCheckableObjectLevel = value;
      if (this._treeView?.RootNode == null)
        return;
      this.RefreshTreeCheckBoxes(maxLevel);
    }
  }

  /// <summary>Можно ли ставить отметки на корневых объектах</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(true)]
  public bool RootObjectsAreCheckable
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rootObjectsAreCheckable;
    }
    set
    {
      if (this._rootObjectsAreCheckable == value)
        return;
      this._rootObjectsAreCheckable = value;
      if (this.InDesignMode)
        return;
      this.RootObjectNavigatorTreeNodes.InvokeForAll<NavigatorTreeNode>(new Action<NavigatorTreeNode>(this.RefreshNodeCheckVisible));
      this._treeView?.Invalidate();
      this.UpdateCountStatuses();
    }
  }

  /// <summary>Число отмеченных объектов (из тех, кто уже загружены в составе)</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int CheckedObjectsCount
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodes.Count<NavigatorTreeNode>();
    }
  }

  /// <summary>Число отмеченных объектов (из тех, кто уже загружены в составе), состав которых не загружен</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int CheckedObjectsWithNotLoadedChildsCount
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodes.Count<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node =>
      {
        if (node.CheckState != CheckState.Unchecked)
        {
          if (this._maxCheckableObjectLevel.HasValue)
          {
            int objectNodeLevel = this.TreeView.GetObjectNodeLevel(node);
            int? checkableObjectLevel = this._maxCheckableObjectLevel;
            int valueOrDefault = checkableObjectLevel.GetValueOrDefault();
            if (!(objectNodeLevel < valueOrDefault & checkableObjectLevel.HasValue))
              goto label_5;
          }
          if (node.HasChildren)
            return !node.Full;
        }
label_5:
        return false;
      }));
    }
  }

  /// <summary>Присутствуют ли в дереве присутствуют отмеченные объекты (из тех, кто уже загружены в составе), состав которых не загружен</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool HasCheckedObjectsWithNotLoadedChilds
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodes.Any<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node =>
      {
        if (node.CheckState == CheckState.Unchecked)
          return false;
        if (!this._maxCheckableObjectLevel.HasValue)
          return true;
        int objectNodeLevel = this.TreeView.GetObjectNodeLevel(node);
        int? checkableObjectLevel = this._maxCheckableObjectLevel;
        int valueOrDefault = checkableObjectLevel.GetValueOrDefault();
        return objectNodeLevel < valueOrDefault & checkableObjectLevel.HasValue;
      }));
    }
  }

  /// <summary>Есть ли неотмеченные объекты</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool HasNotCheckedObjects
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ObjectNodesThatCanBeChecked.Any<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node => node.CheckState == CheckState.Unchecked));
    }
  }

  /// <summary>Метод получения перечисления нод, которые могут быть отмечены. Без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [ItemNotNull]
  private IEnumerable<NavigatorTreeNode> GetNodesThatCanBeChecked(bool autoPopulateNodes = false)
  {
    return this.TreeView.RootNode?.EnumerationWithChilds((Func<NavigatorTreeNode, bool>) (node => node.ShowCheckState), (Func<NavigatorTreeNode, bool>) (node =>
    {
      if (!this._maxCheckableObjectLevel.HasValue)
        return true;
      int objectNodeLevel = this.TreeView.GetObjectNodeLevel(node);
      int? checkableObjectLevel = this._maxCheckableObjectLevel;
      int valueOrDefault = checkableObjectLevel.GetValueOrDefault();
      return objectNodeLevel < valueOrDefault & checkableObjectLevel.HasValue;
    }), autoPopulateNodes) ?? (IEnumerable<NavigatorTreeNode>) Array.Empty<NavigatorTreeNode>();
  }

  /// <summary>Перечисление нод, которые могут быть отмечены. Без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  [ItemNotNull]
  public IEnumerable<NavigatorTreeNode> NodesThatCanBeChecked
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetNodesThatCanBeChecked();
    }
  }

  /// <summary>Число нод, которые могут быть отмечены. Без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int NodesThatCanBeCheckedCount
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetNodesThatCanBeChecked().Count<NavigatorTreeNode>();
    }
  }

  /// <summary>Перечисление нод объектов, которые могут быть отмечены.
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [ItemNotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<NavigatorTreeNode> ObjectNodesThatCanBeChecked
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetNodesThatCanBeChecked().Where<NavigatorTreeNode>(new Func<NavigatorTreeNode, bool>(SelectObjectCompositionNavTreeView.NodeRepresentObject));
    }
  }

  /// <summary>Метод получения перечисления отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  [ItemNotNull]
  private IEnumerable<NavigatorTreeNode> GetCheckedNodes(bool autoPopulateNodes = false)
  {
    return this.TreeView.RootNode?.EnumerationWithChilds((Func<NavigatorTreeNode, bool>) (node => node.ShowCheckState && node.CheckState != 0), (Func<NavigatorTreeNode, bool>) (node =>
    {
      if (node.CheckState == CheckState.Unchecked && node.ShowCheckState)
        return false;
      if (!this._maxCheckableObjectLevel.HasValue)
        return true;
      int objectNodeLevel = this.TreeView.GetObjectNodeLevel(node);
      int? checkableObjectLevel = this._maxCheckableObjectLevel;
      int valueOrDefault = checkableObjectLevel.GetValueOrDefault();
      return objectNodeLevel < valueOrDefault & checkableObjectLevel.HasValue;
    }), autoPopulateNodes) ?? (IEnumerable<NavigatorTreeNode>) Array.Empty<NavigatorTreeNode>();
  }

  /// <summary>Перечисление отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  [ItemNotNull]
  public IEnumerable<NavigatorTreeNode> CheckedNodes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetCheckedNodes();
    }
  }

  /// <summary>Метод фильтрации нод дерева для получения только тех нод, которые представляют объекты БД</summary>
  private static bool NodeRepresentObject([NotNull] NavigatorTreeNode node)
  {
    INodeID nodeId1 = node.NodeID;
    return (nodeId1 != null ? (nodeId1.IsObjectCategory() ? 1 : 0) : 0) != 0 && node.NodeID is NodeID nodeId2 && nodeId2.ObjectID != 0L;
  }

  /// <summary>Перечисление отмеченных нод объектов без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [ItemNotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<NavigatorTreeNode> CheckedObjectNodes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetCheckedNodes().Where<NavigatorTreeNode>(new Func<NavigatorTreeNode, bool>(SelectObjectCompositionNavTreeView.NodeRepresentObject));
    }
  }

  /// <summary>Перечисление интерфейсов идентификаторов отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [ItemNotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<NodeID> CheckedObjectNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodes.Select<NavigatorTreeNode, NodeID>((Func<NavigatorTreeNode, NodeID>) (node => node.NodeID as NodeID));
    }
  }

  /// <summary>Признак того, что в дереве отмечен хотя бы 1 объект</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ObjectIsChecked
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodes.Any<NavigatorTreeNode>();
    }
  }

  /// <summary>Последовательность идентификаторов версий отмеченных в дереве объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<long> CheckedObjectVersionIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDs.Select<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.ObjectID));
    }
  }

  /// <summary>Последовательность идентификаторов отмеченных объектов (!!! НЕ ВЕРСИЙ !!!)</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<long> CheckedObjectIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDs.Select<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.ID));
    }
  }

  /// <summary>Последовательность идентификаторов типов отмеченных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<int> CheckedObjectTypeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDs.Select<NodeID, int>((Func<NodeID, int>) (nodeID => nodeID.TypeID));
    }
  }

  /// <summary>Последовательность идентификаторов связей отмеченных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<long> CheckedObjectPrjLinkIDs
  {
    [DebuggerStepThrough] get
    {
      return this.CheckedObjectNodeIDs.Select<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.PrjLinkID));
    }
  }

  /// <summary>Последовательность заголовков отмеченных в объекта</summary>
  [NotNull]
  [ItemNotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<string> CheckedObjectCaptions
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDs.Select<NodeID, string>((Func<NodeID, string>) (nodeID => nodeID.Caption));
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._btnSelectObjectsWrapper.Dispose();
      this._btnCheckAllWrapper.Dispose();
      this._btnUnCheckAllWrapper.Dispose();
      if (this._btnSettings != null)
      {
        this.TreeToolbar.Items.Remove((ToolbarItemBase) this._btnSettings);
        this._btnSettings.Dispose();
        this._btnSettings = (ButtonItem) null;
      }
      if (this._btnCheckAll_DropDownMenu != null)
      {
        this._btnCheckAll_DropDownMenu.Dispose();
        this._btnCheckAll_DropDownMenu = (ContextMenuBarItem) null;
      }
      if (this._btnCheckAll_CheckAll != null)
      {
        this._btnCheckAll_CheckAll.Dispose();
        this._btnCheckAll_CheckAll = (MenuButtonItem) null;
      }
      if (this._btnCheckAll_CheckLevels != null)
      {
        this._btnCheckAll_CheckLevels.Dispose();
        this._btnCheckAll_CheckLevels = (MenuButtonItem) null;
      }
      if (this._btnCheckAll_CheckObjTypes != null)
      {
        this._btnCheckAll_CheckObjTypes.Dispose();
        this._btnCheckAll_CheckObjTypes = (MenuButtonItem) null;
      }
      if (this._btnCheckAll_ExpandSelected != null)
      {
        this._btnCheckAll_ExpandSelected.Dispose();
        this._btnCheckAll_ExpandSelected = (MenuButtonItem) null;
      }
      if (this._btnCheckAll_LoadCheckedComposition != null)
      {
        this._btnCheckAll_LoadCheckedComposition.Dispose();
        this._btnCheckAll_LoadCheckedComposition = (MenuButtonItem) null;
      }
      if (this._btnUncheckAll_DropDownMenu != null)
      {
        this._btnUncheckAll_DropDownMenu.Dispose();
        this._btnUncheckAll_DropDownMenu = (ContextMenuBarItem) null;
      }
      if (this._btnUncheckAll_UncheckAll != null)
      {
        this._btnUncheckAll_UncheckAll.Dispose();
        this._btnUncheckAll_UncheckAll = (MenuButtonItem) null;
      }
      if (this._btnUncheckAll_UncheckLevels != null)
      {
        this._btnUncheckAll_UncheckLevels.Dispose();
        this._btnUncheckAll_UncheckLevels = (MenuButtonItem) null;
      }
      if (this._btnUncheckAll_UncheckObjTypes != null)
      {
        this._btnUncheckAll_UncheckObjTypes.Dispose();
        this._btnUncheckAll_UncheckObjTypes = (MenuButtonItem) null;
      }
      if (this._treeView_PopupMenu != null)
      {
        this._treeView_PopupMenu.Dispose();
        this._treeView_PopupMenu = (ContextMenuBarItem) null;
      }
      if (this._treeView_CheckAll != null)
      {
        this._treeView_CheckAll.Dispose();
        this._treeView_CheckAll = (MenuButtonItem) null;
      }
      if (this._treeView_CheckLevels != null)
      {
        this._treeView_CheckLevels.Dispose();
        this._treeView_CheckLevels = (MenuButtonItem) null;
      }
      if (this._treeView_CheckObjTypes != null)
      {
        this._treeView_CheckObjTypes.Dispose();
        this._treeView_CheckObjTypes = (MenuButtonItem) null;
      }
      if (this._treeView_UncheckAll != null)
      {
        this._treeView_UncheckAll.Dispose();
        this._treeView_UncheckAll = (MenuButtonItem) null;
      }
      if (this._treeView_UncheckLevels != null)
      {
        this._treeView_UncheckLevels.Dispose();
        this._treeView_UncheckLevels = (MenuButtonItem) null;
      }
      if (this._treeView_UncheckObjTypes != null)
      {
        this._treeView_UncheckObjTypes.Dispose();
        this._treeView_UncheckObjTypes = (MenuButtonItem) null;
      }
      if (this._treeView_ExpandSelected != null)
      {
        this._treeView_ExpandSelected.Dispose();
        this._treeView_ExpandSelected = (MenuButtonItem) null;
      }
      if (this._treeView_LoadCheckedComposition != null)
      {
        this._treeView_LoadCheckedComposition.Dispose();
        this._treeView_LoadCheckedComposition = (MenuButtonItem) null;
      }
      if (this.components != null)
      {
        this.components.Dispose();
        this.components = (IContainer) null;
      }
    }
    base.Dispose(disposing);
  }

  /// <summary>Вызывается при попытки создания ноды дерева, позволяет подменить класс ноды дерева
  /// Если вернуть null, то будет использован стандартный механизм создания</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event SelectObjectCompositionNavTreeView.CreateNavTreeNodeDelegate CreateNavTreeNodeEvent;

  /// <summary>Вызывается после создания ноды дерева, позволяет поправить поля ноды дерева</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event SelectObjectCompositionNavTreeView.AfterNavTreeNodeCreatedDelegate AfterNavTreeNodeCreated;

  /// <summary>Создать кастомную ноду дерева (потомка NavigatorTreeNode) для отображения ноды (INode), созданной по переданному
  /// идентификатору ноды (INodeID). Если вернёт null будет создана обычная NavigatorTreeNode.</summary>
  /// <param name="navTreeView">Дерево навигатора</param>
  /// <param name="parent">Нода дерева навигатора, в составе которой должна быть создана нода дерева</param>
  /// <param name="nodeID">Интерфейс идентификатора создаваемой ноды</param>
  /// <param name="fieldValues">Значения полей</param>
  /// <param name="rawValues">Значения полей в raw виде</param>
  /// <returns>Кастомная нода дерева, которая будет представлять создаваемую ноду. Если null - должна быть создана обычная NavigatorTreeNode</returns>
  [NotNull]
  public NavigatorTreeNode CreateNavTreeNode(
    [NotNull] NavigatorTreeView navTreeView,
    [NotNull] NavigatorTreeNode parent,
    [CanBeNull] INodeID nodeID,
    [CanBeNull] object[] fieldValues,
    [CanBeNull] object[] rawValues)
  {
    NavigatorTreeNode createdNavigatorTreeNode = (NavigatorTreeNode) null;
    if (this.CreateNavTreeNodeEvent != null)
      createdNavigatorTreeNode = this.CreateNavTreeNodeEvent(navTreeView, parent, nodeID, fieldValues, rawValues);
    if (createdNavigatorTreeNode == null)
    {
      createdNavigatorTreeNode = new NavigatorTreeNode(navTreeView, parent, nodeID, fieldValues, rawValues);
      if (nodeID != null && nodeID.CategoryID == Intermech.Navigator.Consts.CategoryMultipleObjectsNode)
        createdNavigatorTreeNode.ShowCheckState = false;
    }
    SelectObjectCompositionNavTreeView.AfterNavTreeNodeCreatedDelegate navTreeNodeCreated = this.AfterNavTreeNodeCreated;
    if (navTreeNodeCreated != null)
      navTreeNodeCreated(navTreeView, parent, nodeID, fieldValues, rawValues, createdNavigatorTreeNode);
    return createdNavigatorTreeNode;
  }

  /// <summary>Список идентификаторов версий объектов</summary>
  IReadOnlyList<long> IDBObjectsSource.ObjectVersionIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.RootObjectVersionIDs;
    }
  }

  /// <summary>Вызов события, информирующего подписчиков о том, что список идентификаторов выбранных версий объектов изменился,
  /// например пользователем были выбраны другие версии объектов</summary>
  protected virtual void Fire_OnObjectsSelectionChanged()
  {
    SelectionChangedEventHandler versionsListChanged = this.RootObjectVersionsListChanged;
    if (versionsListChanged == null)
      return;
    versionsListChanged();
  }

  /// <summary>Событие, информирующее о том, что список идентификаторов выбранных версий объектов изменился, например пользователем были
  /// выбраны другие версии объектов</summary>
  event SelectionChangedEventHandler IDBObjectsSource.Changed
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this.RootObjectVersionsListChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this.RootObjectVersionsListChanged -= value;
    }
  }

  /// <summary>Вызывается после инициализации фрейма</summary>
  protected override void BuildTree([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    base.BuildTree(sender, e);
    this.Fire_OnObjectsSelectionChanged();
    if (this._SelectObjectSettings.CheckAllObjectsOnLoad)
      this.CheckAll();
    if (this._SelectObjectSettings.AutoLoadComposition != SelectObjectCompositionAutoload.None && this.TreeView.RootNode != null)
    {
      if (!this.TreeView.RootNode.Full)
        this.TreeView.RootNode.PopulateAndWaitForFull();
      if (this._SelectObjectSettings.AutoLoadComposition == SelectObjectCompositionAutoload.Full)
        this.TreeView.LoadFullComposition(this.TreeView.RootNode);
      else
        this.TreeView.LoadCompositionToLevel(this.TreeView.RootNode, this._SelectObjectSettings.AutoLoadCompositionDepth);
    }
    this.UpdateCountStatuses();
  }

  /// <summary>Список контролов, дизайнеры которых должны быть активированы</summary>
  /// <returns>&gt;Или список, или null, если таковых не должно быть
  /// Пара "Контрол"-"имя поля, в которые будут сохранятся правки" (полем может выступать wrapper для контрола)</returns>
  protected override List<(Control DesignModeControl, string FieldName)> GetDesignModeChildControls()
  {
    List<(Control, string)> modeChildControls = base.GetDesignModeChildControls();
    modeChildControls.Add(((Control) this._pnlSelectButtons, "PanelSelectButtons"));
    modeChildControls.Add(((Control) this._btnSelectObjects, "_btnSelectObjects"));
    modeChildControls.Add(((Control) this._btnCheckAll, "_btnCheckAll"));
    modeChildControls.Add(((Control) this._btnUncheckAll, "_btnUncheckAll"));
    return modeChildControls;
  }

  /// <summary>Вызов события <see cref="!:OnFirstPaint" /> - после первого отображения контрола (первого WM_PAINT)</summary>
  protected override void FireFirstPaint()
  {
    if (!this.InDesignMode)
    {
      this.UpdateCountStatuses();
      this.CheckSaveLockedStatusChanged();
    }
    base.FireFirstPaint();
  }

  /// <summary>Кнопка "выбрать другие объекты"</summary>
  private void _btnSelectObjects_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    IReadOnlyCollection<long> newRootDbObjectVersionIDs = (IReadOnlyCollection<long>) SelectObjectCompositionNavTreeView.ShowSelectObjectsForm(this.ContextName, this._ObjectTypeIDs);
    if (newRootDbObjectVersionIDs.Count > 0)
    {
      this.ButtonChecksCount.Text = LocalizationHolder.rm.GetString("Client.Core_1654");
      this.ButtonChecksCount.Enabled = false;
      this.ButtonCheckNotLoadedCount.Visible = false;
      this.TreeView.Build(newRootDbObjectVersionIDs);
    }
    this.TreeView.Focus();
  }

  /// <summary>Кнопка "Отметить все"</summary>
  protected virtual void _btnCheckAll_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CheckAll();
    this.TreeView.Focus();
  }

  /// <summary>Кнопка "Снять все отметки"</summary>
  protected virtual void _btnUnCheckAll_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UnCheckAll();
    this.TreeView.Focus();
  }

  protected virtual void BeforeSetCheckState([NotNull] NavigatorTreeNode node, ref CheckState checkState)
  {
    if (checkState != CheckState.Checked || !node.HasChildren)
      return;
    ref CheckState local = ref checkState;
    int num;
    if (node.GetObjectChilds().Count == 0 || SelectObjectCompositionNavTreeView.IsAnyIndeterminateChild(node))
    {
      if (this._maxCheckableObjectLevel.HasValue)
      {
        int objectNodeLevel = this.TreeView.GetObjectNodeLevel(node);
        int? checkableObjectLevel = this._maxCheckableObjectLevel;
        int valueOrDefault = checkableObjectLevel.GetValueOrDefault();
        if (objectNodeLevel == valueOrDefault & checkableObjectLevel.HasValue)
          goto label_4;
      }
      num = 2;
      goto label_6;
    }
label_4:
    num = 1;
label_6:
    local = (CheckState) num;
  }

  private static bool IsAnyIndeterminateChild([NotNull] NavigatorTreeNode treeNode)
  {
    return treeNode.GetObjectChilds().Any<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (childNode =>
    {
      if (!childNode.HasChildren)
        return false;
      return childNode.GetObjectChilds().Count == 0 || SelectObjectCompositionNavTreeView.IsAnyIndeterminateChild(childNode);
    }));
  }

  private void SelectObjectCompositionNavTreeView_TreeView_MouseDown(
    [CanBeNull] object sender,
    [NotNull] MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    NavigatorTreeNode nodeAt = this.TreeView.GetNodeAt(e.X, e.Y);
    if (nodeAt?.NodeID == null || nodeAt == this.TreeView.FocusedNode)
      return;
    this.TreeView.LockTreeEvents();
    try
    {
      if (!this.TreeView.SelectedRows.Contains(nodeAt.Handle))
      {
        this.TreeView.SelectedRows.Clear();
        this.TreeView.SelectedRow = nodeAt.Handle;
      }
      this.TreeView.FocusedNode = nodeAt;
    }
    finally
    {
      this.TreeView.UnlockTreeEvents();
    }
    this.TreeView.RaiseAfterFocusNode(nodeAt);
  }

  private void SelectObjectCompositionNavTreeView_TreeView_CheckStateChanged(
    [CanBeNull] object sender,
    [NotNull] NodeEventArgs e)
  {
    if (this._checksProgressForm != null)
      ++this._checksProgressForm.ObjectsChecked;
    this.UpdateCountStatuses();
    this.CheckSaveLockedStatusChanged();
  }

  private void _menuItemLoadCheckNodesComposition_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.LoadCheckedComposition();
    this.TreeView.Focus();
  }

  [NotNull]
  private ContextMenuBarItem BtnCheckAll_DropDownMenu
  {
    [DebuggerStepThrough] get
    {
      LazyInitializer.EnsureInitialized<ContextMenuBarItem>(ref this._btnCheckAll_DropDownMenu, (Func<ContextMenuBarItem>) (() =>
      {
        ContextMenuBarItem checkAll_DropDownMenuItem = new ContextMenuBarItem();
        this.FillCheckAllDropDownMenu(checkAll_DropDownMenuItem);
        return checkAll_DropDownMenuItem;
      }));
      if (this._btnCheckAll_ExpandSelected != null)
      {
        MenuButtonItem allExpandSelected = this._btnCheckAll_ExpandSelected;
        int num;
        if (this.ObjectIsFocused && this.TreeView.SelectedItem != null)
        {
          NavigatorTreeNode focusedTreeNode = this.FocusedTreeNode;
          num = focusedTreeNode != null ? (focusedTreeNode.HasChildren ? 1 : 0) : 0;
        }
        else
          num = 0;
        allExpandSelected.Enabled = num != 0;
      }
      if (this._btnCheckAll_LoadCheckedComposition != null)
        this._btnCheckAll_LoadCheckedComposition.Enabled = this.HasCheckedObjectsWithNotLoadedChilds;
      return this._btnCheckAll_DropDownMenu;
    }
  }

  /// <summary>Наполнение выпадающего меню кнопки "Выбрать все"</summary>
  protected virtual void FillCheckAllDropDownMenu([NotNull] ContextMenuBarItem checkAll_DropDownMenuItem)
  {
    this._btnCheckAll_CheckAll = this.AddCommandToMenu(checkAll_DropDownMenuItem, LocalizationHolder.GetString("Client.Core_1637"), "CheckAll", true);
    this._btnCheckAll_CheckLevels = this.AddCommandToMenu(checkAll_DropDownMenuItem, LocalizationHolder.GetString("Client.Core_1655"), "CheckLevels", false);
    this._btnCheckAll_CheckObjTypes = this.AddCommandToMenu(checkAll_DropDownMenuItem, LocalizationHolder.GetString("Client.Core_1638"), "CheckObjTypes", false);
    this._btnCheckAll_ExpandSelected = this.AddCommandToMenu(checkAll_DropDownMenuItem, LocalizationHolder.GetString("Client.Core_1639"), "ExpandSelected", true);
    this._btnCheckAll_LoadCheckedComposition = this.AddCommandToMenu(checkAll_DropDownMenuItem, LocalizationHolder.GetString("Client.Core_1656"), "LoadCheckedComposition", false);
  }

  private void _btnCheckAll_ShowMenuStrip([NotNull] SplitButton sender, [NotNull] ShowMenuStripEventArgs e)
  {
    MenuButtonItem menuButtonItem = this.BtnCheckAll_DropDownMenu.Show(this._popupHost, (Control) sender, new Point(0, sender.Height));
    if (menuButtonItem != null && menuButtonItem.CommandName != null)
      e.Handled = this.ProcessCommand(menuButtonItem.CommandName);
    this.TreeView.Focus();
  }

  [NotNull]
  private ContextMenuBarItem BtnUncheckAll_DropDownMenu
  {
    [DebuggerStepThrough] get
    {
      LazyInitializer.EnsureInitialized<ContextMenuBarItem>(ref this._btnUncheckAll_DropDownMenu, (Func<ContextMenuBarItem>) (() =>
      {
        ContextMenuBarItem uncheckAll_DropDownMenuItem = new ContextMenuBarItem();
        this.FillUncheckAllDropDownMenu(uncheckAll_DropDownMenuItem);
        return uncheckAll_DropDownMenuItem;
      }));
      return this._btnUncheckAll_DropDownMenu;
    }
  }

  /// <summary>Наполнение выпадающего меню кнопки "Снять отметки"</summary>
  protected virtual void FillUncheckAllDropDownMenu([NotNull] ContextMenuBarItem uncheckAll_DropDownMenuItem)
  {
    this._btnUncheckAll_UncheckAll = this.AddCommandToMenu(uncheckAll_DropDownMenuItem, LocalizationHolder.GetString("Client.Core_1641"), "UncheckAll", true);
    this._btnUncheckAll_UncheckLevels = this.AddCommandToMenu(uncheckAll_DropDownMenuItem, LocalizationHolder.GetString("Client.Core_1657"), "UncheckLevels", false);
    this._btnUncheckAll_UncheckObjTypes = this.AddCommandToMenu(uncheckAll_DropDownMenuItem, LocalizationHolder.GetString("Client.Core_1658"), "UncheckObjTypes", false);
  }

  private void _btnUncheckAll_ShowMenuStrip([NotNull] SplitButton sender, [NotNull] ShowMenuStripEventArgs e)
  {
    MenuButtonItem menuButtonItem = this.BtnUncheckAll_DropDownMenu.Show(this._popupHost, (Control) sender, new Point(0, sender.Height));
    if (menuButtonItem != null && menuButtonItem.CommandName != null)
      e.Handled = this.ProcessCommand(menuButtonItem.CommandName);
    this.TreeView.Focus();
  }

  /// <summary>Событие позволяет отредактировать контекстное меню дерева по месту использования контрола
  /// Вызывается только один раз при создании меню (первом его вызове)</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event SelectObjectCompositionNavTreeView.PopupEvent OnContextPopupInit;

  /// <summary>Событие позволяет обновиться статусы пунктов контекстного мею. Вызывается при каждом вызове меню</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event SelectObjectCompositionNavTreeView.RefreshContextPopupEvent OnContextPopupRefresh;

  /// <summary>Событие позволяет обработать дополнительные команды контекстного меню дерева по месту использования контрола</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event SelectObjectCompositionNavTreeView.TranslateContextPopupCommandEvent OnTranslateContextPopupCommand;

  [NotNull]
  private ContextMenuBarItem TreeView_PopupMenu
  {
    [DebuggerStepThrough] get
    {
      LazyInitializer.EnsureInitialized<ContextMenuBarItem>(ref this._treeView_PopupMenu, (Func<ContextMenuBarItem>) (() =>
      {
        ContextMenuBarItem treeViewPopupMenu = new ContextMenuBarItem();
        this._treeView_CheckAll = this.AddCommandToMenu(treeViewPopupMenu, LocalizationHolder.GetString("Client.Core_1637"), "CheckAll", false);
        this._treeView_CheckLevels = this.AddCommandToMenu(treeViewPopupMenu, LocalizationHolder.GetString("Client.Core_1655"), "CheckLevels", false);
        this._treeView_CheckObjTypes = this.AddCommandToMenu(treeViewPopupMenu, LocalizationHolder.GetString("Client.Core_1638"), "CheckObjTypes", false);
        this._treeView_UncheckAll = this.AddCommandToMenu(treeViewPopupMenu, LocalizationHolder.GetString("Client.Core_1641"), "UncheckAll", true);
        this._treeView_UncheckLevels = this.AddCommandToMenu(treeViewPopupMenu, LocalizationHolder.GetString("Client.Core_1657"), "UncheckLevels", false);
        this._treeView_UncheckObjTypes = this.AddCommandToMenu(treeViewPopupMenu, LocalizationHolder.GetString("Client.Core_1658"), "UncheckObjTypes", false);
        this._treeView_ExpandSelected = this.AddCommandToMenu(treeViewPopupMenu, LocalizationHolder.GetString("Client.Core_1639"), "ExpandSelected", true);
        this._treeView_LoadCheckedComposition = this.AddCommandToMenu(treeViewPopupMenu, LocalizationHolder.GetString("Client.Core_1656"), "LoadCheckedComposition", false);
        SelectObjectCompositionNavTreeView.PopupEvent contextPopupInit = this.OnContextPopupInit;
        if (contextPopupInit != null)
          contextPopupInit((object) this, treeViewPopupMenu);
        return treeViewPopupMenu;
      }));
      NavigatorTreeNode focusedNode = this.TreeView.FocusedNode;
      if (this._treeView_ExpandSelected != null)
      {
        MenuButtonItem viewExpandSelected = this._treeView_ExpandSelected;
        int num;
        if (this.ObjectIsFocused && focusedNode != null)
        {
          NavigatorTreeNode focusedTreeNode = this.FocusedTreeNode;
          num = focusedTreeNode != null ? (focusedTreeNode.HasChildren ? 1 : 0) : 0;
        }
        else
          num = 0;
        viewExpandSelected.Enabled = num != 0;
      }
      if (this._treeView_LoadCheckedComposition != null)
        this._treeView_LoadCheckedComposition.Enabled = this.HasCheckedObjectsWithNotLoadedChilds;
      SelectObjectCompositionNavTreeView.RefreshContextPopupEvent contextPopupRefresh = this.OnContextPopupRefresh;
      if (contextPopupRefresh != null)
        contextPopupRefresh((object) this, this._treeView_PopupMenu, focusedNode);
      return this._treeView_PopupMenu;
    }
  }

  private void SelectObjectCompositionNavTreeView_TreeView_ShowContextMenu(
    [CanBeNull] object sender,
    [NotNull] MouseEventArgs e)
  {
    if (e.Y <= this.TreeView.HeaderHeight)
      return;
    MenuButtonItem menuButtonItem = this.TreeView_PopupMenu.Show(this._popupHost, (Control) this.TreeView, e.Location);
    if (menuButtonItem == null || menuButtonItem.CommandName == null)
      return;
    this.ProcessCommand(menuButtonItem.CommandName);
  }

  /// <summary>Обработка именованной команды</summary>
  /// <returns>true если команда обработана, иначе false</returns>
  protected virtual bool ProcessCommand([NotNull] string commandName)
  {
    switch (commandName)
    {
      case "CheckAll":
        this.CheckAll();
        return true;
      case "CheckLevels":
        this.CheckLevels();
        return true;
      case "CheckObjTypes":
        this.CheckObjTypes(true);
        return true;
      case "ExpandSelected":
        this.ExpandSelected();
        return true;
      case "LoadCheckedComposition":
        this.LoadCheckedComposition();
        return true;
      case "UncheckAll":
        this.UnCheckAll();
        return true;
      case "UncheckLevels":
        this.UncheckLevels();
        return true;
      case "UncheckObjTypes":
        this.CheckObjTypes(false);
        return true;
      default:
        if (this.OnTranslateContextPopupCommand != null)
          this.OnTranslateContextPopupCommand((object) this, commandName);
        return true;
    }
  }

  /// <summary>Корректируем отметки у дочерних нод после загрузки состава ноды</summary>
  protected virtual void ProcessChecks_AfterChildsLoaded([NotNull] NavigatorTreeNode node)
  {
    CheckState checkState1 = node.CheckState;
    if (!node.ShowCheckState && this.TreeView.IsNodeRootObject(node) && !this._rootObjectsAreCheckable)
      checkState1 = node._checkState;
    if (checkState1 == CheckState.Indeterminate && !node.HasChildren)
    {
      node.SetCheckState(CheckState.Checked, true, false, false);
    }
    else
    {
      if (checkState1 == CheckState.Unchecked && node.ShowCheckState || !node.HasChildren)
        return;
      CheckState checkState2 = CheckState.Checked;
      foreach (NavigatorTreeNode objectChild in (IEnumerable<NavigatorTreeNode>) node.GetObjectChilds())
      {
        if (objectChild.ShowCheckState)
        {
          if (objectChild.HasChildren && (!this._maxCheckableObjectLevel.HasValue || this.TreeView.GetObjectNodeLevel(objectChild) != this._maxCheckableObjectLevel.Value))
          {
            objectChild.SetCheckState(CheckState.Indeterminate, false, false, false);
            checkState2 = CheckState.Indeterminate;
          }
          else
            objectChild.SetCheckState(CheckState.Checked, false, false, false);
        }
      }
      if (checkState2 == node.CheckState || !node.ShowCheckState)
        return;
      node.SetCheckState(checkState2, true, false, false);
    }
  }

  /// <summary>Вызывается после загрузки всех дочерних нод, проставляет отметки у них, корректирует отметку у данной если требуется</summary>
  protected override void AfterChildsLoaded(NavigatorTreeNode node)
  {
    this.TreeView.SuspendDataUpdate();
    try
    {
      this.ProcessChecks_AfterChildsLoaded(node);
    }
    finally
    {
      this.TreeView.ResumeDataUpdate();
    }
    base.AfterChildsLoaded(node);
  }

  /// <summary>Перебирает дочерние ноды от той, которой пытаются назначить CheckState == CheckState.Indeterminate
  /// расставляет отметки у дочерних нод с учётом того есть у них состав или нет, загружен или нет, работает рекурсивно,
  /// возвращает какую отметку надо поставить у вышестоящей (если все дочерние ноды получились CheckState.Checked,
  /// то и вышестоящая должна быть CheckState.Checked)</summary>
  private static CheckState TreeAggregateIndeterminateChecks([NotNull] NavigatorTreeNode treeNode)
  {
    return treeNode.GetObjectChilds().Aggregate<NavigatorTreeNode, CheckState>(CheckState.Checked, (Func<CheckState, NavigatorTreeNode, CheckState>) ((seed, childNode) =>
    {
      CheckState checkState = !childNode.HasChildren ? CheckState.Checked : (childNode.GetObjectChilds().Count == 0 ? CheckState.Indeterminate : SelectObjectCompositionNavTreeView.TreeAggregateIndeterminateChecks(childNode));
      if (checkState != childNode.CheckState)
        childNode.SetCheckState(checkState, true, false, false);
      return seed != CheckState.Indeterminate ? checkState : CheckState.Indeterminate;
    }));
  }

  /// <summary>Назначает всем нодам, которые могут содержать отметки и которые наиболее близки к корню иерархии переданный статус отметки
  /// Работает рекурсивно, упирается в ноду, у которой не может быть отметок (ShowCheckState == false),
  /// то отправляется в рекурсию работать с дочерними нодами этой ноды</summary>
  private void SetChecksRecursive(CheckState checkState)
  {
    this.TreeView.SuspendDataUpdate();
    if (!this._rootObjectsAreCheckable)
      this.RootObjectNavigatorTreeNodes.InvokeForAll<NavigatorTreeNode>((Action<NavigatorTreeNode>) (node =>
      {
        node.ShowCheckState = true;
        node.SetCheckState(checkState == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked, false, false, false);
      }));
    try
    {
      int checksWaiting = 0;
      this.TreeView.InvokeForAllClosestToRootTreeNodes((Func<NavigatorTreeNode, bool>) (treeNode => treeNode.ShowCheckState), (Action<NavigatorTreeNode>) (treeNode => checksWaiting += treeNode.ChildsEnumeration((Func<NavigatorTreeNode, bool>) (node => node.ShowCheckState)).Count<NavigatorTreeNode>() + treeNode.ThisAndParents((Func<NavigatorTreeNode, bool>) (node => node.ShowCheckState)).Count));
      if (checksWaiting > 40)
      {
        this._checksProgressForm = ChecksProgressForm.Init(this.FindForm(), checksWaiting);
        this._checksProgressForm.FormClosed += new FormClosedEventHandler(this._checksProgressForm_FormClosed);
      }
      try
      {
        this.TreeView.InvokeForAllClosestToRootTreeNodes((Func<NavigatorTreeNode, bool>) (treeNode => treeNode.ShowCheckState), (Action<NavigatorTreeNode>) (treeNode =>
        {
          if (checkState != CheckState.Indeterminate)
            treeNode.CheckState = checkState;
          else
            treeNode.SetCheckState(!treeNode.HasChildren || this._maxCheckableObjectLevel.HasValue && this.TreeView.GetObjectNodeLevel(treeNode) == this._maxCheckableObjectLevel.Value ? CheckState.Checked : (treeNode.GetObjectChilds().Count == 0 ? CheckState.Indeterminate : SelectObjectCompositionNavTreeView.TreeAggregateIndeterminateChecks(treeNode)), true, false, false);
        }));
      }
      finally
      {
        if (this._checksProgressForm != null)
          this._checksProgressForm.Close();
      }
    }
    finally
    {
      if (!this._rootObjectsAreCheckable)
        this.RootObjectNavigatorTreeNodes.InvokeForAll<NavigatorTreeNode>(new Action<NavigatorTreeNode>(this.RefreshNodeCheckVisible));
      this.TreeView.ResumeDataUpdate();
    }
  }

  protected virtual void SetChecksRecursiveInternal(CheckState checkState)
  {
  }

  /// <summary>Выполнение команды "Отметки по типам..."</summary>
  private void CheckObjTypes(bool check)
  {
    using (SelectorForm selectorForm = new SelectorForm(LocalizationHolder.rm.GetString("Client.Core_1113"), 4, true))
    {
      selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(this.VisibleObjectTypesWithParents.ToArray<int>(), false, true);
      selectorForm.OnCheckActions = SelectorForm.CheckActions.CheckChildren;
      selectorForm.OnUncheckActions = SelectorForm.CheckActions.UncheckChildren;
      selectorForm.AllowRootSelect = true;
      List<int> c = check ? this._selectedTypesCheck : this._selectedTypesUncheck;
      ArrayList idList1 = c != null ? new ArrayList((ICollection) c) : (ArrayList) null;
      ArrayList typeList = new ArrayList();
      if (idList1 == null)
      {
        idList1 = new ArrayList();
        idList1.Add((object) -1);
        typeList.Add((object) typeof (ObjectTypesFolder));
      }
      selectorForm.InitSelectionAsType(idList1, typeList);
      selectorForm.ExpandAll();
      if (selectorForm.ShowDialog() != DialogResult.OK)
        return;
      ArrayList idList2 = selectorForm.IDList;
      List<int> intList = (idList2 != null ? idList2.Cast<int>().ToList<int>() : (List<int>) null) ?? new List<int>();
      if (check)
        this._selectedTypesCheck = intList;
      else
        this._selectedTypesUncheck = intList;
      if (intList.Any<int>((Func<int, bool>) (selType => selType == -1)))
      {
        if (check)
          this.CheckAll();
        else
          this.UnCheckAll();
      }
      else
      {
        if (intList.Count <= 0)
          return;
        this.CheckByTypes(intList.ToArray<int>(intList.Count), check);
      }
    }
  }

  /// <summary>Выполнение команды "Развернуть выбранный объект..."</summary>
  private void ExpandSelected()
  {
    LevelsNumForm.QueryResult queryResult = LevelsNumForm.Query(true, this.FocusedTreeNode != null ? this.TreeView.GetObjectNodeLevel(this.FocusedTreeNode) - 1 : 1);
    if (queryResult == null || this.FocusedTreeNode == null)
      return;
    this.ExpandNodes((IReadOnlyCollection<NavigatorTreeNode>) new NavigatorTreeNode[1]
    {
      this.FocusedTreeNode
    }, queryResult.ResultType == LevelsNumForm.ResultType.Levels ? queryResult.Levels : (queryResult.ResultType == LevelsNumForm.ResultType.LevelsBreak ? queryResult.Levels - this.TreeView.GetObjectNodeLevel(this.FocusedTreeNode) : int.MaxValue));
  }

  /// <summary>Рекурсивное раскрытие нод на заданную глубину</summary>
  private void ExpandNodes([CanBeNull, ItemNotNull] IReadOnlyCollection<NavigatorTreeNode> nodes = null, int levelsToExpand = 2147483647 /*0x7FFFFFFF*/)
  {
    object obj = (object) nodes;
    if (obj == null)
      obj = (object) new NavigatorTreeNode[1]
      {
        this.TreeView.RootNode
      };
    nodes = (IReadOnlyCollection<NavigatorTreeNode>) obj;
    foreach (NavigatorTreeNode navigatorTreeNode in nodes.NotNull<NavigatorTreeNode>())
    {
      this.TreeView.ExpandNodeAndWaitForFull(navigatorTreeNode);
      if (levelsToExpand > 0)
        this.ExpandNodes((IReadOnlyCollection<NavigatorTreeNode>) navigatorTreeNode.GetObjectChilds(), levelsToExpand - 1);
    }
  }

  /// <summary>Служебная функция для создания выпадающего меню</summary>
  [NotNull]
  public MenuButtonItem AddCommandToMenu(
    [NotNull] ContextMenuBarItem contextMenu,
    [NotNull] string text,
    [NotNull] string name,
    bool beginGroup)
  {
    int index = contextMenu.Items.Add(text);
    MenuButtonItem menu = contextMenu.Items[index];
    menu.BeginGroup = beginGroup;
    menu.CommandName = name;
    return menu;
  }

  /// <summary>Выделить все ноды</summary>
  public void CheckAll() => this.SetChecksRecursive(CheckState.Checked);

  /// <summary>Снять выделение со всех нод</summary>
  public void UnCheckAll() => this.SetChecksRecursive(CheckState.Unchecked);

  /// <summary>Расставить отметки объектам переданных типов</summary>
  public void CheckByTypes([NotNull] int[] objTypes, bool check, [CanBeNull] IEnumerable<NavigatorTreeNode> nodes = null)
  {
    if (this._checksProgressForm == null)
    {
      int checksWaiting = 0;
      this.TreeView.InvokeForTreeNodes((Func<NavigatorTreeNode, bool>) (node =>
      {
        if (!node.ShowCheckState)
          return false;
        INodeID nodeId = node.NodeID;
        return nodeId != null && nodeId.IsObjectCategory();
      }), (Action<NavigatorTreeNode>) (node => ++checksWaiting));
      if (checksWaiting > 100)
      {
        this._checksProgressForm = ChecksProgressForm.Init(this.FindForm(), checksWaiting);
        this._checksProgressForm.FormClosed += new FormClosedEventHandler(this._checksProgressForm_FormClosed);
      }
    }
    CheckState newCheckState;
    this.TreeView.InvokeForTreeNodes((Func<NavigatorTreeNode, bool>) (node =>
    {
      if (!node.ShowCheckState)
        return false;
      INodeID nodeId = node.NodeID;
      return nodeId != null && nodeId.IsObjectCategory();
    }), (Action<NavigatorTreeNode>) (node =>
    {
      if ((check && node.CheckState == CheckState.Unchecked || !check && node.CheckState != CheckState.Unchecked) && ((IEnumerable<int>) objTypes).Contains<int>(node.NodeID.TypeID))
      {
        newCheckState = check ? CheckState.Checked : CheckState.Unchecked;
        if ((node.CheckState == CheckState.Indeterminate ? CheckState.Checked : node.CheckState) != newCheckState)
          node.SetCheckState(newCheckState, true, false);
      }
      if (this._checksProgressForm == null)
        return;
      ++this._checksProgressForm.ObjectsChecked;
    }));
    if (this._checksProgressForm == null)
      return;
    this._checksProgressForm.Close();
  }

  /// <summary>Выполнение команды "Загрузить состав отмеченных объектов"</summary>
  public virtual void LoadCheckedComposition()
  {
    this.LoadComposition((IReadOnlyList<NavigatorTreeNode>) this.CheckedObjectNodes.Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node => node.HasChildren && !node.Full)).AsList<NavigatorTreeNode>());
  }

  /// <summary>Если загружены содержимое всех узлов, то вернёт максимальный уровень вложенности объектов, иначе - максимальный int</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int MaxObjectsLevel
  {
    get
    {
      if (this.InDesignMode)
        return 0;
      int result = 0;
      this.TreeView.InvokeForTreeNodes((Func<NavigatorTreeNode, bool>) (node =>
      {
        INodeID nodeId = node.NodeID;
        return nodeId != null && nodeId.IsObjectCategory();
      }), (Func<NavigatorTreeNode, bool>) (node =>
      {
        if (!node.HasChildren)
          result = Math.Max(result, this.TreeView.GetObjectNodeLevel(node));
        else if (!node.Full)
        {
          result = int.MaxValue;
          return false;
        }
        return true;
      }));
      return result == int.MaxValue || this.TreeView.RootNode?.NodeID == null || this.TreeView.RootNode.NodeID.CategoryID != Intermech.Navigator.Consts.CategoryMultipleObjectsNode ? result : result + 1;
    }
  }

  private void CheckLevels()
  {
    SetCheckLevelsFormResult? nullable = SetCheckLevelsForm.Query(this.FindForm(), this.Services, this.ContextName, this.MaxObjectsLevel);
    if (!nullable.HasValue)
      return;
    this.LockUpdateCountStatusesLocks();
    try
    {
      int levels = nullable.Value.Levels + 1;
      if (this.RootObjectNavigatorTreeNodes.Count > 1)
        ++levels;
      this.LoadToLevel(levels + 1);
      this.CheckToLevel(levels);
    }
    finally
    {
      this.UnlockUpdateCountStatusesLocks();
    }
  }

  /// <summary>Загрузить состав объектов до определённого уровня</summary>
  private void LoadToLevel(int targetLevel)
  {
    this.LockUpdateCountStatusesLocks();
    this.TreeView.SuspendDataUpdate();
    try
    {
      this.LoadComposition((IReadOnlyList<NavigatorTreeNode>) this.RootObjectNavigatorTreeNodes.ToList<NavigatorTreeNode>(), targetLevel, true);
    }
    finally
    {
      this.TreeView.ResumeDataUpdate();
      this.UnlockUpdateCountStatusesLocks();
    }
  }

  /// <summary>Отметить объекты до определённого уровня</summary>
  private void CheckToLevel(int levels)
  {
    this.LockUpdateCountStatusesLocks();
    this.TreeView.SuspendDataUpdate();
    try
    {
      NavigatorTreeNode rootNode = this.TreeView.RootNode;
      IList<NavigatorTreeNode> list = (IList<NavigatorTreeNode>) ((IEnumerable<NavigatorTreeNode>) ((rootNode != null ? (object) rootNode.EnumerationWithChilds((Func<NavigatorTreeNode, bool>) (node =>
      {
        INodeID nodeId = node.NodeID;
        return nodeId != null && nodeId.IsObjectCategory();
      }), (Func<NavigatorTreeNode, bool>) (node => this.TreeView.GetObjectNodeLevel(node) <= levels + 1), true) : (object) null) ?? (object) Array.Empty<NavigatorTreeNode>())).Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node => this.TreeView.GetObjectNodeLevel(node) <= levels)).ToList<NavigatorTreeNode>();
      if (list.Count <= 0)
        return;
      int count = list.Count;
      if (count > 100)
      {
        this._checksProgressForm = ChecksProgressForm.Init(this.FindForm(), count);
        this._checksProgressForm.FormClosed += new FormClosedEventHandler(this._checksProgressForm_FormClosed);
      }
      list.InvokeForAll<NavigatorTreeNode>((Action<NavigatorTreeNode>) (node => this.CheckNode(node, levels)));
      if (this._checksProgressForm == null)
        return;
      this._checksProgressForm.Close();
    }
    finally
    {
      this.TreeView.ResumeDataUpdate();
      this.UnlockUpdateCountStatusesLocks();
    }
  }

  private void CheckNode([NotNull] NavigatorTreeNode node, int levels)
  {
    node.SetCheckState(CheckState.Checked, false, false);
    if (this.TreeView.GetObjectNodeLevel(node) != levels || !node.HasChildren)
      return;
    node.InvokeForChilds((Action<NavigatorTreeNode>) (childNode => childNode.SetCheckState(CheckState.Unchecked, false, childNode.HasChildren && childNode.Full, false)), false);
    node.SetCheckState(CheckState.Indeterminate, true, false, false);
  }

  private void UncheckLevels()
  {
    SetUncheckLevelsFormResult? nullable = SetUncheckLevelsForm.Query(this.FindForm(), this.Services, this.ContextName, this.MaxObjectsLevel);
    if (!nullable.HasValue)
      return;
    this.LockUpdateCountStatusesLocks();
    try
    {
      int levels = nullable.Value.Levels + 1;
      if (this.RootObjectNavigatorTreeNodes.Count > 1)
        ++levels;
      this.LoadToLevel(levels + 1);
      this.UncheckAfterLevel(levels);
    }
    finally
    {
      this.UnlockUpdateCountStatusesLocks();
    }
  }

  /// <summary>Отметить объекты до определённого уровня</summary>
  private void UncheckAfterLevel(int levels)
  {
    this.LockUpdateCountStatusesLocks();
    this.TreeView.SuspendDataUpdate();
    try
    {
      NavigatorTreeNode rootNode = this.TreeView.RootNode;
      IList<NavigatorTreeNode> enumerable = (IList<NavigatorTreeNode>) ((rootNode != null ? (object) rootNode.EnumerationWithChilds((Func<NavigatorTreeNode, bool>) (node =>
      {
        INodeID nodeId = node.NodeID;
        return (nodeId != null ? (nodeId.IsObjectCategory() ? 1 : 0) : 0) != 0 && this.TreeView.GetObjectNodeLevel(node) == levels + 1;
      }), (Func<NavigatorTreeNode, bool>) (node => this.TreeView.GetObjectNodeLevel(node) <= levels), true).ToList<NavigatorTreeNode>() : (object) (List<NavigatorTreeNode>) null) ?? (object) Array.Empty<NavigatorTreeNode>());
      if (enumerable.Count <= 0)
        return;
      int count = enumerable.Count;
      if (count > 100)
      {
        this._checksProgressForm = ChecksProgressForm.Init(this.FindForm(), count);
        this._checksProgressForm.FormClosed += new FormClosedEventHandler(this._checksProgressForm_FormClosed);
      }
      enumerable.InvokeForAll<NavigatorTreeNode>((Action<NavigatorTreeNode>) (node => this.UncheckNode(node, levels)));
      if (this._checksProgressForm == null)
        return;
      this._checksProgressForm.Close();
    }
    finally
    {
      this.TreeView.ResumeDataUpdate();
      this.UnlockUpdateCountStatusesLocks();
    }
  }

  private void UncheckNode([NotNull] NavigatorTreeNode node, int levels)
  {
    if (this.TreeView.GetObjectNodeLevel(node) != levels + 1)
      return;
    node.CheckState = CheckState.Unchecked;
  }

  /// <summary>Виртуальный конструктор настроек контрола по-умолчанию</summary>
  /// <returns>The new default select object composition settings</returns>
  [NotNull]
  protected virtual SelectObjectCompositionSettings CreateDefaultSelectObjectCompositionSettings()
  {
    return this.CreateDefaultSettings == null ? new SelectObjectCompositionSettings() : this.CreateDefaultSettings() ?? new SelectObjectCompositionSettings();
  }

  /// <summary>Настройки контрола</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual SelectObjectCompositionSettings SelectObjectSettings
  {
    [DebuggerStepThrough] get => this._SelectObjectSettings;
    [DebuggerStepThrough] set
    {
      this._SelectObjectSettings = value;
      this.TreeView.BackgroundAutoLoadComposition = this._SelectObjectSettings.BackgroundVisibleObjectsCompositionLoad ? AdvNavigatorTreeView.NullAbleBoolDefault.True : AdvNavigatorTreeView.NullAbleBoolDefault.NotSet;
      if (this.TreeView.RootDescriptor == null)
        return;
      if (this._SelectObjectSettings.AutoLoadComposition != SelectObjectCompositionAutoload.None && this.TreeView.RootNode != null)
        this.LoadComposition((IReadOnlyList<NavigatorTreeNode>) this.TreeView.RootNode.GetObjectChilds().ToList<NavigatorTreeNode>(), this._SelectObjectSettings.AutoLoadComposition == SelectObjectCompositionAutoload.Full ? int.MaxValue : this._SelectObjectSettings.AutoLoadCompositionDepth, false);
      this.UpdateCountStatuses();
    }
  }

  /// <summary>Добавление в тулбар дерева кнопки "диалог настроек"</summary>
  private void CreateSettingsButton()
  {
    this._btnSettings = new ButtonItem();
    this._btnSettings.CommandName = "btSettings";
    using (MemoryStream memoryStream = ClientCoreResourcesAccess.LoadResurce(ClientCoreResourcesAccess.nameSpace + "Settings.ico"))
    {
      using (Icon icon = new Icon((Stream) memoryStream))
        this.ImagesToolbar.Images.Add(SelectObjectCompositionNavTreeView.SettingsIconKey, icon);
    }
    this._btnSettings.Image = this.ImagesToolbar.Images[SelectObjectCompositionNavTreeView.SettingsIconKey];
    this._btnSettings.Text = LocalizationHolder.rm.GetString("Settings");
    this._btnSettings.ShowText = true;
    this._btnSettings.ToolTipText = LocalizationHolder.rm.GetString("SettingsForm");
    this._btnSettings.Click += new EventHandler(this.btSettings_Click);
    this.TreeToolbar.Items.Add((ToolbarItemBase) this._btnSettings);
  }

  /// <summary>Виртуальный метод вызова диалога настроек</summary>
  [NotNull]
  protected virtual SelectObjectCompositionsSettingsForm CreateSelectObjectCompositionsSettingsForm(
    [CanBeNull] Form parentForm,
    [CanBeNull] string contextName,
    [NotNull] SelectObjectCompositionSettings settings)
  {
    return this.CreateSettingsForm == null ? new SelectObjectCompositionsSettingsForm(parentForm, this.Services, contextName, settings) : this.CreateSettingsForm(parentForm, contextName, settings) ?? new SelectObjectCompositionsSettingsForm(parentForm, this.Services, contextName, settings);
  }

  /// <summary>Кнопка "диалог настроек" тулбара над деревом</summary>
  private void btSettings_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    using (SelectObjectCompositionsSettingsForm compositionsSettingsForm = this.CreateSelectObjectCompositionsSettingsForm(this.FindForm(), this.ContextName, this._SelectObjectSettings))
    {
      if (compositionsSettingsForm.ShowDialog() == DialogResult.OK)
        this.SelectObjectSettings = compositionsSettingsForm.Settings;
    }
    this.TreeView.Focus();
  }

  protected override void AfterObjectStructureIsLoadingForm()
  {
    base.AfterObjectStructureIsLoadingForm();
    this.UpdateCountStatuses();
  }

  public void LockUpdateCountStatusesLocks() => ++this._updateCountStatusesLocksCounter;

  public void UnlockUpdateCountStatusesLocks()
  {
    if (this._updateCountStatusesLocksCounter > 0)
      --this._updateCountStatusesLocksCounter;
    this.UpdateCountStatuses();
  }

  /// <summary>Варианты склонение числа для получения человекочитабельной строки</summary>
  public int Sklon(int count)
  {
    count = count <= 10 || count >= 20 ? count % 10 : 7;
    if (count == 1)
      return 1;
    return count > 1 && count < 5 ? 2 : 3;
  }

  [NotNull]
  private StringBuilder GetCheckedCountString(
    [CanBeNull] StringBuilder sb = null,
    int checkedObjectsCount = 0,
    int nodesThatCanBeCheckedCount = 0)
  {
    if (sb == null)
      sb = new StringBuilder(100);
    if (checkedObjectsCount == 0)
      checkedObjectsCount = this.CheckedObjectsCount;
    if (nodesThatCanBeCheckedCount == 0)
      nodesThatCanBeCheckedCount = this.NodesThatCanBeCheckedCount;
    if (checkedObjectsCount > 0)
    {
      switch (this.Sklon(checkedObjectsCount))
      {
        case 1:
          sb.AppendFormat(LocalizationHolder.rm.GetString("Client.Core_1659"), (object) checkedObjectsCount);
          break;
        case 2:
          sb.AppendFormat(LocalizationHolder.rm.GetString("Client.Core_1660"), (object) checkedObjectsCount);
          break;
        default:
          sb.AppendFormat(LocalizationHolder.rm.GetString("Client.Core_1661"), (object) checkedObjectsCount);
          break;
      }
      if (!this.ButtonChecksCount.Enabled)
        this.ButtonChecksCount.Enabled = true;
    }
    else
    {
      sb.Append(LocalizationHolder.rm.GetString("Client.Core_1662"));
      if (this.ButtonChecksCount.Enabled)
        this.ButtonChecksCount.Enabled = false;
    }
    return sb;
  }

  /// <summary>Обновление статус-бара</summary>
  public virtual void UpdateCountStatuses()
  {
    if (this.DesignMode || !this.FirstPaintWasCalled || this._checksProgressForm != null || this._ObjectStructureIsLoadingForm != null || this._updateCountStatusesLocksCounter != 0)
      return;
    StringBuilder sb = new StringBuilder(100);
    int checkedObjectsCount = this.CheckedObjectsCount;
    int canBeCheckedCount = this.NodesThatCanBeCheckedCount;
    this.GetCheckedCountString(sb, checkedObjectsCount, canBeCheckedCount);
    sb.Append(" ").AppendFormat(LocalizationHolder.rm.GetString("Client.Core_1663"), (object) canBeCheckedCount, this.Sklon(canBeCheckedCount) == 1 ? (object) LocalizationHolder.rm.GetString("Client.Core_1664") : (object) LocalizationHolder.rm.GetString("Client.Core_1665"));
    this.ButtonChecksCount.Text = sb.ToString();
    this.ButtonChecksCount.DisplayStyle = !this.SelectObjectSettings.WarningWhenCheckedCountMoreThan || checkedObjectsCount <= this.SelectObjectSettings.WarningWhenCheckedCountMoreThanCount ? ToolStripItemDisplayStyle.Text : ToolStripItemDisplayStyle.ImageAndText;
    sb.Clear();
    int loadedChildsCount = this.CheckedObjectsWithNotLoadedChildsCount;
    if (loadedChildsCount > 0)
    {
      sb.AppendFormat(LocalizationHolder.rm.GetString("Client.Core_1666"), (object) loadedChildsCount, this.Sklon(loadedChildsCount) == 1 ? (object) LocalizationHolder.rm.GetString("Client.Core_1667") : (object) LocalizationHolder.rm.GetString("Client.Core_1668"));
      if (!this.ButtonCheckNotLoadedCount.Visible)
        this.ButtonCheckNotLoadedCount.Visible = true;
    }
    else
      this.ButtonCheckNotLoadedCount.Visible = false;
    this.ButtonCheckNotLoadedCount.Text = sb.ToString();
    this.ButtonCheckNotLoadedCount.DisplayStyle = this.SelectObjectSettings.WarningWhenCheckedNotLoaded ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Text;
  }

  private void SelectObjectCompositionNavTreeView_TreeView_BeforeSetCheckedPacket(
    [CanBeNull] object sender,
    [NotNull] NavigatorTreeNodeEventArgs e)
  {
    this._setCheckedPacketProgressLocked = this._checksProgressForm != null;
    if (!this._setCheckedPacketProgressLocked)
    {
      int nodesWaiting = e.Node.ChildsEnumeration((Func<NavigatorTreeNode, bool>) (node => node.ShowCheckState)).Count<NavigatorTreeNode>() + e.Node.ThisAndParents((Func<NavigatorTreeNode, bool>) (node => node.ShowCheckState)).Count;
      if (nodesWaiting > 100)
      {
        this._checksProgressForm = ChecksProgressForm.Init(this.FindForm(), nodesWaiting);
        this._checksProgressForm.FormClosed += new FormClosedEventHandler(this._checksProgressForm_FormClosed);
      }
    }
    this.LockSave("SetCheckedPacket");
  }

  private void _checksProgressForm_FormClosed([CanBeNull] object sender, [NotNull] FormClosedEventArgs e)
  {
    this._checksProgressForm = (ChecksProgressForm) null;
    this.UpdateCountStatuses();
  }

  private void SelectObjectCompositionNavTreeView_TreeView_AfterSetCheckedPacket(
    [CanBeNull] object sender,
    [NotNull] NavigatorTreeNodeEventArgs e)
  {
    if (!this._setCheckedPacketProgressLocked && this._checksProgressForm != null)
      this._checksProgressForm.Close();
    this.UnlockSave("SetCheckedPacket");
  }

  private void _treeView_AfterCreateNode([CanBeNull] object sender, [NotNull] NodeEventArgs e)
  {
    INodeID nodeId = e.Node.NodeID;
    if ((nodeId != null ? (nodeId.IsObjectCategory() ? 1 : 0) : 0) == 0)
      return;
    this.RefreshNodeCheckVisible(e.Node);
  }

  private void RestoreCheckStatus([NotNull] NavigatorTreeNode node)
  {
    if (node.Parent != null && node.Parent != this.TreeView.RootNode?.Parent && node.Parent.ShowCheckState && node.Parent.CheckState != CheckState.Indeterminate)
    {
      node.SetCheckState(node.Parent.CheckState, false, callBeforeSetCheckState: false);
    }
    else
    {
      if (!node.HasChildren || node.Children == null || node.GetObjectChilds().Count <= 0)
        return;
      CheckState checkState = CheckState.Unchecked;
      bool flag = true;
      foreach (NavigatorTreeNode objectChild in (IEnumerable<NavigatorTreeNode>) node.GetObjectChilds())
      {
        if (objectChild.ShowCheckState && objectChild.CheckState != CheckState.Unchecked)
        {
          if (checkState == CheckState.Unchecked)
            checkState = CheckState.Indeterminate;
          if (objectChild.CheckState != CheckState.Checked & flag)
            flag = false;
        }
        else
          flag = false;
      }
      if (checkState == CheckState.Indeterminate & flag)
        checkState = CheckState.Checked;
      if (node.CheckState == checkState)
        return;
      node.SetCheckState(checkState, true, false, false);
    }
  }

  /// <summary>Дополнительная проверка (дополнительная к счётчику блокировок, событию проверки и дочерним счётчикам блокировки) того,
  /// что данные UserControl-а могут быть сохранены</summary>
  protected override bool CanBeSaved() => this._treeView?.RootNode != null && this.ObjectIsChecked;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._pnlSelectButtons = new Panel();
    this._btnCheckAll = new SplitButton();
    this._btnSelectObjects = new Button();
    this._btnUncheckAll = new SplitButton();
    this._statusStrip = new StatusStrip();
    this._buttonChecksCount = new ToolStripStatusLabel();
    this._labelSpace = new ToolStripStatusLabel();
    this._buttonCheckNotLoadedCount = new ToolStripDropDownButton();
    this._menuItemLoadCheckNodesComposition = new ToolStripMenuItem();
    this._treeView.BeginInit();
    this._pnlSelectButtons.SuspendLayout();
    this._statusStrip.SuspendLayout();
    this.SuspendLayout();
    this._treeView.BackgroundImageMode = ImageDrawMode.Tile;
    this._treeView.BorderStyle = BorderStyle.Fixed3D;
    this._treeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
    this._treeView.DisableDragAndDrop = true;
    this._treeView.DisableIMContextMenu = true;
    this._treeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._treeView.RowEvenStyle.WordWrap = false;
    this._treeView.RowOddStyle.WordWrap = false;
    this._treeView.RowSelectedStyle.WordWrap = false;
    this._treeView.RowStyle.BorderColor = SystemColors.Control;
    this._treeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this._treeView.RowStyle.BorderWidth = 1;
    this._treeView.RowStyle.WordWrap = false;
    this._treeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this._treeView.Size = new Size(715, 455);
    this._treeView.AfterCreateNode += new EventHandler<NodeEventArgs>(this._treeView_AfterCreateNode);
    this._treeView.CheckStateChanged += new EventHandler<NodeEventArgs>(this.SelectObjectCompositionNavTreeView_TreeView_CheckStateChanged);
    this._treeView.ShowContextMenu += new MouseEventHandler(this.SelectObjectCompositionNavTreeView_TreeView_ShowContextMenu);
    this._treeView.BeforeSetCheckedPacket += new EventHandler<NavigatorTreeNodeEventArgs>(this.SelectObjectCompositionNavTreeView_TreeView_BeforeSetCheckedPacket);
    this._treeView.AfterSetCheckedPacket += new EventHandler<NavigatorTreeNodeEventArgs>(this.SelectObjectCompositionNavTreeView_TreeView_AfterSetCheckedPacket);
    this._treeView.MouseDown += new MouseEventHandler(this.SelectObjectCompositionNavTreeView_TreeView_MouseDown);
    this._pnlSelectButtons.Controls.Add((Control) this._btnCheckAll);
    this._pnlSelectButtons.Controls.Add((Control) this._btnSelectObjects);
    this._pnlSelectButtons.Controls.Add((Control) this._btnUncheckAll);
    this._pnlSelectButtons.Controls.Add((Control) this._statusStrip);
    this._pnlSelectButtons.Dock = DockStyle.Bottom;
    this._pnlSelectButtons.Location = new Point(0, 479);
    this._pnlSelectButtons.Name = "_pnlSelectButtons";
    this._pnlSelectButtons.Size = new Size(715, 58);
    this._pnlSelectButtons.TabIndex = 12;
    this._btnCheckAll.ClickedImage = "Clicked";
    this._btnCheckAll.DisabledImage = "Disabled";
    this._btnCheckAll.FocusedImage = "Focused";
    this._btnCheckAll.HoverImage = "Hover";
    this._btnCheckAll.ImageAlign = ContentAlignment.MiddleRight;
    this._btnCheckAll.ImageKey = "Normal";
    this._btnCheckAll.ImeMode = ImeMode.NoControl;
    this._btnCheckAll.Location = new Point(6, 29);
    this._btnCheckAll.Name = "_btnCheckAll";
    this._btnCheckAll.NormalImage = "Normal";
    this._btnCheckAll.Size = new Size(122, 23);
    this._btnCheckAll.TabIndex = 1;
    this._btnCheckAll.Text = "  Отметить все";
    this._btnCheckAll.TextAlign = ContentAlignment.MiddleLeft;
    this._btnCheckAll.ShowMenuStrip += new ShowMenuStripEventHandler(this._btnCheckAll_ShowMenuStrip);
    this._btnCheckAll.Click += new EventHandler(this._btnCheckAll_Click);
    this._btnSelectObjects.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._btnSelectObjects.ImeMode = ImeMode.NoControl;
    this._btnSelectObjects.Location = new Point(553, 29);
    this._btnSelectObjects.Name = "_btnSelectObjects";
    this._btnSelectObjects.Size = new Size(156, 23);
    this._btnSelectObjects.TabIndex = 4;
    this._btnSelectObjects.Text = "Выбрать другие объекты";
    this._btnSelectObjects.Click += new EventHandler(this._btnSelectObjects_Click);
    this._btnUncheckAll.ClickedImage = "Clicked";
    this._btnUncheckAll.DisabledImage = "Disabled";
    this._btnUncheckAll.FocusedImage = "Focused";
    this._btnUncheckAll.HoverImage = "Hover";
    this._btnUncheckAll.ImageAlign = ContentAlignment.MiddleRight;
    this._btnUncheckAll.ImageKey = "Normal";
    this._btnUncheckAll.ImeMode = ImeMode.NoControl;
    this._btnUncheckAll.Location = new Point(137, 29);
    this._btnUncheckAll.Name = "_btnUncheckAll";
    this._btnUncheckAll.NormalImage = "Normal";
    this._btnUncheckAll.Size = new Size(135, 23);
    this._btnUncheckAll.TabIndex = 2;
    this._btnUncheckAll.Text = " Снять все отметки";
    this._btnUncheckAll.TextAlign = ContentAlignment.MiddleLeft;
    this._btnUncheckAll.ShowMenuStrip += new ShowMenuStripEventHandler(this._btnUncheckAll_ShowMenuStrip);
    this._btnUncheckAll.Click += new EventHandler(this._btnUnCheckAll_Click);
    this._statusStrip.AutoSize = false;
    this._statusStrip.Dock = DockStyle.Top;
    this._statusStrip.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this._buttonChecksCount,
      (ToolStripItem) this._labelSpace,
      (ToolStripItem) this._buttonCheckNotLoadedCount
    });
    this._statusStrip.Location = new Point(0, 0);
    this._statusStrip.Margin = new Padding(0, 0, 0, 2);
    this._statusStrip.Name = "_statusStrip";
    this._statusStrip.ShowItemToolTips = true;
    this._statusStrip.Size = new Size(715, 22);
    this._statusStrip.SizingGrip = false;
    this._statusStrip.TabIndex = 5;
    this._statusStrip.TabStop = true;
    this._buttonChecksCount.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this._buttonChecksCount.Enabled = false;
    this._buttonChecksCount.Image = (Image) Intermech.Client.Core.Properties.Resources.WarningBMP;
    this._buttonChecksCount.ImageTransparentColor = Color.Magenta;
    this._buttonChecksCount.Name = "_buttonChecksCount";
    this._buttonChecksCount.Size = new Size(120, 17);
    this._buttonChecksCount.Text = "Идёт загрузка...";
    this._buttonChecksCount.ToolTipText = "Сводка по отмеченным объектам";
    this._labelSpace.Name = "_labelSpace";
    this._labelSpace.Size = new Size(580, 17);
    this._labelSpace.Spring = true;
    this._buttonCheckNotLoadedCount.AutoToolTip = false;
    this._buttonCheckNotLoadedCount.DropDownItems.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this._menuItemLoadCheckNodesComposition
    });
    this._buttonCheckNotLoadedCount.Image = (Image) Intermech.Client.Core.Properties.Resources.WarningBMP;
    this._buttonCheckNotLoadedCount.ImageTransparentColor = Color.Magenta;
    this._buttonCheckNotLoadedCount.Name = "_buttonCheckNotLoadedCount";
    this._buttonCheckNotLoadedCount.Size = new Size(29, 20);
    this._buttonCheckNotLoadedCount.Visible = false;
    this._menuItemLoadCheckNodesComposition.Name = "_menuItemLoadCheckNodesComposition";
    this._menuItemLoadCheckNodesComposition.Size = new Size(349, 26);
    this._menuItemLoadCheckNodesComposition.Text = "Загрузить состав выбранных изделий";
    this._menuItemLoadCheckNodesComposition.Click += new EventHandler(this._menuItemLoadCheckNodesComposition_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._pnlSelectButtons);
    this.MinimumSize = new Size(562, 204);
    this.Name = nameof (SelectObjectCompositionNavTreeView);
    this.Size = new Size(715, 540);
    this.Controls.SetChildIndex((Control) this._pnlSelectButtons, 0);
    this.Controls.SetChildIndex((Control) this._treeView, 0);
    this._treeView.EndInit();
    this._pnlSelectButtons.ResumeLayout(false);
    this._statusStrip.ResumeLayout(false);
    this._statusStrip.PerformLayout();
    this.ResumeLayout(false);
  }

  [CanBeNull]
  public delegate NavigatorTreeNode CreateNavTreeNodeDelegate(
    [NotNull] NavigatorTreeView navTreeView,
    [NotNull] NavigatorTreeNode parent,
    [CanBeNull] INodeID nodeID,
    [CanBeNull] object[] fieldValues,
    [CanBeNull] object[] rawValues);

  public delegate void AfterNavTreeNodeCreatedDelegate(
    [NotNull] NavigatorTreeView navTreeView,
    [NotNull] NavigatorTreeNode parent,
    [CanBeNull] INodeID nodeID,
    [CanBeNull] object[] fieldValues,
    [CanBeNull] object[] rawValues,
    [NotNull] NavigatorTreeNode createdNavigatorTreeNode);

  public delegate void PopupEvent([CanBeNull] object sender, [NotNull] ContextMenuBarItem menu);

  public delegate void RefreshContextPopupEvent(
    [CanBeNull] object sender,
    [NotNull] ContextMenuBarItem menu,
    [CanBeNull] NavigatorTreeNode selectedNode);

  public delegate void TranslateContextPopupCommandEvent([CanBeNull] object sender, [NotNull] string commandName);

  public delegate SelectObjectCompositionSettings CreateDefaultSettingsDelegate();

  public delegate SelectObjectCompositionsSettingsForm CreateSettingsFormDelegate(
    [CanBeNull] Form parentForm,
    [CanBeNull] string contextName,
    [NotNull] SelectObjectCompositionSettings settings);
}
