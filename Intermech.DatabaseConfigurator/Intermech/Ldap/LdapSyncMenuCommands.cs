// Decompiled with JetBrains decompiler
// Type: Intermech.Ldap.LdapSyncMenuCommands
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Ldap;

internal class LdapSyncMenuCommands
{
  public static void Sync(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    using (LdapSyncConfigForm ldapSyncConfigForm = new LdapSyncConfigForm())
    {
      int num = (int) ldapSyncConfigForm.ExecuteDialog();
    }
  }
}
