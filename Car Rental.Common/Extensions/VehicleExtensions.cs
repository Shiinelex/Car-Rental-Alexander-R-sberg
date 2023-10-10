namespace Car_Rental.Common.Extensions;

public static class VehicleExtensions
{
    public static double Duration(this DateTime startDate, DateTime endDate)
    {
        var totalDays = (endDate - startDate).TotalDays <= 0 ? 1 : (endDate - startDate).TotalDays;
        return totalDays;
    }

    public static double? TravelDistance(this double? pointEnd, double? pointStart)
    {
        var totalDistance = (pointEnd - pointStart);
        return totalDistance;
    }
}
