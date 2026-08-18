
// Type: Intermech.Interfaces.WebPortal.TaskStatusHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.WebPortal
{
    public static class TaskStatusHelper
    {
      public static bool StatusInWork(TaskStatus status)
      {
        object[] customAttributes = typeof (TaskStatus).GetField(status.ToString()).GetCustomAttributes(typeof (Intermech.Interfaces.WebPortal.StatusInWork), false);
        return customAttributes != null && customAttributes.Length != 0 && ((Intermech.Interfaces.WebPortal.StatusInWork) customAttributes[0]).InWork;
      }
    }
}
