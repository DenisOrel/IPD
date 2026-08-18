// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileSettingsValidator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Intermech.Tools.Settings;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

public class SingleFileSettingsValidator(string integratorName) : IntegratorSettingsValidator(integratorName)
{
  protected override string DoValidate(
    ISettingsObject settingsObject,
    SettingsValidatorContext context)
  {
    SingleFileSettings singleFileSettings = settingsObject != null ? (SingleFileSettings) settingsObject : throw new ArgumentNullException(nameof (settingsObject));
    return singleFileSettings.DocumentTypes == null || singleFileSettings.DocumentTypes.Items.Count == 0 ? LocalizationHolder.rm.GetString("SR_542") : base.DoValidate(settingsObject, context);
  }
}
