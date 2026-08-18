
// Type: Intermech.Search.Configuration.Configuration
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Protection;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.Configuration;

public sealed class Configuration : ICustomTypeDescriptor
{
  private LazyService<IConfigurationOptionRepository> _configurationOptionRepository = new LazyService<IConfigurationOptionRepository>();
  private Dictionary<ConfigurationOptionKey, object> _optionValueDictionary = new Dictionary<ConfigurationOptionKey, object>();
  private Dictionary<ConfigurationOptionKey, object> _optionValueBackupDictionary = new Dictionary<ConfigurationOptionKey, object>();
  private Dictionary<ConfigurationOptionKey, ConfigurationOptionInfo> _optionInfoDictionary = new Dictionary<ConfigurationOptionKey, ConfigurationOptionInfo>();
  private System.ComponentModel.AttributeCollection _attributeCollection = new System.ComponentModel.AttributeCollection(Array.Empty<Attribute>());
  private EventDescriptorCollection _eventDescriptorCollection = new EventDescriptorCollection(new EventDescriptor[0]);
  private PropertyDescriptorCollection _propertyDescriptorCollection;

  public static Intermech.Search.Configuration.Configuration Load(
    List<ConfigurationOptionInfo> optionsInfo)
  {
    Intermech.Search.Configuration.Configuration configuration = optionsInfo != null ? new Intermech.Search.Configuration.Configuration(optionsInfo) : throw new ArgumentNullException(nameof (optionsInfo));
    IConfigurationOptionRepository optionRepository = ServiceLocator.Get<IConfigurationOptionRepository>();
    foreach (ConfigurationOptionInfo configurationOptionInfo in optionsInfo)
    {
      object obj = configurationOptionInfo.CustomGetHandler != null ? configurationOptionInfo.CustomGetHandler() : optionRepository.Find(configurationOptionInfo.Key);
      configuration._optionValueDictionary.Add(configurationOptionInfo.Key, obj);
      configuration._optionValueBackupDictionary.Add(configurationOptionInfo.Key, obj);
    }
    return configuration;
  }

  private Configuration(List<ConfigurationOptionInfo> optionsInfo)
  {
    this._optionInfoDictionary = optionsInfo != null ? this.CreateOptionInfoDictionary(optionsInfo) : throw new ArgumentNullException(nameof (optionsInfo));
    this._propertyDescriptorCollection = this.CreatePropertyDescriptorCollection();
    this._configurationOptionRepository.Value.OptionChanged += new EventHandler<ConfigurationOptionChangedEventArgs>(this.ConfigurationOptionRepository_OptionChanged);
  }

  public event EventHandler Changed;

  public List<ConfigurationOptionInfo> OptionsInfo
  {
    get => this._optionInfoDictionary.Values.ToList<ConfigurationOptionInfo>();
  }

  public bool IsChanged { get; private set; }

  public bool ShouldSerializeValue(ConfigurationOptionKey optionKey)
  {
    if (optionKey == (ConfigurationOptionKey) null)
      throw new ArgumentNullException(nameof (optionKey));
    if (!this._optionInfoDictionary.ContainsKey(optionKey))
      throw new ArgumentException();
    return !object.Equals(this._optionValueDictionary[optionKey], this._optionValueBackupDictionary[optionKey]);
  }

  public object GetValue(ConfigurationOptionKey optionKey)
  {
    if (optionKey == (ConfigurationOptionKey) null)
      throw new ArgumentNullException(nameof (optionKey));
    return this._optionInfoDictionary.ContainsKey(optionKey) ? this._optionValueDictionary[optionKey] : throw new ArgumentException();
  }

  public void ResetValue(ConfigurationOptionKey optionKey)
  {
    if (optionKey == (ConfigurationOptionKey) null)
      throw new ArgumentNullException(nameof (optionKey));
    this._optionValueDictionary[optionKey] = this._optionInfoDictionary.ContainsKey(optionKey) ? this._optionValueBackupDictionary[optionKey] : throw new ArgumentException();
    this.OnChanged();
  }

  public void SetValue(ConfigurationOptionKey optionKey, object value)
  {
    if (optionKey == (ConfigurationOptionKey) null)
      throw new ArgumentNullException(nameof (optionKey));
    if (!this._optionInfoDictionary.ContainsKey(optionKey))
      throw new ArgumentException();
    if (this._optionInfoDictionary[optionKey].RequestAdminRights && !this.RequestAdminRights())
      return;
    this._optionValueDictionary[optionKey] = value;
    this.OnChanged();
  }

