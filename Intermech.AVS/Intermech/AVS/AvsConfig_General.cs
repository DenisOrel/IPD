// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsConfig_General
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.AVSConfig;
using Intermech.ComponentModel;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS;

/// <summary>Общие настройки AVS</summary>
[Serializable]
public class AvsConfig_General : AvsConfig
{
  private string zagotovkaDlya = "Заготовка для";
  /// <summary>Использовать в запросах на исполнение по Guid флаг CaseSensitive</summary>
  public bool SelectProductsWithCaseSensitive;

  public AvsConfig_General()
  {
    this.Model = new AvsSettingsSection()
    {
      SectionName = "AvsGeneralSettings"
    };
    AVSConfigTypeDescriptor configTypeDescriptor = new AVSConfigTypeDescriptor((AvsConfig) this);
    configTypeDescriptor.GetReadOnly += new ClassWrapperForPropertyGrid.OnGetReadOnly(((AvsConfig) this).OnGetIsReadOnly);
    this.wrapperForPropertyGrid = (object) configTypeDescriptor;
    this.RegisterPropertyPageItem(nameof (FormatSize), typeof (int), "Длина Формата", "Перенос графы \"Формат\" в графу \"Примечание\" при количестве символов более заданного значения", (object) 3);
    this.RegisterPropertyPageItem(nameof (ShowScan), typeof (bool), "Сканированные спецификации", "Разрешить создание сканированных спецификаций", (object) true, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (ShowEvents), typeof (bool), "Показывать изменения в документе при загрузке", "Показывать сообщения об изменениях в документе во время загрузки", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (AllowNoteForSpecRowName), typeof (bool), "Дополнительная информация в графе \"Наименование\"", "Допускается заносить дополнительную информацию в графу \"Наименование\"", (object) true, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (AskUserForOldSPIniFile), typeof (bool), "Запрашивать у пользователя положение настроек при импорте SP файла", "Если при импорте SP файла не найден заданный в нём файл INI, то запрашивать у пользователя положение настроек", (object) true, typeof (CustomBooleanConverter), true);
    this.RegisterPropertyPageItem(nameof (AdditionalChaptersInDataChapter), typeof (bool), "Дополнительные части внутри общих и переменных данных", "Дополнительные части должны находиться внутри общих данных и внутри исполнений", (object) true, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (ZoneSize), typeof (int), "Длина Зоны", "Перенос графы \"Зона\" в графу \"Примечание\" при количестве символов более заданного значения", (object) 3);
    this.RegisterPropertyPageItem(nameof (AutoSort), typeof (bool), "Автоматическая сортировка", "Автоматическая сортировка записей при вставке и редактировании, для сортировки ранее добавленных записей нужно вызвать команду ручной сортировки", (object) true, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (DisableSortPageView), typeof (bool), "Запрет перетаскивания объектов в страничном виде СП", "Запрет перетаскивания объектов мышью в страничном виде СП", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (AddToCurrentGroup), typeof (bool), "Добавлять в текущий набор исполнений", "Добавлять записи в текущий набор исполнений  в спецификации формы Б и В", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (AutoUpdateTemplate), typeof (bool), "Обновлять шаблон документа при открытии", "Обновлять шаблон документа при открытии на редактирование", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (PatchStampReferences), typeof (bool), "Исправлять ссылки в полях основной надписи", "Исправлять ссылки в полях основной надписи старых документов", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (PatchLriId), typeof (bool), "Исправлять идентификаторы в ЛРИ", "Исправлять идентификаторы в ЛРИ старых документов", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (AskRenumber), typeof (bool), "Подтверждение нумерации", "Спрашивать подтверждение при установке\\удалении нумерации позиций", (object) true, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (AskAVS6), typeof (bool), "Чтение документов AVS6", "Отображать команды чтения документов (ведомости, таблицы), разработанных в программе AVS6", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (DefaultSpecificationType), typeof (AVSSpecificationType), "Тип спецификации по умолчанию", "Тип спецификации по умолчанию", (object) AVSSpecificationType.ESKD, typeof (EnumCustomConverter));
    this.RegisterPropertyPageItem(nameof (CreateUndo), typeof (CreateUndoEnum), "Откат изменений", "Запоминать состав спецификации для последующего отката", (object) CreateUndoEnum.No, typeof (EnumCustomConverter));
    this.RegisterPropertyPageItem(nameof (UpdateModeInReadOnly), typeof (UpdateModeInReadOnlyEnum), "Обновлять документ в режиме только для чтения", "Обновлять документ в режиме только для чтения", (object) UpdateModeInReadOnlyEnum.No, typeof (EnumCustomConverter), true);
    this.RegisterPropertyPageItem(nameof (DefaultSpecificationForm), typeof (DefaultGroupSpecificationForm), "Форма групповой спецификации по умолчанию", "Форма групповой спецификации по умолчанию при создании спецификации для сборочной единицы с несколькими исполнениями", (object) DefaultGroupSpecificationForm.A, typeof (EnumCustomConverter));
    this.RegisterPropertyPageItem(nameof (AutoCreateBlank), typeof (bool), "Автоматически добавлять заготовки", "Автоматически добавлять в спецификацию запись об изделии-заготовке", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (ShowSectionNumInNote), typeof (bool), "Выводить в графе \"Примечание\" номер раздела", "Выводить в графе \"Примечание\" раздела документа номер раздела", (object) false, typeof (CustomBooleanConverter));
    this.RegisterPropertyPageItem(nameof (UpdateCountValueForZagotovka), typeof (PerformActionModeEnum), "Обновлять количество в заготовке", "Обновлять значение в графе \"Количество\" записи заготовки при открытии документа", (object) PerformActionModeEnum.Never, typeof (EnumCustomConverter));
    this.RegisterPropertyPageItem(nameof (DisableMergeRelationsWithoutPosDesignation), typeof (bool), "Запретить суммирование записей без \"'Позиционного обозначения\"", "Запретить суммирование нескольких записей c одним изделием без заполненного атрибута \"Позиционное обозначение\"", (object) true, typeof (CustomBooleanConverter));
    this.SetModelValue((object) false, nameof (CheckSpecificationBeforeClose));
    this.SetModelValue((object) false, nameof (CheckElementListBeforeClose));
    this.SetModelValue((object) new byte[0], nameof (SpecificationGridLayout));
    this.SetModelValue((object) new byte[0], nameof (ElementListGridLayout));
    this.SetModelValue((object) string.Empty, nameof (AvsDocTypesTemplateFormSize));
  }

