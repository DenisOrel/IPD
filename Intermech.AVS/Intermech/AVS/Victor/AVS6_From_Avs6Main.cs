// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.AVS6_From_Avs6Main
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.IniFiles;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

/// <summary> Класс, из настроек AVS6, описывающий поля паспорта и записей всех полей (колонок) документов AVS6 </summary>
/// 
///             ВСЕ данные читаются из файла AVS6MAIN.INI
public static class AVS6_From_Avs6Main
{
  public static List<OneField> _list_pasportFields = new List<OneField>();
  public static List<OneField> _list_recordFields = new List<OneField>();
  public static List<OneField> _list_UtvFields = new List<OneField>();
  public static List<OneField> _list_titlesFields = new List<OneField>();
  public static List<OneField> _list_IzmFields = new List<OneField>();
  public static List<ElDocList> _list_ElDocList = new List<ElDocList>();
  public static List<ElDocList> _list_ElDocList_Processed = new List<ElDocList>();
  public static int _isAvs6 = 0;
  public static string sTextError = "";
  public static InMemoryIniFile _inMemoryIniFile6 = (InMemoryIniFile) null;
  public static string _fileIni6 = "";
  public static string _pathAvs6Cfg = "";
  public static bool _isInit = false;

  public static void Inits()
  {
    if (!AVS6_From_Avs6Main.Check_Avs6())
      return;
    AVS6_From_Avs6Main._isAvs6 = 1;
  }

  public static bool Check_Avs6()
  {
    AVS6_From_Avs6Main.Get_pathAvs6Cfg();
    return !string.IsNullOrEmpty(AVS6_From_Avs6Main._pathAvs6Cfg) && File.Exists(AVS6_From_Avs6Main._fileIni6);
  }

  /// <summary> Конструктор </summary>
  /// 
  ///             Находим путь к папке с настройкаи и имя главного файла настроек
  ///             Чтение файла настроек AVS6Main.ini
  ///             Получаем ВСЕ списки полей AVS6
  ///             Получаем секцию DOC
  ///             Получаем обработанный список документов _list_ElDocList_Processed
  public static bool Read()
  {
    AVS6_From_Avs6Main.Get_pathAvs6Cfg();
    if (string.IsNullOrEmpty(AVS6_From_Avs6Main._pathAvs6Cfg))
    {
      AVS6_From_Avs6Main.sTextError = "На данном рабочем месте настройки AVS6 отсутствуют";
      return false;
    }
    if (File.Exists(AVS6_From_Avs6Main._fileIni6))
    {
      if (AVS6_From_Avs6Main.Load_From_AVS6MAIN() && AVS6_From_Avs6Main._list_recordFields != null)
      {
        AVS6_From_Avs6Main._isAvs6 = 1;
        return true;
      }
      AVS6_From_Avs6Main._isAvs6 = 0;
      if (AVS6_From_Avs6Main._list_recordFields == null || AVS6_From_Avs6Main._list_recordFields.Count == 0)
        Vedomost_VB_Static.IsAvs6ToIps = false;
      return false;
    }
    AVS6_From_Avs6Main._isAvs6 = 0;
    AVS6_From_Avs6Main.sTextError = "не найден";
    return false;
  }

  public static bool Read(string filename)
  {
    if (File.Exists(filename) && AVS6_From_Avs6Main.Load_From_AVS6MAIN() && AVS6_From_Avs6Main._list_recordFields != null)
    {
      AVS6_From_Avs6Main._isAvs6 = 2;
      return true;
    }
    if (AVS6_From_Avs6Main._list_recordFields == null || AVS6_From_Avs6Main._list_recordFields.Count == 0)
      Vedomost_VB_Static.IsAvs6ToIps = false;
    return false;
  }

  /// <summary> Прочитать путь к настройкам AVS6 </summary>
  private static void Get_pathAvs6Cfg()
  {
    object obj = Registry.GetValue("HKEY_CURRENT_USER\\Software\\InterMech\\Avs6", "PathAvsCfg6", (object) null);
    if (obj == null)
      return;
    AVS6_From_Avs6Main._pathAvs6Cfg = obj.ToString();
    if (AVS6_From_Avs6Main._pathAvs6Cfg == "")
      return;
    AVS6_From_Avs6Main._fileIni6 = AVS6_From_Avs6Main._pathAvs6Cfg + "\\AVS6MAIN.INI";
  }

  /// <summary> Чтение файла настроек всех AVS6Main.ini </summary>
  /// 
  ///             Получаем ВСЕ списки полей
  ///             Получаем секцию DOC
  ///             Получаем обработанный список документов _list_ElDocList_Processed
  public static bool Load_From_AVS6MAIN()
  {
    if (AVS6_From_Avs6Main._fileIni6 == "")
    {
      AVS6_From_Avs6Main.sTextError = "Файл Avs6Main.ini не определен";
      AVS6_From_Avs6Main._isAvs6 = 0;
      return false;
    }
    if (!string.IsNullOrEmpty(AVS6_From_Avs6Main._fileIni6) && File.Exists(AVS6_From_Avs6Main._fileIni6))
    {
      if (AVS6_From_Avs6Main.Read_Avs6Main(AVS6_From_Avs6Main._fileIni6))
      {
        AVS6_From_Avs6Main._isAvs6 = 1;
        return true;
      }
      AVS6_From_Avs6Main._isAvs6 = 0;
      return false;
    }
    string str = $"Файл\r\n\r\n{AVS6_From_Avs6Main._fileIni6}\r\n\r\nне найден";
    AVS6_From_Avs6Main._isAvs6 = 0;
    return false;
  }

  public static bool Read_Avs6Main(string fileName)
  {
    AVS6_From_Avs6Main.sTextError = "";
    string iniFileContent = !(fileName == "Default") ? AVS6_From_Avs6Main.GetIniFileContent(fileName) : AVS6_From_Avs6Main.DefaultAvs6();
    if (iniFileContent == "")
    {
      AVS6_From_Avs6Main.sTextError = "Файл не найден";
      return false;
    }
    AVS6_From_Avs6Main._inMemoryIniFile6 = new InMemoryIniFile(iniFileContent);
    if (!AVS6_From_Avs6Main.ListDOC_Read(AVS6_From_Avs6Main._inMemoryIniFile6) || !AVS6_From_Avs6Main.ListFields_Read(AVS6_From_Avs6Main._inMemoryIniFile6, "FIELDSLIST"))
      return false;
    AVS6_From_Avs6Main.ListDoc_Processing(false, false, false);
    return true;
  }

