// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.TableValueAddedArgs
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;


namespace Intermech.Kernel;

internal class TableValueAddedArgs : TableChangedEventArgs
{
  public string TableName;
  public DataRow NewRow;

  public TableValueAddedArgs(IUserSession session, string tableName, DataRow newRow)
    : base(TableChangedEventNames.Add, session)
  {
    this.TableName = tableName;
    this.NewRow = newRow;
  }
}
