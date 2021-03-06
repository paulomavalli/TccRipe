﻿using RIPE.API.Convention;
using RIPE.Application.Queries;
using RIPE.Application.Requests;
using RIPE.Application.Responses;
using RIPE.CrossCutting.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using RIPE.Application.Command;

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

        [HttpPost("login")]
        [Authorize]
        public async Task<ActionResult<Response>> Post([FromBody] LoginRequest userDetails)
        {
            var request = new ValidateLoginQuery(userDetails.Login, userDetails.Password);

            var response = await _mediator.Send(request);

            if (response.IsFailure) return BadRequest(response);
            if (response.Value is null) return NoContent();

            return Ok(response.Value);
        }

        [HttpGet("questions")]
        [Authorize]
        public async Task<ActionResult<QuestionsResponse>> GetQuestions(string validateUser)
        {
            var request = new SurveyQuery(validateUser);

            var response = await _mediator.Send(request);

            if (response.IsFailure) return BadRequest(response);
            if (response.Value is null) return NoContent();

            return Ok(response.Value);
        }

        [HttpGet("report")]
        [Authorize]
        public async Task<ActionResult<Response>> Report(QuestionsRequest TypesQuestions)
        {
            var request = new ReportQuery(TypesQuestions.TypesQuestions);

            var response = await _mediator.Send(request);

            if (response.IsFailure) return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpPost("feedback")]
        [Authorize]
        public async Task<IActionResult> Feedback([FromBody] FeedbackRequest feedback)
        {
            var request = new FeedbackCommand(User.GetCustomerId(), feedback.FeedbackOrigin, feedback.CustomerFeedback, feedback.SuggestedLimit);

            var response = await _mediator.Send(request);
            if (response.IsFailure) return BadRequest(response);

            return Ok(response);
        }
    }


}
