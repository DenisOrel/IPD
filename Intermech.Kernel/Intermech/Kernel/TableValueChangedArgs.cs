// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.TableValueChangedArgs
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;


namespace Intermech.Kernel;

internal class TableValueChangedArgs : TableChangedEventArgs
{
  public string FilterStr;
  public string TableName;
  public string FieldName;
  public object[] OldValues;
  public object NewValue;

  public TableValueChangedArgs(
    IUserSession session,
    string filterStr,
    string tableName,
    string fieldName)
    : this(session, filterStr, tableName, fieldName, (object[]) null, (object) null)
  {
  }

  public TableValueChangedArgs(
    IUserSession session,
    string filterStr,
    string tableName,
    string fieldName,
    object[] oldValues,
    object newValue)
    : base(TableChangedEventNames.Change, session)
  {
    this.FilterStr = filterStr;
    this.FieldName = fieldName;
    this.TableName = tableName;
    this.OldValues = oldValues;
    this.NewValue = newValue;
  }
}
