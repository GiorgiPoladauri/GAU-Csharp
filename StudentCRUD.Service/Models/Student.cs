using System.Runtime.Serialization;

namespace StudentCRUD.Service.Models
{
    // [DataContract] გეუბნება WCF-ს: "ეს კლასი გაგზავნე ქსელით კლიენტამდე"
    [DataContract]
    public class Student
    {
        // [DataMember] = ეს property-ც გაგზავნე (სერიალიზაცია)
        [DataMember]
        public int Id { get; set; }           // Primary Key - ავტომატური

        [DataMember]
        public string FirstName { get; set; } // სახელი

        [DataMember]
        public string LastName { get; set; }  // გვარი

        [DataMember]
        public string Email { get; set; }     // ელ-ფოსტა

        [DataMember]
        public int Age { get; set; }          // ასაკი

        [DataMember]
        public string Major { get; set; }     // სპეციალობა
    }
}
