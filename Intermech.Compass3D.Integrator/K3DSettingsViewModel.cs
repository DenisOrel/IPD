// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DSettingsViewModel
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Tools.Integrators.CADInterface;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DSettingsViewModel : CADSettingsViewModel
{
  private bool enableDrawings2DSupport;
  private DocumentGroupViewModel partDrawings2D;
  private DocumentGroupViewModel assemblyDrawings2D;

  public K3DSettingsViewModel(K3DIntegratorSettingsFactory factory)
    : base((CADSettingsFactory) factory)
  {
    this.partDrawings2D = new DocumentGroupViewModel();
    this.assemblyDrawings2D = new DocumentGroupViewModel();
  }

  public override void LoadContent(CADSettings settings)
  {
    base.LoadContent(settings);
    K3DIntegratorSettings dintegratorSettings = (K3DIntegratorSettings) settings;
    this.enableDrawings2DSupport = dintegratorSettings.EnableDrawings2DSupport;
    this.partDrawings2D = new DocumentGroupViewModel(dintegratorSettings.PartDrawings2D);
    this.assemblyDrawings2D = new DocumentGroupViewModel(dintegratorSettings.AssemblyDrawings2D);
  }

  public override void SaveContent(CADSettings settings)
  {
    base.SaveContent(settings);
    K3DIntegratorSettings dintegratorSettings = (K3DIntegratorSettings) settings;
    dintegratorSettings.EnableDrawings2DSupport = this.enableDrawings2DSupport;
    dintegratorSettings.AssemblyDrawings2D.DocumentTypes.Clear();
    dintegratorSettings.AssemblyDrawings2D.DocumentTypes.AddRange((IEnumerable<GlobalId<int>>) this.assemblyDrawings2D.DocumentTypes);
    dintegratorSettings.PartDrawings2D.DocumentTypes.Clear();
    dintegratorSettings.PartDrawings2D.DocumentTypes.AddRange((IEnumerable<GlobalId<int>>) this.partDrawings2D.DocumentTypes);
  }

  protected override void DoAssign(CADSettingsViewModel source)
  {
    base.DoAssign(source);
    if (!(source is K3DSettingsViewModel dsettingsViewModel))
      return;
    this.enableDrawings2DSupport = dsettingsViewModel.enableDrawings2DSupport;
    this.partDrawings2D = dsettingsViewModel.partDrawings2D.Clone();
    this.assemblyDrawings2D = dsettingsViewModel.assemblyDrawings2D.Clone();
  }

  protected override void ResetPropertiesToDefaults()
  {
    base.ResetPropertiesToDefaults();
    this.enableDrawings2DSupport = false;
    this.partDrawings2D.DocumentTypes.Clear();
    this.assemblyDrawings2D.DocumentTypes.Clear();
  }

  [Category("4. Поддержка чертежей 2D")]
  [DisplayName("Включить поддержку чертежей 2D")]
  [Description("Включает или выключает поддержку интегратором чертежей CAD-системы, которые не содержат ссылок на 3D-модели. Информация об изделиях извлекается из файла спецификации, ассоциированного с чертежом.")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool EnableDrawings2DSupport
  {
    get => this.enableDrawings2DSupport;
    set => this.enableDrawings2DSupport = value;
  }

  [Category("4. Поддержка чертежей 2D")]
  [DisplayName("Чертежи деталей 2D")]
  [Description("Содержит список типов объектов IPS, используемых для представления чертежей деталей 2D")]
  [Editor(typeof (DocumentGroupUIEditor), typeof (UITypeEditor))]
  public DocumentGroupViewModel PartDrawings2D
  {
    get => this.partDrawings2D;
    set => this.partDrawings2D = value;
  }

  [Category("4. Поддержка чертежей 2D")]
  [DisplayName("Сборочные чертежи 2D")]
  [Description("Содержит список типов объектов IPS, используемых для представления сборочных чертежей 2D")]
  [Editor(typeof (DocumentGroupUIEditor), typeof (UITypeEditor))]
  public DocumentGroupViewModel AssemblyDrawings2D
  {
    get => this.assemblyDrawings2D;
    set => this.assemblyDrawings2D = value;
  }
}
