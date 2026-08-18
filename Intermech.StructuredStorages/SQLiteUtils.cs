// Decompiled with JetBrains decompiler
// Type: Intermech.Data.SQLite.SQLiteUtils
// Assembly: Intermech.StructuredStorages, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8A874F4F-FB0A-412D-88F5-D43E1009C2E5
// Assembly location: D:\IPS\Client\Intermech.StructuredStorages.dll
// XML documentation location: D:\IPS\Client\Intermech.StructuredStorages.xml

using System;
using System.Data.SQLite;
using System.Runtime.InteropServices;


namespace Intermech.Data.SQLite
{
    public static class SQLiteUtils
    {
      public static string MakeConnectionString(
        string fileName,
        int pageSizeInBytes,
        int cacheSizeInKBytes,
        bool asyncMode)
      {
        if (string.IsNullOrEmpty(fileName))
          throw new ArgumentException("No database file specified.", nameof (fileName));
        if (pageSizeInBytes != 0 && pageSizeInBytes % 512 /*0x0200*/ != 0)
          throw new ArgumentException("Bad page size.", "pageSize");
        SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder();
        connectionStringBuilder.DataSource = fileName;
        connectionStringBuilder.FailIfMissing = true;
        connectionStringBuilder.UseUTF16Encoding = true;
        connectionStringBuilder.DateTimeFormat = SQLiteDateFormats.Ticks;
        connectionStringBuilder.BinaryGUID = true;
        connectionStringBuilder.Pooling = true;
        connectionStringBuilder.Enlist = false;
        connectionStringBuilder.ForeignKeys = false;
        connectionStringBuilder.SyncMode = asyncMode ? SynchronizationModes.Off : SynchronizationModes.Normal;
        if (pageSizeInBytes != 0)
          connectionStringBuilder.PageSize = pageSizeInBytes;
        if (cacheSizeInKBytes != 0)
          connectionStringBuilder.CacheSize = SQLiteUtils.CalcCacheSize(cacheSizeInKBytes, connectionStringBuilder.PageSize);
        connectionStringBuilder.DefaultTimeout = 30;
        return connectionStringBuilder.ToString();
      }

      private static int CalcCacheSize(int kbytes, int pageSizeBytes)
      {
        int num = 1024 /*0x0400*/ * kbytes / pageSizeBytes;
        if (num < 32 /*0x20*/)
          num = 32 /*0x20*/;
        return num;
      }

      public static SQLiteErrorCode EnableSharedCache(bool enableCache)
      {
        return SQLiteFactory.Instance != null ? SQLiteUtils.UnsafeMethods.sqlite3_enable_shared_cache(enableCache ? 1 : 0) : SQLiteErrorCode.Unknown;
      }

      private static class UnsafeMethods
      {
        [DllImport("SQLite.Interop.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern SQLiteErrorCode sqlite3_enable_shared_cache(int mode);
      }
    }
}
