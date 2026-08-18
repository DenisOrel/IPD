// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Reflection
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class Reflection
{
  [CanBeNull]
  private static Assembly _systemDataAssembly;

  [NotNull]
  public static Assembly SystemDataAssembly
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Assembly systemDataAssembly = Intermech.Extensions.Reflection._systemDataAssembly;
      if ((object) systemDataAssembly != null)
        return systemDataAssembly;
      Assembly assembly;
      Intermech.Extensions.Reflection._systemDataAssembly = assembly = Assembly.GetAssembly(typeof (DataTable));
      return (object) assembly != null ? assembly : throw new InvalidOperationException("Can`t load assembly \"System.Data\"!");
    }
  }

  public static class DataRowType
  {
    [NotNull]
    private static readonly Type Type = typeof (DataRow);

    public static class Methods
    {
      [CanBeNull]
      private static MethodInfo _getDefaultRecord;

      [NotNull]
      public static MethodInfo GetDefaultRecord
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          MethodInfo getDefaultRecord = Intermech.Extensions.Reflection.DataRowType.Methods._getDefaultRecord;
          if ((object) getDefaultRecord != null)
            return getDefaultRecord;
          MethodInfo method;
          Intermech.Extensions.Reflection.DataRowType.Methods._getDefaultRecord = method = Intermech.Extensions.Reflection.DataRowType.Type.GetMethod(nameof (GetDefaultRecord), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
          return (object) method != null ? method : throw new TypeLoadException($"Can`t find method {Intermech.Extensions.Reflection.DataRowType.Type.FullName}.GetDefaultRecord()");
        }
      }
    }
  }

  public static class DataColumnType
  {
    [NotNull]
    private static readonly Type Type = typeof (DataColumn);

    public static class Fields
    {
      [CanBeNull]
      private static FieldInfo _storage;

      [NotNull]
      public static FieldInfo Storage
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          FieldInfo storage = Intermech.Extensions.Reflection.DataColumnType.Fields._storage;
          if ((object) storage != null)
            return storage;
          FieldInfo field;
          Intermech.Extensions.Reflection.DataColumnType.Fields._storage = field = Intermech.Extensions.Reflection.DataColumnType.Type.GetField("_storage", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
          return (object) field != null ? field : throw new TypeLoadException($"Can`t find field {Intermech.Extensions.Reflection.DataColumnType.Type.FullName}._storage");
        }
      }
    }
  }

  public static class DataStorage
  {
    private const string Prefix = "System.Data.Common.";
    [NotNull]
    private static readonly Type DataStorageType = Intermech.Extensions.Reflection.SystemDataAssembly.GetType("System.Data.Common.DataStorage") ?? throw new TypeLoadException("Can`t find field System.Data.Common..DataStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType BooleanStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType(nameof (BooleanStorageType));
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType CharStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("CharStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SByteStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SByteStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType ByteStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("ByteStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType Int16StorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("Int16Storage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType UInt16StorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("UInt16Storage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType Int32StorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("Int32Storage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType UInt32StorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("UInt32Storage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType Int64StorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("Int64Storage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType UInt64StorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("UInt64Storage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SingleStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SingleStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType DoubleStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("DoubleStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType DecimalStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("DecimalStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType DateTimeStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("DateTimeStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType TimeSpanStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("TimeSpanStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType StringStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("StringStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType DateTimeOffsetStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("DateTimeOffsetStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType BigIntegerStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("BigIntegerStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlBinaryStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlBinaryStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlBooleanStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlBooleanStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlByteStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlByteStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlBytesStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlBytesStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlCharsStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlCharsStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlDateTimeStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlDateTimeStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlDecimalStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlDecimalStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlDoubleStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlDoubleStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlGuidStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlGuidStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlInt16StorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlInt16Storage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlInt32StorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlInt32Storage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlInt64StorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlInt64Storage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlMoneyStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlMoneyStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlSingleStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlSingleStorage");
    [NotNull]
    public static readonly Intermech.Extensions.Reflection.DataStorage.StorageType SqlStringStorageType = new Intermech.Extensions.Reflection.DataStorage.StorageType("SqlStringStorage");

    public static class Fields
    {
      private const string dbNullBits = "dbNullBits";
      [CanBeNull]
      private static FieldInfo _dbNullBits;

      [NotNull]
      public static FieldInfo DBNullBits
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          FieldInfo dbNullBits = Intermech.Extensions.Reflection.DataStorage.Fields._dbNullBits;
          if ((object) dbNullBits != null)
            return dbNullBits;
          FieldInfo field;
          Intermech.Extensions.Reflection.DataStorage.Fields._dbNullBits = field = Intermech.Extensions.Reflection.DataStorage.DataStorageType.GetField("dbNullBits", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
          return (object) field != null ? field : throw new TypeLoadException($"Can`t find field {Intermech.Extensions.Reflection.DataStorage.DataStorageType.FullName}.dbNullBits");
        }
      }
    }

    public class StorageType
    {
      [NotNull]
      [NotWhitespace]
      private readonly string _name;
      private Maybe<Type> _type;
      [CanBeNull]
      private FieldInfo _valuesField;

      public StorageType([NotNull, NotWhitespace] string name) => this._name = name;

      [NotNull]
      public Type Type
      {
        get
        {
          if (this._type.HasValue)
            return this._type.Value;
          string name = "System.Data.Common." + this._name;
          Type type = Intermech.Extensions.Reflection.SystemDataAssembly.GetType(name);
          this._type = !(type == (Type) null) ? new Maybe<Type>(type) : throw new TypeLoadException($"Can`t get type \"{name}\"!");
          return type;
        }
      }

      [NotNull]
      public FieldInfo ValuesField
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          FieldInfo valuesField = this._valuesField;
          if ((object) valuesField != null)
            return valuesField;
          return (this._valuesField = this.Type.GetField("values", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField)) ?? throw new TypeLoadException($"Can`t get type \"System.Data.Common.{this._name}\"!");
        }
      }
    }
  }
}
