// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.TreeNodeExtended
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Controls;

public class TreeNodeExtended : TreeNodeExtended<TreeNodeExtended>
{
  protected TreeNodeExtended()
  {
  }

  protected TreeNodeExtended([NotNull] string text)
    : base(text)
  {
  }

  protected TreeNodeExtended([NotNull] string text, [CanBeEmpty] int imageIndex)
    : base(text, imageIndex)
  {
  }

  protected TreeNodeExtended([NotNull] SerializationInfo serializationInfo, StreamingContext context)
    : base(serializationInfo, context)
  {
  }
}