  public void ApplyChanges()
  {
    if (!this.IsChanged)
      return;
    IConfigurationOptionRepository optionRepository = ServiceLocator.Get<IConfigurationOptionRepository>();
    foreach (ConfigurationOptionInfo configurationOptionInfo in this._optionInfoDictionary.Values)
    {
      object optionValue = this._optionValueDictionary[configurationOptionInfo.Key];
      if (configurationOptionInfo.CustomSetHandler != null)
        configurationOptionInfo.CustomSetHandler(optionValue);
      else
        optionRepository.AddOrUpdate(configurationOptionInfo.Key, optionValue);
    }
    ServiceLocator.Get<INotificationService>().FireEvent((object) this, new NotificationEventArgs("ProjectChanged"));
    this.IsChanged = false;
  }

  public void CancelChanges()
  {
    if (!this.IsChanged)
      return;
    foreach (ConfigurationOptionInfo configurationOptionInfo in this._optionInfoDictionary.Values)
      this._optionValueDictionary[configurationOptionInfo.Key] = this._optionValueBackupDictionary[configurationOptionInfo.Key];
    this.IsChanged = false;
  }

  System.ComponentModel.AttributeCollection ICustomTypeDescriptor.GetAttributes()
  {
    return this._attributeCollection;
  }

  string ICustomTypeDescriptor.GetClassName() => this.GetType().Name;

  string ICustomTypeDescriptor.GetComponentName() => this.GetType().Name;

  TypeConverter ICustomTypeDescriptor.GetConverter() => (TypeConverter) null;

  EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() => (EventDescriptor) null;

  System.ComponentModel.PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
  {
    return (System.ComponentModel.PropertyDescriptor) null;
  }

  object ICustomTypeDescriptor.GetEditor(System.Type editorBaseType) => (object) null;

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
  {
    return this._eventDescriptorCollection;
  }

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => this._eventDescriptorCollection;

  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
  {
    return this._propertyDescriptorCollection;
  }

  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
  {
    return this._propertyDescriptorCollection;
  }

  object ICustomTypeDescriptor.GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
  {
    return (object) this;
  }

  private void ConfigurationOptionRepository_OptionChanged(
    object sender,
    ConfigurationOptionChangedEventArgs e)
  {
    if (!this._optionInfoDictionary.ContainsKey(e.OptionKey))
      return;
    this._optionValueDictionary[e.OptionKey] = e.NewValue;
    this._optionValueBackupDictionary[e.OptionKey] = e.NewValue;
    this.OnChanged();
  }

  private Dictionary<ConfigurationOptionKey, ConfigurationOptionInfo> CreateOptionInfoDictionary(
    List<ConfigurationOptionInfo> optionsInfo)
  {
    return optionsInfo.ToDictionary<ConfigurationOptionInfo, ConfigurationOptionKey>((System.Func<ConfigurationOptionInfo, ConfigurationOptionKey>) (o => o.Key));
  }

  private Dictionary<ConfigurationOptionKey, object> CreateOptionValueDictionary(
    List<ConfigurationOptionInfo> optionsInfo)
  {
    return optionsInfo.ToDictionary<ConfigurationOptionInfo, ConfigurationOptionKey, object>((System.Func<ConfigurationOptionInfo, ConfigurationOptionKey>) (o => o.Key), (System.Func<ConfigurationOptionInfo, object>) (o => o.DefaultValue));
  }

