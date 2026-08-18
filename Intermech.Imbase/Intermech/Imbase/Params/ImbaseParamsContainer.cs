// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.ImbaseParamsContainer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Imbase.Params;

#nullable disable
namespace Intermech.Imbase.Params;

internal class ImbaseParamsContainer
{
  public ImbaseCommonParams CommonParams { get; }

  public ImbaseUserParams UserParams { get; }

  public ImbaseParamsContainer(ImbaseCommonParams commonParams, ImbaseUserParams userParams)
  {
    this.CommonParams = commonParams;
    this.UserParams = userParams;
  }
}
