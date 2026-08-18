// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.MessageBoxPictureBox
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.UI.Winforms;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

[Serializable]
public class MessageBoxPictureBox : EnumPictureBox<MessageBoxPictureBox.Picture>
{
  public const MessageBoxPictureBox.Picture DefaultIcon = MessageBoxPictureBox.Picture.Warning;

  public MessageBoxPictureBox()
    : base(MessageBoxPictureBox.Picture.Warning)
  {
  }

  [CanBeNull]
  protected override object GetPictureByEnumValue(MessageBoxPictureBox.Picture enumValue)
  {
    return (object) SystemIcon.Get((MessageBoxIcon) enumValue);
  }

  public enum Picture
  {
    Error = 16, // 0x00000010
    Question = 32, // 0x00000020
    Warning = 48, // 0x00000030
    Information = 64, // 0x00000040
  }
}
