
///cheginy 1392/04/24

using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEnity
    {
        object this[string propertyName] { get; set; }
    }
    public class EntityBase<T> : IEnity , IEquatable<T> where T : EntityBase<T> 
    {
        Dictionary<string, object> dicValue;

        public EntityBase()
        {
            dicValue = new Dictionary<string, object>();
            if (_entityInfo == null)
            {
                _entityInfo = GetEntityInfo();
            }
        }

        private Model.EntityInfo GetEntityInfo()
        {
            var entityInfo = new EntityInfo();

            foreach (PropertyInfo propInfo in this.GetType().GetProperties())
            {
                entityInfo.Properties.Add(propInfo.Name, propInfo);
                var keyAtt = Attribute.GetCustomAttribute(propInfo, typeof(KeyAttribute));
                if (keyAtt != null)
                    entityInfo.KeyColumns.Add(propInfo.Name, propInfo);
            }
            return entityInfo;
        }

        private static EntityInfo _entityInfo;

       // [NotMapped]        
        private  EntityInfo EntityInfo
        {
            get { return EntityBase<T>._entityInfo; }
            set { EntityBase<T>._entityInfo = value; }
        }

        public object this[string propertyName]
        {
            get
            {
                if (_entityInfo.Properties.ContainsKey(propertyName))
                {
                    return _entityInfo.Properties[propertyName].GetValue(this); ;
                }
                else
                    throw new ArgumentException("PropertyName Is not exist! (Refah Pardis Exc)");
            }
            set
            {
                if (_entityInfo.Properties.ContainsKey(propertyName))
                {
                    _entityInfo.Properties[propertyName].SetValue(this, value);
                }
                else
                    throw new ArgumentException("PropertyName Is not exist! (Refah Pardis Exc)");
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(EntityBase<T>)) return false;
            return Equals((EntityBase<T>)obj);
        }

        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;
            bool result = true;
            foreach (var key in this.EntityInfo.KeyColumns.Values)
            {

                if (!key.GetValue(other).Equals(key.GetValue(this)))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static bool operator ==(EntityBase<T> left, EntityBase<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EntityBase<T> left, EntityBase<T> right)
        {
            return !Equals(left, right);
        }

    }
}

