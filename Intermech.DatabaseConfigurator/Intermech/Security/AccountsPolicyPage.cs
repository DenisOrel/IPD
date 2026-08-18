// Decompiled with JetBrains decompiler
// Type: Intermech.Security.AccountsPolicyPage
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Security;

public class AccountsPolicyPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private IServiceProvider _provider;
  private ClassWrapperForPropertyGrid _object;
  private AccountsPolicyPage.AccountsPolicyProperties _accountsProps;

  public AccountsPolicyPage(IServiceProvider provider)
  {
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin)
      return;
    this._provider = provider;
    ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("DatabaseConfigurator_108"), (IPropertyPage) this);
  }

  public string HelpTopicID => "1114";

  public object Control
  {
    get
    {
      if (this._object == null)
      {
        this._accountsProps = new AccountsPolicyPage.AccountsPolicyProperties();
        this._object = new ClassWrapperForPropertyGrid((object) this._accountsProps);
      }
      return (object) this._object;
    }
  }

  public void Apply()
  {
    if (this._accountsProps == null)
      return;
    this._accountsProps.ApplyUpdates();
    this._object.ResetOldValues();
  }

  public void Cancel()
  {
    if (this._accountsProps == null)
      return;
    this._accountsProps._inited = false;
  }

  public PropertyPageType Type => PropertyPageType.Object;

  public string PageName => LocalizationHolder.rm.GetString("DatabaseConfigurator_109");

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  public event EventHandler Changed;

  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  public class AccountsPolicyProperties
  {
    private int _PasswordMinLength;
    private bool _StrongPassword;
    private int _PasswordLifetime;
    private int _PasswordMemory;
    private bool _PasswordUserChange = true;
    public int _PasswordCryptoMethod = 1;
    private int _AccessCacheLifetime;
    internal bool _inited;
    private bool _AccessLevelUp;
    private bool _EnableSecret2Public;
    private bool _IsSaveSearchQueriesHistory;
    private int _WrongPasswordsBeforeDeny;

    private void CheckInited()
    {
      if (this._inited)
        return;
      this.LoadCurrentValues();
      this._inited = true;
    }

    public void LoadCurrentValues()
    {
      IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
      this._StrongPassword = service.ReadBool("KERNEL", "SECURITY", "STRONG_PSW", false, DBConfigMode.GlobalOnly);
      this._PasswordMinLength = Convert.ToInt32(service.ReadInteger("KERNEL", "SECURITY", "PSW_LEN", 0L, DBConfigMode.GlobalOnly));
      this._PasswordLifetime = Convert.ToInt32(service.ReadInteger("KERNEL", "SECURITY", "PSW_LIFETIME", 0L, DBConfigMode.GlobalOnly));
      this._PasswordMemory = Convert.ToInt32(service.ReadInteger("KERNEL", "SECURITY", "PSW_MEM", 0L, DBConfigMode.GlobalOnly));
      this._PasswordUserChange = service.ReadBool("KERNEL", "SECURITY", "PSW_USER", true, DBConfigMode.GlobalOnly);
      this._AccessLevelUp = service.ReadBool("KERNEL", "SECURITY", "ACC_AUTO_UP", false, DBConfigMode.GlobalOnly);
      this._EnableSecret2Public = service.ReadBool("KERNEL", "SECURITY", "SECRET2PUBLIC", false, DBConfigMode.GlobalOnly);
      this._PasswordCryptoMethod = Convert.ToInt32(service.ReadInteger("KERNEL", "SECURITY", "CRYPTO_METHOD", 1L, DBConfigMode.GlobalOnly));
      this._AccessCacheLifetime = Convert.ToInt32(service.ReadInteger("KERNEL", "SECURITY", "ACC_CACHE", 60L, DBConfigMode.GlobalOnly));
      this._WrongPasswordsBeforeDeny = Convert.ToInt32(service.ReadInteger("KERNEL", "SECURITY", "WRONG_PSW_COUNT", 0L, DBConfigMode.GlobalOnly));
      this._IsSaveSearchQueriesHistory = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IGlobalIndexSettings)) as IGlobalIndexSettings).IsSaveSearchQueryHistory;
    }

    public void ApplyUpdates()
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this._StrongPassword && this._PasswordMinLength < 6)
          this._PasswordMinLength = 6;
        IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
        service.WriteBool("KERNEL", "SECURITY", "STRONG_PSW", this._StrongPassword, 0L);
        service.WriteInteger("KERNEL", "SECURITY", "PSW_LEN", (long) this._PasswordMinLength, 0L);
        service.WriteInteger("KERNEL", "SECURITY", "PSW_LIFETIME", Convert.ToInt64(this._PasswordLifetime), 0L);
        service.WriteInteger("KERNEL", "SECURITY", "PSW_MEM", Convert.ToInt64(this._PasswordMemory), 0L);
        service.WriteBool("KERNEL", "SECURITY", "PSW_USER", this._PasswordUserChange, 0L);
        service.WriteBool("KERNEL", "SECURITY", "ACC_AUTO_UP", this._AccessLevelUp, 0L);
        service.WriteBool("KERNEL", "SECURITY", "SECRET2PUBLIC", this._EnableSecret2Public, 0L);
        service.WriteInteger("KERNEL", "SECURITY", "CRYPTO_METHOD", Convert.ToInt64(this._PasswordCryptoMethod), 0L);
        service.WriteInteger("KERNEL", "SECURITY", "ACC_CACHE", Convert.ToInt64(this._AccessCacheLifetime), 0L);
        service.WriteInteger("KERNEL", "SECURITY", "WRONG_PSW_COUNT", (long) this._WrongPasswordsBeforeDeny, 0L);
        IAdminUtilsService customService = sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService;
        customService.SetAccessCacheLifetime(sessionKeeper.Session.SessionGUID, this._AccessCacheLifetime);
        (sessionKeeper.Session.GetCustomService(typeof (IGlobalIndexSettings)) as IGlobalIndexSettings).SetSaveSearchQueryHistoryMode(sessionKeeper.Session.SessionGUID, this._IsSaveSearchQueriesHistory);
        customService.ReloadServerSwitches(sessionKeeper.Session.SessionGUID);
      }
    }

    [CustomDescription("WrongPasswordsBeforeDenyNote")]
    [CustomDisplayName("WrongPasswordsBeforeDeny")]
    public int WrongPasswordsBeforeDeny
    {
      get
      {
        this.CheckInited();
        return this._WrongPasswordsBeforeDeny;
      }
      set => this._WrongPasswordsBeforeDeny = value;
    }

    [CustomDescription("Attribute.DatabaseConfigurator_8")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_9")]
    public int AccessCacheLifetime
    {
      get
      {
        this.CheckInited();
        return this._AccessCacheLifetime;
      }
      set => this._AccessCacheLifetime = value;
    }

    [CustomDescription("Attribute.DatabaseConfigurator_10")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_11")]
    public int PasswordMinLength
    {
      get
      {
        this.CheckInited();
        return this._PasswordMinLength;
      }
      set => this._PasswordMinLength = value;
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("Attribute.DatabaseConfigurator_12")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_13")]
    public bool StrongPassword
    {
      get
      {
        this.CheckInited();
        return this._StrongPassword;
      }
      set => this._StrongPassword = value;
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("Attribute.DatabaseConfigurator_14")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_15")]
    public bool PasswordUserChange
    {
      get
      {
        this.CheckInited();
        return this._PasswordUserChange;
      }
      set => this._PasswordUserChange = value;
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("AccessLevelUpNote")]
    [CustomDisplayName("AccessLevelUpName")]
    public bool AccessLevelUp
    {
      get
      {
        this.CheckInited();
        return this._AccessLevelUp;
      }
      set => this._AccessLevelUp = value;
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("EnableSecret2PublicNote")]
    [CustomDisplayName("EnableSecret2PublicName")]
    public bool EnableSecret2Public
    {
      get
      {
        this.CheckInited();
        return this._EnableSecret2Public;
      }
      set => this._EnableSecret2Public = value;
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("ISSQHistoryNote")]
    [CustomDisplayName("ISSQHistoryName")]
    public bool IsSaveSearchQueriesHistory
    {
      get
      {
        this.CheckInited();
        return this._IsSaveSearchQueriesHistory;
      }
      set => this._IsSaveSearchQueriesHistory = value;
    }

    [CustomDescription("Attribute.DatabaseConfigurator_16")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_17")]
    public int PasswordMemory
    {
      get
      {
        this.CheckInited();
        return this._PasswordMemory;
      }
      set => this._PasswordMemory = value;
    }

    [CustomDescription("Attribute.DatabaseConfigurator_18")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_19")]
    public int PasswordLifetime
    {
      get
      {
        this.CheckInited();
        return this._PasswordLifetime;
      }
      set => this._PasswordLifetime = value;
    }

    [TypeConverter(typeof (CryptoMethodConverter))]
    [CustomDescription("Attribute.DatabaseConfigurator_20")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_21")]
    public int PasswordCryptoMethod
    {
      get
      {
        this.CheckInited();
        return this._PasswordCryptoMethod;
      }
      set => this._PasswordCryptoMethod = value;
    }
  }
}
