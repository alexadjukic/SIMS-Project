﻿using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    internal interface IAccommodationRatingRepository
    {
        public List<AccommodationRating> GetAll();
        public AccommodationRating Save(int cleanliness, int correctness, string imageUrl, string comment, int reservationId, int ownerId, int raterId);
        public int NextId();
        AccommodationRating FindByReservationId(int reservationId);
    }
}