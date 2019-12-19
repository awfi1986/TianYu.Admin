using SoWay.BaseFramework.Code;
using SoWay.BaseFramework.Code.BaseViewModel;
using SoWay.BaseFramework.Queue.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SoWay.BaseFramework.FileApi.Controllers
{
    public class TestController : ApiController
    {
        
        [HttpPost]
        public TaskBaseResponse TestMqMessage1([FromBody]Test1 message)
        {
            var statusCode = HttpStatusCode.OK;
            if (message.ToJsonString().GetHashCode() % 3 != 0) {
                statusCode = HttpStatusCode.BadGateway;
            }
            TaskBaseResponse task = new TaskBaseResponse()
            {
                Status = statusCode, 
                ErrorMessage = message.Content1

            };
            return task;
        }
        [HttpPost]
        public TaskBaseResponse TestMqMessage2([FromBody]Test2 message)
        {
            TaskBaseResponse task = new TaskBaseResponse()
            {
                Status = DateTime.Now.Ticks % 5 != 0 ? HttpStatusCode.OK : HttpStatusCode.BadGateway,
                ErrorMessage = message.Content2

            };
            return task;
        }
        [HttpPost]
        public TaskBaseResponse TestMqMessage3(Test3 message)
        {
            TaskBaseResponse task = new TaskBaseResponse()
            {
                Status = DateTime.Now.Ticks % 7 != 0 ? HttpStatusCode.OK : HttpStatusCode.BadGateway,
                ErrorMessage = message.Content3

            };
            return task;
        }

    }
    [RabbitMqMessage("testQueue1", ExchangeName = "testexchange1", RoutingKey = "testroutingkey1", IsProperties = false)]
    public class Test1
    {

        public string Content1 { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
    [RabbitMqMessage("testQueue2", ExchangeName = "testexchange2", RoutingKey = "testroutingkey2", IsProperties = false)]
    public class Test2
    {

        public string Content2 { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
    [RabbitMqMessage("testQueue3", ExchangeName = "testexchange3", RoutingKey = "testroutingkey3", IsProperties = false)]
    public class Test3
    {

        public string Content3 { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }

}
