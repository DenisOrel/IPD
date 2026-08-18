// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.ModelConfigurationException
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

public class ModelConfigurationException : EntityException
{
  private int errorCode;

  public ModelConfigurationException(int errorCode, string message)
    : base(message)
  {
    this.errorCode = errorCode;
  }

  public int ErrorCode
  {
    [DebuggerStepThrough] get => this.errorCode;
  }
}
