// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.LdapProperties
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Interfaces;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

internal class LdapProperties
{
  private string _DefaultCatalog = string.Empty;
  private bool _LdapDeveloperMode;
  internal bool _inited;

  internal void ApplyUpdates()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.Configurations.WriteBool("CLIENT", "LDAP", "LDAP_DEVELOPER_MODE", this._LdapDeveloperMode, 0L);
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        return;
      HybridDictionary catalogsAndExclusionUsers;
      customService.SynchronizeDirectoryReadConfig(sessionKeeper.Session.SessionGUID, out string _, out catalogsAndExclusionUsers);
      if (customService.SynchronizeDirectoryWriteConfig(sessionKeeper.Session.SessionGUID, this._DefaultCatalog, catalogsAndExclusionUsers, false) == 0)
        return;
      int num = (int) IMMessageBox.Show("Ошибка", "Ошибка сохранения настроек синхронизации с каталогами", MessageBoxButtons.OK, IMMessageBoxImage.Error);
    }
  }

  internal void LoadCurrentValues()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._LdapDeveloperMode = sessionKeeper.Session.Configurations.ReadBool("CLIENT", "LDAP", "LDAP_DEVELOPER_MODE", false, DBConfigMode.GlobalOnly);
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        return;
      string defaultCatalog;
      customService.SynchronizeDirectoryReadConfig(sessionKeeper.Session.SessionGUID, out defaultCatalog, out HybridDictionary _);
      this._DefaultCatalog = defaultCatalog;
    }
  }

  private void CheckInited()
  {
    if (this._inited)
      return;
    this.LoadCurrentValues();
    this._inited = true;
  }

  [Description("Режим разработчика (расширенное логирование)")]
  [DisplayName("Режим разработчика")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  [DefaultValue(false)]
  public bool LdapDeveloperMode
  {
    get
    {
      this.CheckInited();
      return this._LdapDeveloperMode;
    }
    set => this._LdapDeveloperMode = value;
  }

  [Description("Имена пользователей для каталога по умолчанию синхронизируются без постфиксов. Для остальных каталогов к именам пользователей при синхронизации добавляется @<имя каталога>. Не рекомендуется производить изменение каталога по умолчанию без крайней необходимости")]
  [DisplayName("Каталог по умолчанию")]
  [TypeConverter(typeof (LdapTypeConverter))]
  [DefaultValue("")]
  public string DefaultCatalog
  {
    get
    {
      this.CheckInited();
      return this._DefaultCatalog;
    }
    set => this._DefaultCatalog = value;
  }
}
