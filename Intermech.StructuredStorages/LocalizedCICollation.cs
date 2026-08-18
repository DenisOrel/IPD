// Decompiled with JetBrains decompiler
// Type: Intermech.Data.SQLite.LocalizedCICollation
// Assembly: Intermech.StructuredStorages, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8A874F4F-FB0A-412D-88F5-D43E1009C2E5
// Assembly location: D:\IPS\Client\Intermech.StructuredStorages.dll
// XML documentation location: D:\IPS\Client\Intermech.StructuredStorages.xml

using System.Data.SQLite;
using System.Globalization;


namespace Intermech.Data.SQLite
{
    [SQLiteFunction(FuncType = FunctionType.Collation, Name = "LOCALIZED_CI")]
    internal sealed class LocalizedCICollation : SQLiteFunctionEx
    {
      public override int Compare(string x, string y)
      {
        return string.Compare(x, y, CultureInfo.CurrentUICulture, CompareOptions.IgnoreCase);
      }
    }
}
