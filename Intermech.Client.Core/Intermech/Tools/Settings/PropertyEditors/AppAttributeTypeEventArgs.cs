
// Type: Intermech.Tools.Settings.PropertyEditors.AppAttributeTypeEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Tools.Settings.PropertyEditors;

public sealed class AppAttributeTypeEventArgs : EventArgs
{
  private GlobalId<int> attrType;
  private bool canAdd;

  public AppAttributeTypeEventArgs(GlobalId<int> attrType, bool canAdd)
  {
    this.attrType = attrType;
    this.canAdd = canAdd;
  }

  public GlobalId<int> AttributeType => this.attrType;

  public bool CanAdd
  {
    get => this.canAdd;
    set => this.canAdd = value;
  }
}
