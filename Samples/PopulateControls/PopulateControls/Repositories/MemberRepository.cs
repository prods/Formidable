using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using PopulateControls.Models;

namespace PopulateControls.Repositories
{
    public class MemberRepository
    {
        public MemberRepository()
        {
            Randomizer.Seed = new Random(int.MaxValue);   
        }

        public IEnumerable<Member> GetMembers(int memberNumber)
        {
            List<Member> _result = new List<Member>();
            for (int i = 0; i < memberNumber; i++)
            {
                var _newMember = new Faker<Member>()
                    .StrictMode(true)
                    .RuleFor(m => m.FirstName, f => f.Name.FirstName())
                    .RuleFor(m => m.LastName, f => f.Name.LastName())
                    .RuleFor(m => m.Age, f => f.Random.Int(20, 56))
                    .RuleFor(m => m.RegistrationDate, f => f.Date.Between(new DateTime(1970, 1, 1), new DateTime(2017, 1, 1)));
                _result.Add(_newMember);
            }

            return _result;
        }
    }
}
