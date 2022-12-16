using GameShop.ViewModels.Catalog.Charts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Charts
{
    public interface IChartService
    {
        Task<List<GameBuyCountModel>> GameStatisticalByMonthAndYear(int Year, int Month);
    }
}