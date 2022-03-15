drop function if exists "ProductsSold";
create or replace function "ProductsSold"
(
	"from" timestamp without time zone,
	"to" timestamp without time zone
)
returns table (
	"Id" int,
	"Product" varchar(255),
	"Quantity" decimal(18,2)
)
language 'plpgsql'
as $$
begin
	return query
		with cte as (
			select	p."Id",
				p."Description" as "Product",
				sum(i."Quantity") as "Quantity"
			from	"OrderItems" i
				inner join "Products" p on
					p."Id" = i."ProductId"
				inner join "Orders" o on
					o."Id" = i."OrderId"
			where	o."Date" between "from" and "to"
			and	o."Status" in (2, 3, 4) --2=Approved,3=Preparing,4=Delivered
			group by p."Id", p."Description"
		)
		select	*
		from	cte
		order by
			"Quantity" desc;
end
$$;