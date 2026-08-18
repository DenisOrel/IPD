// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.DeletedRecord
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class DeletedRecord
{
  internal int _index;
  internal long _key;
  internal DataRow _row;

  internal DeletedRecord(DataRow row, long key)
  {
    this._row = row;
    this._key = key;
    this._index = row.Table.Rows.IndexOf(row);
  }

  internal static DeletedRecord FindRowRecord(long key, List<DeletedRecord> list)
  {
    foreach (DeletedRecord rowRecord in list)
    {
      if (rowRecord._key == key)
        return rowRecord;
    }
    return (DeletedRecord) null;
  }
}