  private PropertyDescriptorCollection CreatePropertyDescriptorCollection()
  {
    List<System.ComponentModel.PropertyDescriptor> propertyDescriptorList = new List<System.ComponentModel.PropertyDescriptor>();
    foreach (ConfigurationOptionInfo optionInfo in this._optionInfoDictionary.Values)
    {
      List<Attribute> attributesForOptionInfo = this.CreateAttributesForOptionInfo(optionInfo);
      ConfigurationOptionPropertyDescriptor propertyDescriptor = new ConfigurationOptionPropertyDescriptor(optionInfo, attributesForOptionInfo.ToArray());
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }

  private List<Attribute> CreateAttributesForOptionInfo(ConfigurationOptionInfo optionInfo)
  {
    List<Attribute> attributesForOptionInfo = new List<Attribute>();
    if (optionInfo.Category != null)
      attributesForOptionInfo.Add((Attribute) new CategoryAttribute(optionInfo.Category));
    if (optionInfo.DefaultValue != null)
      attributesForOptionInfo.Add((Attribute) new DefaultValueAttribute(optionInfo.DefaultValue));
    string displayName = optionInfo.DisplayName ?? optionInfo.Key.Name;
    attributesForOptionInfo.Add((Attribute) new DisplayNameAttribute(displayName));
    string description = optionInfo.Description ?? displayName;
    if (optionInfo.CheckAdmin && !this.IsAdmin())
      description += " (Для изменения настройки нужны права администратора)";
    attributesForOptionInfo.Add((Attribute) new DescriptionAttribute(description));
    if (optionInfo.Editor != (System.Type) null)
      attributesForOptionInfo.Add((Attribute) new EditorAttribute(optionInfo.Editor, typeof (UITypeEditor)));
    if (optionInfo.TypeConverter != (System.Type) null)
      attributesForOptionInfo.Add((Attribute) new TypeConverterAttribute(optionInfo.TypeConverter));
    return attributesForOptionInfo;
  }

  private bool IsAdmin() => ServiceLocator.Get<ICurrentUserAndRole>().IsAdmin;

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed != null)
      changed((object) this, new EventArgs());
    this.IsChanged = true;
  }

  private bool RequestAdminRights()
  {
    if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service)
    {
      if (service.IsAdmin)
        return true;
      using (LoginPasswordForm loginPasswordForm = new LoginPasswordForm())
      {
        loginPasswordForm.Text = "Введите логин и пароль администратора";
        if (loginPasswordForm.ShowDialog() == DialogResult.OK)
        {
          if (!string.IsNullOrEmpty(loginPasswordForm.Login))
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              DBRecordSetParams dbRecordSetParams1 = new DBRecordSetParams();
              dbRecordSetParams1.Columns = new object[2]
              {
                (object) ObligatoryObjectAttributes.F_ID,
                (object) MetaDataHelper.GetAttributeTypeID("cad00019-306c-11d8-b4e9-00304f19f545")
              };
              ref DBRecordSetParams local1 = ref dbRecordSetParams1;
              ConditionStructure[] conditionStructureArray1 = new ConditionStructure[2];
              ConditionStructure conditionStructure = new ConditionStructure();
              conditionStructure.Attribute = (object) MetaDataHelper.GetAttributeTypeID("cad00018-306c-11d8-b4e9-00304f19f545");
              conditionStructure.RelationalOperator = RelationalOperators.Equal;
              conditionStructure.Value = (object) loginPasswordForm.Login.ToUpperInvariant();
              conditionStructure.SQL = string.Empty;
              conditionStructureArray1[0] = conditionStructure;
              conditionStructure = new ConditionStructure();
              conditionStructure.Attribute = (object) MetaDataHelper.GetAttributeTypeID("cad00018-306c-11d8-b4e9-00304f19f545");
              conditionStructure.RelationalOperator = RelationalOperators.Equal;
              conditionStructure.Value = (object) loginPasswordForm.Login.ToLowerInvariant();
              conditionStructure.LogicalOperator = LogicalOperators.OR;
              conditionStructure.SQL = string.Empty;
              conditionStructureArray1[1] = conditionStructure;
              local1.Conditions = conditionStructureArray1;
              dbRecordSetParams1.RecordCount = -1;
              DBRecordSetParams dbRecordSetParams2 = dbRecordSetParams1;
              DataTable dataTable = sessionKeeper.Session.ObjectsSelect(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"), dbRecordSetParams2);
              if (dataTable.Rows.Count > 0)
              {
                long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[0], 0, 0L);
                string stringValue = DataSetProcessor.GetStringValue(dataTable.Rows[0], 1, string.Empty);
                if (!ObjectHelper.IsUnknownObjectID(int64Value))
                {
                  if (CryptHelper.IsPasswordEqual(loginPasswordForm.Password, stringValue))
                  {
                    IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00022-306c-11d8-b4e9-00304f19f545"));
                    relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cad00007-306c-11d8-b4e9-00304f19f545");
                    dbRecordSetParams1 = new DBRecordSetParams();
                    dbRecordSetParams1.Columns = new object[1]
                    {
                      (object) ObligatoryObjectAttributes.F_PROJ_ID
                    };
                    ref DBRecordSetParams local2 = ref dbRecordSetParams1;
                    ConditionStructure[] conditionStructureArray2 = new ConditionStructure[1];
                    conditionStructure = new ConditionStructure();
                    conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_PROJ_ID;
                    conditionStructure.RelationalOperator = RelationalOperators.Equal;
                    conditionStructure.Value = (object) sessionKeeper.Session.IdentHelper.AdminRoleID;
                    conditionStructure.SQL = string.Empty;
                    conditionStructureArray2[0] = conditionStructure;
                    local2.Conditions = conditionStructureArray2;
                    DBRecordSetParams paramSet = dbRecordSetParams1;
                    if (relationCollection.EntersIn(paramSet, int64Value).Rows.Count > 0)
                      return true;
                  }
                }
              }
            }
          }
        }
      }
    }
    return false;
  }
}
