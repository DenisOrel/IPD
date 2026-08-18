// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SettingsService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Settings;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class SettingsService(IIntegrator owner) : 
  IntegratorSettingsService<ADIntegratorSettings>(owner),
  IDocumentAttributesSettingsService,
  IIntegratorSettingsService,
  IIntegratorService,
  IIntegratorSettingsViewModelService
{
  private ISynchronizedObjectAttributes _assemblyAttributes;
  private ISynchronizedObjectAttributes _componentAttributes;
  private ISynchronizedObjectAttributes _docAttributes;
  private ISynchronizedObjectAttributes _projectAttributes;
  private ISynchronizedObjectAttributes _pcbDocumentAttributes;

  protected override void DoAfterInitialize()
  {
    base.DoAfterInitialize();
    this._componentAttributes = (ISynchronizedObjectAttributes) new SynchronizedADComponentAttributes(this);
    this._assemblyAttributes = (ISynchronizedObjectAttributes) new SynchronizedADAssemblyAttributes(this);
    this._docAttributes = (ISynchronizedObjectAttributes) new SynchronizedADSchemaDocumentAttributes(this);
    this._projectAttributes = (ISynchronizedObjectAttributes) new SynchronizedADProjectAttributes(this);
    this._pcbDocumentAttributes = (ISynchronizedObjectAttributes) new SynchronizedADPCBDocumentAttributes(this);
  }

  protected override IntegratorSettingsCodec CreateSettingsCodec()
  {
    return (IntegratorSettingsCodec) new SettingsCodec(this.Integrator.DisplayName);
  }

  protected override IntegratorSettingsValidator CreateSettingsValidator()
  {
    return (IntegratorSettingsValidator) new SettingsValidator(this.Integrator.DisplayName);
  }

  object IIntegratorSettingsViewModelService.CreateViewModel(ISettingsObject settingsObject)
  {
    if (settingsObject == null)
      throw new ArgumentNullException(nameof (settingsObject));
    this.RequireReadyState();
    return (object) new ADSettingsSurrogate((ADIntegratorSettings) settingsObject);
  }

  ISettingsObject IIntegratorSettingsViewModelService.CreateSettingsFromViewModel(
    object viewModelObject)
  {
    if (viewModelObject == null)
      throw new ArgumentNullException(nameof (viewModelObject));
    this.RequireReadyState();
    return (ISettingsObject) ((ECADSettingsSurrogate<ADIntegratorSettings>) viewModelObject).Settings;
  }

  public ISynchronizedObjectAttributes AssemblyAttributes
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this._assemblyAttributes;
    }
  }

  public ISynchronizedObjectAttributes ComponentAttributes
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this._componentAttributes;
    }
  }

  public ISynchronizedObjectAttributes SynchronizedDocumentAttributes
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this._docAttributes;
    }
  }

  public ISynchronizedObjectAttributes PCBDocumentAttributes
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this._pcbDocumentAttributes;
    }
  }

  public ISynchronizedObjectAttributes ProjectAttributes
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this._projectAttributes;
    }
  }
}
