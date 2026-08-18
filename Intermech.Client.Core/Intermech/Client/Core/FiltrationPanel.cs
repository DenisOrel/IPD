
// Type: Intermech.Client.Core.FiltrationPanel
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.PropertyEditors;
using Intermech.Search.VersionSelectionRules;
using Intermech.Search.VersionSelectionRules.AddingToDropdownList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class FiltrationPanel : IFiltrationService
{
  private ComboBoxItem cbFiltrationRule;
  private ButtonItem btRuleBrowser;
  private ButtonItem btRuleVariant;
  private ButtonItem btRuleHint;
  private ButtonItem btUseStoredExplicitPartVersionID;
  /// <summary>Для потокобезопасного доступа</summary>
  private object _syncRoot = new object();
  /// <summary>Количество незакрытых вызовов BeginUpdates</summary>
  private int _lockCount;
  /// <summary>
  /// Кэш настроек фильтрации (чтобы не дёргать сервер для зачитывания настроек)
  /// </summary>
  public Dictionary<string, FiltrationSettings> FiltrationsCache = new Dictionary<string, FiltrationSettings>();
  /// <summary>Кэш правил подбора версий, их валидности и т.п.</summary>
  private Dictionary<string, MyElementEx> _rulesCache = new Dictionary<string, MyElementEx>();
  /// <summary>
  /// Свойство позволяет разрешить или запретить пользователю вносить изменения в настройки фильтрации
  /// </summary>
  private bool _filtrationEnabled = true;
  /// <summary>
  /// Уникальный ID текущего владельца настроек фильтрации,
  /// </summary>
  private string _ownerID = string.Empty;
  /// <summary>
  /// Изменились ли настройки фильтрации (чтобы сохранять их в базу данных)
  /// </summary>
  private bool _filtrationChanged;
  /// <summary>
  /// Текущие настройки фильтрации состава, информация по которым отображена в тулбаре "Фильтрация состава"
  /// </summary>
  private FiltrationSettings _currentFiltration = new FiltrationSettings();
  /// <summary>
  /// Правило фильтрации состава, возвращающее последние версии объектов
  /// </summary>
  private VersionsRule _latestVersionsRule = new VersionsRule();
  /// <summary>
  /// Правило фильтрации состава, возвращающее все версии объектов
  /// </summary>
  private VersionsRule _allVersionsRule = new VersionsRule();
  /// <summary>
  /// Правило фильтрации состава, возвращающее базовые версии объектов
  /// </summary>
  private VersionsRule _baseVersionRule = new VersionsRule();
  /// <summary>
  /// Правило фильтрации состава, возвращающее все версии объектов
  /// </summary>
  private VersionsRule _sequentialModificationsRule = new VersionsRule();
  /// <summary>Правило фильтрации состава по умолчанию</summary>
  private VersionsRule _defaultVersionRule = new VersionsRule();
  /// <summary>
  /// Если выбранное правило является вариантом значений переменных (т.е. создано на базе родительского правила),
  /// то это поле отражает, совместимо ли правило с родительским вариантом (на случай, если были изменения
  /// в родительском правиле после создания вариантов его значений переменных)
  /// </summary>
  private bool _ruleCompatible;
  /// <summary>
  /// Валидно ли выбранное правило подбора версий
  /// (для проверки выполняется метод Valid правила, а также проверяется наличие у него переменных значений)
  /// Если _FSRuleValid = false, правило применять нельзя
  /// </summary>
  private bool _ruleValid;
  /// <summary>Код ошибки для текущего правила:</summary>
  private CurrentRuleErrors _errorCode = CurrentRuleErrors.AllVersions;
  /// <summary>
  /// True, если указано неверное значение варианта значений переменных
  /// </summary>
  private bool _varsOutOfRange;
  /// <summary>Коллекция индексов значков</summary>
  private int[] _fsImages = new int[6];
  /// <summary>
  /// Чтобы избежать рекурсивного вызова одного обработчика событий внутри другого
  /// </summary>
  private bool _fsIsLoading;
  private bool _userLinked = true;

  public FiltrationPanel(
    Intermech.Bars.ToolBar filterToolbar,
    ComboBoxItem cbFiltrationRule,
    ButtonItem btRuleBrowser,
    ButtonItem btRuleVariant,
    ButtonItem btRuleHint,
    ButtonItem btUseStoredExplicitPartVersionID,
    bool userLinked)
  {
    this.ToolBar = filterToolbar;
    this.cbFiltrationRule = cbFiltrationRule;
    this.btRuleBrowser = btRuleBrowser;
    this.btRuleHint = btRuleHint;
    this.btRuleVariant = btRuleVariant;
    this.btUseStoredExplicitPartVersionID = btUseStoredExplicitPartVersionID;
    this._userLinked = userLinked;
    (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).Subscribe("ObjectsChanged", new Intermech.Interfaces.Client.NotificationEventHandler(this.NotificationEventHandler));
  }

  private void NotificationEventHandler(object sender, NotificationEventArgs e)
  {
    DBObjectsEventArgs dbObjectsEventArgs = e as DBObjectsEventArgs;
    if (dbObjectsEventArgs == null || dbObjectsEventArgs.ObjectTypeIDs == null || !((IEnumerable<int>) VersionSelectionRulesConstants.AllVersionSelectionRuluObjectTypeIds).Any<int>((System.Func<int, bool>) (versionRuleTypeID => dbObjectsEventArgs.ObjectTypeIDs.Contains(versionRuleTypeID))))
      return;
    this.FillFilterCombobox();
  }

  public void Initialize()
  {
    this.cbFiltrationRule.ComboBox.SelectedIndexChanged += new EventHandler(this.cbFiltrationRule_SelectedIndexChanged);
    this.cbFiltrationRule.ComboBox.Cursor = Cursors.Hand;
    this.cbFiltrationRule.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbFiltrationRule.ComboBox.MaxDropDownItems = 16 /*0x10*/;
    this.cbFiltrationRule.ComboBox.Sorted = false;
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    if (this.ToolBar.ImageList == null)
      this.ToolBar.ImageList = service.ImageList;
    this.btRuleVariant.ImageIndex = service.ImageIndex("imgVersionRuleImport");
    this.btRuleVariant.Click += new EventHandler(this.btRuleVariant_Click);
    this.btRuleBrowser.ImageIndex = service.ImageIndex("imgVersionRuleEditor");
    this.btRuleBrowser.Click += new EventHandler(this.btRuleBrowser_Click);
    this.btRuleHint.ImageIndex = service.ImageIndex("imgApplyBall");
    this.btRuleHint.Click += new EventHandler(this.btRuleHint_Click);
    this.btUseStoredExplicitPartVersionID.Click += new EventHandler(this.UseStoredExplicitPartVersionIDButtonItem_Click);
    this._fsImages[0] = service.ImageIndex("imgGreenBall");
    this._fsImages[1] = service.ImageIndex("imgYellowBall");
    this._fsImages[2] = service.ImageIndex("imgRedBall");
    this._fsImages[3] = service.ImageIndex("imgApplyBall");
    this._fsImages[4] = service.ImageIndex("imgInvalidRule");
    this._fsImages[5] = service.ImageIndex("imgCorruptedRule");
  }

  private void UseStoredExplicitPartVersionIDButtonItem_Click(object sender, EventArgs e)
  {
    this.ToggleUseStoredExplicitPartVersionID();
  }

  private void ToggleUseStoredExplicitPartVersionID()
  {
    if (this._currentFiltration == null || this._currentFiltration.Tags == null)
      return;
    this._currentFiltration.Tags[(object) "{4534BBF7-86AF-4BCB-B7FF-C9AE40D28CB4}"] = (object) !object.Equals(this._currentFiltration.Tags[(object) "{4534BBF7-86AF-4BCB-B7FF-C9AE40D28CB4}"], (object) true);
    this.SetUseStoredExplicitPartVersionIDButtonItemChecked();
    this.FiltrationApplyUpdates(true);
  }

  private void SetUseStoredExplicitPartVersionIDButtonItemChecked()
  {
    this.btUseStoredExplicitPartVersionID.Checked = this.UseStoredExplicitPartVersionID();
  }

  private bool UseStoredExplicitPartVersionID()
  {
    return this._currentFiltration != null && this._currentFiltration.Tags != null && object.Equals(this._currentFiltration.Tags[(object) "{4534BBF7-86AF-4BCB-B7FF-C9AE40D28CB4}"], (object) true);
  }

  private void cbFiltrationRule_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._ownerID == string.Empty)
      return;
    if (this._fsIsLoading)
      return;
    try
    {
      this._fsIsLoading = true;
      VersionsRule versionsRule = (VersionsRule) null;
      if (this.cbFiltrationRule.ComboBox.SelectedItem != null)
      {
        if (!(this.cbFiltrationRule.ComboBox.SelectedItem is MyElement selectedItem))
          return;
        versionsRule = selectedItem.Value as VersionsRule;
      }
      this._currentFiltration.CurrentRule = versionsRule;
      this.FiltrationApplyUpdates(true);
    }
    finally
    {
      this._fsIsLoading = false;
    }
  }

  private void RemoveDublicatesAndBlanksFromComposeFilterComboBox()
  {
    HashSet<MyElement> source = new HashSet<MyElement>();
    foreach (object obj in this.cbFiltrationRule.ComboBox.Items)
    {
      if (!(obj is MyElement myElement))
        source.Add(myElement);
      if (!source.Contains(myElement) && myElement.Value is VersionsRule versionsRule && !string.IsNullOrEmpty(versionsRule.RuleObjectGuid))
        source.Add(myElement);
    }
    this.cbFiltrationRule.ComboBox.Items.Clear();
    this.cbFiltrationRule.ComboBox.Items.AddRange((object[]) source.ToArray<MyElement>());
  }

  /// <summary>
  /// Заполнить и настроить ComboBox(-ы) тулбара "Фильтрация состава"
  /// </summary>
  private void FillFilterCombobox()
  {
    bool fsIsLoading = this._fsIsLoading;
    try
    {
      this.cbFiltrationRule.ComboBox.Items.Clear();
      if (this.cbFiltrationRule.ComboBox.Items.Count > 0)
        this.RemoveDublicatesAndBlanksFromComposeFilterComboBox();
      this._fsIsLoading = true;
      MyElement myElement1 = (MyElement) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(VersionSelectionRulesConstants.VersionSelectionRuleObjectTypeID);
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
        dbRecordSetParams.Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        };
        // ISSUE: explicit reference operation
        (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
        {
          new ConditionStructure()
          {
            Attribute = (object) AddingToDropdownListConstants.AddToDropdownListAttributeTypeID,
            RelationalOperator = RelationalOperators.Equal,
            Value = (object) true,
            SQL = string.Empty
          }
        };
        dbRecordSetParams.RecordCount = -1;
        DBRecordSetParams paramSet = dbRecordSetParams;
        foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
        {
          long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
          IDBObject RuleObject = sessionKeeper.Session.GetObject(int64Value, false);
          if (RuleObject != null)
          {
            VersionsRule versionsRule = new VersionsRule();
            versionsRule.LoadFromObject(sessionKeeper.Session, RuleObject);
            this.cbFiltrationRule.ComboBox.Items.Add((object) new MyElement((object) versionsRule, versionsRule.RuleObjectCaption, (object) 0));
          }
        }
      }
      if (this.GetComboItem("cad00601-306c-11d8-b4e9-00304f19f545") == null)
        this.cbFiltrationRule.ComboBox.Items.Insert(0, (object) new MyElement((object) this._baseVersionRule, $"{this._baseVersionRule.RuleObjectCaption}", (object) 0));
      if (this.GetComboItem("cad00602-306c-11d8-b4e9-00304f19f545") == null)
        this.cbFiltrationRule.ComboBox.Items.Insert(1, (object) new MyElement((object) this._sequentialModificationsRule, $"{this._sequentialModificationsRule.RuleObjectCaption}", (object) 0));
      MyElement myElement2 = this.GetComboItem(VersionsRuleType.vrtLatestVersionsRule);
      if (myElement2 == null)
      {
        myElement2 = new MyElement((object) this._latestVersionsRule, $"{VersionsRuleConsts.ruleLatestVersions}", (object) 0);
        this.cbFiltrationRule.ComboBox.Items.Insert(2, (object) myElement2);
      }
      if (this.GetComboItem(this._defaultVersionRule.RuleObjectGuid) == null)
        this.cbFiltrationRule.ComboBox.Items.Insert(0, (object) new MyElement((object) this._defaultVersionRule, $"{this._defaultVersionRule.RuleObjectCaption}", (object) 0));
      if (this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtLatestVersionsRule)
        this.cbFiltrationRule.ComboBox.SelectedItem = (object) myElement2;
      else if (this._currentFiltration.CurrentRule == null)
      {
        this.cbFiltrationRule.ComboBox.SelectedIndex = -1;
      }
      else
      {
        if (this.cbFiltrationRule.ComboBox.Items.Count > 0 && this._currentFiltration.CurrentRule != null)
        {
          if (this._currentFiltration.CurrentRule.RuleObjectGuid != null)
            myElement1 = this.GetComboItem(this._currentFiltration.CurrentRule.RuleObjectGuid);
          if (myElement1 == null)
          {
            foreach (MyElement myElement3 in this.cbFiltrationRule.ComboBox.Items)
            {
              if (myElement3 != null && Convert.ToInt64(myElement3.Tag) == this._currentFiltration.CurrentRule.RuleObjectID && (myElement3.Value as VersionsRule).CurrentRuleType == this._currentFiltration.CurrentRule.CurrentRuleType && (this._currentFiltration.CurrentRule.RuleObjectID != 0L || Convert.ToInt64(myElement3.Tag) != 0L || !(this._currentFiltration.CurrentRule.RuleObjectCaption != myElement3.Caption)))
              {
                myElement1 = myElement3;
                break;
              }
            }
          }
        }
        string caption = string.Format("{0}", (object) this._currentFiltration.CurrentRule.RuleObjectCaption, (object) null);
        if (this._currentFiltration.CurrentRuleVars >= 0 && this._errorCode != CurrentRuleErrors.MainVariantIsNotSpecified && this._currentFiltration.CurrentRule != null)
          caption = $"{this._currentFiltration.CurrentRule.RuleObjectCaption} - {this._currentFiltration.CurrentRule.GetDisplayValue(2)}";
        if (myElement1 == null && this._currentFiltration.CurrentRule != null)
        {
          myElement1 = new MyElement((object) (this._currentFiltration.CurrentRule.Clone() as VersionsRule), caption, (object) this._currentFiltration.CurrentRule.RuleObjectID);
          if (this._currentFiltration.CurrentRule.CurrentRuleType != VersionsRuleType.vrtAllVersionsRule)
            this.cbFiltrationRule.ComboBox.Items.Add((object) myElement1);
        }
        else
        {
          myElement1.Caption = caption;
          myElement1.Value = (object) (this._currentFiltration.CurrentRule.Clone() as VersionsRule);
        }
        int index = this.cbFiltrationRule.ComboBox.Items.IndexOf((object) myElement1);
        if (index >= 0 && index < this.cbFiltrationRule.ComboBox.Items.Count)
          this.cbFiltrationRule.ComboBox.Items[index] = (object) myElement1;
        this.cbFiltrationRule.ComboBox.SelectedItem = (object) myElement1;
      }
    }
    finally
    {
      this._fsIsLoading = fsIsLoading;
    }
  }

  /// <summary>
  /// Настроить основной вариант значений для текущего правила подбора версий
  /// </summary>
  private void FiltrationVariant()
  {
    if (this._fsIsLoading)
      return;
    bool fsIsLoading = this._fsIsLoading;
    try
    {
      this._fsIsLoading = true;
      FiltrationSettingsForm.Execute(this._ownerID, false);
    }
    finally
    {
      this._fsIsLoading = fsIsLoading;
      this.FiltrationUpdate(true);
    }
  }

  public void FiltrationUpdate(bool FireEvent)
  {
    lock (this._syncRoot)
      this._filtrationChanged = true;
    this.FiltrationReload(this._ownerID);
    this.FillFilterToolbar();
    lock (this._syncRoot)
    {
      if (!FireEvent || this._lockCount > 0)
        return;
      FiltrationChanged filtrationChanged = this.OnFiltrationChanged;
      if (filtrationChanged == null)
        return;
      filtrationChanged((IFiltrationSettings) this._currentFiltration, this.RuleValid && this.RuleCompatible);
    }
  }

  /// <summary>Выбрать другое правило подбора версий</summary>
  private void FiltrationBrowse()
  {
    if (this._fsIsLoading)
      return;
    bool fsIsLoading = this._fsIsLoading;
    try
    {
      this._fsIsLoading = true;
      long[] numArray = SelectionWindow.SelectObjects(FiltrationConsts.Dialog1, FiltrationConsts.Dialog2, ObjectTypesHelper.GetObjTypeID("cad001b3-306c-11d8-b4e9-00304f19f545"), SelectionOptions.Default);
      if (numArray == null)
        return;
      long Object_ID = numArray[0];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
        if (customService.RuleType((object) sessionKeeper.Session.SessionGUID, Object_ID) == VersionsRuleType.vrtAllVersionsRule)
        {
          int num = (int) MessageBox.Show(FiltrationConsts.Dialog4, FiltrationConsts.Dialog3, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          this._fsIsLoading = fsIsLoading;
          this.FiltrationSetRuleID(customService[Object_ID]);
        }
      }
    }
    finally
    {
      this._fsIsLoading = fsIsLoading;
    }
  }

  /// <summary>
  /// Выполнить переадресацию команды с одного контрола тулбара на другой
  /// в зависимости от того, какой код ошибки установлен
  /// </summary>
  private void FiltrationRedirect()
  {
    switch (this._errorCode)
    {
      case CurrentRuleErrors.NoSelected:
        this.FiltrationBrowse();
        break;
      case CurrentRuleErrors.Changed:
        this.FiltrationVariant();
        break;
      case CurrentRuleErrors.NoVariableValue:
        this.FiltrationVariant();
        break;
      case CurrentRuleErrors.MainVariantIsNotSpecified:
        this.FiltrationVariant();
        break;
      case CurrentRuleErrors.Incorrect:
        this.FiltrationBrowse();
        break;
    }
  }

  /// <summary>Вызовем окно по настройке вариантов значений правил</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void btRuleVariant_Click(object sender, EventArgs e) => this.FiltrationVariant();

  /// <summary>Вызовем окно по выбору правила подбора версий</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void btRuleBrowser_Click(object sender, EventArgs e) => this.FiltrationBrowse();

  /// <summary>
  /// Переадресовать команды в зависимости от текущего кода ошибки
  /// </summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void btRuleHint_Click(object sender, EventArgs e)
  {
    this.UpdateFilterToolbarControls();
    this.FiltrationRedirect();
  }

  /// <summary>
  /// Обновить статус контролов тулбара "Фильтрация состава"
  /// </summary>
  public void UpdateFilterToolbarControls()
  {
    bool flag = this._filtrationEnabled && this._currentFiltration != null && this._currentFiltration.OwnerID != null && this._ownerID.Length > 0;
    this.cbFiltrationRule.Enabled = flag;
    this.btRuleVariant.Enabled = flag && this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule;
    this.btRuleBrowser.Enabled = flag;
    this._errorCode = CurrentRuleErrors.Valid;
    string str = "";
    int fsImage = this._fsImages[3];
    if (this._currentFiltration != null && this._currentFiltration.CurrentRule == null)
    {
      str = FiltrationConsts.Tip0;
      fsImage = this._fsImages[2];
      this._errorCode = CurrentRuleErrors.NoSelected;
    }
    if (this._currentFiltration != null && this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && !this.RuleValid)
    {
      str = FiltrationConsts.Tip6;
      fsImage = this._fsImages[5];
      this._errorCode = CurrentRuleErrors.Incorrect;
    }
    if (this._currentFiltration != null && this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && this.RuleValid && this._currentFiltration.CurrentRule.HasVariableValues() && (this._currentFiltration.CurrentRuleVars < 0 || this._varsOutOfRange))
    {
      str = FiltrationConsts.Tip5;
      fsImage = this._fsImages[1];
      this._errorCode = CurrentRuleErrors.MainVariantIsNotSpecified;
    }
    if (this._currentFiltration != null && this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && this.RuleValid && !this.RuleCompatible)
    {
      str = FiltrationConsts.Tip1;
      fsImage = this._fsImages[2];
      this._errorCode = CurrentRuleErrors.Changed;
    }
    if (this._ownerID.Length <= 0)
    {
      str = "";
      fsImage = this._fsImages[0];
      this._errorCode = CurrentRuleErrors.Valid;
    }
    if (this.btRuleHint.Text != str)
      this.btRuleHint.Text = str;
    if (this.btRuleHint.ImageIndex != fsImage)
      this.btRuleHint.ImageIndex = fsImage;
    this.btRuleHint.Visible = this.btRuleHint.Text.Length > 0;
    this.btRuleHint.Enabled = this._filtrationEnabled;
    this.SetUseStoredExplicitPartVersionIDButtonItemChecked();
  }

  /// <summary>
  /// Найти в ComboBox элемент с системным правилом указанного типа
  /// </summary>
  /// <param name="RuleType">Допустимые типы системных правил - vrtLatestVersionsRule и vrtAllVersionsRule</param>
  /// <returns>Элемент, содержащий ссылку на системное правило указанного типа, или null</returns>
  private MyElement GetComboItem(VersionsRuleType RuleType)
  {
    if (this.cbFiltrationRule.ComboBox.Items.Count > 0)
    {
      foreach (MyElement comboItem in this.cbFiltrationRule.ComboBox.Items)
      {
        if (comboItem != null && comboItem.Value != null && comboItem.Value is VersionsRule versionsRule && versionsRule.CurrentRuleType == RuleType)
          return comboItem;
      }
    }
    return (MyElement) null;
  }

  /// <summary>
  /// Найти в ComboBox элемент с правилом, Guid которого равен указанному значению
  /// </summary>
  /// <param name="RuleGuid">Guid искомого правила</param>
  /// <returns>Элемент, содержащий ссылку на указанное правило</returns>
  private MyElement GetComboItem(string RuleGuid)
  {
    if (this.cbFiltrationRule.ComboBox.Items.Count > 0)
    {
      foreach (MyElement comboItem in this.cbFiltrationRule.ComboBox.Items)
      {
        if (comboItem != null && comboItem.Value != null && comboItem.Value is VersionsRule versionsRule && versionsRule.RuleObjectGuid == RuleGuid)
          return comboItem;
      }
    }
    return (MyElement) null;
  }

  /// <summary>
  /// Заполнить контролы тулбара "Фильтрация состава" данными, хранящимися в приватных переменных _FS...
  /// </summary>
  public void FillFilterToolbar()
  {
    this.FillFilterCombobox();
    this.UpdateFilterToolbarControls();
  }

  /// <summary>
  /// Попробовать загрузить в текущие настройки фильтрации состава правило подбора версий,
  /// а заодно отыскать для этого правила номер его варианта значений переменных
  /// Автоматически будет вызвано событие, уведомляеющее о смене в настройках фильтрации состава.
  /// </summary>
  /// <param name="rule">Правило подбора версий</param>
  private void FiltrationSetRuleID(VersionsRule rule)
  {
    lock (this._syncRoot)
    {
      if (this._ownerID == string.Empty || rule == null)
        return;
      this._currentFiltration.CurrentRule = rule;
      this._currentFiltration.CurrentRuleVars = this._currentFiltration[rule.RuleObjectID];
      this._filtrationChanged = true;
    }
    this.FiltrationSave();
    this.FiltrationReload(this._currentFiltration.OwnerID);
    lock (this._syncRoot)
    {
      if (this._lockCount <= 0)
      {
        FiltrationChanged filtrationChanged = this.OnFiltrationChanged;
        if (filtrationChanged != null)
          filtrationChanged((IFiltrationSettings) this._currentFiltration, this.RuleValid && this.RuleCompatible);
      }
    }
    this.FillFilterToolbar();
  }

  /// <summary>
  /// Перечитать текущие настройки фильтрации состава, а также текущее правило подбора версий
  /// </summary>
  /// <returns>true, если указанные настройки были созданы заново (их требуется сохранить в базе данных)</returns>
  public bool FiltrationReload(string AnOwnerID)
  {
    if (this._currentFiltration == null)
      return false;
    lock (this._currentFiltration)
      this._currentFiltration.Clear();
    this.RuleValid = false;
    this._ownerID = AnOwnerID;
    if (AnOwnerID != string.Empty && this._currentFiltration.OwnerID == AnOwnerID)
      return false;
    VersionsRule AValue = (VersionsRule) null;
    bool flag1 = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService)
      {
        if (this._latestVersionsRule == null || this._latestVersionsRule.Empty())
          this._latestVersionsRule = customService.LatestVersionsRule;
        if (this._allVersionsRule == null || this._allVersionsRule.Empty())
          this._allVersionsRule = customService.AllVersionsRule;
        if (this._baseVersionRule == null || this._baseVersionRule.Empty())
          this._baseVersionRule = customService.BaseVersionsRule;
        if (this._sequentialModificationsRule == null || this._sequentialModificationsRule.Empty())
          this._sequentialModificationsRule = customService.SequentialModificationsRule;
        if (this._defaultVersionRule == null || this._defaultVersionRule.Empty())
          this._defaultVersionRule = customService.GetDefaultVersionRule(sessionKeeper.Session.SessionGUID);
        if (this.FiltrationsCache != null && !this.FiltrationsCache.ContainsKey(AnOwnerID) || this._filtrationChanged)
        {
          this._currentFiltration = customService.GetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, AnOwnerID, true);
          if (this._currentFiltration != null && this._currentFiltration.Tags != null)
            this._currentFiltration.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) false;
          if (this._rulesCache.ContainsKey(AnOwnerID))
            this._rulesCache.Remove(AnOwnerID);
        }
        else
          this._currentFiltration = this.FiltrationsCache[AnOwnerID].Clone() as FiltrationSettings;
        flag1 = this._currentFiltration == null;
        if (flag1)
        {
          this._currentFiltration = customService.GetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, "cad005aa-306c-11d8-b4e9-00304f19f545", true);
          if (this._currentFiltration.Tags != null)
            this._currentFiltration.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) false;
        }
        if (this._currentFiltration != null && this._currentFiltration.OwnerID != null)
        {
          if (this.FiltrationsCache.ContainsKey(this._currentFiltration.OwnerID))
            this.FiltrationsCache.Remove(this._currentFiltration.OwnerID);
          this._currentFiltration.OwnerID = AnOwnerID;
          this.FiltrationsCache[this._currentFiltration.OwnerID] = this._currentFiltration.Clone() as FiltrationSettings;
        }
        if (this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.RuleObjectID != 0L)
        {
          bool flag2 = true;
          if (this._rulesCache.ContainsKey(this._currentFiltration.OwnerID))
          {
            MyElementEx myElementEx = this._rulesCache[this._currentFiltration.OwnerID];
            AValue = myElementEx.Value as VersionsRule;
            if (this._currentFiltration.CurrentRule != null && AValue.RuleObjectID == this._currentFiltration.CurrentRule.RuleObjectID && (AValue.HasVariableValues() && this._currentFiltration.CurrentRuleVars >= 0 || !AValue.HasVariableValues() && this._currentFiltration.CurrentRuleVars < 0))
            {
              this.RuleCompatible = myElementEx.ElementBool;
              this.RuleValid = myElementEx.ElementBool2;
              this._varsOutOfRange = myElementEx.ElementBool3;
              flag2 = false;
            }
          }
          if (flag2)
          {
            AValue = this._currentFiltration.CurrentRule;
            if (AValue == null || !this.RuleValid || this._varsOutOfRange)
              AValue = customService.GetFiltrationRule((object) sessionKeeper.Session.SessionGUID, (IFiltrationSettings) this._currentFiltration, ref this._ruleCompatible, ref this._ruleValid, ref this._varsOutOfRange);
            MyElementEx myElementEx = new MyElementEx((object) AValue, string.Empty, this.RuleCompatible, this.RuleValid, this._varsOutOfRange, 0L, 0, Guid.Empty, Array.Empty<object>());
            if (this._rulesCache.ContainsKey(this._currentFiltration.OwnerID))
              this._rulesCache.Remove(this._currentFiltration.OwnerID);
            this._rulesCache.Add(this._currentFiltration.OwnerID, myElementEx);
          }
          lock (this._currentFiltration)
            this._currentFiltration.CurrentRule = AValue;
        }
        if (this._currentFiltration.CurrentRule == null || this._currentFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtLatestVersionsRule)
        {
          lock (this._currentFiltration)
            this._currentFiltration.CurrentRule = customService.LatestVersionsRule;
          this.RuleCompatible = true;
          this.RuleValid = true;
          this._varsOutOfRange = false;
        }
        if (this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule)
        {
          lock (this._currentFiltration)
            this._currentFiltration.CurrentRule = customService.AllVersionsRule;
          this.RuleCompatible = true;
          this.RuleValid = true;
          this._varsOutOfRange = false;
        }
        if (this._currentFiltration.CurrentRule != null)
        {
          if (this._currentFiltration.CurrentRule.RuleObjectID == 0L)
          {
            this._currentFiltration.CurrentRule.Valid(sessionKeeper.Session);
            this.RuleCompatible = true;
            this.RuleValid = true;
            this._varsOutOfRange = false;
          }
        }
      }
    }
    this._ownerID = this._currentFiltration != null ? this._currentFiltration.OwnerID : string.Empty;
    this._filtrationChanged = flag1;
    return flag1;
  }

  /// <summary>
  /// Сохранить текущие настройки фильтрации состава в базу данных
  /// Автоматически будет вызвано событие, уведомляеющее о смене в настройках фильтрации состава.
  /// </summary>
  public void FiltrationSave()
  {
    if (this._currentFiltration.OwnerID != string.Empty && this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.CurrentRuleType != VersionsRuleType.vrtStandardRule)
    {
      this.RuleCompatible = true;
      this.RuleValid = true;
      this._varsOutOfRange = false;
      if (this._currentFiltration.CurrentRule == null || this._currentFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtLatestVersionsRule)
        this._currentFiltration.CurrentRule = this._latestVersionsRule.Clone() as VersionsRule;
      if (this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule)
        this._currentFiltration.CurrentRule = this._allVersionsRule.Clone() as VersionsRule;
      if (this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.RuleObjectGuid == "cad00601-306c-11d8-b4e9-00304f19f545")
        this._currentFiltration.CurrentRule = this._baseVersionRule.Clone() as VersionsRule;
      if (this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.RuleObjectGuid == "cad00602-306c-11d8-b4e9-00304f19f545")
        this._currentFiltration.CurrentRule = this._sequentialModificationsRule.Clone() as VersionsRule;
      if (this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.RuleObjectGuid == this._defaultVersionRule.RuleObjectGuid)
        this._currentFiltration.CurrentRule = this._defaultVersionRule.Clone() as VersionsRule;
    }
    if (this._filtrationChanged)
    {
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      if (this._currentFiltration.OwnerID != string.Empty)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService)
          {
            if (service.CachedEditingContextSource == EditingContextSource.WindowContext)
              this._currentFiltration.EditingContext = new CurrentEditingContext(service.CachedEditingContextID, service.CachedEditingContextModificationID, service.CachedContextMode);
            if (this._userLinked)
              customService.SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, this._currentFiltration.OwnerID, this._currentFiltration);
            this.FiltrationsCache[this._currentFiltration.OwnerID] = this._currentFiltration.Clone() as FiltrationSettings;
          }
        }
      }
      if (service.CachedEditingContextSource == EditingContextSource.WindowContext)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          sessionKeeper.Session.EditingContextSetData(sessionKeeper.Session.MasterSessionGUID, this._currentFiltration.OwnerID != string.Empty ? new CurrentEditingContext(service.CachedEditingContextID, service.CachedEditingContextModificationID, service.CachedContextMode) : CurrentEditingContext.Empty);
      }
    }
    this._filtrationChanged = false;
  }

  public void FiltrationApplyUpdates(bool fireEvent)
  {
    lock (this._syncRoot)
      this._filtrationChanged = true;
    this.FiltrationSave();
    ServiceUtils.GetService<IInvokeService>((object) ServicesManager.ServiceContainer, true).InvokeAction(-1, (Action) (() =>
    {
      if (fireEvent)
      {
        lock (this._syncRoot)
        {
          if (this._lockCount <= 0)
          {
            FiltrationChanged filtrationChanged = this.OnFiltrationChanged;
            if (filtrationChanged != null)
              filtrationChanged((IFiltrationSettings) this._currentFiltration, this.RuleValid && this.RuleCompatible);
          }
        }
      }
      this.FillFilterToolbar();
    }));
  }

  /// <summary>
  /// Событие вызывается после смены настроек фильтрации состава на новое значение
  /// </summary>
  public event FiltrationChanged OnFiltrationChanged;

  /// <summary>Начать обновления в сервисе</summary>
  public void BeginUpdates()
  {
    lock (this._syncRoot)
      ++this._lockCount;
  }

  /// <summary>Завершить обновления в сервисе</summary>
  public void EndUpdates()
  {
    lock (this._syncRoot)
      --this._lockCount;
  }

  /// <summary>
  /// Свойство позволяет разрешить или запретить пользователю вносить изменения в настройки фильтрации
  /// </summary>
  bool IFiltrationService.Enabled
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this._filtrationEnabled;
    }
    set
    {
      lock (this._syncRoot)
      {
        if (this._filtrationEnabled == value)
          return;
        this._filtrationEnabled = value;
      }
      this.UpdateFilterToolbarControls();
    }
  }

  /// <summary>
  /// Текущее правило фильтрации состава, информация по которому отображена в тулбаре "Фильтрация состава"
  /// Причём если у правила есть переменные, то они уже заданы согласно текущей настройке фильтрации
  /// Автоматически будет вызвано событие, уведомляеющее о смене в настройках фильтрации состава.
  /// </summary>
  public VersionsRule RuleClass
  {
    get
    {
      lock (this._syncRoot)
      {
        if (this._currentFiltration.CurrentRule != null)
          return this._currentFiltration.CurrentRule.Clone() as VersionsRule;
        if (this._currentFiltration.CurrentRule != null && this._currentFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule)
          return this._allVersionsRule.Clone() as VersionsRule;
        if (this._defaultVersionRule.Empty())
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService)
              this._defaultVersionRule.Assign((object) customService.GetDefaultVersionRule(sessionKeeper.Session.SessionGUID));
          }
        }
        return this._defaultVersionRule.Clone() as VersionsRule;
      }
    }
    set
    {
      if (value == null)
        return;
      lock (this._syncRoot)
      {
        this._currentFiltration.CurrentRule = value.Clone() as VersionsRule;
        if (this._currentFiltration.CurrentRule.RuleObjectID == 0L)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            this._currentFiltration.CurrentRule.ConvertVarsToConsts(sessionKeeper.Session);
        }
        this._filtrationChanged = true;
      }
      this.FiltrationSave();
      this.FiltrationReload(this._currentFiltration.OwnerID);
      lock (this._syncRoot)
      {
        if (this._lockCount <= 0)
          this.Fire_OnFiltrationChanged((object) this, new OnFiltrationChangedEventArgs((IFiltrationSettings) this._currentFiltration, this.RuleValid && this.RuleCompatible));
      }
      this.FillFilterToolbar();
    }
  }

  /// <summary>
  /// Произошла смена настроек фильтрации состава на новое значение. Метод вызывает событие OnFiltrationChanged.
  /// </summary>
  /// <param name="NewFiltration">Ссылка на интерфейс самих настроек фильтрации состава</param>
  /// <param name="FiltrationValid">Являются ли указанные настройки фильтрации состава корректными (можно ли их использовать или нет)</param>
  private void Fire_OnFiltrationChanged(object sender, OnFiltrationChangedEventArgs e)
  {
    if (this.OnFiltrationChanged == null)
      return;
    this.OnFiltrationChanged(e.NewFiltration, e.FiltrationValid);
  }

  /// <summary>
  /// Если выбранное правило является вариантом значений переменных (т.е. создано на базе родительского правила),
  /// то это поле отражает, совместимо ли правило с родительским вариантом (на случай, если были изменения
  /// в родительском правиле после создания вариантов его значений переменных)
  /// </summary>
  public bool RuleCompatible
  {
    get
    {
      lock (this._syncRoot)
        return this._ruleCompatible;
    }
    private set => this._ruleCompatible = value;
  }

  /// <summary>
  /// Валидно ли выбранное правило подбора версий
  /// (для проверки выполняется метод Valid правила, а также проверяется наличие у него переменных значений)
  /// Если this.RuleValid = false, правило применять нельзя
  /// </summary>
  public bool RuleValid
  {
    get
    {
      lock (this._syncRoot)
        return this._ruleValid;
    }
    private set => this._ruleValid = value;
  }

  /// <summary>
  /// Код ошибки для текущего правила:
  /// 0 - правило не выбрано,
  /// 1 - настройки недействительны - правило было изменено,
  /// 2 - нет ошибок, правило настроено,
  /// 3 - нет вариантов значений переменных для правила,
  /// 4 - фильтрация состава выключена (obsolete),
  /// 5 - не указан основной вариант значений переменных,
  /// 6 - правило является некорректным
  /// </summary>
  public CurrentRuleErrors RuleErrorCode
  {
    get
    {
      lock (this._syncRoot)
        return this._errorCode;
    }
  }

  /// <summary>
  /// Свойство, определяющее видимость тулбара "Фильтрация состава"
  /// </summary>
  public bool FiltrationToolbarVisible
  {
    get
    {
      lock (this._syncRoot)
        return this.ToolBar.Visible;
    }
    set
    {
      lock (this._syncRoot)
        this.ToolBar.Visible = value;
    }
  }

  /// <summary>
  /// Свойство, определяющее "скрытость" тулбара "Фильтрация состава"
  /// </summary>
  public bool FiltrationToolbarHidden
  {
    get
    {
      lock (this._syncRoot)
        return this.ToolBar.Hidden;
    }
    set
    {
      lock (this._syncRoot)
        this.ToolBar.Hidden = value;
    }
  }

  /// <summary>
  /// OBJECT_ID текущего правила подбора версий.
  /// Автоматически будет вызвано событие, уведомляеющее о смене в настройках фильтрации состава.
  /// </summary>
  public long FiltrationRuleID
  {
    get
    {
      lock (this._syncRoot)
        return this._currentFiltration.CurrentRule != null ? this._currentFiltration.CurrentRule.RuleObjectID : 0L;
    }
  }

  /// <summary>
  /// Уникальный ID владельца текущих настроек фильтрации
  /// Автоматически будет вызвано событие, уведомляеющее о смене в настройках фильтрации состава.
  /// </summary>
  public string FiltrationServiceOwnerID
  {
    get
    {
      lock (this._syncRoot)
        return this._ownerID;
    }
    set
    {
      this.FiltrationReload(value);
      this.FiltrationApplyUpdates(false);
      lock (this._syncRoot)
      {
        if (this._lockCount > 0)
          return;
        this.Fire_OnFiltrationChanged((object) this, new OnFiltrationChangedEventArgs((IFiltrationSettings) this._currentFiltration, this.RuleValid && this.RuleCompatible));
      }
    }
  }

  /// <summary>
  /// Текущие настройки фильтрации состава из ComboBox
  /// Автоматически будет вызвано событие, уведомляющее о смене в настройках фильтрации состава.
  /// </summary>
  public IFiltrationSettings Filtration
  {
    get
    {
      lock (this._syncRoot)
        return (IFiltrationSettings) this._currentFiltration;
    }
  }

  public Intermech.Bars.ToolBar ToolBar { get; }

  /// <summary>Добавить новую кнопку на панель "Фильтрация состава"</summary>
  /// <returns></returns>
  public ButtonItem AddNewButton()
  {
    ButtonItem buttonItem = new ButtonItem();
    this.ToolBar.Items.Add((ToolbarItemBase) buttonItem);
    return buttonItem;
  }

  /// <summary>
  /// Добавить новый выпадающий список на панель "Фильтрация состава"
  /// </summary>
  /// <returns></returns>
  public ComboBoxItem AddNewCombobox()
  {
    ComboBoxItem comboBoxItem = new ComboBoxItem();
    this.ToolBar.Items.Add((ToolbarItemBase) comboBoxItem);
    return comboBoxItem;
  }
}
