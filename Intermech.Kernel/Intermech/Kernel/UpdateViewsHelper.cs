// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UpdateViewsHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using Intermech.Pools;
using Intermech.Text;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

internal class UpdateViewsHelper
{
  private Dictionary<UpdateViewKey, UpdateViewFieldValues> _UpdateDict;
  private IDbManager DB;

  public UpdateViewsHelper(IDbManager db)
  {
    this.DB = db;
    this._UpdateDict = new Dictionary<UpdateViewKey, UpdateViewFieldValues>();
  }

  public void AddData(string viewName, long objID, string keyFld, object value, string fldName)
  {
    UpdateViewKey key = new UpdateViewKey(viewName, objID, keyFld);
    UpdateViewFieldValues updateViewFieldValues;
    if (this._UpdateDict.TryGetValue(key, out updateViewFieldValues))
      updateViewFieldValues.Add(value, fldName);
    else
      this._UpdateDict.Add(key, new UpdateViewFieldValues(value, fldName));
  }

  public void ExecuteSQL()
  {
    if (this._UpdateDict.Count <= 0)
      return;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      int num = 0;
      foreach (KeyValuePair<UpdateViewKey, UpdateViewFieldValues> keyValuePair in this._UpdateDict)
      {
        stringBuilder.Clear();
        stringBuilder.AppendFormat("UPDATE {0} SET ", (object) keyValuePair.Key.ViewName);
        List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
        for (int index = 0; index < keyValuePair.Value.Values.Count; ++index)
        {
          string parameterName = "Value" + index.ToString();
          stringBuilder.AppendFormat("{0} = :{1},", (object) keyValuePair.Value.Values[index].FieldName, (object) parameterName);
          dbDataParameterList.Add(this.DB.Parameter(parameterName, keyValuePair.Value.Values[index].Value));
        }
        --stringBuilder.Length;
        string parameterName1 = "objID" + num++.ToString();
        stringBuilder.AppendFormat(" WHERE {0} = :{1}", (object) keyValuePair.Key.KeyField, (object) parameterName1);
        dbDataParameterList.Add(this.DB.Parameter(parameterName1, (object) keyValuePair.Key.ObjID));
        this.DB.ExecuteNonQuery(stringBuilder.ToString(), dbDataParameterList.ToArray());
      }
    }
    this._UpdateDict.Clear();
  }
}
