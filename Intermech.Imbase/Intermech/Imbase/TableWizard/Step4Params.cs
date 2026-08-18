// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableWizard.Step4Params
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.TableWizard;

[Serializable]
internal class Step4Params
{
  internal Step4Params(DataSet ds) => this.DS = ds;

  public DataSet DS { get; private set; }
}
