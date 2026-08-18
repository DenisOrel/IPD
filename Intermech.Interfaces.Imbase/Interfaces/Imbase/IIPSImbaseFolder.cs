// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IIPSImbaseFolder
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[ComVisible(true)]
[Guid("AF89A5D9-F816-4231-826A-E0B57B12858A")]
public interface IIPSImbaseFolder
{
  string Name { get; set; }

  void Delete();

  IIPSImbaseFolders Folders { get; }

  string Note { get; set; }

  int Sort { get; set; }

  void SetImage(string name, object data);

  byte[] GetImage(out string name);

  string Id { get; }

  void GetProperties(out string[] names, out object[] values);

  object GetProperty(object index);

  void SetProperty(object index, object value);

  IIPSImbaseTable AddTable(string tableName);

  int TablesCount { get; }

  string[] GetTableNames();

  IIPSImbaseTable GetTable(object index);

  void RemoveTable(object index);

  void RemoveAllTables();

  int ImageId { get; }

  int Attributes { get; set; }

  long InternalId { get; }
}
