using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class TourRequestStatisticsService
    {
        public TourRequestService _tourRequestService;
        public TourRequestStatisticsService()
        {
            _tourRequestService = new TourRequestService();
        }

        public IEnumerable<int> GetAllYearsWithRequests()
        {
            return _tourRequestService.GetAll().Select(r => r.RequestArrivalDate.Year).ToHashSet();
        }

        public int GetNumberOfRequestsForYear(int year)
        {
            return _tourRequestService.GetAll().Where(r => r.RequestArrivalDate.Year == year).Count();
        }

        public int GetNumberOfRequestsForMonth(int month, int year)
        {
            return _tourRequestService.GetAll().Where(r => r.RequestArrivalDate.Year == year && r.RequestArrivalDate.Month == month).Count();
        }

        public IEnumerable<int> FilterYearsWithRequests(string country, string city, string language)
        {
            return _tourRequestService.GetAll().Where(r => r.Location.Country == country && r.Location.City == city && r.Language == language).Select(r => r.RequestArrivalDate.Year).ToHashSet();
        }

        public int FilterNumberOfRequestsForYear(int year, string country, string city, string language)
        {
            return _tourRequestService.GetAll().Where(r => r.RequestArrivalDate.Year == year && r.Location.Country == country && r.Location.City == city && r.Language == language).Count();
        }

        public int FilterNumberOfRequestsForMonth(int month, int year, string country, string city, string language)
        {
            return _tourRequestService.GetAll().Where(r => r.RequestArrivalDate.Year == year && r.RequestArrivalDate.Month == month && r.Location.Country == country && r.Location.City == city && r.Language == language).Count();
        }
    }
}
