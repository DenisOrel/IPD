using System.ComponentModel;


namespace Intermech.Map
{
    public enum MapViewSnapStyle
    {
      [Description("Нет")] None,
      [Description("Выравнивать")] Jump,
      [Description("После перетаскивания")] After,
    }
}
