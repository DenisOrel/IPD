// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.NullCollectionException
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace Intermech.Imbase.Controls;

internal class NullCollectionException : Exception
{
  internal string Caption = string.Empty;
  internal string Msg = string.Empty;

  internal NullCollectionException(string caption, string msg)
  {
    this.Caption = caption;
    this.Msg = msg;
  }
}
