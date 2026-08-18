
// Type: Intermech.Diagnostics.StackTraceTransform
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Text;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Реализует эффективный механизм для преобразования строк stack trace. Поддерживается возможность смешивания нескольких преобразований.
    /// Преобразования stack trace используются для добавления дополнительной технической инфомации, деобфускации и т.д.
    /// </summary>
    public class StackTraceTransform
    {
      private List<Tuple<string, StackLineBuilder>> lineData;
      private int sourceLength;
      private bool lineEndRequired;
      private string resultCache;

      /// <summary>Начинает новое преобразование.</summary>
      /// <param name="stackTrace">Исходный stack trace</param>
      /// <exception cref="T:System.ArgumentNullException">Аргумент метода не указан</exception>
      public void StartTransform(string stackTrace)
      {
        if (stackTrace == null)
          throw new ArgumentNullException(nameof (stackTrace));
        this.Clear();
        if (string.IsNullOrEmpty(stackTrace))
        {
          this.lineData = new List<Tuple<string, StackLineBuilder>>(0);
        }
        else
        {
          string[] strArray = stackTrace.Split(TextServices.TextLinesSplitPatterns, StringSplitOptions.None);
          this.lineData = new List<Tuple<string, StackLineBuilder>>(strArray.Length);
          this.sourceLength = stackTrace.Length;
          foreach (string textLine in strArray)
          {
            StackLineBuilder stackLineBuilder = StackLineBuilder.TryParse(textLine);
            this.lineData.Add(Tuple.Create(textLine, stackLineBuilder));
          }
          if (!string.IsNullOrEmpty(this.lineData[this.lineData.Count - 1].Item1))
            return;
          this.lineData.RemoveAt(this.lineData.Count - 1);
          this.lineEndRequired = true;
        }
      }

      /// <summary>Возвращает true, если преобразование было начато.</summary>
      public bool IsStarted => this.lineData != null;

      /// <summary>
      /// Применяет указанное преобразование к каждой строке stack trace.
      /// </summary>
      /// <param name="lineTransform">Объект преобразования</param>
      /// <exception cref="T:System.ArgumentNullException">Объект преобразования не указан</exception>
      public void ApplyTransform(StackLineTransform lineTransform)
      {
        if (lineTransform == null)
          throw new ArgumentNullException(nameof (lineTransform));
        if (!this.IsStarted)
          return;
        this.ResetCachedValues();
        foreach (Tuple<string, StackLineBuilder> tuple in this.lineData)
        {
          if (tuple.Item2 != null)
            lineTransform.ApplyTransform(tuple.Item1, tuple.Item2);
        }
      }

      /// <summary>Возвращает результат преобразования stack trace.</summary>
      /// <returns>Результат преобразования в виде строки</returns>
      public string GetResult()
      {
        if (!this.IsStarted || this.lineData.Count == 0)
          return string.Empty;
        if (this.resultCache == null)
          this.resultCache = this.MakeResult();
        return this.resultCache;
      }

      private string MakeResult()
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(this.sourceLength * 2))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          foreach (Tuple<string, StackLineBuilder> tuple in this.lineData)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.AppendLine();
            if (tuple.Item2 != null)
              stringBuilder.Append(tuple.Item2.ToString());
            else
              stringBuilder.Append(tuple.Item1);
          }
          if (this.lineEndRequired)
            stringBuilder.AppendLine();
          return stringBuilder.ToString();
        }
      }

      private void ResetCachedValues() => this.resultCache = (string) null;

      /// <summary>
      /// Выполняет очистку, удаляя все рабочие данные выполненного преобразования.
      /// </summary>
      public void Clear()
      {
        this.ResetCachedValues();
        this.lineData = (List<Tuple<string, StackLineBuilder>>) null;
        this.sourceLength = 0;
        this.lineEndRequired = false;
      }
    }
}
