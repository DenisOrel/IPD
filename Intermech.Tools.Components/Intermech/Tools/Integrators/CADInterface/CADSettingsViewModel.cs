// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADSettingsViewModel
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.ChangeHighlighting;
using Intermech.Tools.Integrators.Mechanical;
using Intermech.Tools.Settings.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Базовый тип для моделей представления настроек интеграторов, созданных на основе CAD-интерфейса.
/// </summary>
public class CADSettingsViewModel : ICloneable
{
  private CADSettingsFactory factory;
  private bool enableIMViewerFiles;
  private bool drawingsAreDocuments;
  private ChangeTrackingListAdapter<CollectionValueAdapter<string>> drawingSuffixes;
  private bool syncSubstitutions;
  private bool enableCADLinkTypeAttribute;
  private GlobalId<int> standardPartType;
  private bool updateModelAuthenticFilesOnCheckin;
  private bool updateDrawingAuthenticFilesOnCheckin;
  private DocumentGroupViewModel assemblies;
  private DocumentGroupViewModel parts;
  private DocumentGroupViewModel assemblyDrawings;
  private DocumentGroupViewModel partDrawings;
  private DocumentGroupViewModel unpairedDocumentTypes;
  private DocumentGroupViewModel neutralDocumentTypes;
  private ChangeTrackingListAdapter<GlobalId<int>> customDocAttrs;
  private ChangeTrackingListAdapter<GlobalId<int>> customArtAttrs;

  /// <summary>Создает пустую модель представления.</summary>
  /// <param name="factory">Фабрика настроек интегратора</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="factory" /> не должен быть равен null</exception>
  public CADSettingsViewModel(CADSettingsFactory factory)
  {
    this.factory = factory != null ? factory : throw new ArgumentNullException(nameof (factory));
    this.enableIMViewerFiles = false;
    this.drawingsAreDocuments = true;
    this.drawingSuffixes = new ChangeTrackingListAdapter<CollectionValueAdapter<string>>();
    this.syncSubstitutions = true;
    this.updateModelAuthenticFilesOnCheckin = false;
    this.updateDrawingAuthenticFilesOnCheckin = false;
    this.assemblies = new DocumentGroupViewModel();
    this.parts = new DocumentGroupViewModel();
    this.assemblyDrawings = new DocumentGroupViewModel();
    this.partDrawings = new DocumentGroupViewModel();
    this.unpairedDocumentTypes = new DocumentGroupViewModel();
    this.neutralDocumentTypes = new DocumentGroupViewModel();
    this.customDocAttrs = new ChangeTrackingListAdapter<GlobalId<int>>();
    this.customArtAttrs = new ChangeTrackingListAdapter<GlobalId<int>>();
  }

  /// <summary>
  /// Загружает содержимое модели представления из настроек интегратора.
  /// Метод использует перед началом редактирования настроек интегратора в PropertyGrid.
  /// </summary>
  /// <param name="settings">Объект настроек интегратора</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="settings" /> не должен быть равен null</exception>
  public virtual void LoadContent(CADSettings settings)
  {
    this.enableIMViewerFiles = settings != null ? settings.EnableIMViewerFiles : throw new ArgumentNullException(nameof (settings));
    this.drawingsAreDocuments = settings.NewDrawingMode == NewDrawingMode.Document;
    this.drawingSuffixes = new ChangeTrackingListAdapter<CollectionValueAdapter<string>>(settings.DrawingSuffixes.Count);
    foreach (string drawingSuffix in settings.DrawingSuffixes)
      this.drawingSuffixes.Items.Add(new CollectionValueAdapter<string>(drawingSuffix));
    this.syncSubstitutions = settings.SynchronizeSubstitutions;
    this.enableCADLinkTypeAttribute = settings.EnableCADLinkTypeAttribute;
    this.updateModelAuthenticFilesOnCheckin = settings.UpdateModelAuthenticFilesOnCheckin;
    this.updateDrawingAuthenticFilesOnCheckin = settings.UpdateDrawingAuthenticFilesOnCheckin;
    this.assemblies = new DocumentGroupViewModel(settings.FileDocumentGroups.FindByName("Assembly", true));
    this.parts = new DocumentGroupViewModel(settings.FileDocumentGroups.FindByName("Part", true));
    this.assemblyDrawings = new DocumentGroupViewModel(settings.FileDocumentGroups.FindByName("AssemblyDrawing", true));
    this.partDrawings = new DocumentGroupViewModel(settings.FileDocumentGroups.FindByName("PartDrawing", true));
    this.unpairedDocumentTypes = new DocumentGroupViewModel(settings.UnpairedDocumentTypes);
    this.neutralDocumentTypes = new DocumentGroupViewModel(settings.NeutralDocumentTypes);
    this.standardPartType = settings.StandardPartType;
    this.customDocAttrs = new ChangeTrackingListAdapter<GlobalId<int>>((IEnumerable<GlobalId<int>>) settings.CustomDocumentAttributes);
    this.customArtAttrs = new ChangeTrackingListAdapter<GlobalId<int>>((IEnumerable<GlobalId<int>>) settings.CustomArticleAttributes);
  }

