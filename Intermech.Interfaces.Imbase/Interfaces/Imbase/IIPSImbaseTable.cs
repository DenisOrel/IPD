// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IIPSImbaseTable
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[ComVisible(true)]
[Guid("99DBE8B1-CE79-4E82-9430-184B7D47F819")]
public interface IIPSImbaseTable
{
  int Count { get; }

  int Eof { get; }

  void First();

  void Last();

  void Next();

  void Prev();

  object GetValue(object index);

  void Close();

  void Open();

  string Name { get; }

  string TableName { get; }

  void GetProperties(out string[] names, out object[] values);

  object GetProperty(object index);

  void SetProperty(object index, object value);

  IIPSImbaseFolder Folder { get; }

  IIPSImbaseRawTable RawTable { get; }
}
