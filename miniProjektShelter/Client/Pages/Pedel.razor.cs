using System;
using Microsoft.AspNetCore.Components;
using miniProjektShelter.Client.Services;
using miniProjektShelter.Shared;

namespace miniProjektShelter.Client.Pages
{
    public partial class Pedel
    {
        private List<Shelter> SheltersList = new List<Shelter>();
        private List<CostumerBooking> BookingerList = new List<CostumerBooking>();
        private List<string> KommuneList = new List<string>();
        private List<Shelter> SheltersToShow = new List<Shelter>();

        private List<CostumerBooking> bookingsToShow = new List<CostumerBooking>();

        private string chosenKommune = "Samsø Kommune";


        [Inject]
        public IShelterService? Service { get; set; }
        protected override async Task OnInitializedAsync()
        {
            SheltersList = (await Service!.GetAllItems())!.ToList();
            BookingerList = (await Service!.GetAllBookings())!.ToList();

            foreach (Shelter shelterX in SheltersList)
            {
                if (!KommuneList.Contains(shelterX.Properties.KommuneNavn!))
                {
                    KommuneList.Add(shelterX.Properties.KommuneNavn!);
                }
            }



        }


        public int changeMunicipality()
        {

            SheltersToShow.Clear();
            foreach (Shelter shelterX in SheltersList)
            {
                if (shelterX.Properties.KommuneNavn == chosenKommune && shelterX.Properties.Facil_ty == "Shelter")
                {
                    SheltersToShow.Add(shelterX);
                }
            }
            bookingsToShow.Clear();

            foreach (CostumerBooking bookingX in BookingerList)
            {
                foreach (Shelter shelterX in SheltersToShow)
                {
                    if (shelterX.MongoId == bookingX.ShelterID)
                    {
                        bookingsToShow.Add(bookingX);
                    }
                }
            }
            return 1;

        }

    }
}

