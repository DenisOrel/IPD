// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.FileStorages.DataVaultServiceWork
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;


namespace Intermech.Kernel.FileStorages;

public class DataVaultServiceWork : LongLifeObject, IDataVaultServiceWork
{
  private DataVaultServiceWork.StoragesExistsState state = DataVaultServiceWork.StoragesExistsState.Unknown;

  public void ResetStoragesExistState()
  {
    this.state = DataVaultServiceWork.StoragesExistsState.Unknown;
  }

  public void SetStorageExistsState()
  {
    this.state = DataVaultServiceWork.StoragesExistsState.Exist;
  }

  public bool IsDataVaultStorageExists
  {
    get
    {
      if (this.state == DataVaultServiceWork.StoragesExistsState.Unknown)
      {
        IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
        IUserSession userSession = (IUserSession) null;
        try
        {
          userSession = service.GetSystemSessionTemporaryClone("IPS.DVS");
          IDBObjectCollection objectCollection = userSession.GetObjectCollection(new Guid("cad00014-306c-11d8-b4e9-00304f19f545"));
          object[] columns = new object[1]{ (object) -2 };
          ConditionStructure[] conditions = new ConditionStructure[1]
          {
            new ConditionStructure(new Guid("cad00000-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) "Intermech Document Server", LogicalOperators.NONE, 0)
          };
          this.state = Convert.ToBoolean(objectCollection.Select(new DBRecordSetParams(conditions, columns)).Rows.Count) ? DataVaultServiceWork.StoragesExistsState.Exist : DataVaultServiceWork.StoragesExistsState.NonExist;
        }
        finally
        {
          userSession?.Logout("IPS.DVS");
        }
      }
      return Convert.ToBoolean((object) this.state);
    }
  }

  private enum StoragesExistsState
  {
    NonExist,
    Exist,
    Unknown,
  }
}
