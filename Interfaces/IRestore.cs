using CWDM.Enums;

namespace CWDM.Interfaces
{
    public interface IRestore
    {
        RestoreType RestoreType { get; set; }

        float Restore { get; set; }
    }
}