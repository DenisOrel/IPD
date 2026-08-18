
// Type: Intermech.Interfaces.MeasuresConvertor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Interfaces
{
    /// <summary>Класс, который занимается обработкой единиц измерения.
    /// Перед работой должен быть проинициализирован!!!
    /// </summary>
    public class MeasuresConvertor
    {
      /// <summary>
      /// 
      /// </summary>
      /// <remarks>Как показал профайлер не лучшим было ранее решение хранить все в массиве </remarks>
      private Dictionary<long, MeasureDescriptor> _Measures;
      /// <summary>
      /// Гинзбург: это кэш для быстрого поиска дескрипторов при выполнении операций.
      /// НЕЛЬЗЯ сканировать сотни настроек каждый раз заново: каждая операция может вызываться в цикле миллионы раз!
      /// </summary>
      private Dictionary<string, (MeasureDescriptor md, float K)> _OpResultMeasures;
      private bool normalizeString = true;

      /// <summary>Использовать нормализацию текста при преобразовании в MeasuredValue</summary>
      public bool NormalizeString
      {
        get => this.normalizeString;
        set => this.normalizeString = value;
      }

      /// <summary>Инициализировать</summary>
      /// <param name="measures">Единицы измерения которые должены поддерживаться</param>
      public void Init(MeasureDescriptor[] measures)
      {
        this._Measures = measures != null ? ((IEnumerable<MeasureDescriptor>) measures).ToDictionary<MeasureDescriptor, long, MeasureDescriptor>((Func<MeasureDescriptor, long>) (item => item.MeasureID), (Func<MeasureDescriptor, MeasureDescriptor>) (item => item)) : (Dictionary<long, MeasureDescriptor>) null;
        if (this._OpResultMeasures == null)
          this._OpResultMeasures = new Dictionary<string, (MeasureDescriptor, float)>();
        else
          this._OpResultMeasures.Clear();
      }

      /// <summary>Единицы измерения</summary>
      public MeasureDescriptor[] Measures
      {
        get
        {
          return this._Measures == null ? (MeasureDescriptor[]) null : this._Measures.Select<KeyValuePair<long, MeasureDescriptor>, MeasureDescriptor>((Func<KeyValuePair<long, MeasureDescriptor>, MeasureDescriptor>) (item => item.Value)).ToArray<MeasureDescriptor>();
        }
      }

      private void CheckInit()
      {
        if (this._Measures == null)
          throw new Exception("MeasuresConvertor not initialized. Call MeasuresConvertor.Init first.");
      }

      /// <summary>Возвращает ид. единицы измерения по её гуиду</summary>
      /// <param name="measureGuid">Гуид единицы измерения (гуид версии)</param>
      /// <returns>Ид. или -1 если такой единицы измерения не найдено</returns>
      public long GetMeasureID(Guid measureGuid)
      {
        foreach (MeasureDescriptor measureDescriptor in this._Measures.Values)
        {
          if (measureDescriptor.MeasureGuid.Equals(measureGuid))
            return measureDescriptor.MeasureID;
        }
        return -1;
      }

      /// <summary>Возвращает ид. базовой единицы измерения физической величины physicalQuantityID.
      /// Если не находит - возвращает -1.
      /// </summary>
      public long GetBaseMeasureID(long physicalQuantityID)
      {
        foreach (MeasureDescriptor measureDescriptor in this._Measures.Values)
        {
          if (measureDescriptor.PhysicalQuantityID == physicalQuantityID && measureDescriptor.K == 1.0)
            return measureDescriptor.MeasureID;
        }
        return -1;
      }

      /// <summary>
      /// Возвращает единицу измерения по-умолчанию для физической величины physicalQuantityID.
      /// Если такой не задано - возвращает null.
      /// </summary>
      public MeasureDescriptor GetDefaultMeasure(long physicalQuantityID)
      {
        foreach (MeasureDescriptor defaultMeasure in this._Measures.Values)
        {
          if (defaultMeasure.PhysicalQuantityID == physicalQuantityID && defaultMeasure.IsDefault)
            return defaultMeasure;
        }
        return (MeasureDescriptor) null;
      }

      public long GetBaseMeasureID_ByMeasureID(long measureID)
      {
        MeasureDescriptor descriptor = this.FindDescriptor(measureID);
        return descriptor.K == 1.0 ? descriptor.MeasureID : this.GetBaseMeasureID(descriptor.PhysicalQuantityID);
      }

      /// <summary>Возвращает описатель базовой единицы измерения, которая получается в результате выполнения
      /// операции operation над двумя другими единицами измерения (пример operation - м*м)
      /// </summary>
      public (MeasureDescriptor md, float K) FindOperationResultMeasure(string operation)
      {
        (MeasureDescriptor, float) operationResultMeasure;
        if (this._OpResultMeasures.TryGetValue(operation, out operationResultMeasure))
          return operationResultMeasure;
        foreach (MeasureDescriptor measureDescriptor in this._Measures.Values)
        {
          for (int index = 0; index < measureDescriptor.OperationsList.Length; ++index)
          {
            string operations = measureDescriptor.OperationsList[index];
            if (operations == operation || operations.StartsWith(operation + " "))
            {
              float num = 1f;
              string str = operations.Substring(operation.Length).Trim();
              try
              {
                num = (float) Convert.ToDouble(str);
              }
              catch (FormatException ex1)
              {
                if (str.Contains("."))
                  str = str.Replace(".", ",");
                else if (str.Contains(","))
                  str = str.Replace(",", ".");
                try
                {
                  num = (float) Convert.ToDouble(str);
                }
                catch (FormatException ex2)
                {
                }
              }
              this._OpResultMeasures.Add(operation, (measureDescriptor, num));
              return (measureDescriptor, num);
            }
          }
        }
        return (new MeasureDescriptor(true), 1f);
      }

      /// <summary>Производит математическую операцию operation над величинами operand1 и operand2</summary>
      private MeasuredValue DoOperation(
        MeasuredValue operand1,
        MeasuredValue operand2,
        MeasuresConvertor.MeasureOperation operation,
        bool throwException)
      {
        this.CheckInit();
        MeasureDescriptor descriptor1 = this.FindDescriptor(operand1);
        MeasureDescriptor descriptor2 = this.FindDescriptor(operand2);
        MeasuredValue measuredValue;
        switch (operation)
        {
          case MeasuresConvertor.MeasureOperation.Add:
            if (descriptor1.PhysicalQuantityID != descriptor2.PhysicalQuantityID)
            {
              if (throwException)
                throw new KernelExceptionID(222, (object) descriptor1.LongName, (object) descriptor2.LongName);
              return (MeasuredValue) null;
            }
            if (descriptor1.MeasureID == descriptor2.MeasureID)
            {
              measuredValue = new MeasuredValue(operand1.Value + operand2.Value, descriptor1.MeasureID);
              break;
            }
            MeasureDescriptor baseValue1 = this.FindBaseValue(descriptor1);
            measuredValue = new MeasuredValue(operand1.Value * descriptor1.K + operand2.Value * descriptor2.K, baseValue1.MeasureID);
            break;
          case MeasuresConvertor.MeasureOperation.Substract:
            if (descriptor1.PhysicalQuantityID != descriptor2.PhysicalQuantityID)
            {
              if (throwException)
                throw new KernelExceptionID(223, (object) descriptor1.LongName, (object) descriptor2.LongName);
              return (MeasuredValue) null;
            }
            if (descriptor1.MeasureID == descriptor2.MeasureID)
            {
              measuredValue = new MeasuredValue(operand1.Value - operand2.Value, descriptor1.MeasureID);
              break;
            }
            MeasureDescriptor baseValue2 = this.FindBaseValue(descriptor1);
            measuredValue = new MeasuredValue(operand1.Value * descriptor1.K - operand2.Value * descriptor2.K, baseValue2.MeasureID);
            break;
          case MeasuresConvertor.MeasureOperation.Multiply:
            MeasureDescriptor baseValue3 = this.FindBaseValue(descriptor2);
            MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
            if (operand1.Caption == "" && operand1.MeasureID == 0L)
              return operand1;
            if (operand2.Caption == "" && operand2.MeasureID == 0L)
              return operand2;
            MeasureDescriptor baseValue4 = this.FindBaseValue(descriptor1);
            if (descriptor1.PhysicalQuantityGuid == SystemGUIDs.objectQuantityGuid)
              measureDescriptor = descriptor2;
            else if (descriptor2.PhysicalQuantityGuid == SystemGUIDs.objectQuantityGuid)
              measureDescriptor = descriptor1;
            if (measureDescriptor != null)
            {
              measuredValue = new MeasuredValue(operand1.Value * operand2.Value, measureDescriptor.MeasureID);
              break;
            }
            (MeasureDescriptor md1, float K1) = this.FindOperationResultMeasure($"{descriptor1.ShortName}*{descriptor2.ShortName}");
            bool flag = !md1.Empty;
            if (md1.Empty)
              (md1, K1) = this.FindOperationResultMeasure($"{baseValue4.ShortName}*{baseValue3.ShortName}");
            if (md1.Empty)
            {
              if (baseValue4.MeasureID != baseValue3.MeasureID)
              {
                (md1, K1) = this.FindOperationResultMeasure($"{descriptor2.ShortName}*{descriptor1.ShortName}");
                flag = !md1.Empty;
                if (md1.Empty)
                  (md1, K1) = this.FindOperationResultMeasure($"{baseValue3.ShortName}*{baseValue4.ShortName}");
              }
              if (md1.Empty)
              {
                if (throwException)
                  throw new KernelExceptionID(224 /*0xE0*/, (object) descriptor1.LongName, (object) descriptor2.LongName);
                return (MeasuredValue) null;
              }
            }
            measuredValue = !flag ? new MeasuredValue(operand1.Value * descriptor1.K * (operand2.Value * descriptor2.K), md1.MeasureID) : new MeasuredValue(operand1.Value * operand2.Value * (double) K1, md1.MeasureID);
            break;
          case MeasuresConvertor.MeasureOperation.Divide:
            (MeasureDescriptor md2, float K2) = this.FindOperationResultMeasure($"{descriptor1.ShortName}/{descriptor2.ShortName}");
            if (md2.Empty)
            {
              if (throwException)
                throw new KernelExceptionID(225, (object) descriptor1.LongName, (object) descriptor2.LongName);
              return (MeasuredValue) null;
            }
            measuredValue = new MeasuredValue(operand1.Value / operand2.Value * (double) K2, md2.MeasureID);
            break;
          default:
            measuredValue = new MeasuredValue(0.0, 0L, string.Empty);
            break;
        }
        return measuredValue;
      }

      /// <summary>Возвращает результат operand1 + operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public MeasuredValue Add(MeasuredValue operand1, MeasuredValue operand2, bool throwException)
      {
        return this.DoOperation(operand1, operand2, MeasuresConvertor.MeasureOperation.Add, throwException);
      }

      /// <summary>Возвращает результат operand1/operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public MeasuredValue Divide(MeasuredValue operand1, MeasuredValue operand2, bool throwException)
      {
        return this.DoOperation(operand1, operand2, MeasuresConvertor.MeasureOperation.Divide, throwException);
      }

      /// <summary>Возвращает результат operand1*operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public MeasuredValue Multiply(
        MeasuredValue operand1,
        MeasuredValue operand2,
        bool throwException)
      {
        return this.DoOperation(operand1, operand2, MeasuresConvertor.MeasureOperation.Multiply, throwException);
      }

      /// <summary>Возвращает результат operand1 - operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public MeasuredValue Substract(
        MeasuredValue operand1,
        MeasuredValue operand2,
        bool throwException)
      {
        return this.DoOperation(operand1, operand2, MeasuresConvertor.MeasureOperation.Substract, throwException);
      }

      /// <summary>Возвращает результат operand1 + operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public MeasuredValue Add(MeasuredValue operand1, MeasuredValue operand2)
      {
        return this.DoOperation(operand1, operand2, MeasuresConvertor.MeasureOperation.Add, true);
      }

      /// <summary>Возвращает результат operand1/operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public MeasuredValue Divide(MeasuredValue operand1, MeasuredValue operand2)
      {
        return this.DoOperation(operand1, operand2, MeasuresConvertor.MeasureOperation.Divide, true);
      }

      /// <summary>Возвращает результат operand1*operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public MeasuredValue Multiply(MeasuredValue operand1, MeasuredValue operand2)
      {
        return this.DoOperation(operand1, operand2, MeasuresConvertor.MeasureOperation.Multiply, true);
      }

      /// <summary>Возвращает результат operand1 - operand2, выраженный в базовой единице измерения
      /// результирующей физической величины</summary>
      public MeasuredValue Substract(MeasuredValue operand1, MeasuredValue operand2)
      {
        return this.DoOperation(operand1, operand2, MeasuresConvertor.MeasureOperation.Substract, true);
      }

      /// <summary>Заменяет описатель единицы измерения mDescriptor (юзается если изменились свойства описателя).
      /// Возвращает true, если замена была осуществлена.</summary>
      public bool ReplaceDescriptor(MeasureDescriptor mDescriptor)
      {
        this.CheckInit();
        lock (this._Measures)
        {
          if (this._Measures.ContainsKey(mDescriptor.MeasureID))
          {
            this._Measures[mDescriptor.MeasureID] = mDescriptor;
            return true;
          }
        }
        return false;
      }

      /// <summary>Добавляет в список описатель единицы измерения mDescriptor.</summary>
      public void AddDescriptor(MeasureDescriptor mDescriptor)
      {
        this.CheckInit();
        lock (this._Measures)
        {
          if (this._Measures.ContainsKey(mDescriptor.MeasureID))
            throw new KernelException($"MeasureID N{mDescriptor.MeasureID} already exists.");
          this._Measures.Add(mDescriptor.MeasureID, mDescriptor);
        }
      }

      /// <summary>Возвращает описатель базовой единицы измерения для описателя mDescriptor</summary>
      public MeasureDescriptor FindBaseValue(MeasureDescriptor mDescriptor)
      {
        this.CheckInit();
        if (mDescriptor.K == 1.0)
          return mDescriptor;
        foreach (MeasureDescriptor baseValue in this._Measures.Values)
        {
          if (baseValue.PhysicalQuantityID == mDescriptor.PhysicalQuantityID && baseValue.K == 1.0)
            return baseValue;
        }
        return new MeasureDescriptor(true);
      }

      /// <summary>Находит описатель единицы измерения по ее значению. Если описатель не найден, то возвращает
      /// MeasureDescriptor.Empty == true
      /// </summary>
      public MeasureDescriptor FindDescriptor(MeasuredValue mValue)
      {
        return this.FindDescriptor(mValue.MeasureID);
      }

      /// <summary>Находит описатель единицы измерения по ее идентификатору.
      /// Если описатель не найден, то возвращает MeasureDescriptor.Empty == true
      /// </summary>
      public MeasureDescriptor FindDescriptor(long measureID)
      {
        this.CheckInit();
        MeasureDescriptor measureDescriptor;
        return this._Measures.TryGetValue(measureID, out measureDescriptor) ? measureDescriptor : new MeasureDescriptor(true);
      }

      /// <summary>Находит описатель единицы измерения по ее короткому имени.
      /// Если описатель не найден, то возвращает MeasureDescriptor.Empty == true
      /// </summary>
      public MeasureDescriptor FindDescriptor(string shortName)
      {
        this.CheckInit();
        foreach (MeasureDescriptor descriptor in this._Measures.Values)
        {
          if (descriptor.ShortName == shortName)
            return descriptor;
        }
        string str = !this.NormalizeString ? shortName : ClientStringNormalizer.GetIndexedString(shortName);
        foreach (MeasureDescriptor descriptor in this._Measures.Values)
        {
          for (int index = 0; index < descriptor.ShortNameIndex.Length; ++index)
          {
            if (descriptor.ShortNameIndex[index] == str)
              return descriptor;
          }
        }
        return new MeasureDescriptor(true);
      }

      /// <summary>Возвращает результат сравнения двух значений, выраженных в единицах измерения.
      /// Если val1 == val2, то возвращает CompareResult.Equal
      /// Если val1 &gt; val2, то возвращает CompareResult.More
      /// Если val1 меньше val2, то возвращает CompareResult.Less
      /// Если единицы измерения несовместимы, то возвращает CompareResult.NotCompatible
      /// </summary>
      public CompareResult Compare(MeasuredValue val1, MeasuredValue val2, int precision)
      {
        this.CheckInit();
        double num1 = val1.Value;
        double num2 = val2.Value;
        if (val1.MeasureID != val2.MeasureID)
        {
          MeasureDescriptor descriptor1 = this.FindDescriptor(val1);
          MeasureDescriptor descriptor2 = this.FindDescriptor(val2);
          if (descriptor1.PhysicalQuantityID != descriptor2.PhysicalQuantityID)
            return CompareResult.NotCompatible;
          num1 *= descriptor1.K;
          num2 *= descriptor2.K;
        }
        double num3 = Math.Pow(10.0, (double) -precision);
        if (Math.Abs(num1 - num2) < num3)
          return CompareResult.Equal;
        return num1 > num2 ? CompareResult.More : CompareResult.Less;
      }

      /// <summary>Конвертирует строковое значение mValue в структуру MeasuredValue</summary>
      public MeasuredValue ConvertToMeasuredValue(string mValue)
      {
        return this.ConvertToMeasuredValue(mValue, "", true);
      }

      /// <summary>Разобрать стоку на число и единицы измерения</summary>
      /// <param name="mValue">Текстовое значение</param>
      /// <param name="value">Возвращает число</param>
      /// <param name="muShortName">Возвращает текст единицы измерения</param>
      /// <param name="exceptionIfFail">Генерировать исключение если конвертировать в Double нельзя</param>
      /// <returns>true, если конвертация числа прошла успешно</returns>
      public bool ParseString(
        string mValue,
        out double value,
        out string muShortName,
        bool exceptionIfFail)
      {
        try
        {
          mValue = mValue.Trim();
          string textBeforeNumber;
          int num = NumberParserAdvanced.ParseNumber(mValue, true, out value, out textBeforeNumber, out muShortName) ? 1 : 0;
          muShortName = muShortName.Trim();
          if (num != 0)
          {
            if (textBeforeNumber != null)
            {
              if (!(textBeforeNumber != ""))
                goto label_9;
            }
            else
              goto label_9;
          }
          if (exceptionIfFail)
          {
            value = Convert.ToDouble(mValue);
            return false;
          }
          muShortName = string.Empty;
          value = 0.0;
          return false;
        }
        catch
        {
          if (exceptionIfFail)
            throw;
          muShortName = string.Empty;
          value = 0.0;
          return false;
        }
    label_9:
        return true;
      }

      /// <summary>Конвертирует строковое значение mValue в структуру MeasuredValue</summary>
      /// <param name="mValue">Строковое значение</param>
      /// <param name="defaultMeasure">Единица измерения по умолчанию</param>
      /// <param name="exceptionIfFail">Генерировать исключение если конвертировать в Double нельзя</param>
      /// <returns>MeasuredValue</returns>
      public MeasuredValue ConvertToMeasuredValue(
        string mValue,
        MeasureDescriptor defaultMeasure,
        bool exceptionIfFail)
      {
        this.CheckInit();
        double aValue;
        string muShortName;
        this.ParseString(mValue, out aValue, out muShortName, exceptionIfFail);
        MeasureDescriptor measureDescriptor = !string.IsNullOrEmpty(muShortName) ? this.FindDescriptor(muShortName) : defaultMeasure;
        if (measureDescriptor == null)
          throw new KernelExceptionID(381, (object) mValue);
        if (measureDescriptor.Empty)
          throw new KernelExceptionID(161, (object) muShortName, (object) mValue);
        return new MeasuredValue(aValue, measureDescriptor.MeasureID, mValue);
      }

      /// <summary>Конвертирует строковое значение mValue в физическую величину и единицы измерения</summary>
      /// <param name="mValue">Строковое значение</param>
      /// <param name="defaultMeasure">Единица измерения по умолчанию</param>
      /// <param name="value">Значение</param>
      /// <param name="measureDescriptor">Единица измерения</param>
      /// <param name="exceptionIfFail">Генерировать исключение если конвертировать в Double нельзя</param>
      /// <returns>true, если конвертация прошла успешно</returns>
      public bool ConvertToMeasuredValue(
        string mValue,
        MeasureDescriptor defaultMeasure,
        out double value,
        out MeasureDescriptor measureDescriptor,
        bool exceptionIfFail)
      {
        this.CheckInit();
        string muShortName;
        int num = this.ParseString(mValue, out value, out muShortName, exceptionIfFail) ? 1 : 0;
        MeasureDescriptor measureDescriptor1 = muShortName == "" || muShortName == null ? defaultMeasure : this.FindDescriptor(muShortName);
        if (measureDescriptor1 == null || measureDescriptor1.Empty)
        {
          if (exceptionIfFail)
            throw new KernelExceptionID(161, (object) muShortName, (object) mValue);
          measureDescriptor = (MeasureDescriptor) null;
          return num != 0;
        }
        measureDescriptor = measureDescriptor1;
        return num != 0;
      }

      public MeasuredValue ConvertToMeasuredValue(string mValue, bool exceptionIfFail)
      {
        this.CheckInit();
        string muShortName = string.Empty;
        double aValue;
        if (!this.ParseString(mValue, out aValue, out muShortName, exceptionIfFail))
          return (MeasuredValue) null;
        MeasureDescriptor descriptor = this.FindDescriptor(muShortName);
        if (!descriptor.Empty)
          return new MeasuredValue(aValue, descriptor.MeasureID, mValue);
        if (exceptionIfFail)
          throw new KernelExceptionID(161, (object) muShortName, (object) mValue);
        return (MeasuredValue) null;
      }

      /// <summary>Конвертирует строковое значение mValue в структуру MeasuredValue</summary>
      /// <param name="mValue">Строковое значение</param>
      /// <param name="defaultMeasure">Единица измерения по умолчанию</param>
      /// <param name="exceptionIfFail">Генерировать исключение если конвертировать в Double нельзя</param>
      /// <returns>MeasuredValue</returns>
      public MeasuredValue ConvertToMeasuredValue(
        string mValue,
        string defaultMeasure,
        bool exceptionIfFail)
      {
        this.CheckInit();
        string muShortName = string.Empty;
        double aValue;
        this.ParseString(mValue, out aValue, out muShortName, exceptionIfFail);
        if (muShortName == "" || muShortName == null)
          muShortName = defaultMeasure;
        MeasureDescriptor descriptor = this.FindDescriptor(muShortName);
        if (descriptor.Empty && muShortName != "")
          throw new KernelExceptionID(161, (object) muShortName, (object) mValue);
        return new MeasuredValue(aValue, descriptor.MeasureID, mValue);
      }

      /// <summary>
      /// Конвертирует значение mValue в единицу измерения toMeasureID. Если физические величины несовместимы, то генерирует исключение.
      /// </summary>
      public MeasuredValue ConvertToMeasuredValue(MeasuredValue mValue, long toMeasureID)
      {
        this.CheckInit();
        if (mValue.MeasureID == toMeasureID)
          return mValue;
        MeasureDescriptor descriptor1 = this.FindDescriptor(mValue.MeasureID);
        MeasureDescriptor descriptor2 = this.FindDescriptor(toMeasureID);
        if (descriptor1.PhysicalQuantityID != descriptor2.PhysicalQuantityID)
          throw new KernelExceptionID(242, (object) mValue.Caption, (object) descriptor2.LongName);
        return new MeasuredValue(mValue.Value * descriptor1.K / descriptor2.K, toMeasureID);
      }

      /// <summary>Конвертирует значение val в строковое представление этого значения в
      /// единицах измерения measureID. Если measureID не найдено - возвращает пустую строку!!!
      /// Если convertFromBase == true, то конвертирует значение val из базовой величины в
      /// величину measureID
      /// </summary>
      public string ConvertToString(double val, long measureID, bool convertFromBase)
      {
        this.CheckInit();
        MeasureDescriptor descriptor = this.FindDescriptor(measureID);
        if (descriptor.Empty)
          return measureID == 0L ? val.ToString("#################0.#################") : string.Empty;
        if (convertFromBase)
          val /= descriptor.K;
        return $"{val.ToString("#################0.#################")} {descriptor.ShortName}";
      }

      public MeasuredValue ConvertToBaseMeasure(MeasuredValue value)
      {
        MeasureDescriptor descriptor = this.FindDescriptor(value.MeasureID);
        if (descriptor.K == 1.0)
          return value;
        MeasureDescriptor baseValue = this.FindBaseValue(descriptor);
        return new MeasuredValue(value.Value * descriptor.K, baseValue.MeasureID);
      }

      internal enum MeasureOperation
      {
        Add,
        Substract,
        Multiply,
        Divide,
      }
    }
}
