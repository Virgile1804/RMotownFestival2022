using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMotownFestival.Api.DAL;
using RMotownFestival.Api.Data;
using RMotownFestival.Api.Domain;

namespace RMotownFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FestivalController : ControllerBase
    {
        private readonly MotownDbContext _ctx;
        private readonly TelemetryClient _tmc; //Dependance Injection (existe en C#)

        public FestivalController(MotownDbContext ctx, TelemetryClient tmc)
        {
            _ctx = ctx;
            _tmc = tmc;
        }
        [HttpGet("LineUp")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Schedule))]
        public async Task<ActionResult> GetLineUp()
        {
            //throw new ApplicationException("LineUp Failed"); //Renvoie une erreur dans le portail azure
            // return Ok(FestivalDataSource.Current.LineUp);
            var linesUp = await _ctx.Schedules.Include(x => x.Festival)
                                               .Include(x=>x.Items)
                                               .ThenInclude(x=>x.Artist)
                                               .Include(x=>x.Items)
                                               .ThenInclude(x=>x.Stage)
                                               .FirstOrDefaultAsync();
            return Ok(linesUp);
        }

        [HttpGet("Artists")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Artist>))]
        public async Task<ActionResult> GetArtists(bool? withRatings) //Reprendre ici
        {
            if (withRatings.HasValue && withRatings.Value)
                _tmc.TrackEvent($"List of artists with ratings");
            else
                _tmc.TrackEvent($"List of artists without ratings");
            //return Ok(FestivalDataSource.Current.Artists);
            var artists = await _ctx.Artists.ToListAsync();
            return Ok(artists);
        }

        [HttpGet("Stages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Stage>))]
        public async Task<ActionResult> GetStages()
        {
            //return Ok(FestivalDataSource.Current.Stages);
            var stages = await _ctx.Stages.ToListAsync();
            return Ok(stages);
        }

        [HttpPost("Favorite")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ScheduleItem))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult SetAsFavorite(int id)
        {
            var schedule = FestivalDataSource.Current.LineUp.Items
                .FirstOrDefault(si => si.Id == id);
            if (schedule != null)
            {
                schedule.IsFavorite = !schedule.IsFavorite;
                return Ok(schedule);
            }
            return NotFound();
        }

    }
}