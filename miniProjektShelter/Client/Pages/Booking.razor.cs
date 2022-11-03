using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using miniProjektShelter.Client.Services;
using miniProjektShelter.Shared;

namespace miniProjektShelter.Client.Pages
{
    public partial class Booking
    {

        private List<Shelter> SheltersList = new List<Shelter>();
        private List<CustomerBooking> BookingsList = new List<CustomerBooking>();
        private List<string> MunicipalList = new List<string>();
        private List<Shelter> SheltersToShow = new List<Shelter>();
        string selectedString = "Alle kommuner";
        int selectedAntalPersoner = 1;
        string valgtShelterString = "";
        Shelter currentShelter = new Shelter();
        bool bookingButtonHidden = false;
        string modalHidden = "none";
        string bookingConfirmationHidden = "none";
        DateTime chosenDate = new DateTime();
        int BookingDate1 = 0;
        int BookingDate2 = -1;
        bool IsDataLoaded = false;

        int overnatninger2Stk = 1;


        public int ChangeShelter()
        {
            SheltersToShow.Clear();

            if (selectedString == "Alle kommuner")
            {
                foreach (Shelter shelterX in SheltersList)
                {
                    if (shelterX.Properties.FacilityType == "Shelter")
                    {
                        SheltersToShow.Add(shelterX);
                    }

                }
            }
            else
            {
                foreach (Shelter shelter in SheltersList) //Hver Shelter shelter tilføjes til listen SheltersToShow, derefter fjernes overflødelige baseret på en liste af parametre. 
                {
                    if (shelter.Properties.MunicipalName == selectedString && shelter.Properties.FacilityType == "Shelter" && shelter.Properties.Capacity >= selectedAntalPersoner)
                    {
                        SheltersToShow.Add(shelter);
                        BookingDate1 = chosenDate.Year * 10000 + chosenDate.Month * 100 + chosenDate.Day;
                        DateTime tmpDate = chosenDate.AddDays(1);

                        foreach (CustomerBooking tmpBooking in BookingsList)
                        {

                            if (tmpBooking.BookedDate1 == BookingDate1 && tmpBooking.BookedShelterID == shelter.MongoId || tmpBooking.BookedDate2 == BookingDate1 && tmpBooking.BookedShelterID == shelter.MongoId)
                            {
                                TryRemoveShelter(shelter);
                            }
                            else if (overnatninger2Stk == 2)
                            {
                                BookingDate2 = tmpDate.Year * 10000 + tmpDate.Month * 100 + tmpDate.Day;
                                if (tmpBooking.BookedDate1 == BookingDate2 && tmpBooking.BookedShelterID == shelter.MongoId || tmpBooking.BookedDate2 == BookingDate2 && tmpBooking.BookedShelterID == shelter.MongoId)
                                {
                                    TryRemoveShelter(shelter);
                                }
                            }
                        }
                    }
                }
            }


            return 1;
        }

        [Inject]
        public IShelterService? Service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SheltersList = (await Service!.GetAllItems())!.ToList();
            BookingsList = (await Service!.GetAllBookings())!.ToList();

            MunicipalList.Add("Alle kommuner");

            foreach (Shelter shelterX in SheltersList)
            {
                if (!MunicipalList.Contains(shelterX.Properties.MunicipalName!))
                {
                    MunicipalList.Add(shelterX.Properties.MunicipalName!);
                }
            }
            IsDataLoaded = true;

        }

        // 
        protected override void OnInitialized()
        {
            base.OnInitialized();
            chosenDate = GetCurrentDate();
            EditContext = new EditContext(CustomerBookingValidation); // Opretter ny editcontext til validering af brugerinfo


        }


        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        public void TryRemoveShelter(Shelter shelter)
        {
            try
            {
                Console.WriteLine("Remove:" + shelter.Properties.ShelterName);
                SheltersToShow.RemoveAt(SheltersToShow.Count - 1);
            }
            catch
            {
                Shelter NoAvailableSheltersShelter = new Shelter(navn: "Ingen shelters passer til dine valg");
                SheltersToShow.Add(NoAvailableSheltersShelter);
                bookingButtonHidden = true;
            }
        }

        public void bookShelter(Shelter valgtShelter)
        {
            EditContext = new EditContext(CustomerBookingValidation); // Opretter ny editcontext til validering af brugerinfo
            modalHidden = "block";
            bookingButtonHidden = true;
            valgtShelterString = valgtShelter.Properties.ShelterName!;
            currentShelter = valgtShelter;
            bookingConfirmationHidden = "none";
        }

        public void AbortBooking()
        {
            modalHidden = "none";
            bookingButtonHidden = false;
            bookingConfirmationHidden = "block";
        }

        // Validation handler
        private CustomerBooking CustomerBookingValidation = new CustomerBooking();
        private EditContext? EditContext;
        private List<CustomerBooking> ValidationList = new List<CustomerBooking>();

        private void HandleValidSubmit()
        {
            Console.WriteLine("HandleValidSubmit Called...");
            ValidationList.Add(CustomerBookingValidation);

            //Konverterer DateTime formater til int
            //2022-11-02 => 20221102    2022*10000 = 20220000 + 11*100 = 1100 + 02
            BookingDate1 = chosenDate.Year * 10000 + chosenDate.Month * 100 + chosenDate.Day;
            if (overnatninger2Stk == 2)
            {
                DateTime tmpDate = chosenDate.AddDays(1);
                BookingDate2 = tmpDate.Year * 10000 + tmpDate.Month * 100 + tmpDate.Day;
            }

            if (BookingDate2 == -1)
            {
                BookingDate2 = 0;
            }

            CustomerBooking tmpBooking = new CustomerBooking(CustomerBookingValidation.CustomerName!, CustomerBookingValidation.CustomerEmail!, CustomerBookingValidation.CustomerPhoneNumber, BookingDate1, BookingDate2, currentShelter.MongoId!);
            Service.AddItem(tmpBooking);
            bookingButtonHidden = false;
            modalHidden = "none";
            bookingConfirmationHidden = "block";
        }

        private void HandleInvalidSubmit()
        {
            Console.WriteLine("HandleInvalidSubmit Called...");
        }
    }
}

