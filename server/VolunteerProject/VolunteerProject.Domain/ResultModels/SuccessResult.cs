using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerProject.Domain.ResultModels
{
    public class SuccessResult<T> : Result<T>
    {
        private readonly T _data;

        public SuccessResult(T data)
        {
            _data = data;
        }
        public override ResultTypes ResultType => ResultTypes.Success;

        public override List<string> Errors => new List<string>();

        public override T Data => _data;
    }
}
