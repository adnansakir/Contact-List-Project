using Microsoft.EntityFrameworkCore;

namespace Contact_List_Project.Models.Domain
{
    //[Keyless]
    public class Contact
    {
        public Guid Id { get; set; }
        public String Number { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Team{ get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
