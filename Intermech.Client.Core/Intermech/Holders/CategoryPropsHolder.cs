
// Type: Intermech.Holders.CategoryPropsHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DatabaseConfigurator;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;


namespace Intermech.Holders;

/// <summary>
/// Хранит обработчики подписавшихся на дополнение PropertyGrid своими полями.
/// Собирает данные со всех подписчиков.
/// </summary>
public class CategoryPropsHolder
{
  private static int regInstanceGenerator = 0;
  private static int propertyDescriptorIDGenerator = ClientConsts.UserPropertyDescriptorID;
  private static Hashtable categoryPropsHashtable = new Hashtable();

  private static int RegInstanceGenerator => CategoryPropsHolder.regInstanceGenerator++;

  private static int NextPropertyDescriptorID
  {
    get => CategoryPropsHolder.propertyDescriptorIDGenerator++;
  }

  /// <summary>регистрировать подписчика</summary>
  /// <param name="category"></param>
  /// <param name="iCategoryProps"></param>
  /// <returns></returns>
  public static int RegisterCategoryProps(int category, ICategoryProps iCategoryProps)
  {
    int instanceGenerator = CategoryPropsHolder.RegInstanceGenerator;
    CategoryPropsHolder.categoryPropsHashtable.Add((object) instanceGenerator, (object) new CategoryPropsHolder.CategoryPropsObject(category, iCategoryProps));
    return instanceGenerator;
  }

  /// <summary>разрегистрировать подписчика</summary>
  /// <param name="categoryPropsId"></param>
  public static void UnregisterCategoryProps(int categoryPropsId)
  {
    CategoryPropsHolder.categoryPropsHashtable.Remove((object) categoryPropsId);
  }

  public static ICategoryProps[] GetRegisteredCategoryProps(int category)
  {
    ArrayList arrayList = new ArrayList();
    foreach (CategoryPropsHolder.CategoryPropsObject categoryPropsObject in (IEnumerable) CategoryPropsHolder.categoryPropsHashtable.Values)
    {
      if (categoryPropsObject.Category == category)
        arrayList.Add((object) categoryPropsObject.ICategoryProps);
    }
    return (ICategoryProps[]) arrayList.ToArray(typeof (ICategoryProps));
  }

  /// <summary>вернуть список полей со значениями для категории и id</summary>
  /// <param name="pdh">объект, назначаемый в SelectedObject для PropertyGrid, хранитель списка propDescriptor'ов (от ITypeDescriptor)</param>
  /// <param name="category"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  public static PropDescriptor[] GetPropDescriptors(
    PropDescriptorHolder pdh,
    int category,
    object id)
  {
    ArrayList arrayList = new ArrayList();
    foreach (ICategoryProps registeredCategoryProp in CategoryPropsHolder.GetRegisteredCategoryProps(category))
    {
      int count = pdh.PropDescriptorCollection.Count;
      PropDescriptor[] propDescriptors = registeredCategoryProp.GetPropDescriptors(pdh, category, id);
      if (pdh.PropDescriptorCollection.Count != count)
        throw new KernelException($"Подписчик \"{registeredCategoryProp.SubscriberID}\" выполнил недопустимые действия над списком полей: изменение списка");
      if (propDescriptors != null)
      {
        for (int index = 0; index < propDescriptors.Length; ++index)
        {
          if (propDescriptors[index].PropID < ClientConsts.UserPropertyDescriptorID)
            propDescriptors[index].SetPropID(CategoryPropsHolder.NextPropertyDescriptorID);
          propDescriptors[index].Component = (object) pdh;
          arrayList.Add((object) propDescriptors[index]);
        }
      }
    }
    return (PropDescriptor[]) arrayList.ToArray(typeof (PropDescriptor));
  }

  /// <summary>применить</summary>
  /// <param name="pdh"></param>
  /// <param name="category"></param>
  /// <param name="id"></param>
  /// <param name="idOld"></param>
  public static void Apply(PropDescriptorHolder pdh, int category, object id, object idOld)
  {
    foreach (ICategoryProps registeredCategoryProp in CategoryPropsHolder.GetRegisteredCategoryProps(category))
    {
      try
      {
        registeredCategoryProp.Apply(pdh, category, id, idOld);
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(new Exception(string.Format(LocalizationHolder.rm.GetString("Client.Core_230") + ex.Message, (object) category, id, (object) registeredCategoryProp.SubscriberID), ex));
      }
    }
  }

  /// <summary>отменить</summary>
  /// <param name="pdh"></param>
  /// <param name="category"></param>
  /// <param name="id"></param>
  public static void Cancel(PropDescriptorHolder pdh, int category, object id)
  {
    foreach (ICategoryProps registeredCategoryProp in CategoryPropsHolder.GetRegisteredCategoryProps(category))
    {
      try
      {
        registeredCategoryProp.Cancel(pdh, category, id);
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(new Exception(string.Format(LocalizationHolder.rm.GetString("Client.Core_231") + ex.Message, (object) category, id, (object) registeredCategoryProp.SubscriberID), ex));
      }
    }
  }

  /// <summary>известить об изменении</summary>
  /// <param name="pdh"></param>
  /// <param name="category"></param>
  /// <param name="id"></param>
  /// <param name="e"></param>
  public static void ChangeEventData(
    PropDescriptorHolder pdh,
    int category,
    object id,
    EventArgs e)
  {
    foreach (ICategoryProps registeredCategoryProp in CategoryPropsHolder.GetRegisteredCategoryProps(category))
    {
      try
      {
        registeredCategoryProp.ChangeEventData(pdh, category, id, e);
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(new Exception(string.Format(LocalizationHolder.rm.GetString("Client.Core_232") + ex.Message, (object) category, id, (object) registeredCategoryProp.SubscriberID), ex));
      }
    }
  }

  private class CategoryPropsObject
  {
    private int category;
    private ICategoryProps iCategoryProps;

    public int Category => this.category;

    public ICategoryProps ICategoryProps => this.iCategoryProps;

    public CategoryPropsObject(int category, ICategoryProps iCategoryProps)
    {
      this.category = category;
      this.iCategoryProps = iCategoryProps;
    }
  }
}
