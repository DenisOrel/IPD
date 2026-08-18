// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.AttributesValuesEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server;

public class AttributesValuesEventArgs : EventArgs
{
  private List<AttributeValues> _ModifiedValuesList;

  public AttributeValues[] ValuesList { get; private set; }

  public GetAttributeValuesModes Modes { get; private set; }

  public IUserSession Session { get; private set; }

  public AttributesValuesEventArgs(
    AttributeValues[] valuesList,
    GetAttributeValuesModes modes,
    IUserSession session)
  {
    this.ValuesList = valuesList;
    this.Modes = modes;
    this.Session = session;
  }

  public void AddModifiedValue(AttributeValues m_value)
  {
    if (this._ModifiedValuesList == null)
      this._ModifiedValuesList = new List<AttributeValues>();
    this._ModifiedValuesList.Add(m_value);
  }

  public List<AttributeValues> ModifiedValuesList => this._ModifiedValuesList;
}
