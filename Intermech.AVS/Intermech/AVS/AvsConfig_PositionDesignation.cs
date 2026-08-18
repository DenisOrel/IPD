// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsConfig_PositionDesignation
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

#nullable disable
namespace Intermech.AVS;

/// <summary>Настройки AVS для работы с Позиционными обозначениями</summary>
[Serializable]
public class AvsConfig_PositionDesignation : AvsConfig
{
  public AvsConfig_PositionDesignation()
  {
    this.Model = new AvsSettingsSection()
    {
      SectionName = "AvsPosDesignationSettings"
    };
    AVSConfigTypeDescriptor configTypeDescriptor = new AVSConfigTypeDescriptor((AvsConfig) this);
    configTypeDescriptor.GetReadOnly += new ClassWrapperForPropertyGrid.OnGetReadOnly(((AvsConfig) this).OnGetIsReadOnly);
    this.wrapperForPropertyGrid = (object) configTypeDescriptor;
    this.RegisterPropertyPageItem(nameof (ShowPosDesignation), typeof (bool), "Показывать поле \"Позиционное обозначение\"", "Требуется ли показывать поле \"Позиционное обозначение\" в диалогах AVS", (object) true, typeof (CustomBooleanConverter), true);
    this.RegisterPropertyPageItem(nameof (IncludeFunctionalGroupInPositionDesignation), typeof (bool), "Формировать составное позиционное обозначение в спецификации", "Включать позиционное обозначение функциональной группы в позиционное обозначение изделия при отображении в спецификации", (object) true, typeof (CustomBooleanConverter), true);
    this.RegisterPropertyPageItem(nameof (SpliterForFunctionalGroupInPositionDesignation), typeof (string), "Разделитель для функциональной группы и позиционного обозначения", "Разделитель для функциональной группы и позиционного обозначения при выводе в спецификацию", (object) "-", isAdmin: true);
    this.RegisterPropertyPageItem(nameof (SpliterForSummPositionDesignation), typeof (string), "Разделитель для суммированного позиционного обозначения", "Разделитель для суммированного позиционного обозначения", (object) "...", isAdmin: true);
  }

  /// <summary>Позиционное обозначение</summary>
  public bool ShowPosDesignation
  {
    get => (bool?) this.GetModelValue(nameof (ShowPosDesignation)) ?? false;
    set => this.SetModelValue((object) value, nameof (ShowPosDesignation));
  }

  /// <summary>Добавлять функциональную группу в позиционное обозначение</summary>
  public bool IncludeFunctionalGroupInPositionDesignation
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (IncludeFunctionalGroupInPositionDesignation)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (IncludeFunctionalGroupInPositionDesignation));
  }

  /// <summary>Разделитель для функциональной группы и позиционного обозначения</summary>
  public string SpliterForFunctionalGroupInPositionDesignation
  {
    [DebuggerStepThrough] get
    {
      return (string) this.GetModelValue(nameof (SpliterForFunctionalGroupInPositionDesignation));
    }
    set
    {
      this.SetModelValue((object) value, nameof (SpliterForFunctionalGroupInPositionDesignation));
    }
  }

  /// <summary>Разделитель для суммированного позиционного обозначения</summary>
  public string SpliterForSummPositionDesignation
  {
    [DebuggerStepThrough] get
    {
      return (string) this.GetModelValue(nameof (SpliterForSummPositionDesignation));
    }
    set => this.SetModelValue((object) value, nameof (SpliterForSummPositionDesignation));
  }

  /// <summary>Сохранить настройки в конфиг клиента IPS</summary>
  /// <param name="config">Раздел AVS в конфигурации клиента IPS</param>
  public override void SaveToConfigurationManager(IConfiguration config)
  {
    base.SaveToConfigurationManager(config);
    config.SetProperty("ShowPosDesignation", AvsConfig.PositionDesignation.ShowPosDesignation.ToString());
    config.SetProperty("IncludeFunctionalGroupInPositionDesignation", AvsConfig.PositionDesignation.IncludeFunctionalGroupInPositionDesignation.ToString());
    config.SetProperty("SpliterForFunctionalGroupInPositionDesignation", AvsConfig.PositionDesignation.SpliterForFunctionalGroupInPositionDesignation);
    config.SetProperty("SpliterForSummPositionDesignation", AvsConfig.PositionDesignation.SpliterForSummPositionDesignation);
  }

  /// <summary>Загрузить настройки из конфига клиента IPS</summary>
  /// <param name="config">Раздел AVS в конфигурации клиента IPS</param>
  public override void LoadFromConfigurationManager(IConfiguration config)
  {
    base.LoadFromConfigurationManager(config);
    foreach (string name in AvsConfig.PositionDesignation.Model.Names)
    {
      string property = config.GetProperty(name);
      if (!string.IsNullOrEmpty(property))
        AvsConfig.PositionDesignation.SetModelValue((object) property, name);
    }
  }

  [Browsable(false)]
  public override string PageName
  {
    [DebuggerStepThrough] get => base.PageName + "\\Позиционные обозначения";
  }
}
