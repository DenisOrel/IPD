// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADSettingsSurrogate
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Interfaces;
using Intermech.PropertyEditors.ChangeHighlighting;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Settings.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADSettingsSurrogate : ECADSettingsSurrogate<ADIntegratorSettings>
{
  public ADSettingsSurrogate(ADIntegratorSettings settings)
    : base(settings)
  {
    this.ProjectType = (GlobalId<int>) settings.ProjectType?.Clone();
    this.SchemaDocumentTypes = settings.SchemaDocumentTypes != null ? new ChangeTrackingListAdapter<GlobalId<int>>((IEnumerable<GlobalId<int>>) settings.SchemaDocumentTypes) : new ChangeTrackingListAdapter<GlobalId<int>>(0);
    this.ProjectAttributes = settings.ProjectAttributes != null ? new ChangeTrackingListAdapter<AttributesCompliance>((IEnumerable<AttributesCompliance>) settings.ProjectAttributes.ConvertAll<AttributesCompliance>((Converter<Tuple<StringKey, StringKey, bool>, AttributesCompliance>) (item => this.ConvertAttributes(item)))) : new ChangeTrackingListAdapter<AttributesCompliance>(0);
    this.AdditionalFilesExt = settings.AdditionalFilesExt ?? string.Empty;
    this.PartTypeParameter = settings.PartTypeParameter ?? string.Empty;
    this.ComponentsFilter = settings.ComponentsFilter != null ? (ComponentsFilterSettings<ADComponentsCompositionVariants>) settings.ComponentsFilter.Clone() : new ComponentsFilterSettings<ADComponentsCompositionVariants>();
    this.PCBDocumentTypes = settings.PCBDocumentTypes != null ? new ChangeTrackingListAdapter<GlobalId<int>>((IEnumerable<GlobalId<int>>) settings.PCBDocumentTypes) : new ChangeTrackingListAdapter<GlobalId<int>>(0);
    this.GerberFiles = settings.GerberFiles;
    this.VariantsFilter = settings.VariantsFilter == null ? new ChangeTrackingListAdapter<NotImportedVariantSettingsSurrogate>() : new ChangeTrackingListAdapter<NotImportedVariantSettingsSurrogate>((IEnumerable<NotImportedVariantSettingsSurrogate>) settings.VariantsFilter.ConvertAll<NotImportedVariantSettingsSurrogate>((Converter<Tuple<StringKey, StringKey>, NotImportedVariantSettingsSurrogate>) (item => new NotImportedVariantSettingsSurrogate()
    {
      ParameterName = (string) item.Item1,
      ParameterValue = (string) item.Item2
    })));
    this.QuantityParameter = settings.QuantityParameter;
  }

  private AttributesCompliance ConvertAttributes(Tuple<StringKey, StringKey, bool> item)
  {
    return new AttributesCompliance()
    {
      DBAttributeName = (string) item.Item1,
      CADAttributeName = (string) item.Item2
    };
  }

  protected override void SaveSettings(ADIntegratorSettings settings)
  {
    base.SaveSettings(settings);
    settings.ProjectType = (GlobalId<int>) this.ProjectType?.Clone();
    settings.SchemaDocumentTypes = new List<GlobalId<int>>((IEnumerable<GlobalId<int>>) this.SchemaDocumentTypes);
    settings.ProjectAttributes = new List<Tuple<StringKey, StringKey, bool>>();
    foreach (AttributesCompliance projectAttribute in this.ProjectAttributes)
      settings.ProjectAttributes.Add(new Tuple<StringKey, StringKey, bool>(new StringKey(projectAttribute.DBAttributeName), new StringKey(projectAttribute.CADAttributeName), true));
    settings.AdditionalFilesExt = this.AdditionalFilesExt;
    settings.PartTypeParameter = this.PartTypeParameter;
    settings.ComponentsFilter = (ComponentsFilterSettings<ADComponentsCompositionVariants>) this.ComponentsFilter.Clone();
    settings.PCBDocumentTypes = new List<GlobalId<int>>((IEnumerable<GlobalId<int>>) this.PCBDocumentTypes);
    settings.GerberFiles = this.GerberFiles;
    settings.VariantsFilter = new List<Tuple<StringKey, StringKey>>();
    foreach (NotImportedVariantSettingsSurrogate settingsSurrogate in this.VariantsFilter)
      settings.VariantsFilter.Add(new Tuple<StringKey, StringKey>((StringKey) settingsSurrogate.ParameterName, (StringKey) settingsSurrogate.ParameterValue));
    settings.QuantityParameter = this.QuantityParameter;
  }

  public override object Clone() => (object) new ADSettingsSurrogate(this.Settings);

  [DisplayName("Схемы электрические")]
  [Description("Типы объектов IPS, соответствующие различным электрическим схемам.")]
  [Category("Соответствия типов объектов")]
  [Editor(typeof (FileDocumentTypesUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<GlobalId<int>> SchemaDocumentTypes { get; set; }

  [DisplayName("Файлы PCB")]
  [Description("Типы объектов IPS, соответствующие файлу PCB.")]
  [Category("Соответствия типов объектов")]
  [Editor(typeof (FileDocumentTypesUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<GlobalId<int>> PCBDocumentTypes { get; set; }

  [DisplayName("Проекты Altium Designer")]
  [Description("Тип объектов IPS, соответствующий проекту Altium Designer.")]
  [Category("Соответствия типов объектов")]
  [Editor(typeof (SelectObjectTypeUIEditor), typeof (UITypeEditor))]
  public GlobalId<int> ProjectType { get; set; }

  [Category("Настройки синхронизируемых атрибутов")]
  [DisplayName("Атрибуты проекта")]
  [Description("Содержит список атрибутов, синхронизируемых между документом в базе IPS и файлом этого документа.")]
  [Editor(typeof (AttributesComplianceUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<AttributesCompliance> ProjectAttributes { get; set; }

  [DisplayName("Дополнительные файлы проекта")]
  [Description("Содержит список расширений файлов (через запятую, например: emp,emn,schlib), которые при наличии записываются в качестве дополнительных файлов проекту Altium Designer.")]
  [Category("Общие")]
  public string AdditionalFilesExt { get; set; }

  [DisplayName("Фильтрация состава")]
  [Description("Содержит настройки фильтрации списка компонентов схемы для состава изделия и перечня элементов.")]
  [Category("Общие")]
  [Editor(typeof (ComponentsFilterUIEditor), typeof (UITypeEditor))]
  public ComponentsFilterSettings<ADComponentsCompositionVariants> ComponentsFilter { get; set; }

  [DisplayName("Параметр с типом объекта")]
  [Description("Содержит имя параметра компонента схемы, в значении которого указывается тип создаваемого объекта в базе IPS.")]
  [Category("Общие")]
  public string PartTypeParameter { get; set; }

  [DisplayName("Маски файлов для производства печатных плат")]
  [Description("Содержит список масок файлов проекта через запятую, которые сгенерированы для производства печатных плат.")]
  [Category("Общие")]
  public string GerberFiles { get; set; }

  [DisplayName("Фильтрация в многовариантном проекте")]
  [Description("Параметр и значение в свойствах варианта проекта, при которых по варианту не будет создаваться исполнение.")]
  [Category("Общие")]
  [Editor(typeof (ListParamValuesSettingsUIEditor<NotImportedVariantSettingsSurrogate>), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<NotImportedVariantSettingsSurrogate> VariantsFilter { get; set; }

  [Category("Настройки идентифицирующих атрибутов")]
  [DisplayName("Количество для материала")]
  [Description("Наименование параметра компонента-материала, в котором приведены значения для атрибута Количество")]
  public string QuantityParameter { get; set; }
}
