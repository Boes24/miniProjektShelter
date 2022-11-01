using System;
namespace miniProjektShelter.Shared
{

    public class Booking
    {

        String MongoID { get; set; }
        String CostumerName { get; set; }
        String Email { get; set; }
        int PhoneNumber { get; set; }
        int[] Dates { get; set; }



        public Booking(String mongoID, String costumerName, string email, int phoneNumber, int[] dates)
        {
            this.MongoID = mongoID;
            this.CostumerName = costumerName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Dates = dates;
        }

        public Booking()
        {

        }




    }
}