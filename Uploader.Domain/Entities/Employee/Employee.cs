
namespace Uploader.Domain.Entities.Employee
{
    public class Employee
    {
        public Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
