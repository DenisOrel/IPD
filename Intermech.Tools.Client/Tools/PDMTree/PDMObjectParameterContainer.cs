// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMObjectParameterContainer
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Interfaces.Data;
using Interop.CADInterface;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal class PDMObjectParameterContainer : IParametersContainer
{
  private PDMAttributeManager attributeManager;
  private ParametersContainerConverter dataFormatConverter;

  public PDMObjectParameterContainer(IDBObjectRef pdmObject)
  {
    this.attributeManager = new PDMAttributeManager(pdmObject);
    this.dataFormatConverter = ParametersContainerConverter.Default;
  }

  public virtual string[] GetParameterNames(bool bConvertedNames)
  {
    return this.attributeManager.GetAttributeNames().ToArray();
  }

  public virtual void GetParameters(
    string[] pParameterNames,
    bool bConvertedNames,
    out object[] ppValues,
    out short[] ppIsReadOnly)
  {
    if (pParameterNames == null)
      pParameterNames = new string[0];
    List<ValueRecord> attributes = this.attributeManager.GetAttributes(new List<string>((IEnumerable<string>) pParameterNames));
    this.dataFormatConverter.ToValuesAndReadOnlyFlags((IList<string>) pParameterNames, (IEnumerable<ValueRecord>) attributes, out ppValues, out ppIsReadOnly);
  }

  public virtual void SetParameters(
    string[] pParameterNames,
    object[] pValues,
    bool bConvertedNames)
  {
    if (pParameterNames == null)
      pParameterNames = new string[0];
    if (pValues == null)
      pValues = new object[0];
    this.attributeManager.SetParameters(this.dataFormatConverter.ToParameters(pParameterNames, pValues, (short[]) null, false));
  }

  public virtual void DeleteParameters(string[] pParameterNames, bool bConvertedNames)
  {
    if (pParameterNames == null)
      pParameterNames = new string[0];
    this.attributeManager.DeleteParameters(new List<string>((IEnumerable<string>) pParameterNames));
  }
}
