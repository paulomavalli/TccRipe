using System.ComponentModel;

namespace RIPE.Domain.Enums
{
    public enum ProductType
    {
        [Description("TD")]
        TD = 1,
        [Description("CDB")]
        CDB = 2,
        [Description("LC")]
        LC = 3,
        [Description("LCI")]
        LCI = 4,
        [Description("LCA")]
        LCA = 5,
        [Description("LF")]
        LF = 6,
        [Description("FUNDOS")]
        FUNDOS = 7
    }

}
