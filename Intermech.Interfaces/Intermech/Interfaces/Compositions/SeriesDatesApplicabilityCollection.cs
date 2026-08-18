
// Type: Intermech.Interfaces.Compositions.SeriesDatesApplicabilityCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Класс, хранящий коллекцию применяемостей объекта в указанных головных объектах по сериям/датам.
    /// Содержимое класса соответствует содержимому строкового многозначного атрибута
    /// "Применяемость в сериях и датах"
    /// </summary>
    [DebuggerDisplay("{DisplayString}")]
    [Serializable]
    public sealed class SeriesDatesApplicabilityCollection : 
      IAssignable,
      ICloneable,
      IStoreable,
      IVersionApplicabilities
    {
      /// <summary>
      /// Коллекция применяемостей объекта в головных изделиях по сериям/датам
      /// </summary>
      public List<SeriesDatesApplicability> Items = new List<SeriesDatesApplicability>();

      /// <summary>Создать пустой экземпляр класса</summary>
      public SeriesDatesApplicabilityCollection()
      {
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public SeriesDatesApplicabilityCollection(object source) => this.Assign(source);

      /// <summary>Отображаемая на экране строка</summary>
      public string DisplayString
      {
        get
        {
          StringBuilder sb = new StringBuilder();
          if (this.Items != null)
            this.Items.ForEach((Action<SeriesDatesApplicability>) (item => sb.Append(item.DisplayString)));
          return sb.ToString();
        }
      }

      /// <summary>
      /// Преобразовать коллекцию применяемостей в матрицу применяемостей, дополнить ранее созданную матрицу
      /// </summary>
      /// <param name="source">Исходная матрица или null, если нужно создать новую матрицу</param>
      /// <param name="mainID">Идентификатор объекта (F_ID)</param>
      /// <param name="mainObjectID">Идентификатор версии объекта (F_OBJECT_ID), с которым связана матрица</param>
      /// <returns>Матрица применяемостей</returns>
      public SeriesDatesMatrix AlterMatrix(SeriesDatesMatrix source, long mainID, long mainObjectID)
      {
        source = source ?? new SeriesDatesMatrix(mainID, mainObjectID);
        for (int index = 0; index < this.Items.Count; ++index)
        {
          SeriesDatesApplicability source1 = this.Items[index];
          if (source1 != null && !source1.IsEmpty && source1.Set != null && !source1.Set.IsEmpty)
            source.FindApplicability(source1.MainObjectID, mainObjectID, source1.Applicability, true).Assign((object) source1);
        }
        return source;
      }

      /// <summary>Очистить поля класса</summary>
      public void Clear() => this.Items = new List<SeriesDatesApplicability>();

      /// <summary>Очистить поля класса</summary>
      public void Assign(object source)
      {
        if (this == source)
          return;
        switch (source)
        {
          case string _:
            this.FromString((string) source);
            break;
          case SeriesDatesApplicabilityCollection applicabilityCollection:
            if (!(CloneHelper.Clone((object) applicabilityCollection.Items) is List<SeriesDatesApplicability> datesApplicabilityList))
              datesApplicabilityList = new List<SeriesDatesApplicability>();
            this.Items = datesApplicabilityList;
            break;
          case IDBAttributable dbAttributable:
            this.Assign((object) dbAttributable.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cadd940c-306c-11d8-b4e9-00304f19f545")));
            break;
          case IDBAttribute dbAttribute:
            StringBuilder stringBuilder = new StringBuilder();
            if (dbAttribute.ValuesCount == 1)
            {
              stringBuilder.Append(DataSetProcessor.GetStringValue(dbAttribute.Value, string.Empty));
            }
            else
            {
              object[] values = dbAttribute.Values;
              if (values != null)
              {
                for (int index = 0; index < values.Length; ++index)
                  stringBuilder.Append(DataSetProcessor.GetStringValue(values[index], string.Empty));
              }
            }
            this.FromString(stringBuilder.ToString());
            break;
        }
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone()
      {
        return (object) new SeriesDatesApplicabilityCollection((object) this.ToString());
      }

      /// <summary>Загрузить информацию из объекта базы данных</summary>
      /// <param name="obj">Объект-источник</param>
      /// <returns>true - информация загружена успешно, false - были ошибки</returns>
      public bool LoadFromObject(IDBAttributable obj)
      {
        this.Assign((object) obj);
        this.Items.Sort();
        return true;
      }

      /// <summary>Возвращает список значений для записи в атрибут</summary>
      /// <returns></returns>
      public List<string> ToStringValues()
      {
        this.Items.Sort();
        return StringsHelper.SplitString(this.ToLongString().ToString(), Intermech.Consts.MaxStringSize);
      }

      /// <summary>Записать информацию в указанный объект базы данных</summary>
      /// <param name="obj">Объект-назначение</param>
      /// <returns>true - вся информация записана успешно, false - были ошибки</returns>
      public bool SaveToObject(IDBAttributable obj)
      {
        bool flag = false;
        if (obj == null || !(obj is IDBObject dbObject))
          return flag;
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, MetaDataHelper.GetAttributeTypeID("cadd940c-306c-11d8-b4e9-00304f19f545"));
        if (attribute4ObjectType == null)
          return false;
        List<string> stringValues = this.ToStringValues();
        IDBAttribute dbAttribute = obj.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cadd940c-306c-11d8-b4e9-00304f19f545"));
        IUserSession session = obj.Session;
        if (dbAttribute != null)
        {
          if (this.Items == null || this.Items.Count == 0 || stringValues.Count == 0)
          {
            if (attribute4ObjectType.Required == RequiredModes.Manual)
              dbAttribute.Delete(0L);
            return true;
          }
        }
        else
        {
          if (this.Items == null || this.Items.Count == 0 || stringValues.Count == 0)
            return flag;
          if (attribute4ObjectType.Required == RequiredModes.Manual)
            dbAttribute = obj.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cadd940c-306c-11d8-b4e9-00304f19f545"), false);
        }
        if (dbAttribute == null)
          return flag;
        dbAttribute.Values = (object[]) stringValues.ToArray();
        return true;
      }

      /// <summary>
      /// Выполнить проверку применяемости указанной версии по дате и(или) номеру серии
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="objectID">Идентификатор проверяемой версии объекта</param>
      /// <param name="masterArticle">Идентификатор версии головного изделия (Intermech.Consts.UnknownObjectId, если не требуется головное изделие)</param>
      /// <param name="date">Дата для проверки. Если проверка на дату не требуется, следует указать значение DateTime.MinValue</param>
      /// <param name="series">Номер серии для проверки. Если проверка на серию не требуется, следует указать значение Int32.MinValue</param>
      /// <returns>Статус указанной версии</returns>
      public ObjectFiltrationState CheckApplicabilities(
        IUserSession session,
        long objectID,
        long masterArticle,
        DateTime date,
        int series)
      {
        SeriesDatesApplicability datesApplicability1 = (SeriesDatesApplicability) null;
        int num = SeriesDatesHelper.GetWeight(ObjectFiltrationState.fsVariance);
        ObjectFiltrationState objectFiltrationState = ObjectFiltrationState.fsVariance;
        if (this.Items != null)
        {
          for (int index = 0; index < this.Items.Count; ++index)
          {
            SeriesDatesApplicability datesApplicability2 = this.Items[index];
            ObjectFiltrationState state = datesApplicability2.CheckApplicabilities(session, objectID, masterArticle, date, series);
            int weight = SeriesDatesHelper.GetWeight(state);
            if (datesApplicability1 == null)
            {
              datesApplicability1 = datesApplicability2;
              num = SeriesDatesHelper.GetWeight(state);
              objectFiltrationState = state;
            }
            else if (weight < num)
            {
              num = weight;
              datesApplicability1 = datesApplicability2;
              objectFiltrationState = state;
            }
            if (num == 0)
              break;
          }
        }
        return objectFiltrationState;
      }

      /// <summary>
      /// Заполнить экземпляр класса информацией из кодированной строки
      /// </summary>
      /// <param name="val">Кодированная строка</param>
      public void FromString(string val)
      {
        this.Clear();
        if (string.IsNullOrEmpty(val))
          return;
        string[] strArray = val.Split(Intermech.Interfaces.Sets.Consts.SplitterParts, StringSplitOptions.RemoveEmptyEntries);
        if (strArray == null || strArray.Length < 2)
          return;
        List<SeriesDatesApplicability> datesApplicabilityList = new List<SeriesDatesApplicability>(strArray.Length);
        for (int index = 1; index < strArray.Length; ++index)
        {
          SeriesDatesApplicability datesApplicability = new SeriesDatesApplicability((object) strArray[index]);
          if (datesApplicability.Set != null)
            datesApplicabilityList.Add(datesApplicability);
        }
        this.Items = datesApplicabilityList;
      }

      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        return obj is SeriesDatesApplicabilityCollection applicabilityCollection && applicabilityCollection.ToString() == this.ToString();
      }

      /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
      /// <returns>Значение экземпляра класса в виде строки</returns>
      public override string ToString()
      {
        if (this.Items == null || this.Items.Count == 0)
          return string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        int num = 0;
        for (int index = 0; index < this.Items.Count; ++index)
        {
          if (this.Items[index] != null)
          {
            string str = this.Items[index].ToString();
            if (!string.IsNullOrEmpty(str))
            {
              if (num > 0)
                stringBuilder.Append('#');
              stringBuilder.Append(str);
              ++num;
            }
          }
        }
        if (num == 0)
          return string.Empty;
        stringBuilder.Insert(0, '#');
        stringBuilder.Insert(0, StringsHelper.IntToHex(num));
        return stringBuilder.ToString();
      }

      /// <summary>
      /// Вернуть значение экземпляра класса в виде строки, с указанием признака длины строки
      /// </summary>
      /// <returns>Значение экземпляра класса в виде строки</returns>
      public string ToLongString()
      {
        string str = this.ToString();
        if (string.IsNullOrEmpty(str))
          return string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(str);
        if (stringBuilder.Length == 0)
          return string.Empty;
        stringBuilder.Insert(0, '#');
        int num = stringBuilder.Length + 1;
        stringBuilder.Insert(0, num <= Intermech.Consts.MaxStringSize ? "0" : "1");
        return stringBuilder.ToString();
      }
    }
}
