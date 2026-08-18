// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsConfig_CheckSP
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

/// <summary> Настройки AVS для проверки полей спецификации</summary>
[Serializable]
public sealed class AvsConfig_CheckSP : AvsConfig
{
  private static readonly AVSCheckType DefaultChecks = AVSCheckType.EmptyCount | AVSCheckType.EmptyPosition;
  internal static readonly int CheckIsOnFlag = 134217728 /*0x08000000*/;

  public AvsConfig_CheckSP()
  {
    AvsSettingFlagsSection settingFlagsSection = new AvsSettingFlagsSection();
    settingFlagsSection.SectionName = "AvsCheckSettingsSP";
    this.Model = (AvsSettingsSection) settingFlagsSection;
    AVSConfigTypeDescriptor configTypeDescriptor = new AVSConfigTypeDescriptor((AvsConfig) this);
    configTypeDescriptor.GetReadOnly += new ClassWrapperForPropertyGrid.OnGetReadOnly(((AvsConfig) this).OnGetIsReadOnly);
    this.wrapperForPropertyGrid = (object) configTypeDescriptor;
    this.RegisterPropertyPageItem(nameof (CheckSpecificationBeforeClose), typeof (bool), "Запускать процесс проверки перед закрытием", "Проверять ли поля спецификаци перед её закрытием", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (EmptyCount), typeof (bool), "Проверять пустые значения \"Количество\"", "Проверять пустые значения \"Количество\"", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (ObjectWithoutRelation), typeof (bool), "Проверять наличие связи с объектом записи", "Проверять наличие связи с объектом записи", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (EmptyPosition), typeof (bool), "Проверять пустые позиции", "Проверять пустые позиции", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (DuplicatePosition), typeof (bool), "Проверять дублирование позиций", "Проверять дублирование позиций", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (MassCalc), typeof (bool), "Проверять ошибки при расчёте массы", "Проверять ошибки при расчёте массы", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (NotNumberPosition), typeof (bool), "Проверять наличие не числа в позиции", "Проверять наличие не числа в позиции", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (CheckDuplicatePositionDesignation), typeof (bool), "Проверять дублирование позиционного обозначения", "Проверять дублирование позиционного обозначения", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (EmptyCountAllProdFormB), typeof (bool), "Проверять поле «Количество» для всех исполнений формы Б", "Проверять поле «Количество» для всех исполнений формы Б", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (PartWithoutDraft), typeof (bool), "Проверять наличие записи заготовки для детали", "Проверять наличие записи заготовки для существующей записи детали", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (DraftCountDoesntMatch), typeof (bool), "Проверять соответствие графы \"Количество\" заготовки и детали", "Проверять соответствие значения графы \"Количество\" записи заготовки и записи детали", (object) false, typeof (CustomBooleanConverter));
    this.Model[nameof (EnabledChecks)] = (object) new SettingData((object) (int) AvsConfig_CheckSP.DefaultChecks, typeof (int), false);
  }

