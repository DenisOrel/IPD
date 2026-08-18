// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CustomBorder
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Класс определяет стиль линии для каждой границы прямоугольника</summary>
[Serializable]
public class CustomBorder : RectangleBorder
{
  private BorderLine top;
  private BorderLine bottom;
  private BorderLine left;
  private BorderLine right;
  private BorderLine innerHorizontal;

  /// <summary>Стиль линии верхней границы</summary>
  [ImmutableObject(true)]
  public override BorderLine Top
  {
    [DebuggerStepThrough] get => this.top;
    set => this.top = value;
  }

  /// <summary>Стиль линии нижней границы</summary>
  public override BorderLine Bottom
  {
    [DebuggerStepThrough] get => this.bottom;
    set => this.bottom = value;
  }

  /// <summary>Стиль линии левой границы</summary>
  public override BorderLine Left
  {
    [DebuggerStepThrough] get => this.left;
    set => this.left = value;
  }

  /// <summary>Стиль линии правой границы</summary>
  public override BorderLine Right
  {
    [DebuggerStepThrough] get => this.right;
    set => this.right = value;
  }

  /// <summary>Стиль внутренней горизонтальной линии</summary>
  public override BorderLine InnerHorizontal
  {
    [DebuggerStepThrough] get => this.innerHorizontal;
    set => this.innerHorizontal = value;
  }

  /// <summary>Конструктор</summary>
  public CustomBorder()
  {
    this.top = (BorderLine) null;
    this.bottom = (BorderLine) null;
    this.left = (BorderLine) null;
    this.right = (BorderLine) null;
    this.innerHorizontal = (BorderLine) null;
  }

  /// <summary>Установить стили линий границ</summary>
  /// <param name="top">Стиль линии верхней границы</param>
  /// <param name="innerHorizontal">Стиль линии внутренней горизонтальной границы</param>
  /// <param name="bottom">Стиль линии нижней границы</param>
  /// <param name="left">Стиль линии левой границы</param>
  /// <param name="right">Стиль линии правой границы</param>
  public virtual void SetLines(
    BorderLine top,
    BorderLine innerHorizontal,
    BorderLine bottom,
    BorderLine left,
    BorderLine right)
  {
    this.top = top;
    this.innerHorizontal = innerHorizontal;
    this.bottom = bottom;
    this.left = left;
    this.right = right;
  }

  /// <summary>Установить стиль линии верхней границы</summary>
  /// <param name="line">Стиль линии</param>
  public virtual void SetTopLine(BorderLine line) => this.top = line;

  /// <summary>Установить стиль линии внутренней горизонтальной границы</summary>
  /// <param name="line">Стиль линии</param>
  public virtual void SetInnerHorizontalLine(BorderLine line) => this.innerHorizontal = line;

  /// <summary>Установить стиль линии нижней границы</summary>
  /// <param name="line">Стиль линии</param>
  public virtual void SetBottomLine(BorderLine line) => this.bottom = line;

  /// <summary>Установить стиль линии левой границы</summary>
  /// <param name="line">Стиль линии</param>
  public virtual void SetLeftLine(BorderLine line) => this.left = line;

  /// <summary>Установить стиль линии правой границы</summary>
  /// <param name="line">Стиль линии</param>
  public virtual void SetRightLine(BorderLine line) => this.right = line;

