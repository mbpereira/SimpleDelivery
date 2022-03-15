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
		select	p."Id",
			p."Description" as "Product",
			sum(i."Quantity") as "Quantity"
		from	"OrderItems" i
			inner join "Products" p on
				p."Id" = i."ProductId"
		group by p."Id", p."Description";
end
$$;