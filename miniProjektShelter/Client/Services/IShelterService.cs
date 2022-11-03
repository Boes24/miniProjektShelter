using System;
using System.Net.Http;
using miniProjektShelter.Shared;


namespace miniProjektShelter.Client.Services{

    public interface IShelterService{

        Task<Shelter[]?> GetAllItems();
        Task<CustomerBooking[]?> GetAllBookings();

        Task<int> AddItem(CustomerBooking costumerInfo);
        Task<int> DeleteItem(string id);
        Task<Shelter> GetItem(string id);
        Task<int> UpdateItem(CustomerBooking item);

    }
}

