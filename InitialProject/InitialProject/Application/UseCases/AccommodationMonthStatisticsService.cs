﻿using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class AccommodationMonthStatisticsService
    {
        private readonly IAccommodationMonthStatisticsRepository _accommodationMonthStatisticsRepository;

        public AccommodationMonthStatisticsService()
        {
            _accommodationMonthStatisticsRepository = Injector.CreateInstance<IAccommodationMonthStatisticsRepository>();
        }

        public AccommodationMonthStatistics Save(int month, AccommodationYearStatistic yearStatistic, int yearStatisticId, int numberOfReservations, int numberOfDeclinedReservations, int numberOfChangedReservations, int numberOfRenovationSuggestions)
        {
            return _accommodationMonthStatisticsRepository.Save(month, yearStatistic, yearStatisticId, numberOfReservations, numberOfDeclinedReservations, numberOfChangedReservations, numberOfRenovationSuggestions);
        }
    }
}
