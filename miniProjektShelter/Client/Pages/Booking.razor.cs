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
        private List<CostumerBooking> BookingerList = new List<CostumerBooking>();
        private List<string> KommuneList = new List<string>();
        private List<Shelter> SheltersToShow = new List<Shelter>();
        string selectedString = "Samsø Kommune";
        int selectedAntalPersoner = 1;
        string valgtShalterX = "";
        Shelter currentShelter = new Shelter();
        bool hide1 = false;
        string modalHidden = "none";
        DateTime chosenDate = new DateTime();
        int date1 = 0;
        int date2 = -1;

        int overnatninger2Stk = 1;


        public int changeShelter()
        {
            SheltersToShow.Clear();

            if (selectedString == "Alle kommuner")
            {
                foreach (Shelter shelterX in SheltersList)
                {
                    if (shelterX.Properties.Facil_ty == "Shelter")
                    {
                        SheltersToShow.Add(shelterX);
                    }

                }
            }
            else
            {
                foreach (Shelter shelterX in SheltersList)
                {
                    if (shelterX.Properties.KommuneNavn == selectedString && shelterX.Properties.Facil_ty == "Shelter" && shelterX.Properties.Antal_pl >= selectedAntalPersoner)
                    {
                        SheltersToShow.Add(shelterX);
                        date1 = chosenDate.Year * 10000 + chosenDate.Month * 100 + chosenDate.Day;
                        DateTime tmpDate = chosenDate.AddDays(1);


                        foreach (CostumerBooking tmpBooking in BookingerList)
                        {

                            if (tmpBooking.Date1 == date1 && tmpBooking.ShelterID == shelterX.MongoId || tmpBooking.Date2 == date1 && tmpBooking.ShelterID == shelterX.MongoId)
                            {
                                Console.WriteLine("Remove:" + shelterX.Properties.Navn);
                                SheltersToShow.RemoveAt(SheltersToShow.Count - 1);

                            }
                            else if (overnatninger2Stk == 2)
                            {
                                date2 = tmpDate.Year * 10000 + tmpDate.Month * 100 + tmpDate.Day;
                                if (tmpBooking.Date1 == date2 && tmpBooking.ShelterID == shelterX.MongoId || tmpBooking.Date2 == date2 && tmpBooking.ShelterID == shelterX.MongoId)
                                {
                                    Console.WriteLine("Remove:" + shelterX.Properties.Navn);
                                    SheltersToShow.RemoveAt(SheltersToShow.Count - 1);
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
            BookingerList = (await Service!.GetAllBookings())!.ToList();

            KommuneList.Add("Alle kommuner");

            foreach (Shelter shelterX in SheltersList)
            {
                if (!KommuneList.Contains(shelterX.Properties.KommuneNavn!))
                {
                    KommuneList.Add(shelterX.Properties.KommuneNavn!);
                }
            }


        }

        // 
        protected override void OnInitialized()
        {
            base.OnInitialized();
            chosenDate = getCurrentDate();
            EditContext = new EditContext(CustomerBookingValidation); // Opretter ny editcontext til validering af brugerinfo

        }


        public DateTime getCurrentDate()
        {
            return DateTime.Now;
        }

        public void bookShelter(Shelter valgtShelter)
        {
            EditContext = new EditContext(CustomerBookingValidation); // Opretter ny editcontext til validering af brugerinfo
            modalHidden = "block";
            hide1 = true;
            valgtShalterX = valgtShelter.Properties.Navn!;
            currentShelter = valgtShelter;
           
        }

        public void Fortryd()
        {
            modalHidden = "none";
            hide1 = false;
        }
       

        // Validation handler
        private CostumerBooking CustomerBookingValidation = new CostumerBooking();
        private EditContext? EditContext;
        private List<CostumerBooking> ValidationList = new List<CostumerBooking>();


        private void HandleValidSubmit()
        {
            Console.WriteLine("HandleValidSubmit Called...");
            ValidationList.Add(CustomerBookingValidation);

            //Konvetere dateformatere til int
            //2022-11-02 => 20221102    2022*10000 = 20220000 + 11*100 = 1100 + 02
            date1 = chosenDate.Year * 10000 + chosenDate.Month * 100 + chosenDate.Day;
            if (overnatninger2Stk == 2)
            {
                DateTime tmpDate = chosenDate.AddDays(1);
                date2 = tmpDate.Year * 10000 + tmpDate.Month * 100 + tmpDate.Day;
            }

            if (date2 == -1)
            {
                date2 = 0;
            }

            CostumerBooking tmpBooking = new CostumerBooking(CustomerBookingValidation.CostumerName, CustomerBookingValidation.Email, CustomerBookingValidation.PhoneNumber, date1, date2, currentShelter.MongoId!);
            Service.AddItem(tmpBooking);


        }

        private void HandleInvalidSubmit()
        {
            Console.WriteLine("HandleInvalidSubmit Called...");
        }






    }
}

