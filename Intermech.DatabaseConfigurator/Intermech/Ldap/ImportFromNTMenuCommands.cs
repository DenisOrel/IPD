// Decompiled with JetBrains decompiler
// Type: Intermech.Ldap.ImportFromNTMenuCommands
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Ldap;

internal class ImportFromNTMenuCommands
{
  public static void Import(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    using (ImportFromNTDomainForm fromNtDomainForm = new ImportFromNTDomainForm((items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value))
    {
      int num = (int) fromNtDomainForm.ShowDialog();
    }
  }
}
