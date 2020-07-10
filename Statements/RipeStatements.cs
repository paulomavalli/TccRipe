namespace RIPE.Data.Statements
{
    public static class RipeStatements
    {
        public const string GET_COLLATERAL = @"
               select 
	                collateral_id as collateralId, 
                    consumer_id as consumerId,
                    consumer_type_id as consumerTypeId,
                    security_id as securityId,
                    security_type as securityType,
                    quantity
                from 
	                collateral.collateral 
                where
                     is_active = 1
                order by 
	                collateral_id desc;";


        public const string GET_COLLATERAL_PER_SECURITY_ID = @"
               select 
	                collateral_id as collateralId, 
                    consumer_id as consumerId,
                    consumer_type_id as consumerTypeId,
                    security_id as securityId,
                    security_type as securityType,
                    quantity
                from 
	                collateral.collateral 
                where
                    security_id = @securityId
                    and customer_id = @customerId
                    and is_active = 1
                order by 
	                collateral_id desc;";

        public const string WRITE_USER = @"
            INSERT INTO `ripe`.`users`
                        (`login`,
                        `senha`,
                        `request_date`)
                        VALUES
                        (@login,
                        @password,
                        @requestDate)";
        public const string WRITE_FEEDBACK = @"
            INSERT INTO loan_feedback (
                customer_id, 
                origin, 
                customer_feedback, 
                suggested_limit
            ) 
            VALUES (
                @CustomerId, 
                @FeedbackOrigin, 
                @CustomerFeedback, 
                @SuggestedLimit
            )";
    }
}
