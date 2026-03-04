using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatReservation.Domain
{
    public class User
    {
        public User()
        {

        }

        public Guid Id { get; set; }
        public Details Details { get; set; }
    }

    public record Details
    {
        public Details()
        {

        }

        public string Description { get; set; }
        public string FIO { get; set; }
        public IReadOnlyList<SocialNetwork> Socials { get; set; }
    }

    public record SocialNetwork
    {
        public SocialNetwork()
        {
            
        }

        public string Name { get; set; }
        public string Link { get; set; }
    }
}