  /// <summary>Проверяет равенство объектов</summary>
  /// <param name="obj">Объект с которым сравнивать</param>
  /// <returns>true, если объекты эквивалентны</returns>
  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    if (!(this.GetType() == obj.GetType()))
      return base.Equals(obj);
    CustomBorder customBorder = (CustomBorder) obj;
    return object.Equals((object) this.top, (object) customBorder.top) && object.Equals((object) this.bottom, (object) customBorder.bottom) && object.Equals((object) this.left, (object) customBorder.left) && object.Equals((object) this.right, (object) customBorder.right) && object.Equals((object) this.innerHorizontal, (object) customBorder.innerHorizontal);
  }

  /// <summary>Получить хэш код объекта</summary>
  /// <returns>Хэш код объекта</returns>
  public override int GetHashCode()
  {
    int hashCode1 = this.top != null ? this.top.GetHashCode() : 0;
    int hashCode2 = this.bottom != null ? this.bottom.GetHashCode() : 0;
    int hashCode3 = this.left != null ? this.left.GetHashCode() : 0;
    int hashCode4 = this.right != null ? this.right.GetHashCode() : 0;
    int hashCode5 = this.innerHorizontal != null ? this.innerHorizontal.GetHashCode() : 0;
    int num = hashCode2 << 13 | hashCode2 >> 19;
    return hashCode1 ^ num ^ (hashCode3 << 26 | hashCode3 >> 6) ^ (hashCode4 << 7 | hashCode4 >> 25) ^ (hashCode5 << 8 | hashCode5 >> 8);
  }

  /// <summary>Конструктор</summary>
  /// <param name="top">Стиль линии верхней границы</param>
  /// <param name="innerHorizontal">Стиль линии внутренней горизонтальной границы</param>
  /// <param name="bottom">Стиль линии нижней границы</param>
  /// <param name="left">Стиль линии левой границы</param>
  /// <param name="right">Стиль линии правой границы</param>
  public CustomBorder(
    BorderLine top,
    BorderLine innerHorizontal,
    BorderLine bottom,
    BorderLine left,
    BorderLine right)
  {
    this.top = top;
    this.innerHorizontal = innerHorizontal;
    this.bottom = bottom;
    this.left = left;
    this.right = right;
  }

  /// <summary>Клонировать</summary>
  /// <returns>Возвращает полную копию экземпляра класса</returns>
  public override RectangleBorder Clone()
  {
    BorderLine top = (BorderLine) null;
    BorderLine innerHorizontal = (BorderLine) null;
    BorderLine bottom = (BorderLine) null;
    BorderLine left = (BorderLine) null;
    BorderLine right = (BorderLine) null;
    if (this.top != null)
      top = this.top.Clone();
    if (this.innerHorizontal != null)
      innerHorizontal = this.innerHorizontal.Clone();
    if (this.bottom != null)
      bottom = this.bottom.Clone();
    if (this.left != null)
      left = this.left.Clone();
    if (this.right != null)
      right = this.right.Clone();
    return (RectangleBorder) new CustomBorder(top, innerHorizontal, bottom, left, right);
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    if (this.top != null)
      this.top.WriteToXml("Top", xw, objectRefId);
    if (this.innerHorizontal != null)
      this.innerHorizontal.WriteToXml("InnerHorz", xw, objectRefId);
    if (this.bottom != null)
      this.bottom.WriteToXml("Bottom", xw, objectRefId);
    if (this.left != null)
      this.left.WriteToXml("Left", xw, objectRefId);
    if (this.right != null)
      this.right.WriteToXml("Right", xw, objectRefId);
    xw.WriteEndElement();
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if ("Top" == readArgs.Reader.LocalName)
    {
      this.top = new BorderLine();
      this.top.ReadFromXml(readArgs);
      return true;
    }
    if ("InnerHorz" == readArgs.Reader.LocalName)
    {
      this.innerHorizontal = new BorderLine();
      this.innerHorizontal.ReadFromXml(readArgs);
      return true;
    }
    if ("Bottom" == readArgs.Reader.LocalName)
    {
      this.bottom = new BorderLine();
      this.bottom.ReadFromXml(readArgs);
      return true;
    }
    if ("Left" == readArgs.Reader.LocalName)
    {
      this.left = new BorderLine();
      this.left.ReadFromXml(readArgs);
      return true;
    }
    if (!("Right" == readArgs.Reader.LocalName))
      return false;
    this.right = new BorderLine();
    this.right.ReadFromXml(readArgs);
    return true;
  }
}
