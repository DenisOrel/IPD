
// Type: Intermech.Client.Core.TreeListHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Client.Core;

/// <summary> Класс с полезными методами для работы с компонентой TreeList </summary>
public static class TreeListHelper
{
  private static int Vers;

  private static bool IsObjectSerializable(object obj)
  {
    if (obj == null)
      return false;
    Type type = obj.GetType();
    if (!(type != (Type) null))
      return false;
    object[] customAttributes = type.GetCustomAttributes(typeof (SerializableAttribute), false);
    return customAttributes != null && customAttributes.Length != 0;
  }

  /// <summary>
  /// Получить строку с состоянием колонок (размеров, порядка следования и т.п.).
  /// Для уникальной идентификации колонки используется содержимое параметра Tag колонки.
  /// </summary>
  /// <param name="treeList"> Дерево порядок и свойства колонок которого надо сохранить </param>
  /// <returns> Строка Base64 с сохранёным состоянием колонок </returns>
  public static string GetCollumnsState(TreeList treeList)
  {
    return TreeListHelper.GetCollumnsState(treeList, (TreeListHelper.GetTreeListColumnIdDelegate) null, (TreeListHelper.GetIsRelationDelegate) null);
  }

  /// <summary>
  /// Востановить состояние колонок (размеров, порядка следования и т.п.).
  /// Для уникальной идентификации колонки используется содержимое параметра Tag колонки.
  /// </summary>
  /// <param name="treeList"> Дерево порядок и свойства колонок которого надо востановить </param>
  /// <param name="collumnsState"> Дерево порядок и свойства колонок которого надо сохранить</param>
  /// <returns> True, если порядок колонок был выстановлен удачно </returns>
  public static bool SetCollumnsState(TreeList treeList, string collumnsState)
  {
    return TreeListHelper.SetCollumnsState(treeList, collumnsState, (TreeListHelper.GetTreeListColumnByIdDelegate) null);
  }

