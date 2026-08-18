// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.UsedAttributesEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server;

public class UsedAttributesEventArgs : EventArgs
{
  private List<int> _UsedAttributes;

  public UsedAttributesEventArgs() => this._UsedAttributes = new List<int>();

  public List<int> UsedAttributes => this._UsedAttributes;

  public void AddAttribute(int attrID)
  {
    if (this._UsedAttributes.Contains(attrID))
      return;
    this._UsedAttributes.Add(attrID);
  }
}
