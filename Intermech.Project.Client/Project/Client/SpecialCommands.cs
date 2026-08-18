// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.SpecialCommands
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Project.Client;

public sealed class SpecialCommands : IPropertyPage, IPropertyPageSearchOptionEvents, IConfigurable
{
  [NotNull]
  private const string SpecialCommandsConfigurationName = "IMProject.SpecialCommands";
  [NotNull]
  public static readonly string Caption = Localization.GetString(nameof (SpecialCommands));
  private const string ShowWorkshopRouteProcessingCommandProp = "ShowWorkshopRouteProcessingCommand";
  [NotNull]
  public static readonly string WorkshopRouteProcessingCommandCaption = Localization.GetString("WorkshopRouteProcessing");
  [NotNull]
  private static readonly Lazy<SpecialCommands> _instance = new Lazy<SpecialCommands>((Func<SpecialCommands>) (() => new SpecialCommands()), true);
  private bool? _workshopRouteProcessingCommandVisibleVisibleNewValue;

  private SpecialCommands()
  {
    this.Control = (object) new ClassWrapperForPropertyGrid((object) this);
    this.LoadConfiguration(ApplicationServices.Container.GetService<IConfigurationManager>());
  }

  [NotNull]
  public static SpecialCommands Instance
  {
    [DebuggerStepThrough] get
    {
      return Intermech.Diagnostics.Check.Result.NotNull<SpecialCommands>(SpecialCommands._instance.Value);
    }
  }

  [Browsable(false)]
  public static event EventHandler OnChanged
  {
    [DebuggerStepThrough] add => SpecialCommands.Instance.Changed += value;
    [DebuggerStepThrough] remove => SpecialCommands.Instance.Changed -= value;
  }

  [Browsable(false)]
  public static bool AnyCommandVisible => SpecialCommands.Instance.AnyCommand;

  [Browsable(false)]
  public static bool ShowWorkshopRouteProcessingCommand
  {
    [DebuggerStepThrough] get => SpecialCommands.Instance.WorkshopRouteProcessingCommandVisible;
  }

  [Browsable(false)]
  private bool AnyCommand { get; set; }

  [Browsable(false)]
  public bool WorkshopRouteProcessingCommandVisible
  {
    [DebuggerStepThrough] get => this.AnyCommand;
    set
    {
      this.AnyCommand = value;
      this._workshopRouteProcessingCommandVisibleVisibleNewValue = new bool?();
    }
  }

  [CustomDisplayName("WorkshopRouteProcessingVisibility")]
  [CustomDescription("WorkshopRouteProcessingVisibility")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool WorkshopRouteProcessingVisiblePublished
  {
    get => this._workshopRouteProcessingCommandVisibleVisibleNewValue ?? this.AnyCommand;
    set => this._workshopRouteProcessingCommandVisibleVisibleNewValue = new bool?(value);
  }

  [Browsable(false)]
  public event EventHandler Changed;

  private void FireChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
  }

  [Browsable(false)]
  public PropertyPageType Type
  {
    [DebuggerStepThrough] get => PropertyPageType.Object;
  }

  [Browsable(false)]
  public object Control { get; }

  [NotNull]
  [Browsable(false)]
  public string PageName
  {
    [DebuggerStepThrough] get => SpecialCommands.Caption;
  }

  public void Apply()
  {
    if (this._workshopRouteProcessingCommandVisibleVisibleNewValue.HasValue && this._workshopRouteProcessingCommandVisibleVisibleNewValue.Value != this.AnyCommand)
    {
      bool flag = this._workshopRouteProcessingCommandVisibleVisibleNewValue.Value;
      this._workshopRouteProcessingCommandVisibleVisibleNewValue = new bool?();
      if (flag)
        WorkshopRouteProcessingCommand.ValidateTechcardMetadata();
      this.AnyCommand = flag;
    }
    else
      this._workshopRouteProcessingCommandVisibleVisibleNewValue = new bool?();
    this.SaveConfiguration(ApplicationServices.Container.GetService<IConfigurationManager>());
    this.FireChanged();
  }

  public void Cancel()
  {
    this._workshopRouteProcessingCommandVisibleVisibleNewValue = new bool?();
    this.LoadConfiguration(ApplicationServices.Container.GetService<IConfigurationManager>());
  }

  [NotNull]
  [Browsable(false)]
  public string HelpTopicID
  {
    [DebuggerStepThrough] get => string.Empty;
  }

  [NotNull]
  [Browsable(false)]
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  [NotNull]
  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  public void SaveConfiguration([NotNull] IConfigurationManager configurationManager)
  {
    (configurationManager.Open("IMProject.SpecialCommands") ?? configurationManager.Create("IMProject.SpecialCommands")).SetProperty("ShowWorkshopRouteProcessingCommand", this.WorkshopRouteProcessingCommandVisible.ToString());
  }

  public void LoadConfiguration([NotNull] IConfigurationManager configurationManager)
  {
    IConfiguration configuration = configurationManager.Open("IMProject.SpecialCommands");
    if (configuration == null)
      return;
    string property = configuration.GetProperty("ShowWorkshopRouteProcessingCommand");
    if (string.IsNullOrEmpty(property))
      return;
    this.WorkshopRouteProcessingCommandVisible = bool.Parse(property);
  }
}
