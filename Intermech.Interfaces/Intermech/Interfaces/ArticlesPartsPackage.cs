
// Type: Intermech.Interfaces.ArticlesPartsPackage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, в котором хранятся таблицы с общими и переменными частями исполнений
    /// </summary>
    [Serializable]
    public class ArticlesPartsPackage : ICloneable, IAssignable
    {
      /// <summary>
      /// Общие части исполнений.
      /// [(Int64)Идентификатор версии исполнения] =&gt; [List(Int64)Список связей общей части]
      /// </summary>
      private Dictionary<long, List<long>> _commonParts = new Dictionary<long, List<long>>();
      /// <summary>
      /// Переменные части исполнений.
      /// [(Int64)Идентификатор версии исполнения] =&gt; [List(Int64)Список связей переменной части]
      /// </summary>
      private Dictionary<long, List<long>> _variableParts = new Dictionary<long, List<long>>();
      /// <summary>Связи и их виды</summary>
      private Dictionary<long, ArticleRelationState> _relationsStates = new Dictionary<long, ArticleRelationState>();

      /// <summary>Создать пустой экземпляр класса</summary>
      public ArticlesPartsPackage()
      {
      }

      /// <summary>
      /// Создать экземпляр класса на основании указанных данных
      /// </summary>
      /// <param name="source">Исходные данные (ArticlesPartsPackage)</param>
      public ArticlesPartsPackage(object source)
        : this()
      {
        this.Assign(source);
      }

      /// <summary>
      /// Общие части исполнений.
      /// [(Int64)Идентификатор версии исполнения] =&gt; [List(Int64)Список связей общей части]
      /// </summary>
      public Dictionary<long, List<long>> CommonParts
      {
        [DebuggerStepThrough] get => this._commonParts;
      }

      /// <summary>
      /// Переменные части исполнений.
      /// [(Int64)Идентификатор версии исполнения] =&gt; [List(Int64)Список связей переменной части]
      /// </summary>
      public Dictionary<long, List<long>> VariableParts
      {
        [DebuggerStepThrough] get => this._variableParts;
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new ArticlesPartsPackage((object) this);

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this._relationsStates.Clear();
        this._commonParts.Clear();
        this._variableParts.Clear();
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        if (!(source is ArticlesPartsPackage articlesPartsPackage))
          return;
        this._relationsStates = new Dictionary<long, ArticleRelationState>((IDictionary<long, ArticleRelationState>) articlesPartsPackage._relationsStates);
        this._commonParts = new Dictionary<long, List<long>>((IDictionary<long, List<long>>) articlesPartsPackage._commonParts);
        this._variableParts = new Dictionary<long, List<long>>((IDictionary<long, List<long>>) articlesPartsPackage._variableParts);
      }

      /// <summary>Получить список исполнений</summary>
      /// <returns>Список исполнений</returns>
      public List<long> GetArticlesList()
      {
        List<long> articlesList = new List<long>();
        articlesList.AddRange((IEnumerable<long>) this._commonParts.Keys);
        return articlesList;
      }

      /// <summary>Добавить исполнение в список</summary>
      /// <param name="articleID">Идентификатор версии исполнения</param>
      /// <param name="commonPart">Связи общей части</param>
      /// <param name="variablePart">Связи переменной части</param>
      public void AddArticle(long articleID, List<long> commonPart, List<long> variablePart)
      {
        if (this._commonParts.Count > 0)
        {
          List<long> longList = (List<long>) null;
          using (Dictionary<long, List<long>>.Enumerator enumerator = this._commonParts.GetEnumerator())
          {
            if (enumerator.MoveNext())
              longList = enumerator.Current.Value;
          }
          if (longList.Count != commonPart.Count)
            throw new ApplicationException(LocalizationHolder.rm.GetString("Interfaces_702"));
        }
        if (this._commonParts.ContainsValue(commonPart))
          throw new ApplicationException(LocalizationHolder.rm.GetString("Interfaces_703"));
        if (this._variableParts.ContainsValue(variablePart))
          throw new ApplicationException(LocalizationHolder.rm.GetString("Interfaces_704"));
        this._commonParts[articleID] = commonPart;
        this._variableParts[articleID] = variablePart;
      }

      /// <summary>Удалить информацию об исполнении из словариков</summary>
      /// <param name="articleID">Идентификатор версии исполнения</param>
      public void Remove(long articleID)
      {
        if (this._commonParts.ContainsKey(articleID))
          this._commonParts.Remove(articleID);
        if (this._variableParts.ContainsKey(articleID))
          this._variableParts.Remove(articleID);
        this._relationsStates.Clear();
      }

      /// <summary>
      /// Получить список связей из общей части указанного исполнения
      /// </summary>
      /// <param name="articleID">Идентификатор исполнения</param>
      /// <returns>Список связей из общей части указанного исполнения или null</returns>
      public List<long> GetArticleCommonPart(long articleID)
      {
        return this._commonParts.ContainsKey(articleID) ? this._commonParts[articleID] : (List<long>) null;
      }

      /// <summary>
      /// Получить список связей из переменной части указанного исполнения
      /// </summary>
      /// <param name="articleID">Идентификатор исполнения</param>
      /// <returns>Список связей из переменной части указанного исполнения или null</returns>
      public List<long> GetArticleVariablePart(long articleID)
      {
        return this._variableParts.ContainsKey(articleID) ? this._variableParts[articleID] : (List<long>) null;
      }

      /// <summary>Определить вид указанной связи в исполнении</summary>
      /// <param name="articleID">Идентификатор исполнения</param>
      /// <param name="prjLinkId">Идентификатор связи</param>
      /// <returns>Вид указанной связи</returns>
      public ArticleRelationState GetRelationState(long articleID, long prjLinkId)
      {
        if (this._relationsStates.ContainsKey(prjLinkId))
          return this._relationsStates[prjLinkId];
        if (!this._commonParts.ContainsKey(articleID))
          return ArticleRelationState.Unknown;
        if (this._commonParts[articleID].IndexOf(prjLinkId) >= 0)
        {
          this._relationsStates[prjLinkId] = ArticleRelationState.CommonPart;
          return ArticleRelationState.CommonPart;
        }
        if (this._variableParts[articleID].IndexOf(prjLinkId) < 0)
          return ArticleRelationState.Unknown;
        this._relationsStates[prjLinkId] = ArticleRelationState.VariablePart;
        return ArticleRelationState.VariablePart;
      }

      public void MergeWith(ArticlesPartsPackage app)
      {
        foreach (long key in app._commonParts.Keys)
        {
          List<long> longList;
          if (this._commonParts.ContainsKey(key))
          {
            longList = this._commonParts[key];
          }
          else
          {
            longList = new List<long>();
            this._commonParts.Add(key, longList);
          }
          foreach (long num in app._commonParts[key])
          {
            if (!longList.Contains(num))
              longList.Add(num);
          }
        }
        foreach (long key in app._variableParts.Keys)
        {
          List<long> longList;
          if (this._variableParts.ContainsKey(key))
          {
            longList = this._variableParts[key];
          }
          else
          {
            longList = new List<long>();
            this._variableParts.Add(key, longList);
          }
          foreach (long num in app._variableParts[key])
          {
            if (!longList.Contains(num))
              longList.Add(num);
          }
        }
        foreach (long key in app._relationsStates.Keys)
        {
          if (!this._relationsStates.ContainsKey(key))
            this._relationsStates.Add(key, app._relationsStates[key]);
        }
      }
    }
}