  /// <summary>Запускать ли процесс проверки перед закрытием</summary>
  public bool CheckSpecificationBeforeClose
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (CheckSpecificationBeforeClose)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (CheckSpecificationBeforeClose));
  }

  /// <summary>Проверять пустые значения "Количество"</summary>
  public bool EmptyCount
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (EmptyCount)) ?? false;
    set => this.SetModelValue((object) value, nameof (EmptyCount));
  }

  /// <summary>Проверять наличие связи с объектом записи</summary>
  public bool ObjectWithoutRelation
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (ObjectWithoutRelation)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (ObjectWithoutRelation));
  }

  /// <summary>Проверять пустые позиции</summary>
  public bool EmptyPosition
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (EmptyPosition)) ?? false;
    set => this.SetModelValue((object) value, nameof (EmptyPosition));
  }

  /// <summary>Проверять дублирование позиций</summary>
  public bool DuplicatePosition
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (DuplicatePosition)) ?? false;
    set => this.SetModelValue((object) value, nameof (DuplicatePosition));
  }

  /// <summary>Проверять ошибки при расчёте массы</summary>
  public bool MassCalc
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (MassCalc)) ?? false;
    set => this.SetModelValue((object) value, nameof (MassCalc));
  }

  /// <summary>Проверять наличие не числа в позиции</summary>
  public bool NotNumberPosition
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (NotNumberPosition)) ?? false;
    set => this.SetModelValue((object) value, nameof (NotNumberPosition));
  }

  /// <summary>Проверять дублирование позиционного обозначения</summary>
  public bool CheckDuplicatePositionDesignation
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (CheckDuplicatePositionDesignation)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (CheckDuplicatePositionDesignation));
  }

  public bool EmptyCountAllProdFormB
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (EmptyCountAllProdFormB)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (EmptyCountAllProdFormB));
  }

  public bool PartWithoutDraft
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (PartWithoutDraft)) ?? false;
    set => this.SetModelValue((object) value, nameof (PartWithoutDraft));
  }

  public bool DraftCountDoesntMatch
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (DraftCountDoesntMatch)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (DraftCountDoesntMatch));
  }

  public AVSCheckType EnabledChecks
  {
    [DebuggerStepThrough] get
    {
      object modelValue = base.GetModelValue(nameof (EnabledChecks));
      AVSCheckType result;
      return modelValue != null && Enum.TryParse<AVSCheckType>(modelValue.ToString(), out result) ? result : AvsConfig_CheckSP.DefaultChecks;
    }
    set => base.SetModelValue((object) (int) value, nameof (EnabledChecks));
  }

  /// <summary>Сохранить настройки в конфиг клиента IPS</summary>
  /// <param name="config">Раздел AVS в конфигурации клиента IPS</param>
  public override void SaveToConfigurationManager(IConfiguration config)
  {
    base.SaveToConfigurationManager(config);
    config.SetProperty("ShowPodbor", AvsConfig.Podbor.ShowPodbor.ToString());
    config.SetProperty("InsertStarAfterPositionDesignationInPE", AvsConfig.Podbor.InsertStarAfterPositionDesignationInPE.ToString());
    config.SetProperty("InsertStarAfterPositionDesignationInSP", AvsConfig.Podbor.InsertStarAfterPositionDesignationInSP.ToString());
    config.SetProperty("SymbolAfterPosDesignationGetFromCAD", AvsConfig.Podbor.SymbolAfterPosDesignationGetFromCAD.ToString());
    config.SetProperty("SummarizePartsForPodbor", AvsConfig.Podbor.SummarizePartsForPodbor.ToString());
    config.SetProperty("TextInNoteFieldOfPodborRow", AvsConfig.Podbor.TextInNoteFieldOfPodborRow);
  }

  /// <summary>Загрузить настройки из конфига клиента IPS</summary>
  /// <param name="config">Раздел AVS в конфигурации клиента IPS</param>
  public override void LoadFromConfigurationManager(IConfiguration config)
  {
    base.LoadFromConfigurationManager(config);
    foreach (string name in AvsConfig.Podbor.Model.Names)
    {
      string property = config.GetProperty(name);
      if (!string.IsNullOrEmpty(property))
        AvsConfig.Podbor.SetModelValue((object) property, name);
    }
  }

  [Browsable(false)]
  public override string PageName
  {
    [DebuggerStepThrough] get => base.PageName + "\\Спецификация - проверка при закрытии";
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
    return propName.Equals("CheckSpecificationBeforeClose") && base.GetModelValue(propName) is int modelValue ? (object) ((modelValue & AvsConfig_CheckSP.CheckIsOnFlag) != 0) : (object) null;
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
      if (!propName.Equals("CheckSpecificationBeforeClose"))
        return;
      object modelValue = base.GetModelValue(propName);
      int num = 0;
      object obj2 = modelValue;
      if (modelValue is int)
        num = (int) obj2;
      base.SetModelValue((object) (flag ? num | AvsConfig_CheckSP.CheckIsOnFlag : num & ~AvsConfig_CheckSP.CheckIsOnFlag), propName);
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
    AvsConfig.General.CheckSpecificationBeforeClose = this.CheckSpecificationBeforeClose;
    this.OnChanged();
  }
}
