using System.ComponentModel;


namespace Intermech.Map
{
    /// <summary>стили рамки</summary>
    public enum RectangleStyle
    {
      /// <summary>рамка без фасок</summary>
      [Description("рамка без фасок")] Box,
      /// <summary>рамка с фаской</summary>
      [Description("рамка с фаской")] BoxFacet,
      /// <summary>рамка с закруглением</summary>
      [Description("рамка с закруглением")] BoxBluntPoint,
    }
}
