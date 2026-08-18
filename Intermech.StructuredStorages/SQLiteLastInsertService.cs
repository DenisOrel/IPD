// Decompiled with JetBrains decompiler
// Type: Intermech.Data.SQLite.SQLiteLastInsertService
// Assembly: Intermech.StructuredStorages, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8A874F4F-FB0A-412D-88F5-D43E1009C2E5
// Assembly location: D:\IPS\Client\Intermech.StructuredStorages.dll
// XML documentation location: D:\IPS\Client\Intermech.StructuredStorages.xml

using System;
using System.Data;
using System.Data.SQLite;


namespace Intermech.Data.SQLite
{
    internal sealed class SQLiteLastInsertService : ISqlLastInsertService
    {
      public long LastInsertRowId(IDbCommand command)
      {
        if (command == null)
          throw new ArgumentNullException(nameof (command));
        if (command.Connection == null)
          throw new InvalidOperationException();
        return ((SQLiteConnection) command.Connection).LastInsertRowId;
      }
    }
}
