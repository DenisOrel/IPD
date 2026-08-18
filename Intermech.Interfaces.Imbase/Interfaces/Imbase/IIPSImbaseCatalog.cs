// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IIPSImbaseCatalog
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[ComVisible(true)]
[Guid("30166A7D-61C0-45A4-876A-69AD1853C71E")]
public interface IIPSImbaseCatalog
{
  string Name { get; set; }

  IIPSImbaseFolders Folders { get; }

  IIPSImbaseFolder FindFolder(object value, IpsFindObject findBy);

  int GetTableId();
}
