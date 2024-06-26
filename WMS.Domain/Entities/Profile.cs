namespace WMS.Domain.Entities
{
    public class Profile
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Name { get; set; }

        public User User { get; set; }

        public string Address { get; set; }

        public byte Age { get; set; }
    }
}
