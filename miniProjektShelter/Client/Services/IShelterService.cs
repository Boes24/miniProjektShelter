using System;
using System.Net.Http;
using miniProjektShelter.Shared;


namespace miniProjektShelter.Client.Services{

    public interface IShelterService{

        Task<Shelter[]?> GetAllItems();
        Task<CostumerBooking[]?> GetAllBookings();

        Task<int> AddItem(CostumerBooking costumerInfo);
        Task<int> DeleteItem(string id);
        Task<Shelter> GetItem(string id);
        Task<int> UpdateItem(CostumerBooking item);

    }
}

