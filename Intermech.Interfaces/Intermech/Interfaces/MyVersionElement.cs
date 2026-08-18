
// Type: Intermech.Interfaces.MyVersionElement
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для хранения текущего статуса фильтрации версии объекта
    /// </summary>
    [Serializable]
    public sealed class MyVersionElement : ICloneable
    {
      /// <summary>F_OBJECT_ID версии</summary>
      public long ID;
      /// <summary>F_PRJLINK_ID - идентификатор связи</summary>
      public long PrjLinkID;
      /// <summary>F_RELATION_TYPE - идентификатор типа связи</summary>
      public int RelTypeID;
      /// <summary>
      /// "Вес" данной версии объекта (оценка, сколько критерией пришлось перебрать, чтобы данная версия подошла)
      /// </summary>
      public int Weigth;
      /// <summary>Текущее состояние фильтрации</summary>
      public ObjectFiltrationState State = ObjectFiltrationState.fsVariance;
      /// <summary>Текущие результаты проверки объекта</summary>
      public bool BoolState;
      /// <summary>
      /// Текущая булева операция (нужна на следующем шаге фильтрации)
      /// </summary>
      public string BoolOp = "NOP";
      /// <summary>Является ли версия базовой</summary>
      public bool IsBase;
      /// <summary>Какие-то пользовательские данные</summary>
      public object Tag;

      /// <summary>Создать пустой экземпляр класса</summary>
      public MyVersionElement()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="AnID">F_OBJECT_ID версии</param>
      /// <param name="AWeigth">"Вес" данной версии объекта (к скольки основным критериям подбора версия подошла). 0 - начало фильтрации.</param>
      /// <param name="AState">Текущее состояние фильтрации</param>
      /// <param name="ABoolState">Текущие результаты проверки объекта</param>
      /// <param name="ABoolOp">Логическая операция (нужна на следующем шаге фильтрации)</param>
      /// <param name="AnIsBase">Является ли версия базовой</param>
      /// <param name="ATag">Пользовательские данные</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      public MyVersionElement(
        long AnID,
        int AWeigth,
        ObjectFiltrationState AState,
        bool ABoolState,
        string ABoolOp,
        bool AnIsBase,
        object ATag,
        long prjLinkID,
        int relTypeID)
      {
        this.ID = AnID;
        this.Weigth = AWeigth;
        this.State = AState;
        this.BoolState = ABoolState;
        this.BoolOp = ABoolOp;
        this.IsBase = AnIsBase;
        this.Tag = ATag;
        this.PrjLinkID = prjLinkID;
        this.RelTypeID = relTypeID;
      }

      /// <summary>Перекрытый метод для возвращения заголовка</summary>
      /// <returns></returns>
      public override string ToString()
      {
        return $"{Convert.ToString(this.ID)}.{Convert.ToString(this.Weigth)}";
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public object Clone()
      {
        return (object) new MyVersionElement(this.ID, this.Weigth, this.State, this.BoolState, this.BoolOp, this.IsBase, this.Tag, this.PrjLinkID, this.RelTypeID);
      }
    }
}
