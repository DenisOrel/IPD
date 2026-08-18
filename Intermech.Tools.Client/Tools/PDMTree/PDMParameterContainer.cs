// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMParameterContainer
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces.Data;
using System;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class PDMParameterContainer : PDMObjectParameterContainer
{
  private readonly IPDMSystemProvider pdmSystemProvider;

  public PDMParameterContainer(IDBObjectRef pdmObject, IPDMSystemProvider pdmSystemProvider)
    : base(pdmObject)
  {
    this.pdmSystemProvider = pdmSystemProvider;
  }

  public override string[] GetParameterNames(bool bConvertedNames)
  {
    this.PDMSystem.PrepareCall();
    try
    {
      return base.GetParameterNames(bConvertedNames);
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public override void GetParameters(
    string[] pParameterNames,
    bool bConvertedNames,
    out object[] ppValues,
    out short[] ppIsReadOnly)
  {
    this.PDMSystem.PrepareCall();
    try
    {
      base.GetParameters(pParameterNames, bConvertedNames, out ppValues, out ppIsReadOnly);
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public override void SetParameters(
    string[] pParameterNames,
    object[] pValues,
    bool bConvertedNames)
  {
    this.PDMSystem.PrepareCall();
    try
    {
      base.SetParameters(pParameterNames, pValues, bConvertedNames);
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public override void DeleteParameters(string[] pParameterNames, bool bConvertedNames)
  {
    this.PDMSystem.PrepareCall();
    try
    {
      base.DeleteParameters(pParameterNames, bConvertedNames);
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  private PDMSystem PDMSystem => this.pdmSystemProvider.PDMSystem;
}
