using Bookify.Application.Abstractions.Clock;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Bookings.ReservedBooking;

internal sealed class ReserveBookingCommandHandler
{
    private readonly IUserReporsitory _userReporsitory;
    private readonly IApartmentReporsitory _apartmentReporsitory;
    private readonly IBookingRepository _bookingReporsitory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PricingService _pricingService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReserveBookingCommandHandler(
        IUserReporsitory userReporsitory,
        IApartmentReporsitory apartmentReporsitory,
        IBookingRepository bookingReporsitory,
        IUnitOfWork unitOfWork,
        PricingService pricingService,
        IDateTimeProvider dateTimeProvider)
    {
        _userReporsitory = userReporsitory;
        _apartmentReporsitory = apartmentReporsitory;
        _bookingReporsitory = bookingReporsitory;
        _unitOfWork = unitOfWork;
        _pricingService = pricingService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await _userReporsitory.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var apartment = await _apartmentReporsitory.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment is null)
        {
            return Result.Failure<Guid>(ApartmentErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if(await _bookingReporsitory.IsOverlappingAsync(apartment,duration, cancellationToken))
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }

        var booking = Booking.Reserve(
            apartment,
            user.Id,
            duration,
            _dateTimeProvider.UtcNow,
            _pricingService);

        _bookingReporsitory.Add(booking);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return booking.Id;




    }

}