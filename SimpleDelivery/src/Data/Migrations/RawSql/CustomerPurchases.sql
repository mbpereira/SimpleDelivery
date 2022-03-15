drop function if exists "CustomerPurchases";
create or replace function "CustomerPurchases"
(
	"from" timestamp without time zone,
	"to" timestamp without time zone
)
returns table (
	"Id" int,
	"Customer" varchar(255),
	"Quantity" bigint
)
language 'plpgsql'
as $$
begin
	return query
		with cte as (
			select	c."Id",
				c."Name" as "Customer",
				count(o.*) as "Quantity"
			from	"Orders" o
				inner join "Customers" c on
					c."Id" = o."CustomerId"
			where	o."Date" between "from" and "to"
			and	o."Status" in (2, 3, 4) --Approved,Preparing,Delivered
			group by c."Id", c."Name"			
		) 
		select	*
		from	cte
		order by
			"Quantity" desc;
end
$$;