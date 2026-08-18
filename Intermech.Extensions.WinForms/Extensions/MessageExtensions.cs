// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.MessageExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions;

public static class MessageExtensions
{
  [NotNull]
  public static T GetLParam<T>(this Message message) where T : new()
  {
    return (T) message.GetLParam(typeof (T));
  }
}
