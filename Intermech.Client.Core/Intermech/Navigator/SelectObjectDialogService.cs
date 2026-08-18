
// Type: Intermech.Navigator.SelectObjectDialogService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator;

public class SelectObjectDialogService : ISelectObjectDialogService
{
  private Dictionary<int, IDescriptor> _descriptors = new Dictionary<int, IDescriptor>();

  public void Register(int typeID, IDescriptor rootDescriptor)
  {
    if (this._descriptors.ContainsKey(typeID))
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Client.Core_1530"), (object) typeID));
    this._descriptors.Add(typeID, rootDescriptor);
  }

  public IDescriptor GetDescriptor(int typeID)
  {
    IDescriptor descriptor;
    return !this._descriptors.TryGetValue(typeID, out descriptor) ? (IDescriptor) new Descriptor(typeID) : descriptor;
  }
}
