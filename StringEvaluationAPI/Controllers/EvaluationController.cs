using EvaluationLib;
using EvalutonCalculatorApi.Models;
using System.Web.Http;

namespace EvaluationCalculatorApi.Controllers
{
    public class EvaluationController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody]Operation model)
        {
            Evaluation eval = new Evaluation(model.Operand);
            double result = eval.EvaluateExpression(model.EvaluationString);

            Response response = new Response()
            {
                Result = result,
                Message = "Success"
            };

            return Ok(response);
        }
    }
}
