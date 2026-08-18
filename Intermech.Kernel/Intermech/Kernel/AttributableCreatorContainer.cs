// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.AttributableCreatorContainer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

public abstract class AttributableCreatorContainer : CreatorContainer
{
  protected abstract string SystemTableName { get; }

  protected abstract string KeyFieldName { get; }

  public DataTable GetMainTable(IUserSession uSession, long[] identifiers, string notFoundMessage)
  {
    IDbManager db = (uSession as UserSession).DataManager;
    DataTable resultTable = (DataTable) null;
    StringBuilder sb = new StringBuilder();
    List<IDbDataParameter> dbDataParameters = new List<IDbDataParameter>();
    int index1 = 0;
    int num = 0;
    for (; index1 < identifiers.Length; ++index1)
    {
      dbDataParameters.Add(db.Parameter("p" + index1.ToString(), (object) identifiers[index1]));
      sb.AppendFormat(":p{0},", (object) index1);
      if (++num >= db.DataProvider.MaximumINOperands)
      {
        FillResultTable();
        num = 0;
      }
    }
    if (dbDataParameters.Count > 0)
      FillResultTable();
    if (notFoundMessage != string.Empty && resultTable.Rows.Count < identifiers.Length)
    {
      resultTable.PrimaryKey = new DataColumn[1]
      {
        resultTable.Columns[this.KeyFieldName]
      };
      for (int index2 = 0; index2 < identifiers.Length; ++index2)
      {
        if (resultTable.Rows.Find((object) identifiers[index2]) == null)
          throw new KernelException(string.Format(notFoundMessage, (object) identifiers[index2]));
      }
    }
    return resultTable;

    void FillResultTable()
    {
      --sb.Length;
      DataTable dataTable = db.ExecuteDataTable($"SELECT * FROM {this.SystemTableName} WHERE {this.KeyFieldName} IN ({sb.ToString()})", dbDataParameters.ToArray());
      sb.Clear();
      dbDataParameters.Clear();
      if (resultTable == null)
        resultTable = dataTable;
      else
        SqlHelper.AssignRows(resultTable, (IEnumerable<DataRow>) dataTable.Select());
    }
  }
}
