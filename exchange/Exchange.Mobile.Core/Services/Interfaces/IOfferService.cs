﻿using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IOfferService
    {
        Task<IEnumerable<Offer>> ShowOfferAsync(FilterRequestModel model = null);
        Task<IEnumerable<OfferCategory>> GetOfferCategoryAsync();
        Task<string> UploadOfferAsync(UploadOfferRequestModel model);
    }
}
