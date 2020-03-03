using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public static class ControolerExtensions
    {
        public static ActionResult<T> Maybe <T>(this Controller controller, T entity)
        {
            if (entity == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(entity);
            }
        }

        public static ActionResult Either<some,none>(this Controller controller, bool condition) 
            where some: ActionResult, new ()
            where none: ActionResult, new ()
        {
            if (condition)
            {
                return new some();
            }
            else
            {
                return new none();
            }
        }
    }
}
