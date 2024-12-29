<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations.Schema;
=======
﻿using System.ComponentModel.DataAnnotations;
>>>>>>> 05a26b99e5e72480244a2ecd399c7d4405112596

namespace ExMart_Backend.Model
{
    public class User
    {
<<<<<<< HEAD
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; }



        // Navigation Properties
        public ICollection<Order> Orders { get; set; }
=======
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
>>>>>>> 05a26b99e5e72480244a2ecd399c7d4405112596
    }
}
