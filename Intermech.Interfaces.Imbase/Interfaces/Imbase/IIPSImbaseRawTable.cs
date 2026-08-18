// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IIPSImbaseRawTable
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[ComVisible(true)]
[Guid("1120879F-88C4-41F2-B0BB-E805755D6ABE")]
public interface IIPSImbaseRawTable
{
  int Count { get; }

  int Eof { get; }

  void First();

  void Last();

  void Next();

  void Prev();

  void Append();

  void Delete();

  void SetValue(object index, object value);

  object GetValue(object index);

  void Close();

  void Post();

  void Edit();

  void Open();

  string Name { get; }

  string TableName { get; }

  void DeleteTable();
}