  /// <summary> Востановить состояние колонок (размеров, порядка следования и т.п.). </summary>
  /// 
  ///             Для уникальной идентификации колонки используется Caption столбца.
  ///             <param name="treeList"></param>
  /// <param name="collumnsState"></param>
  /// <param name="needCreateColumn"></param>
  /// <returns> True, если порядок колонок был выстановлен удачно </returns>
  public static bool SetCollumnsStateByCaption(
    TreeList treeList,
    string collumnsState,
    bool needCreateColumn)
  {
    if (treeList == null || collumnsState == string.Empty || treeList.Columns.Count == 0 && !needCreateColumn)
      return false;
    try
    {
      object obj;
      using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(collumnsState)))
        obj = new BinaryFormatter().Deserialize((Stream) serializationStream);
      if (obj is Array)
      {
        TreeListHelper.TreeListCollumnParams[] listCollumnParamsArray = (TreeListHelper.TreeListCollumnParams[]) (obj as Array);
        if (listCollumnParamsArray.Length != 0)
        {
          foreach (TreeListHelper.TreeListCollumnParams listCollumnParams in listCollumnParamsArray)
          {
            TreeListColumn treeListColumn1 = (TreeListColumn) null;
            foreach (TreeListColumn column in (CollectionBase) treeList.Columns)
            {
              if (column != null && column.Caption.Equals(listCollumnParams.Caption))
              {
                treeListColumn1 = column;
                break;
              }
            }
            if (treeListColumn1 != null)
            {
              if (treeListColumn1.VisibleIndex != listCollumnParams.VisibleIndex)
                treeListColumn1.VisibleIndex = listCollumnParams.VisibleIndex;
              if (treeListColumn1.Width != listCollumnParams.Width)
                treeListColumn1.Width = listCollumnParams.Width;
            }
            else if (needCreateColumn)
            {
              TreeListColumn treeListColumn2 = treeList.Columns.Add();
              treeListColumn2.Caption = listCollumnParams.Caption;
              treeListColumn2.Name = listCollumnParams.Name;
              treeListColumn2.VisibleIndex = listCollumnParams.VisibleIndex;
              treeListColumn2.Width = listCollumnParams.Width;
              treeListColumn2.Tag = listCollumnParams.Tag;
            }
          }
          return true;
        }
      }
      return false;
    }
    catch
    {
      return false;
    }
  }

  /// <summary> Получить строку с состоянием колонок (размеров, порядка следования и т.п.). </summary>
  /// <param name="treeList"></param>
  /// <param name="getTreeListColumnIdDelegate"></param>
  /// <param name="getIsRelationDelegate"></param>
  /// <returns></returns>
  public static string GetCollumnsState(
    TreeList treeList,
    TreeListHelper.GetTreeListColumnIdDelegate getTreeListColumnIdDelegate,
    TreeListHelper.GetIsRelationDelegate getIsRelationDelegate)
  {
    if (treeList == null || treeList.Columns.Count == 0)
      return string.Empty;
    TreeListHelper.TreeListCollumnParams[] listCollumnParamsArray1 = new TreeListHelper.TreeListCollumnParams[treeList.Columns.Count];
    int index1 = 0;
    foreach (TreeListColumn column in (CollectionBase) treeList.Columns)
    {
      if (column != null)
      {
        listCollumnParamsArray1[index1].Caption = column.Caption;
        listCollumnParamsArray1[index1].Name = column.Name;
        listCollumnParamsArray1[index1].VisibleIndex = column.VisibleIndex;
        listCollumnParamsArray1[index1].ID = getTreeListColumnIdDelegate == null ? column.Tag : getTreeListColumnIdDelegate(column);
        listCollumnParamsArray1[index1].Width = column.Width;
        listCollumnParamsArray1[index1].Tag = column.Tag;
        listCollumnParamsArray1[index1].IsRelation = getIsRelationDelegate != null && getIsRelationDelegate(index1);
        ++index1;
      }
    }
    TreeListHelper.TreeListCollumnParams[] listCollumnParamsArray2 = new TreeListHelper.TreeListCollumnParams[index1];
    for (int index2 = 0; index2 < index1; ++index2)
      listCollumnParamsArray2[index2] = listCollumnParamsArray1[index2];
    Array.Sort<TreeListHelper.TreeListCollumnParams>(listCollumnParamsArray2);
    try
    {
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) listCollumnParamsArray2);
        return Convert.ToBase64String(serializationStream.ToArray());
      }
    }
    catch
    {
      return string.Empty;
    }
  }

  public static bool SetCollumnsState(
    TreeList treeList,
    string collumnsState,
    TreeListHelper.GetTreeListColumnByIdDelegate getTreeListColumnByIdDelegate)
  {
    return TreeListHelper.SetCollumnsState(treeList, collumnsState, getTreeListColumnByIdDelegate, false);
  }

  /// <summary> Востановить состояние колонок (размеров, порядка следования и т.п.). </summary>
  /// <param name="treeList"></param>
  /// <param name="collumnsState"></param>
  /// <param name="getTreeListColumnByIdDelegate"></param>
  /// <param name="needCreateColumn"></param>
  /// <returns> True, если порядок колонок был выстановлен удачно </returns>
  public static bool SetCollumnsState(
    TreeList treeList,
    string collumnsState,
    TreeListHelper.GetTreeListColumnByIdDelegate getTreeListColumnByIdDelegate,
    bool needCreateColumn)
  {
    if (treeList != null)
    {
      if (!(collumnsState == string.Empty))
      {
        try
        {
          object obj;
          using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(collumnsState)))
            obj = new BinaryFormatter().Deserialize((Stream) serializationStream);
          List<TreeListColumn> treeListColumnList = new List<TreeListColumn>();
          if (obj is Array)
          {
            TreeListHelper.TreeListCollumnParams[] listCollumnParamsArray = (TreeListHelper.TreeListCollumnParams[]) (obj as Array);
            if (listCollumnParamsArray.Length != 0)
            {
              foreach (TreeListHelper.TreeListCollumnParams listCollumnParams in listCollumnParamsArray)
              {
                TreeListColumn treeListColumn;
                if (getTreeListColumnByIdDelegate == null)
                {
                  treeListColumn = (TreeListColumn) null;
                  foreach (TreeListColumn column in (CollectionBase) treeList.Columns)
                  {
                    if (column != null && column.Tag != null && column.Tag.Equals(listCollumnParams.ID))
                    {
                      treeListColumn = column;
                      break;
                    }
                  }
                }
                else
                  treeListColumn = getTreeListColumnByIdDelegate(treeList, listCollumnParams.ID);
                if (treeListColumn != null)
                {
                  if (treeListColumn.VisibleIndex != listCollumnParams.VisibleIndex)
                    treeListColumn.VisibleIndex = listCollumnParams.VisibleIndex;
                  if (treeListColumn.Width != listCollumnParams.Width)
                    treeListColumn.Width = listCollumnParams.Width;
                }
                else if (needCreateColumn)
                  treeListColumnList.Add(new TreeListColumn()
                  {
                    Caption = listCollumnParams.Caption,
                    Name = listCollumnParams.Name,
                    VisibleIndex = listCollumnParams.VisibleIndex,
                    Width = listCollumnParams.Width,
                    Tag = listCollumnParams.Tag
                  });
              }
              if (treeListColumnList.Count > 0)
              {
                TreeListColumn[] columns = new TreeListColumn[treeListColumnList.Count];
                for (int index = 0; index < treeListColumnList.Count; ++index)
                  columns[index] = treeListColumnList[index];
                treeList.Columns.AddRange(columns);
              }
              return true;
            }
          }
          return false;
        }
        catch
        {
          return false;
        }
      }
    }
    return false;
  }

  /// <summary> Делегат для получения уникального идентификатора колонки </summary>
  /// <param name="treeListColumn"> Колонка </param>
  /// <returns> Идентификатор колонки </returns>
  public delegate object GetTreeListColumnIdDelegate(TreeListColumn treeListColumn);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public delegate bool GetIsRelationDelegate(int index);

  /// <summary> Делегат для получения колонки из её уникального идентификатора </summary>
  /// <param name="treeList"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  public delegate TreeListColumn GetTreeListColumnByIdDelegate(TreeList treeList, object id);

  [Serializable]
  public struct TreeListCollumnParams(
    string caption,
    string name,
    int visibleIndex,
    object id,
    int width,
    object tag,
    bool isRelation) : IComparable<TreeListHelper.TreeListCollumnParams>, ISerializable
  {
    public string Caption = caption;
    public string Name = name;
    public int VisibleIndex = visibleIndex;
    public object ID = id;
    public int Width = width;
    public object Tag = tag;
    public bool IsRelation = isRelation;

    public int CompareTo(TreeListHelper.TreeListCollumnParams other)
    {
      if (this.VisibleIndex == other.VisibleIndex)
        return 0;
      return this.VisibleIndex <= other.VisibleIndex ? -1 : 1;
    }

    public void GetObjectData(SerializationInfo si, StreamingContext ctx)
    {
      si.AddValue("StructVers", TreeListHelper.Vers);
      si.AddValue("Caption", (object) this.Caption);
      si.AddValue("Name", (object) this.Name);
      si.AddValue("VisibleIndex", this.VisibleIndex);
      si.AddValue("Width", this.Width);
      si.AddValue("IsRelation", this.IsRelation);
      bool flag1 = TreeListHelper.IsObjectSerializable(this.ID);
      si.AddValue("ID_Serializable", flag1);
      if (flag1)
      {
        si.AddValue("ID_Type", (object) this.ID.GetType().ToString());
        si.AddValue("ID_Data", this.ID, this.ID.GetType());
      }
      bool flag2 = TreeListHelper.IsObjectSerializable(this.Tag);
      si.AddValue("Tag_Serializable", flag2);
      if (!flag2)
        return;
      si.AddValue("Tag_Type", (object) this.Tag.GetType().ToString());
      si.AddValue("Tag_Data", this.Tag, this.Tag.GetType());
    }

    private TreeListCollumnParams(SerializationInfo si, StreamingContext ctx)
      : this("", "", -1, (object) null, 0, (object) null, true)
    {
      this.Tag = this.ID = (object) null;
      int num = -1;
      foreach (SerializationEntry serializationEntry in si)
      {
        if (serializationEntry.Name == "StructVers")
        {
          num = Convert.ToInt32(serializationEntry.Value);
          break;
        }
      }
      if (num != 0)
        return;
      this.Caption = si.GetString(nameof (Caption));
      this.Name = si.GetString(nameof (Name));
      this.VisibleIndex = si.GetInt32(nameof (VisibleIndex));
      this.Width = si.GetInt32(nameof (Width));
      try
      {
        this.IsRelation = si.GetBoolean(nameof (IsRelation));
      }
      catch
      {
      }
      if (si.GetBoolean("ID_Serializable"))
      {
        Type type = Type.GetType(si.GetString("ID_Type"));
        if (type != (Type) null)
          this.ID = si.GetValue("ID_Data", type);
      }
      if (!si.GetBoolean("Tag_Serializable"))
        return;
      Type type1 = Type.GetType(si.GetString("Tag_Type"));
      if (!(type1 != (Type) null))
        return;
      this.Tag = si.GetValue("Tag_Data", type1);
    }
  }
}
