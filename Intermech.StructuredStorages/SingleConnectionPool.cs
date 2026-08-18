// Decompiled with JetBrains decompiler
// Type: Intermech.Data.SQLite.SingleConnectionPool
// Assembly: Intermech.StructuredStorages, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8A874F4F-FB0A-412D-88F5-D43E1009C2E5
// Assembly location: D:\IPS\Client\Intermech.StructuredStorages.dll
// XML documentation location: D:\IPS\Client\Intermech.StructuredStorages.xml

using System.Data.Common;


namespace Intermech.Data.SQLite
{
    internal sealed class SingleConnectionPool(DbProviderFactory factory, string connectionString) : 
      DbConnectionPool(factory, connectionString, 1)
    {
    }
}
