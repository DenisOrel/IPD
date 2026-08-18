// Decompiled with JetBrains decompiler
// Type: Intermech.Map.IMapRelative
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System.Drawing;


namespace Intermech.Map
{
    /// <summary>положения объектов относительно элемента в документе</summary>
    public interface IMapRelative
    {
      /// <summary>получить по точке в документе найти ID элемента состовляющего документ</summary>
      /// <param name="point">по точке в документе </param>
      /// <returns>ID элемента в документе на который указывает точка</returns>
      string GetId(PointF point);

      /// <summary>получить ID текущей страницы в документе</summary>
      /// <returns>ID текущей страницы в документе</returns>
      string GetCurrentPageId();

      /// <summary>получить базовую точку элемента </summary>
      /// <param name="id">ID элемента в документе</param>
      /// <returns>базовая точка</returns>
      PointF GetBasePoint(string id);

      /// <summary> видим ли графику к указанному элементу </summary>
      /// <param name="id">ID элемента в документе</param>
      /// <returns>true, если элемент видим</returns>
      bool GetVisible(string id);

      /// <summary> проверить сущетвование элемента в документе</summary>
      /// <param name="id">ID элемента в документе</param>
      /// <returns>true, если элемент существует</returns>
      bool CheckElementId(string id);

      /// <summary>
      /// Получение страницы в документе для указанного элемента
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      object GetPage(string id);

      /// <summary>
      /// Получение ID страницы в документе для указанного элемента
      /// </summary>
      /// <param name="id">ID элемента в документе</param>
      /// <returns></returns>
      object GetPageId(string id);
    }
}