  public static string DefaultAvs6()
  {
    return "[DOC]\r\n1=;Спецификация. Базовые настройки;AVS6_SP_BASES.INI;;Спецификация#;SYSTEM;1;1;;1;Спецификация\r\n2=SP;Спецификация ЕСКД. Базовые настройки;AVS6_ESKD_BASES.INI;;Спецификация#ЕСКД#;SYSTEM;2;2;;1;Спецификация\r\n3=SP;Спецификация ЕСКД. Единичная;AVS6_ESKD_1.INI;;Спецификация#ЕСКД#Единичная#;SYSTEM;3;3;;3;Спецификация\r\n4=SP;Спецификация ЕСКД. Групповая, форма А;AVS6_ESKD_A.INI;;Спецификация#ЕСКД#Групповая А#;SYSTEM;4;4;;3;Спецификация\r\n5=SP;Спецификация ЕСКД. Групповая, форма Б;AVS6_ESKD_B.INI;;Спецификация#ЕСКД#Групповая Б#;SYSTEM;5;5;;3;Спецификация\r\n6=SP;Спецификация ЕСКД. Групповая, форма В;AVS6_ESKD_W.INI;;Спецификация#ЕСКД#Групповая В#;SYSTEM;22;22;;3;Спецификация\r\n7=SP;Спецификация ЕСКД. Групповая, форма Г;AVS6_ESKD_G.INI;;Спецификация#ЕСКД#Групповая Г#;SYSTEM;23;23;;3;Спецификация\r\n8=SPK;Спецификация ЕСКД. Спецификация комплектации;AVS6_SPK.INI;;Спецификация#ЕСКД#Комплектации#;SYSTEM;6;6;;3;Спецификация\r\n9=SPZ;Спецификация ЕСКД. Спецификация заказа;AVS6_SPZ.INI;;Спецификация#ЕСКД#Заказа#;SYSTEM;7;7;;3;Спецификация\r\n10=SP;Спецификация автомобильная. Базовые настройки;AVS6_MAZ_BASES.INI;;Спецификация#Автомобильная#;SYSTEM;8;8;;1;Спецификация\r\n11=SP;Спецификация автомобильная. Единичная;AVS6_MAZ_1.INI;;Спецификация#Автомобильная#Единичная#;SYSTEM;9;9;;3;Спецификация\r\n12=SP;Спецификация автомобильная. Групповая, форма А;AVS6_MAZ_A.INI;;Спецификация#Автомобильная#Групповая А#;SYSTEM;10;10;;3;Спецификация\r\n13=SP;Спецификация автомобильная. Групповая, форма Б;AVS6_MAZ_B.INI;;Спецификация#Автомобильная#Групповая Б#;SYSTEM;11;11;;3;Спецификация\r\n14=SP;Спецификация автомобильная. Зеркальная;AVS6_MAZ_Z.INI;;Спецификация#Автомобильная#Зеркальная#;SYSTEM;12;12;;3;Спецификация\r\n15=SPS;Спецификация судостроительная;AVS6_SP_SHIP.INI;;Спецификация#Судостроительная#;SYSTEM;13;13;;3;Спецификация\r\n16=SP;Спецификация экспортная;AVS6_EXP.INI;;Спецификация#Экспортная#;SYSTEM;24;24;;3;Спецификация\r\n17=SPP;Спецификация программная;AVS6_SPESPD.INI;;Спецификация#ЕСПД#;SYSTEM;26;26;;3;Спецификация\r\n18=PE;Перечень элементов. Базовые настройки;AVS6_PE_BASES.INI;;Перечень элементов#;SYSTEM;14;14;;1;Перечень элементов\r\n19=PE;Перечень элементов. ПЭ3;AVS6_PE3.INI;ПЭ3;Перечень элементов#ПЭ3#;SYSTEM;15;15;Перечень элементов;3;\r\n20=TB;Таблица. Базовые настройки;AVS6_TB_BASES.INI;;Таблица#;SYSTEM;16;16;;1;\r\n21=PA;Таблица. Таблица соединений (развернутая);AVS6_TB_PA.INI;ТЭ4;Таблица#Таблица соединений (развернутая)#;SYSTEM;27;27;;3;Таблица соединений\r\n22=PB;Таблица. Таблица соединений (сжатая);AVS6_TB_PB.INI;ТЭ4;Таблица#Таблица соединений (сжатая)#;SYSTEM;28;28;;3;Таблица соединений\r\n23=P6;Таблица. Таблица внешних соединений;AVS6_TB_P6.INI;ТЭ6;Таблица#Таблица внешних соединений#;SYSTEM;29;29;;3;Таблица соединений\r\n24=PC;Таблица. Таблица наборов зажимов;AVS6_TB_PC.INI;ТНЗ;Таблица#Таблица наборов зажимов#;SYSTEM;30;30;;3;Набор зажимов\r\n25=;Ведомость. Базовые настройки;AVS6_VED_BASES.INI;;Ведомость#;SYSTEM;17;17;;5;\r\n26=VS;Ведомость спецификаций;AVS6_VS.INI;ВС;Ведомость#Ведомость спецификаций#;SYSTEM;18;18;;7;Ведомость спецификаций\r\n27=VS;Ведомость спецификаций. Групповая Б;AVS6_VS_B.INI;ВС;Ведомость#Ведомость спецификаций#Групповая Б#;SYSTEM;31;18;;7;Ведомость спецификаций\r\n28=VP;Ведомость покупных изделий;AVS6_VP.INI;ВП;Ведомость#Ведомость покупных изделий#;SYSTEM;19;19;;7;Ведомость покупных изделий\r\n29=VP;Ведомость покупных изделий. Групповая Б;AVS6_VP_B.INI;ВП;Ведомость#Ведомость покупных изделий#Групповая Б#;SYSTEM;32;19;;7;Ведомость покупных изделий\r\n30=RS;Общая спецификация;AVS6_RS.INI;РСП;Ведомость#Общая спецификация#;SYSTEM;20;20;;7;Общая спецификация\r\n31=VY;Ведомость состава изделия;AVS6_VY.INI;;Ведомость#Ведомость состава изделия#;SYSTEM;21;21;;7;Ведомость состава изделий\r\n32=;Разное. Базовые настройки;AVS6_OTHERS_BASES.INI;;Разное#;SYSTEM;34;34;;5;\r\n33=LUK;Лист утверждения ЕСКД;AVS6_LUK.INI;ЛУ;Разное#Лист утверждения ЕСКД#;SYSTEM;35;35;;7;Лист утвержения\r\n34=LUP;Лист утверждения ЕСПД;AVS6_LUP.INI;ЛУ;Разное#Лист утверждения ЕСПД#;SYSTEM;36;36;;7;Лист утвержения\r\n\r\n[RECTYPES]\r\nI=Информационная\r\nS=Наименование раздела\r\nP=Наименование части\r\nN=Номер исполнения\r\nV=Переменные данные...\r\nR=Примечание\r\nT=Примечание 2\r\nX=Дополнительная 1\r\nY=Дополнительная 2\r\nE=Дополнительная 3\r\nF=Дополнительная 4\r\nG=Дополнительная 5\r\nB=Входящая ведомость\r\nC=Заголовок \"Ведомости составных частей\"\r\nK=Титульный лист\r\nL=Лист утверждения\r\nM=Лист регистрации изменений\r\nH=Для отдельных листов\r\n\r\n[FIELDSLIST]\r\n1=Формат\r\n2=Зона\r\n3=Поз\r\n4=Обозначение\r\n5=Наименование\r\n6=Кол\r\n7=Примечание\r\n8=Ключ IMBASE\r\n9=Часть\r\n10=Раздел\r\n11=Исполнение\r\n12=УслПоз\r\n13=Строк до\r\n14=Строк после\r\n15=Шаг строк\r\n16=Страниц\r\n17=Поз до\r\n18=Поз после\r\n19=Шаг поз\r\n20=Путь\r\n21=Имя файла\r\n22=Инвентарный номер изделия (ArtId)\r\n23=Масса одного изделия (Служебное)\r\n24=Масса\r\n25=Материал\r\n27=Атрибут CADMECH\r\n28=ОКП\r\n29=Сортировка\r\n30=Тип документа\r\n31=МассаN\r\n32=Поз. обозначение\r\n33=Размеры\r\n36=Imbase\r\n37=Смотри\r\n38=CADSYSTEM\r\n39=Литера\r\n40=Единица измерения количества\r\n41=Единица измерения массы\r\n42=Допустимые замены\r\n43=Применяется взамен (служебное)\r\n44=Замена совместно с (служебное)\r\n45=Применяется совместно с (служебное)\r\n46=VariantsMode (служебное)\r\n47=Количество (служебное)\r\n48=Допустимые замены (служебное)\r\n49=Признак принадлежности (служебное)\r\n50=ДопЗамены из CADMECH (служебное)\r\n100=Куда входит\r\n101=Кол. на изделие\r\n102=Кол. на комплекты\r\n103=Кол. на регулировку\r\n104=Кол. всего\r\n105=Суммарное количество\r\n106=Процент на регулировку\r\n108=Функциональная группа\r\n109=Документ\r\n110=Поставщик\r\n111=Покупной\r\n112=Каталог\r\n113=Кол. в одной спецификации\r\n114=Уровень\r\n125=Вспомогательное\r\n126=Вспомогательное2\r\n127=ВспомогательноеВед\r\n129=Идентификатор строки\r\n130=Заготовка для\r\n131=СП сборочной единицы\r\n132=Не печатать\r\n133=Служебное1\r\n134=Служебное2\r\n135=Чистая масса\r\n136=GUID записи\r\n137=Инвентарный номер документа (DocId)\r\n138=Описание допзамен (служебное)\r\n139=Признак разбитой записи (служебное)\r\n140=GUID условий применения\r\n141=Условие применения\r\n142=Листов формата А1\r\n143=Листов формата А4\r\n144=Серийный номер\r\n145=Условное наименование исполнения\r\n146=Изменил\r\n147=Дата изм\r\n148=Лист_Изм\r\n149=Изменение\r\n150=N извещения\r\n161=Не раскрывать при сборе ведомости\r\n162=Год файла\r\n163=Лист\r\n164=Формат (длинный)\r\n165=Зона (длинная)\r\n166=Базовое исполнение\r\n170=Наименование документа\r\n171=Код единицы измерения количества\r\n172=Код единицы измерения массы\r\n173=Код материала\r\n174=Код ведомости заказа\r\n175=Заготовка\r\n176=Обработка\r\n177=Сборка\r\n178=Монтаж\r\n179=Марка\r\n180=Норматив\r\n181=Код входимости\r\n182=Узел, номер\r\n183=Узел, кол-во\r\n184=№ п/п\r\n185=M3\r\n186=T\r\n187=X\r\n188=Y\r\n189=Z\r\n190=Наименование и размеры материала\r\n191=Фрагмент\r\n192=Таблица\r\n193=По ОСТ85.0069-72\r\n211=Откуда идет\r\n212=Куда поступает\r\n213=Данные провода\r\n214=Цвет провода\r\n215=Длина, м\r\n216=Наконечник\r\n217=Откуда идет (устр.)\r\n218=Куда поступает (устр.)\r\n219=Откуда идет (апп.-заж.)\r\n220=Куда поступает (апп.-заж.)\r\n221=Жгут\r\n222=Откуда идет (апп.)\r\n223=Куда поступает (апп.)\r\n224=Откуда идет (заж.)\r\n225=Куда поступает (заж.)\r\n226=Обозначение провода\r\n227=Соединения\r\n228=Обозначение набора\r\n229=Обозначение зажима\r\n230=Обозначение подключаемого провода\r\n231=Данные зажима\r\n232=Количество\r\n234=КЛАСС\r\n235=НАИМЕНОВАНИЕ1\r\n236=Description\r\n237=Наименование (exp)\r\n238=Наименование (ru)\r\n239=Note\r\n\r\n[PASPORT_FIELDS]\r\n1=Обозначение\r\n2=Наименование\r\n3=Перв. примен.\r\n6=Групповой\r\n7=Номера исполнений\r\n8=Дата\r\n9=Разработал\r\n10=Подразделение\r\n11=Масса\r\n12=Ед. изм. массы\r\n13=Литера\r\n15=Файл связи\r\n16=Проверил\r\n17=Н.контр.\r\n18=Утвердил\r\n19=Т.контр.\r\n20=Примечание\r\n21=Условное наименование исполнения\r\n22=Графа-Исполнение\r\n23=ОКП\r\n24=Начальник\r\n25=Признак принадлежности\r\n26=Имя файла\r\n27=Формат\r\n28=Размер файла\r\n29=Дата файла\r\n121=Лист\r\n122=Листов\r\n123=Вид штампа первого листа\r\n124=Начать нумерацию с номера\r\n129=Идентификатор строки\r\n130=Текущая дата\r\n131=Размещение исполнений\r\n132=Файл настройки\r\n133=Должность начальника\r\n134=Количество листов регистрации изменений\r\n135=Вывести титульный лист\r\n136=Файл титульного листа\r\n137=Инвентарный номер документа\r\n138=Изменение\r\n139=N извещения\r\n140=Вывести лист утверждения\r\n141=Файл листа утверждения\r\n142=Лист_Изм\r\n144=Серийный номер\r\n145=Только для чтения\r\n146=Изменил\r\n147=Дата изм\r\n161=Не раскрывать при сборе ведомости\r\n162=Год файла\r\n163=Разбиение на страницы зафиксировано\r\n164=Литера2\r\n165=Литера3\r\n166=Версия AVS\r\n167=Результаты контроля документа\r\n168=Параметры создания документа\r\n169=Параметры редактирования документа\r\n170=Наименование документа\r\n171=Создан из CADMECH 3D\r\n185=M3\r\n186=T\r\n187=X\r\n188=Y\r\n189=Z\r\n191=Фрагментов\r\n192=Таблица\r\n193=По ОСТ85.0069-72\r\n194=Титульный лист\r\n195=Лист регистрации изменений\r\n196=Вид документа\r\n197=Обозначение листа утверждения\r\n201=Tree_SysNumber\r\n202=Parent_SysNumber\r\n203=Tree_Guid\r\n204=Групповой v.6\r\n205=Перенос полей\r\n234=Name\r\n235=Заказчик\r\n236=Наименование АЭС\r\n237=Name NPP\r\n238=AKZ\r\n239=Код AKZ\r\n240=Перевод\r\n243=Штрихкод документа\r\n244=Штрихкод объекта\r\n245=Штрихкод ОТД\r\n\r\n[TitlesFields]\r\n1=Утвердил должность\r\n2=Утвердил фамилия\r\n3=Согласовал1 должность\r\n4=Согласовал1 фамилия\r\n5=Согласовал2 должность\r\n6=Согласовал2 фамилия\r\n7=Согласовал3 должность\r\n8=Согласовал3 фамилия\r\n9=Согласовал4 должность\r\n10=Согласовал4 фамилия\r\n11=Разработчик1 должность\r\n12=Разработчик1 фамилия\r\n13=Разработчик2 должность\r\n14=Разработчик2 фамилия\r\n\r\n[ListUtvFields]\r\n1=Утвердил должность\r\n2=Утвердил фамилия\r\n3=Согласовал1 должность\r\n4=Согласовал1 фамилия\r\n5=Согласовал2 должность\r\n6=Согласовал2 фамилия\r\n7=Согласовал3 должность\r\n8=Согласовал3 фамилия\r\n9=Согласовал4 должность\r\n10=Согласовал4 фамилия\r\n11=Разработчик1 должность\r\n12=Разработчик1 фамилия\r\n13=Разработчик2 должность\r\n14=Разработчик2 фамилия\r\n\r\n[LIZMFields]\r\n1=Изм\r\n2=Измененных\r\n3=Замененных\r\n4=Новых\r\n5=Аннулированных\r\n6=Всего листов\r\n7=№ документа\r\n8=Входящий\r\n9=Подп\r\n10=Дата\r\n11=Лист\r\n12=Зона\r\n13=№ Листа документа\r\n\r\n";
  }

