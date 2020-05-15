﻿using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IOfferService
    {
        Task<Offer> ShowOfferAsync(FilterRequestModel model = null);
    }
}
