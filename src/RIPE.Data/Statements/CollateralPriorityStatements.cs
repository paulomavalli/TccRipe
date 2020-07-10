namespace RIPE.Data.Statements
{
    public static class CollateralPriorityStatements
    {
        public const string GET_COLLATERAL_PRIORITY = @"
        select 
             product_type_id as productTypeId,
             product_type_description  as productTypeDescription,
	         priority_scale as priorityScale, 
             priority_scale_description    as priorityScaleDescription
        from 
	        collateral.collateral_priority
        order by 
            priority_scale";
    }
}