  /// <summary>
  /// Сохраняет содержимое модели представления в настройках интегратора.
  /// Метод используется после завершения редактирования настроек интегратора в PropertyGrid.
  /// </summary>
  /// <param name="settings">Объект настроек интегратора</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="settings" /> не должен быть равен null</exception>
  public virtual void SaveContent(CADSettings settings)
  {
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    settings.EnableIMViewerFiles = this.enableIMViewerFiles;
    settings.NewDrawingMode = this.drawingsAreDocuments ? NewDrawingMode.Document : NewDrawingMode.AdditionalModelFile;
    settings.SynchronizeSubstitutions = this.syncSubstitutions;
    settings.EnableCADLinkTypeAttribute = this.enableCADLinkTypeAttribute;
    settings.UpdateModelAuthenticFilesOnCheckin = this.updateModelAuthenticFilesOnCheckin;
    settings.UpdateDrawingAuthenticFilesOnCheckin = this.updateDrawingAuthenticFilesOnCheckin;
    settings.DrawingSuffixes.Clear();
    foreach (CollectionValueAdapter<string> drawingSuffix in this.drawingSuffixes)
      settings.DrawingSuffixes.Add(drawingSuffix.Value);
    this.UpdateDocumentGroup(settings, "Assembly", this.assemblies);
    this.UpdateDocumentGroup(settings, "Part", this.parts);
    this.UpdateDocumentGroup(settings, "AssemblyDrawing", this.assemblyDrawings);
    this.UpdateDocumentGroup(settings, "PartDrawing", this.partDrawings);
    this.UpdateDocumentGroup(settings.UnpairedDocumentTypes, this.unpairedDocumentTypes);
    this.UpdateDocumentGroup(settings.NeutralDocumentTypes, this.neutralDocumentTypes);
    settings.StandardPartType = this.standardPartType;
    settings.CustomDocumentAttributes.Clear();
    settings.CustomDocumentAttributes.AddRange((IEnumerable<GlobalId<int>>) this.customDocAttrs.Items);
    settings.CustomArticleAttributes.Clear();
    settings.CustomArticleAttributes.AddRange((IEnumerable<GlobalId<int>>) this.customArtAttrs.Items);
  }

  private void UpdateDocumentGroup(
    CADSettings settings,
    string groupName,
    DocumentGroupViewModel groupViewModel)
  {
    this.UpdateDocumentGroup(settings.FileDocumentGroups.FindByName(groupName, true), groupViewModel);
  }

  private void UpdateDocumentGroup(DocumentGroup group, DocumentGroupViewModel groupViewModel)
  {
    group.DocumentTypes.Clear();
    group.DocumentTypes.AddRange((IEnumerable<GlobalId<int>>) groupViewModel.DocumentTypes);
  }

