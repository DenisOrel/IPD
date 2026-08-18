// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ArrayEditHelper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс для изменения массивов</summary>
public abstract class ArrayEditHelper
{
  /// <summary> Удаление из массива всех элементов не удовлетворяющих условиям валидации </summary>
  /// <param name="array"> массив </param>
  /// <param name="itemValidator"> процедура валидации элемента массива </param>
  /// <returns> Подчищеный массив </returns>
  public static Array DeleteValues(Array array, ArrayEditHelper.ValidateItemDelegate itemValidator)
  {
    if (array.Length > 0 && itemValidator != null)
    {
      int num1 = 0;
      int[] numArray = new int[array.Length];
      for (int index = 0; index < array.Length; ++index)
      {
        object arrayItem = array.GetValue(index);
        if (!itemValidator(arrayItem))
        {
          numArray.SetValue((object) num1, index);
          ++num1;
        }
      }
      if (num1 > 0)
      {
        Array instance = Array.CreateInstance(array.GetType().GetElementType(), array.Length - num1);
        if (instance.Length > 0)
        {
          int index1 = 0;
          int num2 = numArray[index1];
          for (int index2 = 0; index2 < array.Length; ++index2)
          {
            if (num2 != index2)
            {
              instance.SetValue(array.GetValue(index2), index2 - index1);
            }
            else
            {
              ++index1;
              num2 = numArray[index1];
            }
          }
        }
        return instance;
      }
    }
    return array;
  }

  /// <summary> Удаление из массива всех элементов со значениями = delValue </summary>
  /// <param name="array"> массив </param>
  /// <param name="delValue"> Удаляемое значение </param>
  /// <returns> Подчищенный массив </returns>
  public static Array DeleteValues(Array array, object delValue)
  {
    if (array.Length > 0)
    {
      int num1 = 0;
      int[] numArray = new int[array.Length];
      for (int index = 0; index < array.Length; ++index)
      {
        object obj = array.GetValue(index);
        if (obj == null || delValue == null || obj.Equals(delValue))
        {
          numArray.SetValue((object) num1, index);
          ++num1;
        }
      }
      if (num1 > 0)
      {
        Array instance = Array.CreateInstance(array.GetType().GetElementType(), array.Length - num1);
        if (instance.Length > 0)
        {
          int index1 = 0;
          int num2 = numArray[index1];
          for (int index2 = 0; index2 < array.Length; ++index2)
          {
            if (num2 != index2)
            {
              instance.SetValue(array.GetValue(index2), index2 - index1);
            }
            else
            {
              ++index1;
              num2 = numArray[index1];
            }
          }
        }
        return instance;
      }
    }
    return array;
  }

  /// <summary>Сравнение содержимого двух массивов вне зависимости от того, в каком порядке они расположены</summary>
  /// <param name="array1">Массив 1</param>
  /// <param name="array2">Массив 2</param>
  /// <returns>Содержат ли переданные массивы одинаковые элементы</returns>
  public static bool IsArraysContentEqual(Array array1, Array array2)
  {
    if (array1.Length != array2.Length)
      return false;
    foreach (object obj in array1)
    {
      if (Array.IndexOf(array2, obj) < 0)
        return false;
    }
    return true;
  }

  /// <summary>Добавить элемент в массив</summary>
  /// <param name="array">Массив</param>
  /// <param name="newItem">Новый элемент массива</param>
  /// <returns>Новый массив с новым элементом</returns>
  public static Array AddItemToArray(Array array, object newItem)
  {
    if (array == null)
      throw new ArgumentNullException(nameof (array));
    if (newItem == null)
      throw new ArgumentNullException(nameof (newItem));
    Array instance = Array.CreateInstance(array.GetType().GetElementType(), array.Length + 1);
    array.CopyTo(instance, 0);
    instance.SetValue(newItem, instance.Length - 1);
    return instance;
  }

  /// <summary>Вставить элемент в массив</summary>
  /// <param name="array">Массив</param>
  /// <param name="newItem">Новый элемент массива</param>
  /// <param name="index">Индекс элемента</param>
  /// <returns>Новый массив с новым элементом</returns>
  public static Array InsertItemToArray(Array array, object newItem, int index)
  {
    if (array == null)
      throw new ArgumentNullException(nameof (array));
    if (newItem == null)
      throw new ArgumentNullException(nameof (newItem));
    if (index < 0 || index > array.Length)
      throw new ArgumentOutOfRangeException(nameof (index));
    Array instance = Array.CreateInstance(array.GetType().GetElementType(), array.Length + 1);
    if (index > 0)
      Array.Copy(array, instance, index);
    instance.SetValue(newItem, index);
    if (index < array.Length)
      Array.Copy(array, index, instance, index + 1, array.Length - index);
    return instance;
  }

