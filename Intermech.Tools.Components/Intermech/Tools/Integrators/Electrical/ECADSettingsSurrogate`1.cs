// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ECADSettingsSurrogate`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.ChangeHighlighting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public abstract class ECADSettingsSurrogate<TSettings> : ICloneable where TSettings : ECADIntegratorSettings, new()
{
  protected ChangeTrackingListAdapter<AttributeTableItemSurrogate> relationPartAttributesTable;
  protected ChangeTrackingListAdapter<AttributeTableItemSurrogate> partAttributesTable;
  protected ChangeTrackingListAdapter<AttributeTableItemSurrogate> assemblyAttributesTable;
  protected ChangeTrackingListAdapter<AttributeTableItemSurrogate> documentAttributesTable;
  protected ChangeTrackingListAdapter<ParameterValuePairSurrogate> tuningParameters;
  protected ChangeTrackingListAdapter<ParameterValuePairSurrogate> replaceParameters;
  protected ChangeTrackingListAdapter<FolderNameSurrogate> notImportingDir;
  protected string nominalsParameter;
  protected string fgName;
  protected string fgDesignation;
  protected string asPosDesignation;
  protected GlobalId<int> imbaseSyncAttribute;
  protected bool imbaseSync;
  protected bool imbaseSyncCheckApplicability;

  public ECADSettingsSurrogate(TSettings settings)
  {
    this.notImportingDir = settings.NotImportingDir != null ? new ChangeTrackingListAdapter<FolderNameSurrogate>((IEnumerable<FolderNameSurrogate>) settings.NotImportingDir.ConvertAll<FolderNameSurrogate>((Converter<string, FolderNameSurrogate>) (folder => new FolderNameSurrogate()
    {
      FolderName = folder
    }))) : new ChangeTrackingListAdapter<FolderNameSurrogate>();
    this.partAttributesTable = this.CloneList(settings.PartAttributesTable);
    this.relationPartAttributesTable = this.CloneList(settings.RelationPartAttributesTable);
    this.assemblyAttributesTable = this.CloneList(settings.AssemblyAttributesTable);
    this.documentAttributesTable = this.CloneList(settings.DocumentAttributesTable);
    this.imbaseSync = settings.ImbaseSync;
    this.imbaseSyncCheckApplicability = settings.ImbaseSyncCheckApplicability;
    this.imbaseSyncAttribute = settings.ImbaseSyncAttribute ?? new GlobalId<int>(Guid.Empty, 0, "Не задан");
    this.nominalsParameter = settings.NominalsParameter;
    this.tuningParameters = this.CloneList2(settings.TuningParameters);
    this.replaceParameters = this.CloneList2(settings.ReplaceParameters);
    this.fgName = settings.FGName;
    this.fgDesignation = settings.FGDesignation;
    this.asPosDesignation = settings.ASPosDesignation;
  }

  /// <summary>
  /// Выполняет преобразования объекта суррогата в контейнер настроек интегратора. Этот метод
  /// используется при завершении работы редактора настроек интегратора.
  /// </summary>
  /// <returns></returns>
  protected virtual void SaveSettings(TSettings settings)
  {
    settings.NotImportingDir = new List<string>(this.notImportingDir.Count<FolderNameSurrogate>());
    foreach (FolderNameSurrogate folderNameSurrogate in this.notImportingDir)
      settings.NotImportingDir.Add(folderNameSurrogate.FolderName);
    settings.PartAttributesTable = this.RevertCloneList(this.partAttributesTable);
    settings.RelationPartAttributesTable = this.RevertCloneList(this.relationPartAttributesTable);
    settings.AssemblyAttributesTable = this.RevertCloneList(this.assemblyAttributesTable);
    settings.DocumentAttributesTable = this.RevertCloneList(this.documentAttributesTable);
    settings.ImbaseSync = this.imbaseSync;
    settings.ImbaseSyncCheckApplicability = this.imbaseSyncCheckApplicability;
    settings.ImbaseSyncAttribute = this.imbaseSyncAttribute;
    settings.NominalsParameter = this.nominalsParameter;
    settings.ReplaceParameters = this.RevertCloneList2(this.replaceParameters);
    settings.TuningParameters = this.RevertCloneList2(this.tuningParameters);
    settings.FGName = this.fgName;
    settings.FGDesignation = this.fgDesignation;
    settings.ASPosDesignation = this.asPosDesignation;
  }

  [Browsable(false)]
  public TSettings Settings
  {
    get
    {
      TSettings settings = new TSettings();
      this.SaveSettings(settings);
      return settings;
    }
  }

  protected ChangeTrackingListAdapter<AttributeTableItemSurrogate> CloneList(
    List<Tuple<StringKey, StringKey, bool>> source)
  {
    return source == null ? new ChangeTrackingListAdapter<AttributeTableItemSurrogate>() : new ChangeTrackingListAdapter<AttributeTableItemSurrogate>((IEnumerable<AttributeTableItemSurrogate>) source.ConvertAll<AttributeTableItemSurrogate>((Converter<Tuple<StringKey, StringKey, bool>, AttributeTableItemSurrogate>) (item => new AttributeTableItemSurrogate()
    {
      DBAttributeName = (string) item.Item1,
      CADAttributeName = (string) item.Item2,
      Obligatory = item.Item3
    })));
  }

  private ChangeTrackingListAdapter<ParameterValuePairSurrogate> CloneList2(
    List<Tuple<StringKey, StringKey>> source)
  {
    return source == null ? new ChangeTrackingListAdapter<ParameterValuePairSurrogate>() : new ChangeTrackingListAdapter<ParameterValuePairSurrogate>((IEnumerable<ParameterValuePairSurrogate>) source.ConvertAll<ParameterValuePairSurrogate>((Converter<Tuple<StringKey, StringKey>, ParameterValuePairSurrogate>) (item => new ParameterValuePairSurrogate()
    {
      ParameterName = (string) item.Item1,
      ParameterValue = (string) item.Item2
    })));
  }

  protected List<Tuple<StringKey, StringKey, bool>> RevertCloneList(
    ChangeTrackingListAdapter<AttributeTableItemSurrogate> source)
  {
    if (source == null)
      return (List<Tuple<StringKey, StringKey, bool>>) null;
    List<Tuple<StringKey, StringKey, bool>> tupleList = new List<Tuple<StringKey, StringKey, bool>>(source.Items.Count);
    foreach (AttributeTableItemSurrogate tableItemSurrogate in source)
      tupleList.Add(new Tuple<StringKey, StringKey, bool>(new StringKey(tableItemSurrogate.DBAttributeName), new StringKey(tableItemSurrogate.CADAttributeName), tableItemSurrogate.Obligatory));
    return tupleList;
  }

  private List<Tuple<StringKey, StringKey>> RevertCloneList2(
    ChangeTrackingListAdapter<ParameterValuePairSurrogate> source)
  {
    if (source == null)
      return (List<Tuple<StringKey, StringKey>>) null;
    List<Tuple<StringKey, StringKey>> tupleList = new List<Tuple<StringKey, StringKey>>(source.Items.Count);
    foreach (ParameterValuePairSurrogate valuePairSurrogate in source)
      tupleList.Add(new Tuple<StringKey, StringKey>(new StringKey(valuePairSurrogate.ParameterName), new StringKey(valuePairSurrogate.ParameterValue)));
    return tupleList;
  }

  [Category("Настройки импорта файлов проекта")]
  [DisplayName("Не импортируемые папки")]
  [Description("В этом списке перечислены папки, которые не будут импортированы в базу данных IPS. (различные временные папки и папки с бэкапами)")]
  [Editor(typeof (ListFoldersUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<FolderNameSurrogate> NotImportingDir
  {
    get => this.notImportingDir;
    set => this.notImportingDir = value;
  }

  [Category("Настройки синхронизируемых атрибутов")]
  [DisplayName("Атрибуты элемента")]
  [Description("Это свойство позволяет задать синхронизируемые атрибуты для элемента.")]
  [Editor(typeof (TableAttributesUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<AttributeTableItemSurrogate> PartAttributesTable
  {
    get => this.partAttributesTable;
    set => this.partAttributesTable = value;
  }

  [Category("Настройки синхронизируемых атрибутов")]
  [DisplayName("Атрибуты сборочной единицы")]
  [Description("Это свойство позволяет задать синхронизируемые атрибуты для сборочной единицы.")]
  [Editor(typeof (TableAttributesUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<AttributeTableItemSurrogate> AssemblyAttributesTable
  {
    get => this.assemblyAttributesTable;
    set => this.assemblyAttributesTable = value;
  }

  [Category("Настройки синхронизируемых атрибутов")]
  [DisplayName("Атрибуты документа")]
  [Description("Это свойство позволяет задать синхронизируемые атрибуты для документа.")]
  [Editor(typeof (TableAttributesUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<AttributeTableItemSurrogate> DocumentAttributesTable
  {
    get => this.documentAttributesTable;
    set => this.documentAttributesTable = value;
  }

  [Category("Синхронизация с Imbase")]
  [DisplayName("Проводить синхронизацию")]
  [Description("Обязательная синхронизация с базой Imbase при расширенном сохранении.")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool ImbaseSync
  {
    get => this.imbaseSync;
    set => this.imbaseSync = value;
  }

  [Category("Синхронизация с Imbase")]
  [DisplayName("Проверка применяемости")]
  [Description("Проверка применяемости изделия")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool ImbaseSyncCheckApplicability
  {
    get => this.imbaseSyncCheckApplicability;
    set => this.imbaseSyncCheckApplicability = value;
  }

  [Category("Синхронизация с Imbase")]
  [DisplayName("Атрибут для синхронизации")]
  [Description("Атрибут изделия, по которому будет происходить синхронизация с Imbase.")]
  [Editor(typeof (ImbaseSyncAttributeEditor), typeof (UITypeEditor))]
  public GlobalId<int> ImbaseSyncAttribute
  {
    get => this.imbaseSyncAttribute;
    set => this.imbaseSyncAttribute = value;
  }

  [Category("Настройки идентифицирующих атрибутов")]
  [DisplayName("Компонент подбирается при регулировании")]
  [Description("Параметр компонента схемы и его значение, при котором компонент в IPS идентифицируется как основной для подбора")]
  [Editor(typeof (ParameterValuePairUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<ParameterValuePairSurrogate> TuningParameters
  {
    get => this.tuningParameters;
    set => this.tuningParameters = value;
  }

  [Category("Настройки идентифицирующих атрибутов")]
  [DisplayName("Допускается замена компонента")]
  [Description("Параметр компонента схемы и его значение, при котором компонент в IPS идентифицируется как имеющий доп.замены")]
  [Editor(typeof (ParameterValuePairUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<ParameterValuePairSurrogate> ReplaceParameters
  {
    get => this.replaceParameters;
    set => this.replaceParameters = value;
  }

  [Category("Настройки идентифицирующих атрибутов")]
  [DisplayName("Предельные значения")]
  [Description("Наименование параметра компонента, в котором приведены предельные значения для выполнения подбора")]
  public string NominalsParameter
  {
    get => this.nominalsParameter;
    set => this.nominalsParameter = value;
  }

  [Category("Настройки функциональных групп")]
  [DisplayName("Наименование функциональной группы")]
  [Description("Наименование параметра штампа в котором указано наименование функциональной группы")]
  public string FGName
  {
    get => this.fgName;
    set => this.fgName = value;
  }

  [Category("Настройки функциональных групп")]
  [DisplayName("Обозначение функциональной группы")]
  [Description("Наименование параметра штампа в котором указано обозначение функциональной группы")]
  public string FGDesignation
  {
    get => this.fgDesignation;
    set => this.fgDesignation = value;
  }

  [Category("Общие")]
  [DisplayName("Позиционное обозначение ДС")]
  [Description("Наименование параметра в котором указано Позиционное обозначение ДС")]
  public string ASPosDesignation
  {
    get => this.asPosDesignation;
    set => this.asPosDesignation = value;
  }

  [Category("Настройки синхронизируемых атрибутов")]
  [DisplayName("Атрибуты связи с элементом")]
  [Description("Это свойство позволяет задать синхронизируемые атрибуты для связи с элементом.")]
  [Editor(typeof (TableAttributesUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<AttributeTableItemSurrogate> RelationPartAttributesTable
  {
    get => this.relationPartAttributesTable;
    set => this.relationPartAttributesTable = value;
  }

  public abstract object Clone();
}
