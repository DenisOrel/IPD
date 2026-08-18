
// Type: Intermech.ControlFlow.DynamicScope
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.ControlFlow
{
    /// <summary>
    /// <para>
    /// Реализует динамическую область видимости переменных по аналогии с тем, как они устроены в LISP. В первом приближении напоминает расширенный механизм глобальных
    /// переменных. Отличается от лексической области видимости тем, что существует только в момент выполнения приложения, а границы и время существования динамической
    /// области видимости задает программист, а не компилятор.
    /// </para>
    /// <para>
    /// Динамическая область видимости доступна только тому потоку, в котором она была создана. А каждый поток обладает своей цепочкой динамических областей видимости.
    /// Поэтому класс <see cref="T:Intermech.ControlFlow.DynamicScope" />  является thread-safe.
    /// </para>
    /// </summary>
    public sealed class DynamicScope : IDisposable
    {
      private int scopeDepth;
      private bool active;
      [ThreadStatic]
      private static ThreadData threadData;

      /// <summary>
      /// Создает новую область видимости переменных и делает ее текущей.
      /// </summary>
      public DynamicScope()
      {
        if (DynamicScope.threadData == null)
          DynamicScope.threadData = new ThreadData();
        this.scopeDepth = ++DynamicScope.threadData.CurrentDepth;
        this.active = true;
      }

      /// <summary>
      /// Закрывает текущую область видимости переменных. Все привязки значений переменных, сделанные в этой области видимости будут утеряны, а ресурсы,
      /// связанные со значениями переменных, будут освобождены с помощью <see cref="T:System.IDisposabke" />.
      /// </summary>
      public void Dispose()
      {
        if (!this.active)
          return;
        this.active = false;
        List<DynamicScopeSymbol> currentScopeNames = DynamicScope.threadData.GetCurrentScopeNames(false);
        if (currentScopeNames != null && currentScopeNames.Count != 0)
        {
          foreach (DynamicScopeSymbol varName in currentScopeNames)
          {
            LinkedList<VariableBox> valueList = DynamicScope.threadData.GetValueList(varName, false);
            DisposeUtils.SafelyDispose(valueList.First.Value.VariableValue as IDisposable);
            valueList.RemoveFirst();
          }
          DynamicScope.threadData.RemoveCurrentScopeNames();
        }
        --DynamicScope.threadData.CurrentDepth;
      }

      /// <summary>
      /// Возвращает true, если динамическая область видимости задана.
      /// </summary>
      public static bool ScopePresent
      {
        get => DynamicScope.threadData != null && DynamicScope.threadData.CurrentDepth != 0;
      }

      /// <summary>
      /// Проверяет наличие динамической области видимости, и, при ее отсутствии, сбрасывает исключение.
      /// </summary>
      /// <exception cref="T:Intermech.ControlFlow.DynamicScopeException">Ни одна динамическая область видимости не задана</exception>
      public static void ScopeRequired()
      {
        if (!DynamicScope.ScopePresent)
          throw new DynamicScopeException("No dynamic scope found.");
      }

      internal static bool IsDeclared(DynamicScopeSymbol varName)
      {
        if (varName == null)
          throw new ArgumentNullException(nameof (varName));
        return DynamicScope.ScopePresent && DynamicScope.GetDeclarationDepth(varName) != 0;
      }

      internal static bool IsDeclaredInCurrentScope(DynamicScopeSymbol varName)
      {
        if (varName == null)
          throw new ArgumentNullException(nameof (varName));
        return DynamicScope.ScopePresent && DynamicScope.GetDeclarationDepth(varName) == DynamicScope.threadData.CurrentDepth;
      }

      internal static int GetDeclarationDepth(DynamicScopeSymbol varName)
      {
        if (varName == null)
          throw new ArgumentNullException(nameof (varName));
        if (DynamicScope.ScopePresent)
        {
          LinkedList<VariableBox> valueList = DynamicScope.threadData.GetValueList(varName, false);
          if (valueList != null && valueList.Count != 0)
            return valueList.First.Value.ScopeDepth;
        }
        return 0;
      }

      internal static void Declare(DynamicScopeSymbol varName, object varValue)
      {
        if (varName == null)
          throw new ArgumentNullException(nameof (varName));
        DynamicScope.ScopeRequired();
        LinkedList<VariableBox> valueList = DynamicScope.threadData.GetValueList(varName, true);
        if (valueList.Count != 0 && valueList.First.Value.ScopeDepth == DynamicScope.threadData.CurrentDepth)
          throw new DynamicScopeException($"The dynamic variable '{varName}' is already declared in the current dynamic scope.");
        DynamicScope.threadData.GetCurrentScopeNames(true).Add(varName);
        valueList.AddFirst(new VariableBox(DynamicScope.threadData.CurrentDepth, varValue));
      }

      internal static void RemoveDeclaration(DynamicScopeSymbol varName, bool disposeValue)
      {
        if (varName == null)
          throw new ArgumentNullException(nameof (varName));
        if (!DynamicScope.ScopePresent)
          return;
        LinkedList<VariableBox> valueList = DynamicScope.threadData.GetValueList(varName, false);
        if (valueList == null || valueList.Count == 0)
          return;
            VariableBox variableBox = valueList.First.Value;
        if (variableBox.ScopeDepth != DynamicScope.threadData.CurrentDepth)
          return;
        if (disposeValue)
          DisposeUtils.SafelyDispose(variableBox.VariableValue as IDisposable);
        DynamicScope.threadData.GetCurrentScopeNames(false).Remove(varName);
        valueList.RemoveFirst();
      }

      internal static bool TryRead(DynamicScopeSymbol varName, out object varValue)
      {
        if (varName == null)
          throw new ArgumentNullException(nameof (varName));
        if (DynamicScope.ScopePresent)
        {
          LinkedList<VariableBox> valueList = DynamicScope.threadData.GetValueList(varName, false);
          if (valueList != null && valueList.Count != 0)
          {
            varValue = valueList.First.Value.VariableValue;
            return true;
          }
        }
        varValue = (object) null;
        return false;
      }

      internal static void Write(DynamicScopeSymbol varName, object varValue)
      {
        if (varName == null)
          throw new ArgumentNullException(nameof (varName));
        DynamicScope.ScopeRequired();
        LinkedList<VariableBox> valueList = DynamicScope.threadData.GetValueList(varName, true);
        if (valueList.Count == 0)
        {
          DynamicScope.threadData.GetCurrentScopeNames(true).Add(varName);
          valueList.AddFirst(new VariableBox(DynamicScope.threadData.CurrentDepth, varValue));
        }
        else
          valueList.First.Value.VariableValue = varValue;
      }

      private sealed class ThreadData
      {
        private LinkedList<ScopeNames> scopeDeclarations;
        private Dictionary<DynamicScopeSymbol, LinkedList<VariableBox>> values;
        private int currentDepth;

        public ThreadData()
        {
          this.scopeDeclarations = new LinkedList<ScopeNames>();
          this.values = new Dictionary<DynamicScopeSymbol, LinkedList<VariableBox>>();
        }

        public List<DynamicScopeSymbol> GetCurrentScopeNames(bool allowCreate)
        {
          if (this.scopeDeclarations.Count != 0)
          {
                    ScopeNames scopeNames = this.scopeDeclarations.First.Value;
            if (scopeNames.ScopeDepth == this.currentDepth)
              return scopeNames.NameList;
          }
          if (!allowCreate)
            return (List<DynamicScopeSymbol>) null;
          List<DynamicScopeSymbol> nameList = new List<DynamicScopeSymbol>();
          this.scopeDeclarations.AddFirst(new ScopeNames(this.currentDepth, nameList));
          return nameList;
        }

        public void RemoveCurrentScopeNames()
        {
          if (this.scopeDeclarations.Count == 0 || this.scopeDeclarations.First.Value.ScopeDepth != this.currentDepth)
            return;
          this.scopeDeclarations.RemoveFirst();
        }

        public LinkedList<VariableBox> GetValueList(
          DynamicScopeSymbol varName,
          bool allowCreate)
        {
          LinkedList<VariableBox> valueList;
          if (!this.values.TryGetValue(varName, out valueList) & allowCreate)
          {
            valueList = new LinkedList<VariableBox>();
            this.values.Add(varName, valueList);
          }
          return valueList;
        }

        public int CurrentDepth
        {
          get => this.currentDepth;
          set => this.currentDepth = value;
        }
      }

      private sealed class ScopeNames
      {
        public readonly int ScopeDepth;
        public readonly List<DynamicScopeSymbol> NameList;

        public ScopeNames(int scopeDepth, List<DynamicScopeSymbol> nameList)
        {
          this.ScopeDepth = scopeDepth;
          this.NameList = nameList;
        }
      }

      private sealed class VariableBox
      {
        public readonly int ScopeDepth;
        public object VariableValue;

        public VariableBox(int scopeDepth, object varValue)
        {
          this.ScopeDepth = scopeDepth;
          this.VariableValue = varValue;
        }
      }
    }
}
