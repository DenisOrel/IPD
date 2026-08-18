
// Type: Intermech.Navigator.Controls.ClassifierSelectedEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using System;


namespace Intermech.Navigator.Controls;

public class ClassifierSelectedEventArgs : EventArgs
{
  public IDBObjectID SelectionID { get; }

  public bool EnableClassify { get; }

  public ClassifierSelectedEventArgs(IDBObjectID selectionID, bool enableClassify)
  {
    this.SelectionID = selectionID;
    this.EnableClassify = enableClassify;
  }
}
