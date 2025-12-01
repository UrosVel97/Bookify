using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Bookings.GetBooking
{
    public sealed class GetBookingQueryHandler : IQuerryHandler<GetBookingQuery, BookingResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetBookingQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT 
                    id AS Id,
                    apartment_id AS ApartmentId,
                    user_id AS UserId,
                    status AS Status,
                    price_for_period_amount AS PriceAmount,
                    price_for_period_currency AS PriceCurrency,
                    cleaning_fee_amount AS CleaningFeeAmount,
                    cleaning_fee_currency AS CleaningFeeCurrency,
                    amenities_upcharge_amount AS AmenititesUpChargeAmount,
                    amenities_upcharge_currency AS AmenititesUpChargeCurrency,
                    total_price_amount AS TotalPriceAmount,
                    total_price_currency AS TotalPriceCurrency,
                    duration_start AS DurrationStart,
                    duration_end AS DurrationEnd,
                    created_on_utc AS CreatedOnUtc
                FROM bookings
                WHERE id = @BookingId
                """;

            var booking = await connection.QueryFirstOrDefaultAsync<BookingResponse>(
                sql,
                new
                {
                    request.BookingId
                });
            return booking;
        }
    }
}
