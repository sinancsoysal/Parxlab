using System;
using Parxlab.Entities.Enums;

namespace Parxlab.Service.Contracts.Impl
{
    public class ParkingCalculator:IParkingCalculator
    {
        public double Price(TimeSpan tp, ParkType type)
        {
            var tax = 4.5;
            return tp.Minutes * 10 + tax;
        }
    }
}
