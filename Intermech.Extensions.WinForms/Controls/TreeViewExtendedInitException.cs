// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.TreeViewExtendedInitException
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Controls;

[Serializable]
public class TreeViewExtendedInitException : Exception, ISerializable
{
  private const string DefaultMessage = "Ошибка инициализации дерева";

  public TreeViewExtendedInitException()
    : base("Ошибка инициализации дерева")
  {
  }

  public TreeViewExtendedInitException([NotNull, NotWhitespace] string message)
    : base(message)
  {
  }

  public TreeViewExtendedInitException([NotNull, NotWhitespace] string message, [NotNull] Exception innerException)
    : base(message, innerException)
  {
  }

  public TreeViewExtendedInitException([NotNull] Exception innerException)
    : base("Ошибка инициализации дерева", innerException)
  {
  }

  protected TreeViewExtendedInitException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
