using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Core.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Core.Mvc.ViewModel;
using Core.Model;


namespace Core.Mvc.Controller
{
    public class CrudApiControllerBase<EntityT, ViewModelT, ServiceT> : Core.Mvc.Controller.ApiControllerBase
        where EntityT : EntityBase<EntityT>, new()
        where ViewModelT : IViewModelBase<EntityT>, new()
        where ServiceT : IServiceBase<EntityT>
    {
        private ServiceT _service;

        public ServiceT Service
        {
            get { return _service; }
        }
        public CrudApiControllerBase(ServiceT service)
        {
            _service = service;
        }

        public virtual DataSourceResult GetEntities([ModelBinder(typeof(ModelBinders.DataSourceRequestModelBinder))] DataSourceRequest request)
        {
            var entities = _service.Load();
            return entities.ToDataSourceResult(request, rEntity => new ViewModelT().SetModel(rEntity));
        }

        public virtual HttpResponseMessage PostEntitiy([ModelBinder(typeof(ModelBinders.DataSourceRequestModelBinder))] DataSourceRequest request,ViewModelT viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addedEntity = _service.Create(viewModel.Model);
                    viewModel.SetModel(addedEntity);
                    return Request.CreateResponse(HttpStatusCode.Created, new { Data = new[] { viewModel }, Total = 1 });                                                 
                }
                catch (DbEntityValidationException ex)
                {
                    var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);

            //if (userViewModel.ValidationErrors.Any())
            //    AddModelError(userViewModel.ValidationErrors);
        }

        public virtual HttpResponseMessage PutEntitiy([ModelBinder(typeof(ModelBinders.DataSourceRequestModelBinder))] DataSourceRequest request,ViewModelT viewModel)
        {
            if (ModelState.IsValid)
            {
                var updatedEntityId = _service.Update(viewModel.Model);
                return Request.CreateResponse(HttpStatusCode.OK, new { Data = new[] { viewModel }, Total = 1 });
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        public virtual HttpResponseMessage DeleteEntitiy(ViewModelT viewModel)
        {
            try
            {
                var deletedEntity = _service.Delete(viewModel.Model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Entity");
        }
    }
}
