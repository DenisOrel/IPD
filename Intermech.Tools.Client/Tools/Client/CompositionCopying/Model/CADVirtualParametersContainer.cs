// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADVirtualParametersContainer
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal class CADVirtualParametersContainer : IParametersContainerProxy
{
  private ValueBag _valueBag;

  public CADVirtualParametersContainer() => this._valueBag = new ValueBag();

  public IList<string> GetParameterNames()
  {
    return (IList<string>) this.ValueBag.Keys.Select<StringKey, string>((Func<StringKey, string>) (x => x.ToString())).ToList<string>();
  }

  public List<ValueRecord> GetParameters() => this.ValueBag.GetItemsList();

  public List<ValueRecord> GetParameters(IList<string> parameterNames)
  {
    List<ValueRecord> itemsList = this.ValueBag.GetItemsList();
    foreach (string parameterName in (IEnumerable<string>) parameterNames)
    {
      ValueRecord parameter = this.TryGetParameter(parameterName);
      if (parameter != null)
        itemsList.Remove(parameter);
    }
    return itemsList;
  }

  public void SetParameters(IList<ValueRecord> parameters)
  {
    foreach (ValueRecord parameter in (IEnumerable<ValueRecord>) parameters)
      this.SetParameter(parameter);
  }

  public ValueRecord TryGetParameter(string parameterName)
  {
    return this.ValueBag.Find((StringKey) parameterName);
  }

  public ValueRecord GetParameter(string parameterName)
  {
    return this.TryGetParameter(parameterName) ?? throw new Exception($"Не удалось получить значение параметра '{parameterName}', так как он отсутствует у объекта CAD-интерфейса.");
  }

  public void SetParameter(ValueRecord parameter)
  {
    this.ValueBag.Update(parameter.Key, parameter.Value, true);
  }

  public ValueBag ValueBag => this._valueBag;
}