  /// <summary> По имени секции AVS6MAIN.INI заполняется соответствующий список полей </summary>
  /// <param name="inMemoryIniFile"></param>
  /// <param name="SectionName"></param>
  private static bool ListFields_Read(InMemoryIniFile inMemoryIniFile, string SectionName)
  {
    if (inMemoryIniFile == null || SectionName == "")
      return false;
    List<OneField> oneFieldList = (List<OneField>) null;
    if (SectionName.ToUpper() == "PASPORT_FIELDS")
      oneFieldList = AVS6_From_Avs6Main._list_pasportFields;
    if (SectionName.ToUpper() == "FIELDSLIST")
      oneFieldList = AVS6_From_Avs6Main._list_recordFields;
    if (SectionName.ToUpper() == "LISTUTVFIELDS")
      oneFieldList = AVS6_From_Avs6Main._list_UtvFields;
    if (SectionName.ToUpper() == "TITLESFIELDS")
      oneFieldList = AVS6_From_Avs6Main._list_titlesFields;
    if (SectionName.ToUpper() == "LIZMFIELDS")
      oneFieldList = AVS6_From_Avs6Main._list_IzmFields;
    if (oneFieldList == null)
      return false;
    oneFieldList.Clear();
    try
    {
      List<string> valueName = inMemoryIniFile.ValueNames[SectionName];
      byte result = 0;
      string empty = string.Empty;
      foreach (string str1 in valueName)
      {
        if (byte.TryParse(str1, out result))
        {
          string str2 = inMemoryIniFile.ReadString(SectionName, str1, string.Empty);
          if (str2.Trim() != string.Empty)
            oneFieldList.Add(new OneField()
            {
              _fieldType_Avs6 = result,
              _fieldName_Avs6 = str2.Trim()
            });
        }
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show("В файле отсутствует секция\r\n\r\n" + SectionName, "Ошибка!");
      return false;
    }
    return true;
  }

  private static bool ListDOC_Read(InMemoryIniFile inMemoryIniFile)
  {
    if (inMemoryIniFile == null)
      return false;
    AVS6_From_Avs6Main._list_ElDocList.Clear();
    try
    {
      List<string> valueName = inMemoryIniFile.ValueNames["DOC"];
      byte result = 0;
      string empty = string.Empty;
      foreach (string str in valueName)
      {
        if (byte.TryParse(str, out result))
        {
          string string_From_Doc = inMemoryIniFile.ReadString("DOC", str, string.Empty);
          if (str.Trim() != string.Empty)
          {
            ElDocList listByStringFromDoc = AVS6_From_Avs6Main.Create_elDocList_by_String_FromDoc(string_From_Doc);
            if (listByStringFromDoc != null)
              AVS6_From_Avs6Main._list_ElDocList.Add(listByStringFromDoc);
          }
        }
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show("В файле отсутствует секция\r\n\r\nDOC", "Ошибка!");
      return false;
    }
    return true;
  }

  /// <summary>  Обработка одной строки секции DOC </summary>
  /// <param name="string_From_Doc"></param>
  /// <returns></returns>
  private static ElDocList Create_elDocList_by_String_FromDoc(string string_From_Doc)
  {
    if (string.IsNullOrEmpty(string_From_Doc))
      return (ElDocList) null;
    ElDocList listByStringFromDoc = new ElDocList();
    int num1 = string_From_Doc.IndexOf(';');
    int length1 = num1;
    if (length1 > 0)
      listByStringFromDoc._fileType = string_From_Doc.Substring(0, length1);
    int length2 = string_From_Doc.Length - num1 - 1;
    string_From_Doc = string_From_Doc.Substring(num1 + 1, length2);
    int num2 = string_From_Doc.IndexOf(';');
    int length3 = num2;
    if (length3 > 0)
      listByStringFromDoc._comment = string_From_Doc.Substring(0, length3);
    int length4 = string_From_Doc.Length - num2 - 1;
    string_From_Doc = string_From_Doc.Substring(num2 + 1, length4);
    int num3 = string_From_Doc.IndexOf(';');
    int length5 = num3;
    if (length5 > 0)
      listByStringFromDoc._fileIni = string_From_Doc.Substring(0, length5);
    int length6 = string_From_Doc.Length - num3 - 1;
    string_From_Doc = string_From_Doc.Substring(num3 + 1, length6);
    int num4 = string_From_Doc.IndexOf(';');
    int length7 = num4;
    if (length7 > 0)
      listByStringFromDoc._kod = string_From_Doc.Substring(0, length7);
    int length8 = string_From_Doc.Length - num4 - 1;
    string_From_Doc = string_From_Doc.Substring(num4 + 1, length8);
    int num5 = string_From_Doc.IndexOf(';');
    int length9 = num5;
    if (length9 > 0)
      listByStringFromDoc._tree = string_From_Doc.Substring(0, length9);
    if (listByStringFromDoc._tree.IndexOf("Спецификация") == 0)
      listByStringFromDoc.typeDoc = Vedomost_VB.TypeDoc.Sp;
    else if (listByStringFromDoc._tree.IndexOf("Перечень") == 0)
      listByStringFromDoc.typeDoc = Vedomost_VB.TypeDoc.Pe;
    else if (listByStringFromDoc._tree.IndexOf("Таблица") == 0)
      listByStringFromDoc.typeDoc = Vedomost_VB.TypeDoc.Tabl;
    else if (listByStringFromDoc._tree.IndexOf("Ведомость") == 0)
      listByStringFromDoc.typeDoc = Vedomost_VB.TypeDoc.Ved;
    int length10 = string_From_Doc.Length - num5 - 1;
    string_From_Doc = string_From_Doc.Substring(num5 + 1, length10);
    int num6 = string_From_Doc.IndexOf(';');
    int length11 = num6;
    if (length11 > 0)
      listByStringFromDoc._level = string_From_Doc.Substring(0, length11);
    int length12 = string_From_Doc.Length - num6 - 1;
    string_From_Doc = string_From_Doc.Substring(num6 + 1, length12);
    int num7 = string_From_Doc.IndexOf(';');
    int length13 = num7;
    if (length13 > 0)
      listByStringFromDoc._sysNumber = Convert.ToInt32(string_From_Doc.Substring(0, length13));
    int length14 = string_From_Doc.Length - num7 - 1;
    string_From_Doc = string_From_Doc.Substring(num7 + 1, length14);
    int num8 = string_From_Doc.IndexOf(';');
    int length15 = num8;
    if (length15 > 0)
      listByStringFromDoc._parentSysNumber = Convert.ToInt32(string_From_Doc.Substring(0, length15));
    int length16 = string_From_Doc.Length - num8 - 1;
    string_From_Doc = string_From_Doc.Substring(num8 + 1, length16);
    int num9 = string_From_Doc.IndexOf(';');
    int length17 = num9;
    if (length17 > 0)
      listByStringFromDoc._guidSysNumber = string_From_Doc.Substring(0, length17);
    int length18 = string_From_Doc.Length - num9 - 1;
    string_From_Doc = string_From_Doc.Substring(num9 + 1, length18);
    int num10 = string_From_Doc.IndexOf(';');
    int length19 = num10;
    if (length19 > 0)
      listByStringFromDoc._typDoc = Convert.ToInt32(string_From_Doc.Substring(0, length19));
    int length20 = string_From_Doc.Length - num10 - 1;
    string_From_Doc = string_From_Doc.Substring(num10 + 1, length20);
    listByStringFromDoc._vidDoc = string_From_Doc.Substring(0, length20);
    listByStringFromDoc._title = listByStringFromDoc.Title();
    return listByStringFromDoc;
  }

  private static void ListDoc_Processing(bool isSpecification, bool isPe, bool isRaznoe)
  {
    if (AVS6_From_Avs6Main._list_ElDocList == null || AVS6_From_Avs6Main._list_ElDocList.Count == 0)
      return;
    AVS6_From_Avs6Main._list_ElDocList_Processed = new List<ElDocList>();
    ElDocList elDocList1 = (ElDocList) null;
    for (int index = 0; index < AVS6_From_Avs6Main._list_ElDocList.Count; ++index)
    {
      ElDocList listElDoc = AVS6_From_Avs6Main._list_ElDocList[index];
      switch (Vedomost_VB_Static.Count_Substr_in_String(listElDoc._tree, "#"))
      {
        case 2:
        case 3:
        case 4:
          if (Vedomost_VB_Static.Count_Substr_in_String(listElDoc._tree, "Единич") <= 0 && Vedomost_VB_Static.Count_Substr_in_String(listElDoc._tree, "Групп") <= 0 && Vedomost_VB_Static.Count_Substr_in_String(listElDoc._tree, "Зерк") <= 0 && (isSpecification || Vedomost_VB_Static.Count_Substr_in_String(listElDoc._tree, "Спецификация") != 1) && (isPe || Vedomost_VB_Static.Count_Substr_in_String(listElDoc._tree, "Перечень элементов") != 1) && (isRaznoe || Vedomost_VB_Static.Count_Substr_in_String(listElDoc._tree, "Разное") != 1))
          {
            elDocList1 = new ElDocList();
            ElDocList elDocList2 = listElDoc.Copy();
            AVS6_From_Avs6Main._list_ElDocList_Processed.Add(elDocList2);
            break;
          }
          break;
      }
    }
    for (int index = 0; index < AVS6_From_Avs6Main._list_ElDocList_Processed.Count; ++index)
      elDocList1 = AVS6_From_Avs6Main._list_ElDocList_Processed[index];
  }

  /// <summary> Список полей AVS6 default </summary>
  public static void AVS6_From_Avs6Main_Init()
  {
    AVS6_From_Avs6Main.PasportFieldsInit();
    AVS6_From_Avs6Main.RecordFieldsInit();
    AVS6_From_Avs6Main.ListUtvFieldsInit();
    AVS6_From_Avs6Main.TitlesFieldsInit();
    AVS6_From_Avs6Main.LisIzmFieldsInit();
    AVS6_From_Avs6Main.List_ElDocList_Processed_Init();
  }

  /// <summary> Список полей паспорта AVS6 default </summary>
  private static void PasportFieldsInit()
  {
    AVS6_From_Avs6Main._list_pasportFields = new List<OneField>();
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 1,
      _fieldName_Avs6 = "Обозначение"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 2,
      _fieldName_Avs6 = "Наименование"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 3,
      _fieldName_Avs6 = "Перв. примен."
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 6,
      _fieldName_Avs6 = "Групповой"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 7,
      _fieldName_Avs6 = "Номера исполнений"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 8,
      _fieldName_Avs6 = "Дата"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 9,
      _fieldName_Avs6 = "Разработал"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 10,
      _fieldName_Avs6 = "Подразделение"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 11,
      _fieldName_Avs6 = "Масса"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 12,
      _fieldName_Avs6 = "Ед. изм. массы"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 13,
      _fieldName_Avs6 = "Литера"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 15,
      _fieldName_Avs6 = "Файл связи"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 16 /*0x10*/,
      _fieldName_Avs6 = "Проверил"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 17,
      _fieldName_Avs6 = "Н.контр."
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 18,
      _fieldName_Avs6 = "Утвердил"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 19,
      _fieldName_Avs6 = "Т.контр."
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 20,
      _fieldName_Avs6 = "Примечание"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 21,
      _fieldName_Avs6 = "Условное наименование исполнения"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 22,
      _fieldName_Avs6 = "Графа-Исполнение"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 23,
      _fieldName_Avs6 = "ОКП"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 24,
      _fieldName_Avs6 = "Начальник"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 25,
      _fieldName_Avs6 = "Признак принадлежности"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 26,
      _fieldName_Avs6 = "Имя файла"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 27,
      _fieldName_Avs6 = "Формат"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 28,
      _fieldName_Avs6 = "Размер файла"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 29,
      _fieldName_Avs6 = "Дата файла"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 121,
      _fieldName_Avs6 = "Лист"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 122,
      _fieldName_Avs6 = "Листов"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 123,
      _fieldName_Avs6 = "Вид штампа первого листа"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 124,
      _fieldName_Avs6 = "Начать нумерацию с номера"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 129,
      _fieldName_Avs6 = "Идентификатор строки"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 130,
      _fieldName_Avs6 = "Текущая дата"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 131,
      _fieldName_Avs6 = "Размещение исполнений"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 132,
      _fieldName_Avs6 = "Файл настройки"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 133,
      _fieldName_Avs6 = "Должность начальника"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 134,
      _fieldName_Avs6 = "Количество листов регистрации изменений"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 135,
      _fieldName_Avs6 = "Вывести титульный лист"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 136,
      _fieldName_Avs6 = "Файл титульного листа"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 137,
      _fieldName_Avs6 = "Инвентарный номер документа"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 138,
      _fieldName_Avs6 = "Изменение"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 139,
      _fieldName_Avs6 = "N извещения"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 140,
      _fieldName_Avs6 = "Вывести лист утверждения"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 141,
      _fieldName_Avs6 = "Файл листа утверждения"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 142,
      _fieldName_Avs6 = "Лист_Изм"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 144 /*0x90*/,
      _fieldName_Avs6 = "Серийный номер"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 145,
      _fieldName_Avs6 = "Только для чтения"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 146,
      _fieldName_Avs6 = "Изменил"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 147,
      _fieldName_Avs6 = "Дата изм."
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 161,
      _fieldName_Avs6 = "Не раскрывать при сборе ведомости"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 162,
      _fieldName_Avs6 = "Год файла"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 163,
      _fieldName_Avs6 = "Разбиение на страницы зафиксировано"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 164,
      _fieldName_Avs6 = "Литера2"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 165,
      _fieldName_Avs6 = "Литера3"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 166,
      _fieldName_Avs6 = "Версия AVS"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 167,
      _fieldName_Avs6 = "Результаты контроля документа"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 168,
      _fieldName_Avs6 = "Параметры создания документа"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 169,
      _fieldName_Avs6 = "Параметры редактирования документа"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 170,
      _fieldName_Avs6 = "Наименование документа"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 171,
      _fieldName_Avs6 = "Создан из CADMECH 3D"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 185,
      _fieldName_Avs6 = "M3"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 186,
      _fieldName_Avs6 = "T"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 187,
      _fieldName_Avs6 = "X"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 188,
      _fieldName_Avs6 = "Y"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 189,
      _fieldName_Avs6 = "Z"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 191,
      _fieldName_Avs6 = "Фрагментов"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 192 /*0xC0*/,
      _fieldName_Avs6 = "Таблица"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 193,
      _fieldName_Avs6 = "По ОСТ85.0069-72"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 194,
      _fieldName_Avs6 = "Титульный лист"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 195,
      _fieldName_Avs6 = "Лист регистрации изменений"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 196,
      _fieldName_Avs6 = "Вид документа"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 197,
      _fieldName_Avs6 = "Обозначение листа утверждения"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 201,
      _fieldName_Avs6 = "Tree_SysNumber"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 202,
      _fieldName_Avs6 = "Parent_SysNumber"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 203,
      _fieldName_Avs6 = "Tree_Guid"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 204,
      _fieldName_Avs6 = "Групповой v.6"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 205,
      _fieldName_Avs6 = "Перенос полей"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 234,
      _fieldName_Avs6 = "Name"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 235,
      _fieldName_Avs6 = "Заказчик"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 236,
      _fieldName_Avs6 = "Наименование АЭС"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 237,
      _fieldName_Avs6 = "Name NPP"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 238,
      _fieldName_Avs6 = "AKZ"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 239,
      _fieldName_Avs6 = "Код AKZ"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 240 /*0xF0*/,
      _fieldName_Avs6 = "Перевод"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 243,
      _fieldName_Avs6 = "Штрихкод документа"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 244,
      _fieldName_Avs6 = "Штрихкод объекта"
    });
    AVS6_From_Avs6Main._list_pasportFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 245,
      _fieldName_Avs6 = "Штрихкод ОТД"
    });
  }

  /// <summary> Список полей записи AVS6 default </summary>
  private static void RecordFieldsInit()
  {
    AVS6_From_Avs6Main._list_recordFields = new List<OneField>();
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 1,
      _fieldName_Avs6 = "Формат"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 2,
      _fieldName_Avs6 = "Зона"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 3,
      _fieldName_Avs6 = "Поз"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 4,
      _fieldName_Avs6 = "Обозначение"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 5,
      _fieldName_Avs6 = "Наименование"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 6,
      _fieldName_Avs6 = "Кол"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 7,
      _fieldName_Avs6 = "Примечание"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 8,
      _fieldName_Avs6 = "Ключ IMBASE"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 9,
      _fieldName_Avs6 = "Часть"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 10,
      _fieldName_Avs6 = "Раздел"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 11,
      _fieldName_Avs6 = "Исполнение"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 12,
      _fieldName_Avs6 = "УслПоз"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 13,
      _fieldName_Avs6 = "Строк до"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 14,
      _fieldName_Avs6 = "Строк после"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 15,
      _fieldName_Avs6 = "Шаг строк"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 16 /*0x10*/,
      _fieldName_Avs6 = "Страниц"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 17,
      _fieldName_Avs6 = "Поз до"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 18,
      _fieldName_Avs6 = "Поз после"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 19,
      _fieldName_Avs6 = "Шаг поз"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 20,
      _fieldName_Avs6 = "Путь"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 21,
      _fieldName_Avs6 = "Имя файла"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 22,
      _fieldName_Avs6 = "Инвентарный номер изделия (ArtId)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 23,
      _fieldName_Avs6 = "Масса одного изделия (Служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 24,
      _fieldName_Avs6 = "Масса"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 25,
      _fieldName_Avs6 = "Материал"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 27,
      _fieldName_Avs6 = "Атрибут CADMECH"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 28,
      _fieldName_Avs6 = "ОКП"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 29,
      _fieldName_Avs6 = "Сортировка"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 30,
      _fieldName_Avs6 = "Тип документа"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 31 /*0x1F*/,
      _fieldName_Avs6 = "МассаN"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 32 /*0x20*/,
      _fieldName_Avs6 = "Поз. обозначение"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 33,
      _fieldName_Avs6 = "Размеры"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 36,
      _fieldName_Avs6 = "Imbase"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 37,
      _fieldName_Avs6 = "Смотри"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 38,
      _fieldName_Avs6 = "CADSYSTEM"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 39,
      _fieldName_Avs6 = "Литера"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 40,
      _fieldName_Avs6 = "Единица измерения количества"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 41,
      _fieldName_Avs6 = "Единица измерения массы"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 42,
      _fieldName_Avs6 = "Допустимые замены"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 43,
      _fieldName_Avs6 = "Применяется взамен (служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 44,
      _fieldName_Avs6 = "Замена совместно с (служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 45,
      _fieldName_Avs6 = "Применяется совместно с (служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 46,
      _fieldName_Avs6 = "VariantsMode (служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 47,
      _fieldName_Avs6 = "Количество (служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 48 /*0x30*/,
      _fieldName_Avs6 = "Допустимые замены (служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 49,
      _fieldName_Avs6 = "Признак принадлежности (служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 50,
      _fieldName_Avs6 = "ДопЗамены из CADMECH (служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 100,
      _fieldName_Avs6 = "Куда входит"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 101,
      _fieldName_Avs6 = "Кол. на изделие"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 102,
      _fieldName_Avs6 = "Кол. на комплекты"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 103,
      _fieldName_Avs6 = "Кол. на регулировку"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 104,
      _fieldName_Avs6 = "Кол. всего"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 105,
      _fieldName_Avs6 = "Суммарное количество"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 106,
      _fieldName_Avs6 = "Процент на регулировку"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 108,
      _fieldName_Avs6 = "Функциональная группа"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 109,
      _fieldName_Avs6 = "Документ"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 110,
      _fieldName_Avs6 = "Поставщик"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 111,
      _fieldName_Avs6 = "Покупной"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 112 /*0x70*/,
      _fieldName_Avs6 = "Каталог"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 113,
      _fieldName_Avs6 = "Кол. в одной спецификации"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 114,
      _fieldName_Avs6 = "Уровень"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 125,
      _fieldName_Avs6 = "Вспомогательное"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 126,
      _fieldName_Avs6 = "Вспомогательное2"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 127 /*0x7F*/,
      _fieldName_Avs6 = "ВспомогательноеВед"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 129,
      _fieldName_Avs6 = "Идентификатор строки"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 130,
      _fieldName_Avs6 = "Заготовка для"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 131,
      _fieldName_Avs6 = "СП сборочной единицы"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 132,
      _fieldName_Avs6 = "Не печатать"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 133,
      _fieldName_Avs6 = "Служебное1"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 134,
      _fieldName_Avs6 = "Служебное2"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 135,
      _fieldName_Avs6 = "Чистая масса"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 136,
      _fieldName_Avs6 = "GUID записи"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 137,
      _fieldName_Avs6 = "Инвентарный номер документа (DocId)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 138,
      _fieldName_Avs6 = "Описание допзамен (служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 139,
      _fieldName_Avs6 = "Признак разбитой записи (служебное)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 140,
      _fieldName_Avs6 = "GUID условий применения"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 141,
      _fieldName_Avs6 = "Условие применения"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 142,
      _fieldName_Avs6 = "Листов формата А1"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 143,
      _fieldName_Avs6 = "Листов формата А4"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 144 /*0x90*/,
      _fieldName_Avs6 = "Серийный номер"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 145,
      _fieldName_Avs6 = "Условное наименование исполнения"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 146,
      _fieldName_Avs6 = "Изменил"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 147,
      _fieldName_Avs6 = "Дата изм."
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 148,
      _fieldName_Avs6 = "Лист_Изм"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 161,
      _fieldName_Avs6 = "Не раскрывать при сборе ведомости"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 162,
      _fieldName_Avs6 = "Год файла"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 163,
      _fieldName_Avs6 = "Лист"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 164,
      _fieldName_Avs6 = "Формат (длинный)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 165,
      _fieldName_Avs6 = "Зона (длинная)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 170,
      _fieldName_Avs6 = "Наименование документа"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 171,
      _fieldName_Avs6 = "Код единицы измерения количества"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 172,
      _fieldName_Avs6 = "Код единицы измерения массы"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 173,
      _fieldName_Avs6 = "Код материала"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 174,
      _fieldName_Avs6 = "Код ведомости заказа"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 175,
      _fieldName_Avs6 = "Заготовка"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 176 /*0xB0*/,
      _fieldName_Avs6 = "Обработка"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 177,
      _fieldName_Avs6 = "Сборка"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 178,
      _fieldName_Avs6 = "Монтаж"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 179,
      _fieldName_Avs6 = "Марка"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 180,
      _fieldName_Avs6 = "Норматив"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 181,
      _fieldName_Avs6 = "Код входимости"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 182,
      _fieldName_Avs6 = "Узел, номер"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 183,
      _fieldName_Avs6 = "Узел, кол-во"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 184,
      _fieldName_Avs6 = "№ п/п"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 185,
      _fieldName_Avs6 = "M3"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 186,
      _fieldName_Avs6 = "T"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 187,
      _fieldName_Avs6 = "X"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 188,
      _fieldName_Avs6 = "Y"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 189,
      _fieldName_Avs6 = "Z"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 190,
      _fieldName_Avs6 = "Наименование и размеры материала"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 191,
      _fieldName_Avs6 = "Фрагмент"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 192 /*0xC0*/,
      _fieldName_Avs6 = "Таблица"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 193,
      _fieldName_Avs6 = "По ОСТ85.0069-72"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 211,
      _fieldName_Avs6 = "Откуда идет"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 212,
      _fieldName_Avs6 = "Куда поступает"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 213,
      _fieldName_Avs6 = "Данные провода"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 214,
      _fieldName_Avs6 = "Цвет провода"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 215,
      _fieldName_Avs6 = "Длина, м"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 216,
      _fieldName_Avs6 = "Наконечник"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 217,
      _fieldName_Avs6 = "Откуда идет (устр.)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 218,
      _fieldName_Avs6 = "Куда поступает (устр.)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 219,
      _fieldName_Avs6 = "Откуда идет (апп.-заж.)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 220,
      _fieldName_Avs6 = "Куда поступает (апп.-заж.)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 221,
      _fieldName_Avs6 = "Жгут"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 222,
      _fieldName_Avs6 = "Откуда идет (апп.)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 223,
      _fieldName_Avs6 = "Куда поступает (апп.)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 224 /*0xE0*/,
      _fieldName_Avs6 = "Откуда идет (заж.)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 225,
      _fieldName_Avs6 = "Куда поступает (заж.)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 226,
      _fieldName_Avs6 = "Обозначение провода"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 227,
      _fieldName_Avs6 = "Соединения"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 228,
      _fieldName_Avs6 = "Обозначение набора"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 229,
      _fieldName_Avs6 = "Обозначение зажима"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 230,
      _fieldName_Avs6 = "Обозначение подключаемого провода"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 231,
      _fieldName_Avs6 = "Данные зажима"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 232,
      _fieldName_Avs6 = "Количество"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 234,
      _fieldName_Avs6 = "КЛАСС"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 235,
      _fieldName_Avs6 = "НАИМЕНОВАНИЕ1"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 236,
      _fieldName_Avs6 = "Description"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 237,
      _fieldName_Avs6 = "Наименование (exp)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 238,
      _fieldName_Avs6 = "Наименование (ru)"
    });
    AVS6_From_Avs6Main._list_recordFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 239,
      _fieldName_Avs6 = "Note"
    });
  }

  /// <summary> Список полей листа утверждения AVS6 default </summary>
  private static void ListUtvFieldsInit()
  {
    AVS6_From_Avs6Main._list_UtvFields = new List<OneField>();
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 1,
      _fieldName_Avs6 = "Утвердил должность"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 2,
      _fieldName_Avs6 = "Утвердил фамилия"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 3,
      _fieldName_Avs6 = "Согласовал1 должность"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 4,
      _fieldName_Avs6 = "Согласовал1 фамилия"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 5,
      _fieldName_Avs6 = "Согласовал2 должность"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 6,
      _fieldName_Avs6 = "Согласовал2 фамилия"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 7,
      _fieldName_Avs6 = "Согласовал3 должность"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 8,
      _fieldName_Avs6 = "Согласовал3 фамилия"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 9,
      _fieldName_Avs6 = "Согласовал4 должность"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 10,
      _fieldName_Avs6 = "Согласовал4 фамилия"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 11,
      _fieldName_Avs6 = "Разработчик1 должность"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 12,
      _fieldName_Avs6 = "Разработчик1 фамилия"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 13,
      _fieldName_Avs6 = "Разработчик2 должность"
    });
    AVS6_From_Avs6Main._list_UtvFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 14,
      _fieldName_Avs6 = "Разработчик2 фамилия"
    });
  }

  /// <summary> Список полей титульного листа AVS6 default </summary>
  private static void TitlesFieldsInit()
  {
    AVS6_From_Avs6Main._list_titlesFields = new List<OneField>();
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 1,
      _fieldName_Avs6 = "Утвердил должность"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 2,
      _fieldName_Avs6 = "Утвердил фамилия"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 3,
      _fieldName_Avs6 = "Согласовал1 должность"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 4,
      _fieldName_Avs6 = "Согласовал1 фамилия"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 5,
      _fieldName_Avs6 = "Согласовал2 должность"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 6,
      _fieldName_Avs6 = "Согласовал2 фамилия"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 7,
      _fieldName_Avs6 = "Согласовал3 должность"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 8,
      _fieldName_Avs6 = "Согласовал3 фамилия"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 9,
      _fieldName_Avs6 = "Согласовал4 должность"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 10,
      _fieldName_Avs6 = "Согласовал4 фамилия"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 11,
      _fieldName_Avs6 = "Разработчик1 должность"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 12,
      _fieldName_Avs6 = "Разработчик1 фамилия"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 13,
      _fieldName_Avs6 = "Разработчик2 должность"
    });
    AVS6_From_Avs6Main._list_titlesFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 14,
      _fieldName_Avs6 = "Разработчик2 фамилия"
    });
  }

  /// <summary> Список полей листа регистрации изменений AVS6 default </summary>
  private static void LisIzmFieldsInit()
  {
    AVS6_From_Avs6Main._list_IzmFields = new List<OneField>();
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 1,
      _fieldName_Avs6 = "Изм"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 2,
      _fieldName_Avs6 = "Измененных"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 3,
      _fieldName_Avs6 = "Замененных"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 4,
      _fieldName_Avs6 = "Новых"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 5,
      _fieldName_Avs6 = "Аннулированных"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 6,
      _fieldName_Avs6 = "Всего листов"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 7,
      _fieldName_Avs6 = "№ документа"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 8,
      _fieldName_Avs6 = "Входящий"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 9,
      _fieldName_Avs6 = "Подп"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 10,
      _fieldName_Avs6 = "Дата"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 11,
      _fieldName_Avs6 = "Лист"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 12,
      _fieldName_Avs6 = "Зона"
    });
    AVS6_From_Avs6Main._list_IzmFields.Add(new OneField()
    {
      _fieldType_Avs6 = (byte) 11,
      _fieldName_Avs6 = "№ Листа документа"
    });
  }

  private static void List_ElDocList_Processed_Init()
  {
    AVS6_From_Avs6Main._list_ElDocList_Processed = new List<ElDocList>();
  }

  /// <summary> Чтение файла настроек в одну длинную строку </summary>
  /// <param name="fileIni"></param>
  /// <returns></returns>
  private static string GetIniFileContent(string fileIni)
  {
    return File.Exists(fileIni) ? File.ReadAllText(fileIni, Encoding.Default) : string.Empty;
  }

  /// <summary> Имя по индексу </summary>
  /// <param name="TypeListFields1"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  public static string FieldNameByIndex(
    AVS6_From_Avs6Main.TypeListFields TypeListFields1,
    int index)
  {
    OneField oneField = AVS6_From_Avs6Main.FieldByIndex(TypeListFields1, index);
    return oneField == null ? "" : oneField._fieldName_Avs6;
  }

  /// <summary> Тип по индексу </summary>
  /// <param name="TypeListFields1"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  public static byte FieldTypeByIndex(AVS6_From_Avs6Main.TypeListFields TypeListFields1, int index)
  {
    OneField oneField = AVS6_From_Avs6Main.FieldByIndex(TypeListFields1, index);
    return oneField == null ? (byte) 0 : oneField._fieldType_Avs6;
  }

  /// <summary> Имя по типу </summary>
  /// <param name="TypeListFields1"></param>
  /// <param name="type"></param>
  /// <returns></returns>
  public static string FieldNameByType(AVS6_From_Avs6Main.TypeListFields TypeListFields1, byte type)
  {
    OneField oneField = AVS6_From_Avs6Main.FieldByType(TypeListFields1, type);
    return oneField == null ? "" : oneField._fieldName_Avs6;
  }

  /// <summary> Тип по имени </summary>
  /// <param name="TypeListFields1"></param>
  /// <param name="name"></param>
  /// <returns></returns>
  public static byte FieldTypeByName(AVS6_From_Avs6Main.TypeListFields TypeListFields1, string name)
  {
    OneField oneField = AVS6_From_Avs6Main.FieldByName(TypeListFields1, name);
    return oneField == null ? (byte) 0 : oneField._fieldType_Avs6;
  }

  /// <summary> Описание поля по индексу </summary>
  /// <param name="TypeListFields1"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  public static OneField FieldByIndex(AVS6_From_Avs6Main.TypeListFields TypeListFields1, int index)
  {
    if (index < 0)
      return (OneField) null;
    List<OneField> oneFieldList;
    switch (TypeListFields1)
    {
      case AVS6_From_Avs6Main.TypeListFields.Pasport:
        oneFieldList = AVS6_From_Avs6Main._list_pasportFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Record:
        oneFieldList = AVS6_From_Avs6Main._list_recordFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Titles:
        oneFieldList = AVS6_From_Avs6Main._list_titlesFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Utv:
        oneFieldList = AVS6_From_Avs6Main._list_UtvFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Lizm:
        oneFieldList = AVS6_From_Avs6Main._list_IzmFields;
        break;
      default:
        return (OneField) null;
    }
    if (oneFieldList == null)
      return (OneField) null;
    return index > oneFieldList.Count - 1 ? (OneField) null : oneFieldList[index];
  }

  /// <summary> Описание поля по типу </summary>
  /// <param name="TypeListFields1"></param>
  /// <param name="type"></param>
  /// <returns></returns>
  public static OneField FieldByType(AVS6_From_Avs6Main.TypeListFields TypeListFields1, byte type)
  {
    if (type < (byte) 1)
      return (OneField) null;
    List<OneField> oneFieldList;
    switch (TypeListFields1)
    {
      case AVS6_From_Avs6Main.TypeListFields.Pasport:
        oneFieldList = AVS6_From_Avs6Main._list_pasportFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Record:
        oneFieldList = AVS6_From_Avs6Main._list_recordFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Titles:
        oneFieldList = AVS6_From_Avs6Main._list_titlesFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Utv:
        oneFieldList = AVS6_From_Avs6Main._list_UtvFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Lizm:
        oneFieldList = AVS6_From_Avs6Main._list_IzmFields;
        break;
      default:
        return (OneField) null;
    }
    if (oneFieldList == null)
      return (OneField) null;
    for (int index = 0; index < oneFieldList.Count; ++index)
    {
      OneField oneField = oneFieldList[index];
      if ((int) oneField._fieldType_Avs6 == (int) type)
        return oneField;
    }
    return (OneField) null;
  }

  /// <summary> Описание поля по имени </summary>
  /// <param name="TypeListFields1"></param>
  /// <param name="name"></param>
  /// <returns></returns>
  public static OneField FieldByName(AVS6_From_Avs6Main.TypeListFields TypeListFields1, string name)
  {
    if (name == null || name == "")
      return (OneField) null;
    List<OneField> oneFieldList;
    switch (TypeListFields1)
    {
      case AVS6_From_Avs6Main.TypeListFields.Pasport:
        oneFieldList = AVS6_From_Avs6Main._list_pasportFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Record:
        oneFieldList = AVS6_From_Avs6Main._list_recordFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Titles:
        oneFieldList = AVS6_From_Avs6Main._list_titlesFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Utv:
        oneFieldList = AVS6_From_Avs6Main._list_UtvFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Lizm:
        oneFieldList = AVS6_From_Avs6Main._list_IzmFields;
        break;
      default:
        return (OneField) null;
    }
    if (oneFieldList == null)
      return (OneField) null;
    for (int index = 0; index < oneFieldList.Count; ++index)
    {
      OneField oneField = oneFieldList[index];
      if (oneField._fieldName_Avs6 == name)
        return oneField;
    }
    return (OneField) null;
  }

  public static int IndexByType(AVS6_From_Avs6Main.TypeListFields TypeListFields1, byte type)
  {
    if (type < (byte) 1)
      return -1;
    List<OneField> oneFieldList;
    switch (TypeListFields1)
    {
      case AVS6_From_Avs6Main.TypeListFields.Pasport:
        oneFieldList = AVS6_From_Avs6Main._list_pasportFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Record:
        oneFieldList = AVS6_From_Avs6Main._list_recordFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Titles:
        oneFieldList = AVS6_From_Avs6Main._list_titlesFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Utv:
        oneFieldList = AVS6_From_Avs6Main._list_UtvFields;
        break;
      case AVS6_From_Avs6Main.TypeListFields.Lizm:
        oneFieldList = AVS6_From_Avs6Main._list_IzmFields;
        break;
      default:
        return -1;
    }
    if (oneFieldList == null)
      return -1;
    for (int index = 0; index < oneFieldList.Count; ++index)
    {
      if ((int) oneFieldList[index]._fieldType_Avs6 == (int) type)
        return index;
    }
    return -1;
  }

  public enum TypeListFields
  {
    Pasport,
    Record,
    Titles,
    Utv,
    Lizm,
  }
}
