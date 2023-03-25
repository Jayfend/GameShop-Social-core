using GameShop.ViewModels.Catalog.Charts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Charts
{
    public interface IChartService
    {
        Task<List<GameBuyCountModel>> GameStatisticalByMonthAndYear(int Year, int Month, int take);

        Task<List<GameBuyCountModel>> GameStatisticalByMonthAndYearSortbyTotal(int Year, int Month, int take);

        Task<decimal> TotalProfit();

        Task<List<GameBuyCountModel>> GameTotalPurchased();
    }
}