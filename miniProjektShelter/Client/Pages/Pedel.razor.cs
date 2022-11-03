using System;
using Microsoft.AspNetCore.Components;
using miniProjektShelter.Client.Services;
using miniProjektShelter.Shared;

namespace miniProjektShelter.Client.Pages
{
    public partial class Pedel
    {
        private List<Shelter> SheltersList = new List<Shelter>();
        private List<CustomerBooking> BookingerList = new List<CustomerBooking>();
        private List<string> MunicipalList = new List<string>();
        private List<Shelter> SheltersToShow = new List<Shelter>();

        private List<CustomerBooking> bookingsToShow = new List<CustomerBooking>();

        private string chosenMunicipal = "Samsø Kommune";


        [Inject]
        public IShelterService? Service { get; set; }
        protected override async Task OnInitializedAsync()
        {
            SheltersList = (await Service!.GetAllItems())!.ToList();
            BookingerList = (await Service!.GetAllBookings())!.ToList();

            foreach (Shelter shelterX in SheltersList)
            {
                if (!MunicipalList.Contains(shelterX.Properties.MunicipalName!))
                {
                    MunicipalList.Add(shelterX.Properties.MunicipalName!);
                }
            }



        }


        public int changeMunicipality()
        {

            SheltersToShow.Clear();
            foreach (Shelter shelterX in SheltersList)
            {
                if (shelterX.Properties.MunicipalName == chosenMunicipal && shelterX.Properties.FacilityType == "Shelter")
                {
                    SheltersToShow.Add(shelterX);
                }
            }
            bookingsToShow.Clear();

            foreach (CustomerBooking bookingX in BookingerList)
            {
                foreach (Shelter shelterX in SheltersToShow)
                {
                    if (shelterX.MongoId == bookingX.BookedShelterID)
                    {
                        bookingsToShow.Add(bookingX);
                    }
                }
            }
            return 1;

        }

    }
}

