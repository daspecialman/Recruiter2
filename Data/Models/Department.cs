namespace Data.Models
{
    public class Department : BaseModel
	{
		public string Name { get; set; }
        public User HoD { get; set; }
        public int? HoDId { get; set; }
    }
}