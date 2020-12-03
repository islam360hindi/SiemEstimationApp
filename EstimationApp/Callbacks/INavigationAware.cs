using System;
using System.Threading.Tasks;

namespace EstimationApp.Callbacks
{
    public interface INavigationAware
    {
        Task NavigatedToAsync<T>(T parameters = default);

        Task NavigatedBackAsync<T>(T parameters = default);
    }
}
