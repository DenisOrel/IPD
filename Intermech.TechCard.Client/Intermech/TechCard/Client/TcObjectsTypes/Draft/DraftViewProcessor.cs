// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Draft.DraftViewProcessor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost;
using Intermech.Client.Core.Show.Net.ShowNew;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Show;
using Intermech.Interfaces.TechCard;
using Intermech.Map;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Draft;

/// <summary>
/// 
/// </summary>
internal class DraftViewProcessor
{
  /// <summary>
  /// 
  /// </summary>
  private static readonly string[] ExcludeLayers = new string[1]
  {
    "{0}_BLANK"
  };
  /// <summary>
  /// 
  /// </summary>
  private readonly MapObject _mapObject;
  /// <summary>
  /// 
  /// </summary>
  private readonly ISelectedItems _selectedItems;
  /// <summary>Ид. слоев для просмотра</summary>
  private List<string> _sketchIdList;

  /// <summary>Загрузка информации по текущим слоям</summary>
  private void LoadSketchInfo()
  {
    long aRelationID = this._selectedItems.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData ? itemData.Value : 0L;
    if (aRelationID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeByGuid = sessionKeeper.Session.GetRelation(aRelationID, false)?.GetAttributeByGuid(TechCardConsts.AttributeTypes.SketchNameGuid, false);
      if (attributeByGuid == null)
        return;
      this._sketchIdList = new List<string>(attributeByGuid.Values.Length);
      foreach (object obj in attributeByGuid.Values)
      {
        if (obj != null && obj != DBNull.Value)
          this._sketchIdList.Add(obj.ToString());
      }
    }
  }

  /// <summary>Отображение информации по тек. слоям</summary>
  private void UpdateMapObject()
  {
    if (!(this._mapObject is ShowObject mapObject))
      return;
    foreach (ILayer layer in (IEnumerable) mapObject.Layers)
    {
      if (layer != null)
        layer.Visible = this.GetLayerVisibility(layer);
    }
    RectangleD rectangleD = this.CalcVisibleDwgLayerBounds(mapObject.Layers);
    if (Math.Abs(rectangleD.Height) < 9.9999997473787516E-06)
    {
      ILayout inFile = mapObject.Layouts.InFile;
      if (inFile != null)
        rectangleD = inFile.Bounds;
    }
    RectangleF drawBox = new RectangleF((float) rectangleD.X, (float) rectangleD.Y, (float) rectangleD.Width, (float) rectangleD.Height);
    mapObject.SetClip((RectangleD) drawBox);
    this._mapObject.Bounds = drawBox;
  }

  /// <summary>Анализ слоя эскизов</summary>
  /// <param name="layer"></param>
  private bool GetLayerVisibility(ILayer layer)
  {
    if (layer == null)
      return false;
    if (this._sketchIdList == null)
      return layer.Visible;
    bool layerVisibility = false;
    string upperInvariant1 = layer.Name.ToUpperInvariant();
    foreach (string sketchId in this._sketchIdList)
    {
      string upperInvariant2 = sketchId.ToUpperInvariant();
      if (upperInvariant2 == layer.Name || DraftViewProcessor.IsExtraLayer(upperInvariant2, upperInvariant1))
      {
        layerVisibility = true;
        break;
      }
    }
    return layerVisibility;
  }

  /// <summary>Получение габаритов включенных слоев DWG</summary>
  /// <param name="layersDwg">таблица слоёв DWG</param>
  /// <returns>габариты включённых слоёв</returns>
  private RectangleD CalcVisibleDwgLayerBounds(ILayerTable layersDwg)
  {
    RectangleD first = RectangleD.Empty;
    if (layersDwg == null)
      return first;
    List<string> stringList = new List<string>();
    if (this._sketchIdList != null)
    {
      foreach (string sketchId in this._sketchIdList)
      {
        string upperInvariant = sketchId.ToUpperInvariant();
        stringList.Add(upperInvariant);
      }
    }
    ILayer layer1 = (ILayer) null;
    foreach (ILayer layer2 in (IEnumerable) layersDwg)
    {
      if (layer2.Name.ToUpperInvariant() == "BLANK")
        layer1 = layer2;
    }
    if (stringList.Count == 0)
      return first;
    bool flag = false;
    foreach (string str1 in stringList)
    {
      string str2 = str1.ToUpperInvariant() + "_BLANK";
      foreach (ILayer layer3 in (IEnumerable) layersDwg)
      {
        string upperInvariant = layer3.Name.ToUpperInvariant();
        if (!(upperInvariant == "") && upperInvariant == str2)
        {
          flag = true;
          if (Math.Abs(first.Height) < 9.9999997473787516E-06)
            first = layer3.Bound;
          first = RectangleD.Union(first, layer3.Bound);
        }
      }
    }
    if (Math.Abs(first.Height) < 9.9999997473787516E-06 && stringList.Count != 1)
      first = layersDwg.Bounds;
    if (!flag && layer1 != null)
      first = RectangleD.Union(first, layer1.Bound);
    if (Math.Abs(first.Height) < 9.9999997473787516E-06)
      first = layersDwg.Bounds;
    return first;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="mapObject"></param>
  /// <param name="selectedItems"></param>
  public DraftViewProcessor(MapObject mapObject, ISelectedItems selectedItems)
  {
    this._mapObject = mapObject ?? throw new ArgumentNullException(nameof (mapObject));
    this._selectedItems = selectedItems ?? throw new ArgumentNullException(nameof (selectedItems));
  }

  /// <summary>
  /// 
  /// </summary>
  public void Execute()
  {
    this.LoadSketchInfo();
    this.UpdateMapObject();
  }

  /// <summary>Проверка является ли слой дополнительным</summary>
  /// <param name="shortLayerName">Краткое имя слоя</param>
  /// <param name="layerName">Полное имя слоя</param>
  /// <returns></returns>
  private static bool IsExtraLayer(string shortLayerName, string layerName)
  {
    if (layerName.IndexOf(shortLayerName, 0, StringComparison.InvariantCultureIgnoreCase) != 0)
      return false;
    foreach (string excludeLayer in DraftViewProcessor.ExcludeLayers)
    {
      if (string.Format(excludeLayer, (object) shortLayerName) == layerName)
        return false;
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public static void BeforeMapObjectView(object sender, NotificationEventArgs e)
  {
    if (!(e is BeforeMapObjectViewEventArgs objectViewEventArgs))
      return;
    ISelectedItems selectedItems = objectViewEventArgs.SelectedItems;
    if (selectedItems == null || ((IDBObjectTypeID) selectedItems.GetItemData(0, typeof (IDBObjectTypeID))).Value != TechCardConsts.ObjectTypes.DraftCadmechID)
      return;
    new DraftViewProcessor(objectViewEventArgs.MapObject, selectedItems).Execute();
  }
}