  /// <summary>
  /// Заполняет свойства модели представления, используя другую модель представления в качестве источника данных.
  /// Метод используется при клонировании моделей представления, когда новая пустая модель заполняется, используя текущий объект в качестве источника данных.
  /// </summary>
  /// <param name="source">Модель представления, являющаяся источником данных</param>
  protected virtual void DoAssign(CADSettingsViewModel source)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    this.ResetPropertiesToDefaults();
    this.enableIMViewerFiles = source.enableIMViewerFiles;
    this.drawingsAreDocuments = source.drawingsAreDocuments;
    this.syncSubstitutions = source.syncSubstitutions;
    this.enableCADLinkTypeAttribute = source.enableCADLinkTypeAttribute;
    this.updateModelAuthenticFilesOnCheckin = source.updateModelAuthenticFilesOnCheckin;
    this.updateDrawingAuthenticFilesOnCheckin = source.updateDrawingAuthenticFilesOnCheckin;
    this.drawingSuffixes = source.drawingSuffixes.Clone();
    this.assemblies = source.assemblies.Clone();
    this.parts = source.parts.Clone();
    this.assemblyDrawings = source.assemblyDrawings.Clone();
    this.partDrawings = source.partDrawings.Clone();
    this.unpairedDocumentTypes = source.unpairedDocumentTypes.Clone();
    this.neutralDocumentTypes = source.neutralDocumentTypes.Clone();
    this.standardPartType = source.standardPartType;
    this.customDocAttrs = source.customDocAttrs.Clone();
    this.customArtAttrs = source.customArtAttrs.Clone();
  }

  /// <summary>
  /// Заполняет свойства модели значениями по умолчанию.
  /// Метод вызывается из метода DoAssign() перед началом заполнения свойств модели представления из источника данных.
  /// </summary>
  protected virtual void ResetPropertiesToDefaults()
  {
    this.enableIMViewerFiles = false;
    this.drawingsAreDocuments = true;
    this.drawingSuffixes.Items.Clear();
    this.syncSubstitutions = true;
    this.enableCADLinkTypeAttribute = false;
    this.updateModelAuthenticFilesOnCheckin = false;
    this.updateDrawingAuthenticFilesOnCheckin = false;
    this.assemblies.DocumentTypes.Clear();
    this.parts.DocumentTypes.Clear();
    this.assemblyDrawings.DocumentTypes.Clear();
    this.partDrawings.DocumentTypes.Clear();
    this.unpairedDocumentTypes.DocumentTypes.Clear();
    this.neutralDocumentTypes.DocumentTypes.Clear();
    this.standardPartType = (GlobalId<int>) null;
    this.customDocAttrs.Items.Clear();
    this.customArtAttrs.Items.Clear();
  }

  /// <summary>Клонирует модель представления.</summary>
  /// <returns>Клон модели представления</returns>
  public CADSettingsViewModel Clone() => (CADSettingsViewModel) this.DoClone();

  /// <summary>Клонирует объект.</summary>
  /// <returns>Клон объекта</returns>
  object ICloneable.Clone() => this.DoClone();

  /// <summary>Клонирует модель представления.</summary>
  /// <returns>Клон модели представления</returns>
  protected object DoClone()
  {
    CADSettingsViewModel settingsViewModel = this.factory.CreateSettingsViewModel();
    settingsViewModel.DoAssign(this);
    return (object) settingsViewModel;
  }

  [DisplayName("Включить поддержку файлов IMViewer")]
  [Description("Если включено, то интегратор будет создавать и обновлять файлы IMViewer при сохранении изменений в документах CAD-системы.")]
  [CustomCategory("Attribute.Tools.Components_17")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool EnableIMViewerFile
  {
    get => this.enableIMViewerFiles;
    set => this.enableIMViewerFiles = value;
  }

  [CustomDisplayName("Attribute.Tools.Components_34")]
  [CustomDescription("Attribute.Tools.Components_35")]
  [CustomCategory("Attribute.Tools.Components_17")]
  [Editor(typeof (ChangeTrackingListUIEditor<CollectionValueAdapter<string>>), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<CollectionValueAdapter<string>> DrawingSuffixes
  {
    get => this.drawingSuffixes;
    set => this.drawingSuffixes = value;
  }

  [CustomDisplayName("Attribute.Tools.Components_16")]
  [CustomCategory("Attribute.Tools.Components_17")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool DrawingsAreDocuments
  {
    get => this.drawingsAreDocuments;
    set => this.drawingsAreDocuments = value;
  }

  [CustomDisplayName("SR_41")]
  [CustomDescription("SR_42")]
  [CustomCategory("Attribute.Tools.Components_17")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool SyncSubstitutions
  {
    get => this.syncSubstitutions;
    set => this.syncSubstitutions = value;
  }

  [DisplayName("Передавать в IPS типы связей между 3D-моделями")]
  [Description("Включает и выключает режим, в котором интегратор определяет тип каждой связи между 3D-моделями и сохраняет его в атрибуте 'Тип связи в CAD-системе' на связи между документами. Включение этого режима снижает быстродействие интегратора.")]
  [CustomCategory("Attribute.Tools.Components_17")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool EnableCADLinkTypeAttribute
  {
    get => this.enableCADLinkTypeAttribute;
    set => this.enableCADLinkTypeAttribute = value;
  }

  [DisplayName("Обновлять аутентичные .pdf-файлы для моделей")]
  [Description("Если включено, то интегратор будет создавать и обновлять .pdf-файлы для моделей при завершении редактирования.")]
  [Category("3. Завершение редактирования")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool UpdateModelAuthenticFilesOnCheckin
  {
    get => this.updateModelAuthenticFilesOnCheckin;
    set => this.updateModelAuthenticFilesOnCheckin = value;
  }

  [DisplayName("Обновлять аутентичные .pdf-файлы для чертежей")]
  [Description("Если включено, то интегратор будет создавать и обновлять .pdf-файлы для чертежей при завершении редактирования.")]
  [Category("3. Завершение редактирования")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool UpdateDrawingAuthenticFilesOnCheckin
  {
    get => this.updateDrawingAuthenticFilesOnCheckin;
    set => this.updateDrawingAuthenticFilesOnCheckin = value;
  }

  [CustomDisplayName("Attribute.Tools.Components_18")]
  [CustomCategory("Attribute.Tools.Components_19")]
  [CustomDescription("Attribute.Tools.Components_20")]
  [Editor(typeof (DocumentGroupUIEditor), typeof (UITypeEditor))]
  public DocumentGroupViewModel Assemblies
  {
    get => this.assemblies;
    set => this.assemblies = value;
  }

  [CustomDisplayName("Attribute.Tools.Components_21")]
  [CustomCategory("Attribute.Tools.Components_19")]
  [CustomDescription("Attribute.Tools.Components_22")]
  [Editor(typeof (DocumentGroupUIEditor), typeof (UITypeEditor))]
  public DocumentGroupViewModel Parts
  {
    get => this.parts;
    set => this.parts = value;
  }

  [CustomDisplayName("Attribute.Tools.Components_23")]
  [CustomCategory("Attribute.Tools.Components_19")]
  [CustomDescription("Attribute.Tools.Components_24")]
  [Editor(typeof (SelectObjectTypeUIEditor), typeof (UITypeEditor))]
  public GlobalId<int> StandardPartType
  {
    get => this.standardPartType;
    set => this.standardPartType = value;
  }

  [CustomDisplayName("Attribute.Tools.Components_25")]
  [CustomCategory("Attribute.Tools.Components_19")]
  [CustomDescription("Attribute.Tools.Components_26")]
  [Editor(typeof (DocumentGroupUIEditor), typeof (UITypeEditor))]
  public DocumentGroupViewModel AssemblyDrawings
  {
    get => this.assemblyDrawings;
    set => this.assemblyDrawings = value;
  }

  [CustomDisplayName("Attribute.Tools.Components_27")]
  [CustomCategory("Attribute.Tools.Components_19")]
  [CustomDescription("Attribute.Tools.Components_28")]
  [Editor(typeof (DocumentGroupUIEditor), typeof (UITypeEditor))]
  public DocumentGroupViewModel PartDrawings
  {
    get => this.partDrawings;
    set => this.partDrawings = value;
  }

  [CustomDisplayName("SR_44")]
  [CustomCategory("SR_43")]
  [CustomDescription("SR_45")]
  [Editor(typeof (DocumentGroupUIEditor), typeof (UITypeEditor))]
  public DocumentGroupViewModel UnpairedDocumentTypes
  {
    get => this.unpairedDocumentTypes;
    set => this.unpairedDocumentTypes = value;
  }

  [CustomDisplayName("SR_47")]
  [CustomCategory("SR_46")]
  [CustomDescription("SR_48")]
  [Editor(typeof (DocumentGroupUIEditor), typeof (UITypeEditor))]
  public DocumentGroupViewModel NeutralDocumentTypes
  {
    get => this.neutralDocumentTypes;
    set => this.neutralDocumentTypes = value;
  }

  [CustomDisplayName("Attribute.Tools.Components_29")]
  [CustomCategory("Attribute.Tools.Components_17")]
  [CustomDescription("Attribute.Tools.Components_30")]
  [Editor(typeof (AttributeTypesUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<GlobalId<int>> CustomDocumentAttributes
  {
    get => this.customDocAttrs;
    set => this.customDocAttrs = value;
  }

  [CustomDisplayName("Attribute.Tools.Components_31")]
  [CustomCategory("Attribute.Tools.Components_17")]
  [CustomDescription("Attribute.Tools.Components_32")]
  [Editor(typeof (AttributeTypesUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<GlobalId<int>> CustomArticleAttributes
  {
    get => this.customArtAttrs;
    set => this.customArtAttrs = value;
  }
}
