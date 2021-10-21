using System;
using Parxlab.Entities.Enums;

namespace Parxlab.Service.Contracts
{
    public interface IParkingCalculator
    {
        double Price(TimeSpan tp, ParkType type);
    }
}
