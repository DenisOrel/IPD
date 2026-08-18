// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.DataFilter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Data;
using System.Reflection;

#nullable disable
namespace GridViewExtensions;

public class DataFilter
{
  private static Type _internalDataFilterType = typeof (DataTable).Assembly.GetType("System.Data.DataExpression");
  private static ConstructorInfo _constructorInfo = DataFilter._internalDataFilterType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, new Type[2]
  {
    typeof (DataTable),
    typeof (string)
  }, (ParameterModifier[]) null);
  private static MethodInfo _methodInvokeInfo = DataFilter._internalDataFilterType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public, (Binder) null, new Type[2]
  {
    typeof (DataRow),
    typeof (DataRowVersion)
  }, (ParameterModifier[]) null);
  private object _internalDataFilter;

  public DataFilter(string expression, DataTable dataTable)
  {
    this._internalDataFilter = DataFilter._constructorInfo.Invoke(new object[2]
    {
      (object) dataTable,
      (object) expression
    });
  }

  public bool Invoke(DataRow row) => this.Invoke(row, DataRowVersion.Default);

  public bool Invoke(DataRow row, DataRowVersion version)
  {
    return (bool) DataFilter._methodInvokeInfo.Invoke(this._internalDataFilter, new object[2]
    {
      (object) row,
      (object) version
    });
  }
}
