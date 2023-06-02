using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domein.AccessEntities;

public class Response : BaseAuditableEntity
{
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = "";
    public bool IsSuccess { get; set; } = true;
    public object Result { get; set; }

    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 10;
}
