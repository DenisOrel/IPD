// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IIPSImbaseFolders
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[ComVisible(true)]
[Guid("6CFE13EE-3780-413D-9124-919A70F7A350")]
public interface IIPSImbaseFolders
{
  int Count { get; }

  IIPSImbaseFolder Item(object index);

  IIPSImbaseFolder Add(string newName);

  IIPSImbaseFolder FindFolder(object value, IpsFindObject findBy);
}
