using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests.Core.Repository
{
    /// <summary>
    /// This class is used,since each fakedb type has its own scenarion to find and return its entities
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TypesWithIdDbSet<T> : FakeDbSet<T> where T : class , new()
    {
        public override T Find(params object[] keyValues)
        {
            var keyValue = (int)keyValues.FirstOrDefault();
            return this.SingleOrDefault(t => (int)(t.GetType().GetProperty(t.GetType().Name + "Id").GetValue(t, null)) == keyValue);
        }
    }
}
