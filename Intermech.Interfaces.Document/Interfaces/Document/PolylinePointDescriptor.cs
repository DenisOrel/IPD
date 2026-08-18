// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PolylinePointDescriptor
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Описатель атрибута из AdditionalAttributeCollection для преставления его в PropertyGrid</summary>
[Serializable]
public class PolylinePointDescriptor : PropertyDescriptor
{
  /// <summary>Имя атрибута</summary>
  private PolylineData polyline;
  private int pointIndex = -1;

  /// <summary>Конструктор</summary>
  public PolylinePointDescriptor(PolylineData polyline, int pointIndex)
    : base(pointIndex.ToString(), (Attribute[]) null)
  {
    this.polyline = polyline;
    this.pointIndex = pointIndex;
  }

  /// <summary>Получить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Значение свойства</returns>
  public override object GetValue(object component)
  {
    if (this.polyline != null && this.pointIndex >= 0)
    {
      PointF[] pathPoints = this.polyline.PathPoints;
      if (pathPoints != null && this.pointIndex < pathPoints.Length)
      {
        PointF user = pathPoints[this.pointIndex];
        PageData page = this.polyline.Page;
        if (page != null)
          user = page.ConvertInternalToUser(user);
        return (object) user;
      }
    }
    return (object) PointF.Empty;
  }

  /// <summary>Установить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <param name="value">Значение свойства</param>
  public override void SetValue(object component, object value)
  {
    if (this.polyline == null || this.pointIndex < 0)
      return;
    PointF[] pathPoints = this.polyline.PathPoints;
    if (pathPoints == null || this.pointIndex >= pathPoints.Length)
      return;
    PointF point = value != null ? (PointF) value : throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_80"));
    PageData page = this.polyline.Page;
    if (page != null)
      point = page.ConvertUserToInternal(point);
    if (!(pathPoints[this.pointIndex] != point))
      return;
    pathPoints[this.pointIndex] = point;
    this.polyline.Path = new GraphicsPath(pathPoints, this.polyline.PathTypes);
  }

  /// <summary>Можно ли сбросить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Можно ли сбросить значение свойства</returns>
  public override bool CanResetValue(object component) => false;

  /// <summary>Тип владельца свойства</summary>
  public override Type ComponentType
  {
    [DebuggerStepThrough] get => typeof (PointF[]);
  }

  /// <summary>Тип свойства</summary>
  public override Type PropertyType
  {
    [DebuggerStepThrough] get => typeof (PointF);
  }

  /// <summary>Сбросить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  public override void ResetValue(object component)
  {
  }

  /// <summary>Нужно ли сохранить данное значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Нужно ли сохранить данное значение свойства</returns>
  public override bool ShouldSerializeValue(object component) => false;

  /// <summary>Только для чтения</summary>
  public override bool IsReadOnly
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Категория свойства</summary>
  public override string Category
  {
    [DebuggerStepThrough] get
    {
      string category = base.Category;
      if (category == "Misc")
        category = LocalizationHolder.rm.GetString("Interfaces.Document_81");
      return category;
    }
  }

  public override TypeConverter Converter => (TypeConverter) new PointFConverter();

  public override PropertyDescriptorCollection GetChildProperties(
    object instance,
    Attribute[] filter)
  {
    return instance != null ? new PointFConverter().GetProperties((object) (PointF) instance) : base.GetChildProperties(instance, filter);
  }
}