  /// <summary>Удалить элемент из массива</summary>
  /// <param name="array">Массив</param>
  /// <param name="index">Индекс удаляемого элемента</param>
  /// <returns>Новый массив без элемента</returns>
  public static Array RemoveItemAt(Array array, int index)
  {
    if (array == null)
      throw new ArgumentNullException(nameof (array));
    if (index < 0 || index >= array.Length)
      throw new ArgumentOutOfRangeException(nameof (index));
    Array instance = Array.CreateInstance(array.GetType().GetElementType(), array.Length - 1);
    if (index > 0)
      Array.Copy(array, instance, index);
    if (index < array.Length - 1)
      Array.Copy(array, index + 1, instance, index, array.Length - index - 1);
    return instance;
  }

  /// <summary>Переместить элемент массива</summary>
  /// <param name="list">Исходный массив</param>
  /// <param name="index">Индекс элемента</param>
  /// <param name="newIndex">Новый индекс элемента</param>
  /// <returns>Массив с перемещенным элементом</returns>
  public static void MoveItem(IList list, int index, int newIndex)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (index < 0 || index >= list.Count)
      throw new ArgumentOutOfRangeException(nameof (index));
    if (newIndex < 0 || newIndex >= list.Count)
      throw new ArgumentOutOfRangeException(nameof (newIndex));
    object obj = list[index];
    list.RemoveAt(index);
    list.Insert(newIndex, obj);
  }

  /// <summary>Переместить элемент массива</summary>
  /// <param name="array">Исходный массив</param>
  /// <param name="index">Индекс элемента</param>
  /// <param name="newIndex">Новый индекс элемента</param>
  /// <param name="useSourceArray">Перемещать в исходном массиве,
  /// иначе создавать копию и перемещать в ней</param>
  /// <returns>Массив с перемещенным элементом</returns>
  public static Array MoveItem(Array array, int index, int newIndex, bool useSourceArray)
  {
    if (array == null)
      throw new ArgumentNullException(nameof (array));
    if (index < 0 || index >= array.Length)
      throw new ArgumentOutOfRangeException(nameof (index));
    if (newIndex < 0 || newIndex >= array.Length)
      throw new ArgumentOutOfRangeException(nameof (newIndex));
    object obj = array.GetValue(index);
    Array destinationArray;
    if (useSourceArray)
    {
      destinationArray = array;
      if (newIndex < index)
      {
        for (int index1 = index - 1; index1 >= newIndex; --index1)
          array.SetValue(array.GetValue(index1), index1 + 1);
        destinationArray.SetValue(obj, newIndex);
      }
      else if (index < newIndex)
      {
        for (int index2 = index + 1; index2 <= newIndex; ++index2)
          array.SetValue(array.GetValue(index2), index2 - 1);
        destinationArray.SetValue(obj, newIndex);
      }
    }
    else
    {
      destinationArray = Array.CreateInstance(array.GetType().GetElementType(), array.Length);
      if (newIndex < index)
      {
        if (newIndex > 0)
          Array.Copy(array, destinationArray, newIndex);
        destinationArray.SetValue(obj, newIndex);
        if (index - newIndex > 1)
          Array.Copy(array, newIndex, destinationArray, newIndex + 1, index - newIndex - 1);
        if (array.Length - index > 1)
          Array.Copy(array, index + 1, destinationArray, index + 1, array.Length - index - 1);
      }
      else if (index < newIndex)
      {
        if (index > 0)
          Array.Copy(array, destinationArray, index);
        if (newIndex - index > 1)
          Array.Copy(array, index + 1, destinationArray, index, newIndex - index - 1);
        destinationArray.SetValue(obj, newIndex);
        if (array.Length - newIndex > 1)
          Array.Copy(array, newIndex + 1, destinationArray, newIndex + 1, array.Length - newIndex - 1);
      }
      else
        Array.Copy(array, destinationArray, array.Length);
    }
    return destinationArray;
  }

  /// <summary> Процедура валидаци элемента массива </summary>
  public delegate bool ValidateItemDelegate(object arrayItem);
}
