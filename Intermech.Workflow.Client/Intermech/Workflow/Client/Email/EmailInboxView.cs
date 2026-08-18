// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Email.EmailInboxView
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Workflow.Client.Email;

internal class EmailInboxView : ChildrenView
{
  private int _imageIndex = -1;
  private string _caption = LocalizationHolder.rm.GetString("Workflow.Client_68");

  public override string Caption => this._caption;

  public override int ImageIndex => this._imageIndex;

  public override ContentType ViewContentType => ContentType.NonFolders;
}
