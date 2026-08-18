
// Type: Intermech.PropertyEditors.StatesController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for StatesController.</summary>
public class StatesController
{
  private static ArrayList _ObjectList = new ArrayList();
  private static ArrayList _LoadedList = new ArrayList();
  private static ArrayList _ModifiedList = new ArrayList();

  public static ArrayList ObjectList => StatesController._ObjectList;

  public static ArrayList LoadedList => StatesController._LoadedList;

  public static ArrayList ModifiedList => StatesController._ModifiedList;

  public static void Clear()
  {
    StatesController._ObjectList.Clear();
    StatesController._LoadedList.Clear();
    StatesController._ModifiedList.Clear();
  }

  public static void Add(object aObject, bool aLoadState, bool aModifiedState)
  {
    StatesController._ObjectList.Add(aObject);
    StatesController._LoadedList.Add((object) aLoadState);
    StatesController._ModifiedList.Add((object) aModifiedState);
  }

  public static int IndexOf(object aObject) => StatesController._ObjectList.IndexOf(aObject);

  public static bool GetLoadState(object aObject)
  {
    int index = StatesController.IndexOf(aObject);
    return index != -1 && (bool) StatesController._LoadedList[index];
  }

  public static void SetLoadState(object aObject, bool aState)
  {
    int index = StatesController.IndexOf(aObject);
    if (index == -1)
      return;
    StatesController._LoadedList[index] = (object) aState;
  }

  public static bool GetModifiedState(object aObject)
  {
    int index = StatesController.IndexOf(aObject);
    return index != -1 && (bool) StatesController._ModifiedList[index];
  }

  public static void SetModifiedState(object aObject, bool aState)
  {
    int index = StatesController.IndexOf(aObject);
    if (index == -1)
      return;
    StatesController._ModifiedList[index] = (object) aState;
  }
}
