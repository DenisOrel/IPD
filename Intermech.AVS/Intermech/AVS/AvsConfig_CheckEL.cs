// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsConfig_CheckEL
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.AVSConfig;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.AVS;

/// <summary> Настройки AVS для проверки полей Перечня Элементов</summary>
[Serializable]
public sealed class AvsConfig_CheckEL : AvsConfig
{
  private static readonly AVSCheckType DefaultChecks = AVSCheckType.None;
  internal static readonly int CheckIsOnFlag = 134217728 /*0x08000000*/;

  public AvsConfig_CheckEL()
  {
    AvsSettingFlagsSection settingFlagsSection = new AvsSettingFlagsSection();
    settingFlagsSection.SectionName = "AvsCheckSettingsEL";
    this.Model = (AvsSettingsSection) settingFlagsSection;
    AVSConfigTypeDescriptor configTypeDescriptor = new AVSConfigTypeDescriptor((AvsConfig) this);
    configTypeDescriptor.GetReadOnly += new ClassWrapperForPropertyGrid.OnGetReadOnly(((AvsConfig) this).OnGetIsReadOnly);
    this.wrapperForPropertyGrid = (object) configTypeDescriptor;
    this.RegisterPropertyPageItem(nameof (CheckElementListBeforeClose), typeof (bool), "Запускать процесс проверки перед закрытием", "Проверять ли поля спецификации перед её закрытием", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (EmptyCount), typeof (bool), "Проверять пустые значения \"Количество\"", "Проверять пустые значения \"Количество\"", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (EmptyPositionDesignation), typeof (bool), "Проверять пустые позиционные обозначения", "Проверять пустые позиционные обозначения", (object) false, typeof (CustomBooleanConverter));
    this.Model[nameof (EnabledChecks)] = (object) new SettingData((object) (int) AvsConfig_CheckEL.DefaultChecks, typeof (int), false);
  }

  /// <summary>Запускать ли процесс проверки перед закрытием</summary>
  public bool CheckElementListBeforeClose
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (CheckElementListBeforeClose)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (CheckElementListBeforeClose));
  }

  /// <summary>Проверять пустые значения "Количество"</summary>
  public bool EmptyCount
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (EmptyCount)) ?? false;
    set => this.SetModelValue((object) value, nameof (EmptyCount));
  }

  /// <summary>Проверять пустые позиционные обозначения</summary>
  public bool EmptyPositionDesignation
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (EmptyPositionDesignation)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (EmptyPositionDesignation));
  }

  public AVSCheckType EnabledChecks
  {
    [DebuggerStepThrough] get
    {
      object modelValue = base.GetModelValue(nameof (EnabledChecks));
      AVSCheckType result;
      return modelValue != null && Enum.TryParse<AVSCheckType>(modelValue.ToString(), out result) ? result : AvsConfig_CheckEL.DefaultChecks;
    }
    set => base.SetModelValue((object) (int) value, nameof (EnabledChecks));
  }

  /// <summary>Сохранить настройки в конфиг клиента IPS</summary>
  /// <param name="config">Раздел AVS в конфигурации клиента IPS</param>
  public override void SaveToConfigurationManager(IConfiguration config)
  {
    base.SaveToConfigurationManager(config);
    config.SetProperty("EmptyCount", AvsConfig.CheckEL.EmptyCount.ToString());
    config.SetProperty("EmptyPositionDesignation", AvsConfig.CheckEL.EmptyPositionDesignation.ToString());
  }

  /// <summary>Загрузить настройки из конфига клиента IPS</summary>
  /// <param name="config">Раздел AVS в конфигурации клиента IPS</param>
  public override void LoadFromConfigurationManager(IConfiguration config)
  {
    base.LoadFromConfigurationManager(config);
    foreach (string name in AvsConfig.CheckEL.Model.Names)
    {
      string property = config.GetProperty(name);
      if (!string.IsNullOrEmpty(property))
        AvsConfig.CheckEL.SetModelValue((object) property, name);
    }
  }

  [Browsable(false)]
  public override string PageName
  {
    [DebuggerStepThrough] get => base.PageName + "\\Перечень элементов - проверка при закрытии";
  }

  public override object this[string sname]
  {
    get
    {
      return !this.uiproperties.ContainsKey(sname) || this.uiproperties[sname] == null ? this.GetModelValue(sname) : this.uiproperties[sname];
    }
    set => this.uiproperties[sname] = value;
  }

  public override object GetModelValue([CallerMemberName] string propName = "")
  {
    AVSCheckType result;
    if (Enum.TryParse<AVSCheckType>(propName, true, out result))
      return (object) ((this.EnabledChecks & result) != 0);
    return propName.Equals("CheckElementListBeforeClose") && base.GetModelValue(propName) is int modelValue ? (object) ((modelValue & AvsConfig_CheckEL.CheckIsOnFlag) != 0) : (object) null;
  }

  public override void SetModelValue(object value, [CallerMemberName] string propName = "")
  {
    object obj1;
    if (!((obj1 = value) is bool))
      return;
    bool flag = (bool) obj1;
    AVSCheckType result;
    if (Enum.TryParse<AVSCheckType>(propName, true, out result))
    {
      this.EnabledChecks = flag ? this.EnabledChecks | result : this.EnabledChecks & ~result;
      this.uiproperties[propName] = (object) null;
    }
    else
    {
      if (!propName.Equals("CheckElementListBeforeClose"))
        return;
      object modelValue = base.GetModelValue(propName);
      int num = 0;
      object obj2 = modelValue;
      if (modelValue is int)
        num = (int) obj2;
      base.SetModelValue((object) (flag ? num | AvsConfig_CheckEL.CheckIsOnFlag : num & ~AvsConfig_CheckEL.CheckIsOnFlag), propName);
      this.uiproperties[propName] = (object) null;
    }
  }

  public override void Apply()
  {
    foreach (string str in this.uiproperties.Keys.ToList<string>())
    {
      if (this.uiproperties[str] != null)
      {
        this.SetModelValue(this.uiproperties[str], str);
        this.uiproperties[str] = (object) null;
      }
    }
    AvsConfig.General.CheckElementListBeforeClose = this.CheckElementListBeforeClose;
    this.OnChanged();
  }
}
