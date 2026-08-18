// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SettingsValidator
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Settings;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class SettingsValidator(string integratorName) : IntegratorSettingsValidator(integratorName)
{
  protected override string DoValidate(
    ISettingsObject settingsObject,
    SettingsValidatorContext context)
  {
    return settingsObject != null ? base.DoValidate(settingsObject, context) : throw new ArgumentNullException(nameof (settingsObject));
  }
}
