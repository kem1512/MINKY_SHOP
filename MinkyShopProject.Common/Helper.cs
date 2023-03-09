using Microsoft.AspNetCore.Mvc;

namespace MinkyShopProject.Common
{
    public class Helper
    {
        public static ActionResult TransformData(Response data)
        {
            var result = new ObjectResult(data) { StatusCode = (int)data.Code };
            return result;
        }
    }
}
