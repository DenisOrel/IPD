
// Type: Intermech.Interfaces.Imbase.ImbaseExtendedItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace Intermech.Interfaces.Imbase
{
    /// <summary>Элемент для хранения информации для типа аттрибута</summary>
    [Serializable]
    public class ImbaseExtendedItem : IComparable, IComparable<ImbaseExtendedItem>
    {
      /// <summary>Ид. версии каталога/справочника Imbase</summary>
      /// <remarks>Для загрузки "старых" настроек only</remarks>
      protected long _catalogID;
      /// <summary>Ид. версий каталогов/справочников Imbase</summary>
      protected List<long> _catalogIDs = new List<long>();
      /// <summary>Режим выбора объектов из каталогов Imbase</summary>
      protected ImbaseCatalogSelectMode _selectMode = ImbaseCatalogSelectMode.imcmNone;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="context"></param>
      [System.Runtime.Serialization.OnDeserialized]
      protected void OnDeserialized(StreamingContext context)
      {
        if (this._catalogIDs == null)
          this._catalogIDs = new List<long>();
        if (this._catalogID == 0L)
          return;
        if (this._catalogIDs.Count == 0)
          this._catalogIDs.Add(this._catalogID);
        this._catalogID = 0L;
      }

      /// <summary>Конструктор</summary>
      public ImbaseExtendedItem()
        : this(0L)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="catalogID">Ид. версии каталога/справочника Imbase</param>
      public ImbaseExtendedItem(long catalogID)
        : this(catalogID, ImbaseCatalogSelectMode.imcmNone)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="catalogID">Ид. версии каталога/справочника Imbase</param>
      /// <param name="selectMode">Режим выбора объектов из каталогов Imbase</param>
      public ImbaseExtendedItem(long catalogID, ImbaseCatalogSelectMode selectMode)
      {
        this._selectMode = selectMode;
        this._catalogIDs.Add(catalogID);
      }

      /// <summary>Конструктор</summary>
      /// <param name="catalogIDs">Ид. версий каталогов/справочников Imbase</param>
      /// <param name="selectMode">Режим выбора объектов из каталогов Imbase</param>
      public ImbaseExtendedItem(IEnumerable<long> catalogIDs, ImbaseCatalogSelectMode selectMode)
      {
        this._selectMode = selectMode;
        if (catalogIDs == null)
          return;
        this._catalogIDs.AddRange(catalogIDs);
      }

      protected ImbaseExtendedItem(SerializationInfo info, StreamingContext context)
      {
      }

      /// <summary>Ид. версий каталогов/справочников Imbase</summary>
      public List<long> CatalogIDs
      {
        [DebuggerStepThrough] get => this._catalogIDs;
        [DebuggerStepThrough] set => this._catalogIDs = value;
      }

      /// <summary>Режим выбора объектов из каталогов Imbase</summary>
      public ImbaseCatalogSelectMode SelectMode
      {
        [DebuggerStepThrough] get => this._selectMode;
        [DebuggerStepThrough] set => this._selectMode = value;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public int CompareTo(object obj) => this.CompareTo(obj as ImbaseExtendedItem);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public int CompareTo(ImbaseExtendedItem other)
      {
        if (other == null)
          return 1;
        int num = GenericListHelper.Compare<long>((IList<long>) this.CatalogIDs, (IList<long>) other.CatalogIDs);
        return num != 0 ? num : this.SelectMode.CompareTo((object) (int) other.SelectMode);
      }
    }
}
