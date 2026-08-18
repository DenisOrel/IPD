// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ToolStripMenuItemExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions;

public static class ToolStripMenuItemExtensions
{
  [NotNull]
  public static T Clone<T>([NotNull] this T item) where T : ToolStripMenuItem
  {
    IFormatter formatter = (IFormatter) new BinaryFormatter();
    Stream serializationStream = (Stream) new MemoryStream();
    using (serializationStream)
    {
      formatter.Serialize(serializationStream, (object) item);
      serializationStream.Seek(0L, SeekOrigin.Begin);
      return (T) formatter.Deserialize(serializationStream) ?? throw new NullReferenceException("Result of Clone");
    }
  }
}
