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

        public async Task<double> CalculatePrice(string unitPrice)
        {
            double toReturn = 0;
            await Task.Delay(1000); //Mocking Api call
            if (string.IsNullOrEmpty(unitPrice))
                throw new ArgumentException("Unit price not be null or empty");
            var commaSaperatedPrices = unitPrice.Split(',');
            if (commaSaperatedPrices.Length < 2)
                throw new ArgumentException("Unit price must have atleast two comma saperated values");
            foreach (var p in commaSaperatedPrices)
            {
                if (!double.TryParse(p, out double n))
                {
                    throw new ArgumentException("comma saperated values must contain only numbers");
                }
                else
                {
                    toReturn += n;
                }
            }
            return toReturn;
        }
    }
}
