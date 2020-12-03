using System;
using System.Threading.Tasks;
using EstimationApp.Enums;

namespace EstimationApp.DataAccess
{
    public class PriceEstimationDataAccess : IPriceEstimationDataAccess
    {
        public async Task<double> CalculatePrice(double unitPrice, double weightInGram, float discount)
        {
            await Task.Delay(1000); //Mocking Api call
            var totalPrice = unitPrice * weightInGram;
            if (discount > 0)
                totalPrice = totalPrice - totalPrice * (discount / 100);
            return totalPrice;
        }

        public async Task<float> GetDiscountPercentage(UserType userType)
        {
            await Task.Delay(500);
            return userType == UserType.PrivilegedUser ? 2 : 0;
        }
    }
}
