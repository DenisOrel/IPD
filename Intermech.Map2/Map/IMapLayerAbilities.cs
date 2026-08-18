namespace Intermech.Map
{
    public interface IMapLayerAbilities
    {
      bool CanCopyObjects();

      bool CanDeleteObjects();

      bool CanEditObjects();

      bool CanInsertObjects();

      bool CanLinkObjects();

      bool CanMoveObjects();

      bool CanReshapeObjects();

      bool CanResizeObjects();

      bool CanSelectObjects();

      void SetModifiable(bool b);

      bool AllowCopy { get; set; }

      bool AllowDelete { get; set; }

      bool AllowEdit { get; set; }

      bool AllowInsert { get; set; }

      bool AllowLink { get; set; }

      bool AllowMove { get; set; }

      bool AllowReshape { get; set; }

      bool AllowResize { get; set; }

      bool AllowSelect { get; set; }
    }
}
