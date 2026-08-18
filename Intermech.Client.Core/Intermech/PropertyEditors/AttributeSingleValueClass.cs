
// Type: Intermech.PropertyEditors.AttributeSingleValueClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Drawing;


namespace Intermech.PropertyEditors;

public class AttributeSingleValueClass
{
  public BlobInformation bi;
  public long boxId = -1;

  /// <summary>цвет в звисимости от типа файла</summary>
  public Color ColorText { get; private set; }

  public AttributeSingleValueClass(BlobInformation bi, long boxId)
  {
    this.bi = bi;
    this.boxId = boxId;
    this.ColorText = Color.Empty;
  }

  /// <summary>получить цвет в звисимости от типа файла</summary>
  /// <param name="fileType">Тип файла в файловом шкафу</param>
  /// <param name="contentModifyDate">Дата модификации содержимого объекта в локальном времени с округлением до секунд (чтобы потом правильно работало сравнение)</param>
  /// <returns>цвет</returns>
  public void InitializeColorText(FileTypes fileType, DateTime contentModifyDate)
  {
    ColorFileTypesAttribute attribute = fileType.GetAttribute<ColorFileTypesAttribute>();
    Color color = attribute.Color;
    if (fileType == FileTypes.ftAuthentical && DateTime.Compare(contentModifyDate, this.bi.ModifyDate) > 0)
      color = attribute.Obsolete;
    this.ColorText = color;
  }
}
