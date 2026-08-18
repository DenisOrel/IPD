// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.HelperClasses.AVSRowErrorMessage
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.UI;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.HelperClasses;

internal class AVSRowErrorMessage : ImErrorMessage
{
  private AVSRow row;
  private SpecRowCheckMessage message;

  public AVSCheckType ErrorType
  {
    get
    {
      SpecRowCheckMessage message = this.message;
      return message == null ? AVSCheckType.None : message.CheckType;
    }
  }

  public AVSRowErrorMessage(AVSRow row, SpecRowCheckMessage message)
  {
    this.row = row;
    this.message = message;
    string str = message.CheckMessage;
    if (string.IsNullOrEmpty(str))
    {
      str = EnumDescConverter.GetEnumDescription((Enum) message.CheckType);
      if (string.IsNullOrEmpty(str))
        str = "Неизвестная ошибка";
    }
    this.Text = $"{str}: {row.ObjCaption}";
  }

  public static List<ImErrorMessage> CreateMessages(
    Dictionary<AVSRow, List<SpecRowCheckMessage>> messages)
  {
    List<ImErrorMessage> messages1 = new List<ImErrorMessage>();
    foreach (KeyValuePair<AVSRow, List<SpecRowCheckMessage>> message1 in messages)
    {
      foreach (SpecRowCheckMessage message2 in message1.Value)
        messages1.Add((ImErrorMessage) new AVSRowErrorMessage(message1.Key, message2));
    }
    return messages1;
  }

  public override void DoubleClick()
  {
    this.row.avsDocument.AVSWindow.Activate();
    this.row.avsDocument.AVSWindow.Activated();
    if (this.message != null && this.message.Attr != null)
    {
      this.row.avsDocument.SetFocusTo(this.row, this.message.Attr, this.message.ProductIndex);
    }
    else
    {
      AVSWindow avsWindow = this.row.avsDocument.AVSWindow;
      List<AVSRow> selectedSpecRows = new List<AVSRow>();
      selectedSpecRows.Add(this.row);
      AVSRow row = this.row;
      avsWindow.RestoreSelection(selectedSpecRows, row);
    }
  }
}
