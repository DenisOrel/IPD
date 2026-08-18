// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AuthFileReplaceEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

[Serializable]
public class AuthFileReplaceEventArgs : EventArgs
{
  /// <summary>
  /// Yes = DialogResult.Yes
  /// YesForAll = DialogResult.OK
  /// No = DialogResult.No
  /// Cancel = DialogResult.Cancel
  /// </summary>
  public DialogResult WhatDo;
  private long objectId;
  private string authFile = string.Empty;

  public long ObjectId => this.objectId;

  public string AuthFile => this.authFile;

  public AuthFileReplaceEventArgs(long objectId, string authFile, DialogResult whatDo)
  {
    this.objectId = objectId;
    this.authFile = authFile;
    this.WhatDo = whatDo;
  }
}
