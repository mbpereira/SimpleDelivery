using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class OrdersInProgressFunctionCreation : Migration
    {
		public static string GetFunctionScript() => @"
			drop function if exists ""OrdersInProgress"";
			create or replace function ""OrdersInProgress""
			(
				""from"" timestamp without time zone,
				""to"" timestamp without time zone
			)
			returns table (
				""Status"" int,
				""Description"" varchar(255),
				""Quantity"" bigint
			)
			language 'plpgsql'
			as $$
			begin
				return query
					with cte as (
						select	o.""Status"",
							cast('' as varchar(255)) as ""Description"",
							count(o.*) as ""Quantity""
						from	""Orders"" o
						where	o.""Date"" between ""from"" and ""to""
						group by o.""Status""
					)
					select	*
					from	cte
					order by
						""Quantity"" desc;
			end
			$$;";

		protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(GetFunctionScript());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"drop function if exists ""OrdersInProgress""");
        }
    }
}
