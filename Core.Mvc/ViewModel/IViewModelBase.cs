using Core.Model;
using Core.Mvc.Helpers.RefahKendoGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Mvc.ViewModel
{
    public interface IViewModelBase<T> where T : EntityBase<T>, new()
    {
        T Model
        {
            get;
        }

        // [JsonIgnore]
        T GetModel();

        IViewModelBase<T> SetModel(T model);

        EntityBase<T> GetObjectModel();
    }

    public interface IViewModelBase
    {
      
    }

}
