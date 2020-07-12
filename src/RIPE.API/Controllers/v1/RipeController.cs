﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RIPE.API.Convention;
using RIPE.Application.Command;
using RIPE.Application.Queries;
using RIPE.Application.Requests;
using RIPE.Application.Responses;
using System.Threading.Tasks;

namespace RIPE.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/ripe")]
    [Produces("application/json")]
    [ApiConventionType(typeof(ApiConvention))]
    public class RipeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createUser")]
       // [Authorize]
        public async Task<ActionResult<Response>> NewUserPost([FromBody] LoginRequest userDetails)
        {
            var request = new NewUserQuery(userDetails.Login,
                                           userDetails.Password,
                                           userDetails.UserName,
                                           userDetails.FoneNumber,
                                           userDetails.Office,
                                           userDetails.Birth,
                                           userDetails.CompanyName);

            var response = await _mediator.Send(request);

            if (response.IsFailure) return BadRequest(response);
            if (response.Value is null) return NoContent();

            return Ok(response.Value);
        }

        [HttpPost("login")]
       // [Authorize]
        public async Task<ActionResult<Response>> LoginPost([FromBody] AuthenticationLoginRequest userDetails)
        {
            var request = new ValidateLoginQuery(userDetails.Login, userDetails.Password);

            var response = await _mediator.Send(request);

            if (response.IsFailure) return BadRequest(response);
            if (response.Value is null) return NoContent();

            return Ok(response.Value);
        }

        [HttpGet("questions")]
       // [Authorize]
        public async Task<ActionResult<QuestionsResponse>> GetQuestions(string validateUser)
        {
            var request = new SurveyQuery(validateUser);

            var response = await _mediator.Send(request);

            if (response.IsFailure) return BadRequest(response);
            if (response.Value is null) return NoContent();

            return Ok(response.Value);
        }

        [HttpGet("report")]
       // [Authorize]
      //  public async Task<ActionResult<ReportResponse>> Report([FromBody] AnswersSurveyRequest answers)
        public async Task<ActionResult<ReportResponse>> Report(string QuantityPositiveAnswer,string QuantityNegativeAnswer,string QuantityNullableAnswer)
        {
            var request = new ReportQuery(QuantityPositiveAnswer,QuantityNegativeAnswer,QuantityNullableAnswer);

            var response = await _mediator.Send(request);

            if (response.IsFailure) return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpPost("feedback")]
       // [Authorize]
        public async Task<IActionResult> Feedback([FromBody] FeedbackRequest feedback)
        {
            var request = new FeedbackCommand(feedback.CustomerFeedback, feedback.Email);

            var response = await _mediator.Send(request);
            if (response.IsFailure) return BadRequest(response);

            return Ok(response);
        }
    }


}
