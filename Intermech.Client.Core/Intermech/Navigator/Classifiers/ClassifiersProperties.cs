
// Type: Intermech.Navigator.Classifiers.ClassifiersProperties
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Localization;
using System.ComponentModel;
using System.Diagnostics;


namespace Intermech.Navigator.Classifiers;

/// <summary>Общие настройки</summary>
internal sealed class ClassifiersProperties
{
  /// <summary>
  /// При расчете значения атрибута по формуле классификатора не учитывать объекты, полученные с портала.
  /// </summary>
  private bool _localOnly;
  /// <summary>
  /// При включении объектов типа изделия в ручную выборку, принадлежащую архивам или типам объектов, являющихся документами, включать в эту выборку документы, включенные в состав этих изделий
  /// </summary>
  private bool _documentsIncludeIntoHandleSelection;
  private bool _saveSelectionConditionState;
  private bool _setProjectID;
  private bool _multiSelectClassifier;

  public void ApplyUpdates()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      configurations.WriteBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.LocalOnlyParamName, this._localOnly, 0L);
      configurations.WriteBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.DocumentsIncludeIntoHandleSelectionParamName, this._documentsIncludeIntoHandleSelection, 0L);
      configurations.WriteBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.SaveSelectionConditionStateParamName, this._saveSelectionConditionState, 0L);
      configurations.WriteBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.SetProjectIDParamName, this._setProjectID, 0L);
      configurations.WriteBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.MultiSelectClassifierParamName, this._multiSelectClassifier, 0L);
    }
  }

  public void LoadCurrentValues()
  {
    IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    this._localOnly = service.ReadBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.LocalOnlyParamName, false, DBConfigMode.GlobalOnly);
    this._documentsIncludeIntoHandleSelection = service.ReadBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.DocumentsIncludeIntoHandleSelectionParamName, false, DBConfigMode.GlobalOnly);
    this._saveSelectionConditionState = service.ReadBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.SaveSelectionConditionStateParamName, false, DBConfigMode.GlobalOnly);
    this._setProjectID = service.ReadBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.SetProjectIDParamName, false, DBConfigMode.GlobalOnly);
    this._multiSelectClassifier = service.ReadBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.MultiSelectClassifierParamName, false, DBConfigMode.GlobalOnly);
  }

  private void CheckInited()
  {
    if (this.Inited)
      return;
    this.LoadCurrentValues();
    this.Inited = true;
  }

  [CustomDescription("Attribute.Client.Core_295")]
  [CustomDisplayName("Attribute.Client.Core_294")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  public bool LocalOnly
  {
    [DebuggerStepThrough] get
    {
      this.CheckInited();
      return this._localOnly;
    }
    set => this._localOnly = value;
  }

  [CustomDescription("Attribute.Client.Core_297")]
  [CustomDisplayName("Attribute.Client.Core_298")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  public bool DocumentsIncludeIntoHandleSelection
  {
    [DebuggerStepThrough] get
    {
      this.CheckInited();
      return this._documentsIncludeIntoHandleSelection;
    }
    set => this._documentsIncludeIntoHandleSelection = value;
  }

  [CustomDescription("Attribute.Client.Core_319")]
  [CustomDisplayName("Attribute.Client.Core_318")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  public bool SaveSelectionConditionState
  {
    [DebuggerStepThrough] get
    {
      this.CheckInited();
      return this._saveSelectionConditionState;
    }
    set => this._saveSelectionConditionState = value;
  }

  [CustomDescription("Attribute.Client.Core_321")]
  [CustomDisplayName("Attribute.Client.Core_320")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  public bool SetProjectID
  {
    [DebuggerStepThrough] get
    {
      this.CheckInited();
      return this._setProjectID;
    }
    set => this._setProjectID = value;
  }

  [Description("Настройка позволяет классифицировать объект сразу несколькими классификаторами.")]
  [DisplayName("Множественный выбор папок классификатора")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  public bool MultiSelectClassifier
  {
    [DebuggerStepThrough] get
    {
      this.CheckInited();
      return this._multiSelectClassifier;
    }
    set => this._multiSelectClassifier = value;
  }

  [Browsable(false)]
  public bool Inited { [DebuggerStepThrough] get; set; }
}
