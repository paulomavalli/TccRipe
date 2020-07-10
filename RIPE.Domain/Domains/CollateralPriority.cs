namespace RIPE.Domain.Domains
{
    public class CollateralPriority
    {
        public CollateralPriority(string productTypeId, string productTypeDescription, int priorityScale, string priorityScaleDescription)
        {
            ProductTypeId = productTypeId;
            ProductTypeDescription = productTypeDescription;
            PriorityScale = priorityScale;
            PriorityScaleDescription = priorityScaleDescription;
        }

        public string ProductTypeId { get; set; }
        public string ProductTypeDescription { get; set; }
        public int PriorityScale { get; set; }
        public string PriorityScaleDescription { get; set; }

    }

}
