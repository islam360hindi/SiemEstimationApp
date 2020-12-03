using System;
using System.Threading.Tasks;
using EstimationApp.Enums;

namespace EstimationApp.DataAccess
{
    public interface IPriceEstimationDataAccess
    {
        Task<double> CalculatePrice(double unitPrice, double weightInGram, float discount = default);

        Task<float> GetDiscountPercentage(UserType userType);
    }
}
