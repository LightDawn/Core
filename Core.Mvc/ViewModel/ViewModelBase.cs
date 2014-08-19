using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Core.Service;
using Newtonsoft.Json;


namespace Core.Mvc.ViewModel
{
    [DataContract(Name = "ViewModelBase")]

   
    public class ViewModelBase<T> : IViewModelBase<T>, IViewModelBase, IValidatableObject where T : EntityBase<T>, new()
    {

       

        protected T _model;

        // [JsonIgnore]
        public T GetModel()
        {
         
            return _model;
        }

        public virtual IViewModelBase<T> SetModel(T model)
        {
            _model = model;
            return this;
        }

        public ViewModelBase(T model)
        {
            SetModel(model);
        }

        public ViewModelBase()
        {
            
            SetModel(new T());
           
        }


        public List<ValidationResult> ValidationErrors = new List<ValidationResult>();

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return ValidationErrors;
        }

        [ScriptIgnore]
        [JsonIgnore]
        public T Model
        {
           
            get
            {
               
                return _model;
            }
        }

        public EntityBase<T> GetObjectModel()
        {
           
            return this.Model;
        }

      
      
        //public void SetIp()
        //{
        //    _model["CurrentUserId"] = Core.Service.AppBase.GetCurrenrUserId();
        //}
        public static IEnumerable<ViewModel> GetViewModels<ViewModel>(IEnumerable<T> models) where ViewModel : ViewModelBase<T>, new()
        {
            
            if (models != null)
                foreach (var model in models)
                {
                    yield return new ViewModel() { _model = model };
                }
        }
        public static ViewModel GetViewModel<ViewModel>(T model) where ViewModel : ViewModelBase<T>, new()
        {
            return new ViewModel() { _model = model };
        }
    }
}
