using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public interface  IUser
    {
        int Id { get; set; }
        string FName { get; set; }
        string LName { get; set; }
      // IUserProfile UserProfile { get; set; }
    }
}
