
// Type: Intermech.Interfaces.MeasureHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Interfaces
{
    /// <summary>Статический класс, который занимается обработкой единиц измерения на сервере и клиенте.
    /// Перед работой должен быть проинициализирован!!!
    /// </summary>
    public static class MeasureHelper
    {
      private static MeasuresConvertor instance;
      private static List<Guid> asQuantityPhysList;

      /// <summary>Экземпляр класса конвертера единиц измерения</summary>
      public static MeasuresConvertor Instance
      {
        get
        {
          if (MeasureHelper.instance == null)
            MeasureHelper.instance = new MeasuresConvertor();
          return MeasureHelper.instance;
        }
        set => MeasureHelper.instance = value;
      }

      /// <summary>Инициализировать хелпер</summary>
      /// <param name="measures">Единицы измерения которые должены поддерживаться</param>
      public static void Init(MeasureDescriptor[] measures) => MeasureHelper.Instance.Init(measures);

      public static (MeasureDescriptor md, float K) FindOperationResultMeasure(string operation)
      {
        return MeasureHelper.Instance.FindOperationResultMeasure(operation);
      }

      /// <summary>
      /// Метод выполняет преобразование значений -1, 0 и 1 в соответствующие значения CompareResult.
      /// Используется для конвертации значений системных функций сравнения в CompareResult.
      /// Для использования метода класс инициализировать не требуется.
      /// </summary>
      /// <param name="SysCompareResult">Значения для преобразования (-1, 0, 1)</param>
      /// <returns>Преобразованное значение или CompareResult.NotCompatible</returns>
      public static CompareResult IntToCompareResult(int SysCompareResult)
      {
        switch (SysCompareResult)
        {
          case -1:
            return CompareResult.Less;
          case 0:
            return CompareResult.Equal;
          case 1:
            return CompareResult.More;
          default:
            return CompareResult.NotCompatible;
        }
      }

      /// <summary>Единицы измерения</summary>
      public static MeasureDescriptor[] Measures => MeasureHelper.Instance.Measures;

      /// <summary>Возвращает ид. единицы измерения по её гуиду</summary>
      /// <param name="measureGuid">Гуид единицы измерения (гуид версии)</param>
      /// <returns>Ид. или -1 если такой единицы измерения не найдено</returns>
      public static long GetMeasureID(Guid measureGuid)
      {
        return MeasureHelper.Instance.GetMeasureID(measureGuid);
      }

      /// <summary>Возвращает ид. базовой единицы измерения физической величины physicalQuantityID.
      /// Если не находит - возвращает -1.
      /// </summary>
      public static long GetBaseMeasureID(long physicalQuantityID)
      {
        return MeasureHelper.Instance.GetBaseMeasureID(physicalQuantityID);
      }

      public static long GetBaseMeasureID_ByMeasureID(long measureID)
      {
        return MeasureHelper.Instance.GetBaseMeasureID_ByMeasureID(measureID);
      }

      /// <summary>Возвращает результат operand1 + operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public static MeasuredValue Add(
        MeasuredValue operand1,
        MeasuredValue operand2,
        bool throwException)
      {
        return MeasureHelper.Instance.Add(operand1, operand2, throwException);
      }

      /// <summary>Возвращает результат operand1/operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public static MeasuredValue Divide(
        MeasuredValue operand1,
        MeasuredValue operand2,
        bool throwException)
      {
        return MeasureHelper.Instance.Divide(operand1, operand2, throwException);
      }

      /// <summary>Возвращает результат operand1*operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public static MeasuredValue Multiply(
        MeasuredValue operand1,
        MeasuredValue operand2,
        bool throwException)
      {
        return MeasureHelper.Instance.Multiply(operand1, operand2, throwException);
      }

      /// <summary>Возвращает результат operand1 - operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public static MeasuredValue Substract(
        MeasuredValue operand1,
        MeasuredValue operand2,
        bool throwException)
      {
        return MeasureHelper.Instance.Substract(operand1, operand2, throwException);
      }

      /// <summary>Возвращает результат operand1 + operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public static MeasuredValue Add(MeasuredValue operand1, MeasuredValue operand2)
      {
        return MeasureHelper.Instance.Add(operand1, operand2);
      }

      /// <summary>Возвращает результат operand1/operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public static MeasuredValue Divide(MeasuredValue operand1, MeasuredValue operand2)
      {
        return MeasureHelper.Instance.Divide(operand1, operand2);
      }

      /// <summary>Возвращает результат operand1*operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public static MeasuredValue Multiply(MeasuredValue operand1, MeasuredValue operand2)
      {
        return MeasureHelper.Instance.Multiply(operand1, operand2);
      }

      /// <summary>Возвращает результат operand1 - operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public static MeasuredValue Substract(MeasuredValue operand1, MeasuredValue operand2)
      {
        return MeasureHelper.Instance.Substract(operand1, operand2);
      }

      /// <summary>Заменяет описатель единицы измерения mDescriptor (юзается если изменились свойства описателя).
      /// Возвращает true, если замена была осуществлена.</summary>
      public static bool ReplaceDescriptor(MeasureDescriptor mDescriptor)
      {
        return MeasureHelper.Instance.ReplaceDescriptor(mDescriptor);
      }

      /// <summary>Добавляет описатель единицы измерения mDescriptor.</summary>
      public static void AddDescriptor(MeasureDescriptor mDescriptor)
      {
        MeasureHelper.Instance.AddDescriptor(mDescriptor);
      }

      /// <summary>Возвращает описатель базовой единицы измерения для описателя mDescriptor</summary>
      public static MeasureDescriptor FindBaseValue(MeasureDescriptor mDescriptor)
      {
        return MeasureHelper.Instance.FindBaseValue(mDescriptor);
      }

      /// <summary>Находит описатель единицы измерения по ее значению. Если описатель не найден, то возвращает
      /// MeasureDescriptor.Empty == true
      /// </summary>
      public static MeasureDescriptor FindDescriptor(MeasuredValue mValue)
      {
        return MeasureHelper.Instance.FindDescriptor(mValue);
      }

      /// <summary>Находит описатель единицы измерения по ее идентификатору.
      /// Если описатель не найден, то возвращает MeasureDescriptor.Empty == true
      /// </summary>
      public static MeasureDescriptor FindDescriptor(long measureID)
      {
        return MeasureHelper.Instance.FindDescriptor(measureID);
      }

      /// <summary>Находит описатель единицы измерения по ее короткому имени.
      /// Если описатель не найден, то возвращает MeasureDescriptor.Empty == true
      /// </summary>
      public static MeasureDescriptor FindDescriptor(string shortName)
      {
        return MeasureHelper.Instance.FindDescriptor(shortName);
      }

      /// <summary>Возвращает результат сравнения двух значений, выраженных в единицах измерения.
      /// Если val1 == val2, то возвращает CompareResult.Equal
      /// Если val1 &gt; val2, то возвращает CompareResult.More
      /// Если val1 меньше val2, то возвращает CompareResult.Less
      /// Если единицы измерения несовместимы, то возвращает CompareResult.NotCompatible
      /// </summary>
      public static CompareResult Compare(MeasuredValue val1, MeasuredValue val2)
      {
        return MeasureHelper.Instance.Compare(val1, val2, Consts.MaxPrecision);
      }

      /// <summary>Возвращает результат сравнения двух значений, выраженных в единицах измерения.
      /// Если val1 == val2, то возвращает CompareResult.Equal
      /// Если val1 &gt; val2, то возвращает CompareResult.More
      /// Если val1 меньше val2, то возвращает CompareResult.Less
      /// Если единицы измерения несовместимы, то возвращает CompareResult.NotCompatible
      /// </summary>
      public static CompareResult Compare(MeasuredValue val1, MeasuredValue val2, int precision)
      {
        return MeasureHelper.Instance.Compare(val1, val2, precision);
      }

      /// <summary>
      /// Возвращает true если значения oldVal и newVal отличаются по величине или строковой составляющей
      /// </summary>
      public static bool IsNewValue(MeasuredValue oldVal, MeasuredValue newVal)
      {
        return oldVal.Caption.Trim() != newVal.Caption.Trim() || MeasureHelper.Instance.Compare(oldVal, newVal, Consts.MaxPrecision) != 0;
      }

      /// <summary>Конвертирует строковое значение mValue в структуру MeasuredValue</summary>
      public static MeasuredValue ConvertToMeasuredValue(string mValue)
      {
        return MeasureHelper.Instance.ConvertToMeasuredValue(mValue);
      }

      /// <summary>Конвертирует строковое значение mValue в структуру MeasuredValue</summary>
      /// <param name="mValue">Строковое значение</param>
      /// <param name="defaultMeasure">Единица измерения по умолчанию</param>
      /// <param name="exceptionIfFail">Генерировать исключение если конвертировать в Double нельзя</param>
      /// <returns>MeasuredValue</returns>
      public static MeasuredValue ConvertToMeasuredValue(
        string mValue,
        string defaultMeasure,
        bool exceptionIfFail)
      {
        return MeasureHelper.Instance.ConvertToMeasuredValue(mValue, defaultMeasure, exceptionIfFail);
      }

      /// <summary>Конвертирует строковое значение mValue в структуру MeasuredValue</summary>
      /// <param name="mValue">Строковое значение</param>
      /// <param name="exceptionIfFail">Генерировать исключение если конвертировать нельзя</param>
      /// <returns>MeasuredValue</returns>
      public static MeasuredValue ConvertToMeasuredValue(string mValue, bool exceptionIfFail)
      {
        return MeasureHelper.Instance.ConvertToMeasuredValue(mValue, exceptionIfFail);
      }

      /// <summary>Конвертирует строковое значение mValue в физическую величину и единицы измерения</summary>
      /// <param name="mValue">Строковое значение</param>
      /// <param name="defaultMeasure">Единица измерения по умолчанию</param>
      /// <param name="value">Значение</param>
      /// <param name="measureDescriptor">Единица измерения</param>
      /// <param name="exceptionIfFail">Генерировать исключение если конвертировать в Double нельзя</param>
      /// <returns>true, если конвертация прошла успешно</returns>
      public static bool ConvertToMeasuredValue(
        string mValue,
        MeasureDescriptor defaultMeasure,
        out double value,
        out MeasureDescriptor measureDescriptor,
        bool exceptionIfFail)
      {
        return MeasureHelper.Instance.ConvertToMeasuredValue(mValue, defaultMeasure, out value, out measureDescriptor, exceptionIfFail);
      }

      /// <summary>Конвертирует строковое значение mValue в структуру MeasuredValue</summary>
      /// <param name="mValue">Строковое значение</param>
      /// <param name="defaultMeasure">Единица измерения по умолчанию</param>
      /// <param name="exceptionIfFail">Генерировать исключение если конвертировать в Double нельзя</param>
      /// <returns>MeasuredValue</returns>
      public static MeasuredValue ConvertToMeasuredValue(
        string mValue,
        MeasureDescriptor defaultMeasure,
        bool exceptionIfFail)
      {
        return MeasureHelper.Instance.ConvertToMeasuredValue(mValue, defaultMeasure, exceptionIfFail);
      }

      /// <summary>
      /// Конвертирует значение mValue в единицу измерения toMeasureID. Если физические величины несовместимы, то генерирует исключение.
      /// </summary>
      public static MeasuredValue ConvertToMeasuredValue(MeasuredValue mValue, long toMeasureID)
      {
        return MeasureHelper.Instance.ConvertToMeasuredValue(mValue, toMeasureID);
      }

      /// <summary>Конвертирует значение val в строковое представление этого значения в
      /// единицах измерения measureID. Если measureID не найдено - возвращает пустую строку!!!
      /// Если convertFromBase == true, то конвертирует значение val из базовой величины в
      /// величину measureID
      /// </summary>
      public static string ConvertToString(double val, long measureID, bool convertFromBase)
      {
        return MeasureHelper.Instance.ConvertToString(val, measureID, convertFromBase);
      }

      /// <summary>
      /// Конвертирует значение value в базовую единицу измерения
      /// </summary>
      public static MeasuredValue ConvertToBaseMeasure(MeasuredValue value)
      {
        return MeasureHelper.Instance.ConvertToBaseMeasure(value);
      }

      /// <summary>
      /// Возвращает единицу измерения по-умолчанию для физической величины physicalQuantityID.
      /// Если такой не задано - возвращает null.
      /// </summary>
      public static MeasureDescriptor GetDefaultMeasure(long physicalQuantityID)
      {
        return MeasureHelper.Instance.GetDefaultMeasure(physicalQuantityID);
      }

      /// <summary>
      /// Проверяет строковое представление измеряемой величины и корректирует его, если оно не содержит указания единицы измерения.
      /// </summary>
      /// <param name="mv">Корректируемое значение</param>
      public static void CorrectCaption(MeasuredValue mv)
      {
        if (mv == null)
          throw new ArgumentNullException(nameof (mv));
        string s = mv.IsCaptionPresent ? mv.Caption.Trim() : string.Empty;
        if (string.IsNullOrEmpty(s))
        {
          mv.Caption = MeasureHelper.ConvertToString(mv.Value, mv.MeasureID, false);
        }
        else
        {
          if (!char.IsDigit(s, s.Length - 1) || Consts.IsUndefinedObjectId(mv.MeasureID))
            return;
          MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(mv);
          if (descriptor.Empty || string.IsNullOrEmpty(descriptor.ShortName))
            return;
          mv.Caption = $"{s} {descriptor.ShortName}";
        }
      }

      /// <summary>
      /// Список физических величин, которые используются в системе в контексте "Количество" (по умолчанию + настройка)
      /// </summary>
      public static List<Guid> AsQuantityPhysList
      {
        get
        {
          if (MeasureHelper.asQuantityPhysList == null)
          {
            MeasureHelper.asQuantityPhysList = new List<Guid>((IEnumerable<Guid>) SystemGUIDs.objectQuantityPhysListGuids);
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              string str = sessionKeeper.Session.Configurations.ReadString("CLIENT", "COMMON", "QUANTITYPHYSLIST", string.Empty, DBConfigMode.GlobalOnly);
              if (str != string.Empty)
              {
                string[] strArray = str.Split(';');
                if (strArray != null)
                {
                  for (int index = 0; index < strArray.Length; ++index)
                  {
                    try
                    {
                      MeasureHelper.asQuantityPhysList.Add(Guid.Parse(strArray[index]));
                    }
                    catch
                    {
                    }
                  }
                }
              }
            }
          }
          return MeasureHelper.asQuantityPhysList;
        }
        set => MeasureHelper.asQuantityPhysList = (List<Guid>) null;
      }

      /// <summary>
      /// Список описателей физических величин, которые используются в системе в контексте "Количество"
      /// Включает в себя список физических величин по умолчанию + список физических величин из настройки параметров IPS
      /// </summary>
      public static MeasureDescriptor[] AsQuantityPhysMeasureDescriptors
      {
        get
        {
          return ((IEnumerable<MeasureDescriptor>) MeasureHelper.Measures).Where<MeasureDescriptor>((Func<MeasureDescriptor, bool>) (x => MeasureHelper.AsQuantityPhysList.IndexOf(x.PhysicalQuantityGuid) != -1)).ToArray<MeasureDescriptor>();
        }
      }

      /// <summary>
      /// Добавить краткое наименование единиц измерения, если нужно
      /// </summary>
      /// <param name="mvalue"></param>
      public static void AppendShortNameToCaption(this MeasuredValue mvalue)
      {
        MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(mvalue.MeasureID);
        if (descriptor.Empty || string.IsNullOrEmpty(descriptor.ShortName) || mvalue.Caption.EndsWith(descriptor.ShortName))
          return;
        string normCaption = ClientStringNormalizer.GetIndexedString(mvalue.Caption);
        if (((IEnumerable<string>) descriptor.ShortNameIndex).Any<string>((Func<string, bool>) (sn => normCaption.EndsWith(sn))))
          return;
        mvalue.Caption = $"{mvalue.Caption} {descriptor.ShortName}";
      }
    }
}
