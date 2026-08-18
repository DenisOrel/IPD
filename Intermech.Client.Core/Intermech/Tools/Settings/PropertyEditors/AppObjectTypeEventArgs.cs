
// Type: Intermech.Tools.Settings.PropertyEditors.AppObjectTypeEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Tools.Settings.PropertyEditors;

public sealed class AppObjectTypeEventArgs : EventArgs
{
  private GlobalId<int> objType;
  private bool canAdd;

  public AppObjectTypeEventArgs(GlobalId<int> objType, bool canAdd)
  {
    this.objType = objType;
    this.canAdd = canAdd;
  }

  public GlobalId<int> ObjectType => this.objType;

  public bool CanAdd
  {
    get => this.canAdd;
    set => this.canAdd = value;
  }
}
