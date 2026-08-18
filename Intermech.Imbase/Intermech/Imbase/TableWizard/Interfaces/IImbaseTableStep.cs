// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableWizard.Interfaces.IImbaseTableStep
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.TableWizard.Interfaces;

internal interface IImbaseTableStep
{
  ImbaseTableWizardForm WizardForm { set; }

  Dictionary<Type, object> Context { get; set; }

  Type NextStep { get; }

  Type PrevStep { get; }
}
