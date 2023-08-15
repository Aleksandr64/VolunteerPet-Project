﻿using Microsoft.AspNetCore.Mvc;
using VolunteerProject.Domain.ResultModels;

namespace VolunteerProject.Web.Decryptor
{
    public static class ResultDecryptor
    {
        public static ActionResult GetResponse<T>(this ControllerBase controller, Result<T> result)
        {
            switch (result.ResultType)
            {
                case ResultTypes.Success:
                    return result.Data == null ? controller.NoContent()
                        : controller.Ok(result.Data);
                case ResultTypes.Invalid:
                    return controller.BadRequest(result.Errors);
                case ResultTypes.NotFound:
                    return controller.NotFound(result.Errors);
                default: return controller.BadRequest();
            }
        }
    }
}
