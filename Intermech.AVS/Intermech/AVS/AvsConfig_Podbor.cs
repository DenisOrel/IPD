// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsConfig_Podbor
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

/// <summary> Настройки AVS для работы с подборами</summary>
[Serializable]
public sealed class AvsConfig_Podbor : AvsConfig
{
  public AvsConfig_Podbor()
  {
    this.Model = new AvsSettingsSection()
    {
      SectionName = "AvsPodborSettings"
    };
    AVSConfigTypeDescriptor configTypeDescriptor = new AVSConfigTypeDescriptor((AvsConfig) this);
    configTypeDescriptor.GetReadOnly += new ClassWrapperForPropertyGrid.OnGetReadOnly(((AvsConfig) this).OnGetIsReadOnly);
    this.wrapperForPropertyGrid = (object) configTypeDescriptor;
    this.RegisterPropertyPageItem(nameof (ShowPodbor), typeof (bool), "Показывать поле \"Подбор\"", "Требуется ли показывать поле \"Подбор\" в диалогах AVS", (object) true, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (InsertStarAfterPositionDesignationInPE), typeof (bool), "Выводить символ «*» в Перечне элементов", "Выводить символ «*» рядом с Позиционным обозначением основного компонента в Перечне элементов", (object) true, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (InsertStarAfterPositionDesignationInSP), typeof (bool), "Выводить символ «*» в Спецификации", "Выводить символ «*» рядом с Позиционным обозначением основного компонента в Спецификации", (object) true, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (SymbolAfterPosDesignationGetFromCAD), typeof (bool), "Символы дополняющие позиционное обозначение берутся со схемы", "Если компонент пришел в состав из CAD-системы, то символы дополняющие позиционное обозначение должны браться из свойств компонента на схеме", (object) true, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (SummarizePartsForPodbor), typeof (bool), "Суммировать компоненты, которые определяются при регулировании", "По данной настройке при значение «Да» - оформление по ГОСТ2.413-72, при значении «Нет» записи с подборными компонентами следуют за основным компонентом", (object) true, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (TextInNoteFieldOfPodborRow), typeof (string), "Текст для графы \"Примечание\" у компонента для подбора", "В графу \"Примечание\" спецификации у компонента для подбора вписывать заданное слово", (object) "Подбор");
  }

  /// <summary>Показывать флаг Подбор в диалогах</summary>
  public bool ShowPodbor
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (ShowPodbor)) ?? false;
    set => this.SetModelValue((object) value, nameof (ShowPodbor));
  }

  /// <summary>Выводить символ «*» в Перечне элементов</summary>
  public bool InsertStarAfterPositionDesignationInPE
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (InsertStarAfterPositionDesignationInPE)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (InsertStarAfterPositionDesignationInPE));
  }

  /// <summary>Выводить символ «*» в Спецификации</summary>
  public bool InsertStarAfterPositionDesignationInSP
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (InsertStarAfterPositionDesignationInSP)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (InsertStarAfterPositionDesignationInSP));
  }

  /// <summary>Символы дополняющие позиционное обозначение берутся со схемы</summary>
  public bool SymbolAfterPosDesignationGetFromCAD
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (SymbolAfterPosDesignationGetFromCAD)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (SymbolAfterPosDesignationGetFromCAD));
  }

  /// <summary>Суммировать компоненты, которые определяются при регулировании</summary>
  public bool SummarizePartsForPodbor
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (SummarizePartsForPodbor)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (SummarizePartsForPodbor));
  }

  /// <summary>Текст для графы "Примечание" у подборного компонента</summary>
  public string TextInNoteFieldOfPodborRow
  {
    [DebuggerStepThrough] get => (string) this.GetModelValue(nameof (TextInNoteFieldOfPodborRow));
    set => this.SetModelValue((object) value, nameof (TextInNoteFieldOfPodborRow));
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
    [DebuggerStepThrough] get => base.PageName + "\\Работа с подборами";
  }
}