  /// <summary>Длина Формата</summary>
  public int FormatSize
  {
    [DebuggerStepThrough] get => (int?) this.GetModelValue(nameof (FormatSize)) ?? 0;
    set => this.SetModelValue((object) value, nameof (FormatSize));
  }

  /// <summary>Разрешить создание сканированных спецификаций</summary>
  public bool ShowScan
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (ShowScan)) ?? false;
    set => this.SetModelValue((object) value, nameof (ShowScan));
  }

  /// <summary>Показывать сообщения</summary>
  public bool ShowEvents
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (ShowEvents)) ?? false;
    set => this.SetModelValue((object) value, nameof (ShowEvents));
  }

  /// <summary>Разрешить дополнительную информацию в графе Наименование</summary>
  public bool AllowNoteForSpecRowName
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (AllowNoteForSpecRowName)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (AllowNoteForSpecRowName));
  }

  /// <summary>Разрешить дополнительную информацию в графе Наименование</summary>
  public bool AskUserForOldSPIniFile
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (AskUserForOldSPIniFile)) ?? true;
    }
    set => this.SetModelValue((object) value, nameof (AskUserForOldSPIniFile));
  }

  /// <summary>Дополнительные части должны находиться вне блоков общих и переменных данных</summary>
  public bool AdditionalChaptersInDataChapter
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (AdditionalChaptersInDataChapter)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (AdditionalChaptersInDataChapter));
  }

  /// <summary>Длина Зоны</summary>
  public int ZoneSize
  {
    [DebuggerStepThrough] get => (int?) this.GetModelValue(nameof (ZoneSize)) ?? 0;
    set => this.SetModelValue((object) value, nameof (ZoneSize));
  }

  /// <summary>Автосортировка</summary>
  public bool AutoSort
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (AutoSort)) ?? false;
    set => this.SetModelValue((object) value, nameof (AutoSort));
  }

  /// <summary>Запрет перетаскивания объектов в страничном виде СП</summary>
  public bool DisableSortPageView
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (DisableSortPageView)) ?? false;
    set => this.SetModelValue((object) value, nameof (DisableSortPageView));
  }

  /// <summary>Добавлять в текущий набор исполнений</summary>
  public bool AddToCurrentGroup
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (AddToCurrentGroup)) ?? false;
    set => this.SetModelValue((object) value, nameof (AddToCurrentGroup));
  }

  /// <summary>Заменять шаблон документа при открытии</summary>
  public bool AutoUpdateTemplate
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (AutoUpdateTemplate)) ?? false;
    set => this.SetModelValue((object) value, nameof (AutoUpdateTemplate));
  }

  /// <summary>Исправлять ссылки в полях основной надписи старых документов</summary>
  public bool PatchStampReferences
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (PatchStampReferences)) ?? false;
    set => this.SetModelValue((object) value, nameof (PatchStampReferences));
  }

  /// <summary>Исправлять идентификаторы в ЛРИ старых документов</summary>
  public bool PatchLriId
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (PatchLriId)) ?? false;
    set => this.SetModelValue((object) value, nameof (PatchLriId));
  }

  /// <summary>Спрашивать при простановке нумерации</summary>
  public bool AskRenumber
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (AskRenumber)) ?? false;
    set => this.SetModelValue((object) value, nameof (AskRenumber));
  }

  /// <summary>Отображать команды чтения документов (ведомости, таблицы), разработанных в программе AVS6</summary>
  public bool AskAVS6
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (AskAVS6)) ?? false;
    set => this.SetModelValue((object) value, nameof (AskAVS6));
  }

  /// <summary>Проверять Спецификацию перед закрытием</summary>
  public bool CheckSpecificationBeforeClose
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (CheckSpecificationBeforeClose)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (CheckSpecificationBeforeClose));
  }

  /// <summary>Проверять Перечень элементов перед закрытием</summary>
  public bool CheckElementListBeforeClose
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (CheckElementListBeforeClose)) ?? false;
    }
    set => this.SetModelValue((object) value, nameof (CheckElementListBeforeClose));
  }

  /// <summary>Тип спецификации по умолчанию</summary>
  public AVSSpecificationType DefaultSpecificationType
  {
    get
    {
      AVSSpecificationType specificationType1 = AVSSpecificationType.ESKD;
      if (!(this.GetModelValue(nameof (DefaultSpecificationType)) is int modelValue))
        return specificationType1;
      AVSSpecificationType specificationType2 = (AVSSpecificationType) modelValue;
      return specificationType2 != AVSSpecificationType.Export ? specificationType2 : specificationType1;
    }
    set => this.SetModelValue((object) value, nameof (DefaultSpecificationType));
  }

  /// <summary>Выводить в поле примечания номер раздела</summary>
  public bool ShowSectionNumInNote
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (ShowSectionNumInNote)) ?? false;
    set => this.SetModelValue((object) value, nameof (ShowSectionNumInNote));
  }

  /// <summary>Запретить суммирование записей без "Позиционного обозначения"</summary>
  public bool DisableMergeRelationsWithoutPosDesignation
  {
    [DebuggerStepThrough] get
    {
      return (bool?) this.GetModelValue(nameof (DisableMergeRelationsWithoutPosDesignation)) ?? true;
    }
    set => this.SetModelValue((object) value, nameof (DisableMergeRelationsWithoutPosDesignation));
  }

  /// <summary>Запоминать состав спецификации для последующего отката</summary>
  public CreateUndoEnum CreateUndo
  {
    [DebuggerStepThrough] get
    {
      int? modelValue = (int?) this.GetModelValue(nameof (CreateUndo));
      return (modelValue.HasValue ? new CreateUndoEnum?((CreateUndoEnum) modelValue.GetValueOrDefault()) : new CreateUndoEnum?()) ?? CreateUndoEnum.No;
    }
    set => this.SetModelValue((object) value, nameof (CreateUndo));
  }

  /// <summary>Обновлять документ в режиме только для чтения</summary>
  public UpdateModeInReadOnlyEnum UpdateModeInReadOnly
  {
    [DebuggerStepThrough] get
    {
      int? modelValue = (int?) this.GetModelValue(nameof (UpdateModeInReadOnly));
      return (modelValue.HasValue ? new UpdateModeInReadOnlyEnum?((UpdateModeInReadOnlyEnum) modelValue.GetValueOrDefault()) : new UpdateModeInReadOnlyEnum?()) ?? UpdateModeInReadOnlyEnum.No;
    }
    set => this.SetModelValue((object) value, nameof (UpdateModeInReadOnly));
  }

  /// <summary>Форма групповой спецификации по умолчанию</summary>
  public DefaultGroupSpecificationForm DefaultSpecificationForm
  {
    [DebuggerStepThrough] get
    {
      int? modelValue = (int?) this.GetModelValue(nameof (DefaultSpecificationForm));
      return (modelValue.HasValue ? new DefaultGroupSpecificationForm?((DefaultGroupSpecificationForm) modelValue.GetValueOrDefault()) : new DefaultGroupSpecificationForm?()) ?? DefaultGroupSpecificationForm.A;
    }
    set => this.SetModelValue((object) value, nameof (DefaultSpecificationForm));
  }

  /// <summary>Автоматически создавать заготовки</summary>
  public bool AutoCreateBlank
  {
    [DebuggerStepThrough] get => (bool?) this.GetModelValue(nameof (AutoCreateBlank)) ?? false;
    set => this.SetModelValue((object) value, nameof (AutoCreateBlank));
  }

  /// <summary>Настройка текста ссылки на изделие для которого используется заготовка</summary>
  [Browsable(false)]
  public string ZagotovkaDlya
  {
    [DebuggerStepThrough] get => this.zagotovkaDlya;
  }

  /// <summary>Обновлять значение в графе Count заготовки при изменении в детали</summary>
  public PerformActionModeEnum UpdateCountValueForZagotovka
  {
    [DebuggerStepThrough] get
    {
      int? modelValue = (int?) this.GetModelValue(nameof (UpdateCountValueForZagotovka));
      return (modelValue.HasValue ? new PerformActionModeEnum?((PerformActionModeEnum) modelValue.GetValueOrDefault()) : new PerformActionModeEnum?()) ?? PerformActionModeEnum.Never;
    }
    set => this.SetModelValue((object) value, nameof (UpdateCountValueForZagotovka));
  }

  /// <summary>
  /// Сериализованное представление настройки табличного вида СП
  /// </summary>
  public byte[] SpecificationGridLayout
  {
    get => (byte[]) this.GetModelValue(nameof (SpecificationGridLayout));
    set => this.SetModelValue((object) value, nameof (SpecificationGridLayout));
  }

  /// <summary>
  /// Сериализованное представление настройки табличного вида ПЭ
  /// </summary>
  public byte[] ElementListGridLayout
  {
    get => (byte[]) this.GetModelValue(nameof (ElementListGridLayout));
    set => this.SetModelValue((object) value, nameof (ElementListGridLayout));
  }

  /// <summary>
  /// Строковое представление размеров окна Настроек шаблонов AVS
  /// </summary>
  public string AvsDocTypesTemplateFormSize
  {
    get => (string) this.GetModelValue(nameof (AvsDocTypesTemplateFormSize));
    set => this.SetModelValue((object) value, nameof (AvsDocTypesTemplateFormSize));
  }

  [Browsable(false)]
  public override string PageName
  {
    [DebuggerStepThrough] get => base.PageName + "\\Общие настройки";
  }
}
