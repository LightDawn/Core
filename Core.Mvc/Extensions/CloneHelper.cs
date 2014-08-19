using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Core.Mvc.Extensions
{
    public static class CloneHelper
    {
        //public static T DeepCopy<T>(this T obj) where T : class , new()
        //{
        //    //var serializingObj = new JavaScriptSerializer();
        //    //var clonedObj = serializingObj.Deserialize<T>(serializingObj.Serialize(obj));
        //    //return clonedObj;
        //    var ser = new DataContractSerializer(typeof(T));
        //    object clonedObj = null;
        //    //using (var stream = new MemoryStream())
        //    //{
        //    //    ser.WriteObject(stream, obj);
        //    //    stream.Seek(0, SeekOrigin.Begin);
        //    //    clonedObj = ser.ReadObject(stream);           
        //    //}
        //    //return (T)clonedObj;
            
        //}
    }
}
