using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.Domain;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LibraryApi.Controllers
{
    
    public class ReservationController : Controller
    {
        LibraryDataContext Context;
        ISendMessagesToTheReservationProcessor Processor;

        public ReservationController(LibraryDataContext context, ISendMessagesToTheReservationProcessor processor)
        {
            Context = context;
            Processor = processor;
        }

        [HttpPost("reservations")]
        [ValidateModel]
        public async Task<ActionResult> AddReservation([FromBody] PostReservationRequest reservation)
        {
            var reservationToSave = new Reservation
            {
                For = reservation.For,
                Book = string.Join(',', reservation.Books),
                ReservationCreated = DateTime.Now,
                Status = ReservationStatus.Pending
            };

            Context.Reservations.Add(reservationToSave);
            await Context.SaveChangesAsync();

            var response = MapIt(reservationToSave);
            Processor.SendReservationForProcessing(response);

            return Ok(response); // ToDo : Make it a 200 with a location header
        }



        [HttpGet("reservations")]
        public async Task<ActionResult> GetAllReservations()
        {
            var response = new HttpCollection<GetReservationItemResponse>();

            var data = await Context.Reservations.ToListAsync();

            response.Data = data.Select(r => MapIt(r)).ToList();

            return Ok(response);
        }

        [HttpPost("reservations/pending")]
        public async Task<ActionResult> GetPendingReservations()
        {
            var response = new HttpCollection<GetReservationItemResponse>();

            var data = await Context.Reservations.Where(r => r.Status == ReservationStatus.Pending).ToListAsync();

            response.Data = data.Select(r => MapIt(r)).ToList();

            return Ok(response); 
        }

        [HttpPost("reservations/approved")]
        public async Task<ActionResult> GetApprovedReservations()
        {
            var response = new HttpCollection<GetReservationItemResponse>();

            var data = await Context.Reservations.Where(r => r.Status == ReservationStatus.Approved).ToListAsync();

            response.Data = data.Select(r => MapIt(r)).ToList();

            return Ok(response);
        }


        [HttpPost("reservations/cancelled")]
        public async Task<ActionResult> GetCancelledReservations()
        {
            var response = new HttpCollection<GetReservationItemResponse>();

            var data = await Context.Reservations.Where(r => r.Status == ReservationStatus.Cancelled).ToListAsync();

            response.Data = data.Select(r => MapIt(r)).ToList();

            return Ok(response);
        }

        private GetReservationItemResponse MapIt(Reservation r)
        {
            return new GetReservationItemResponse
            {
                Id = r.Id,
                For = r.For,
                ReservationCreated = r.ReservationCreated,
                Books = r.Book.Split(',')
                .Select(id => Url.ActionLink("GetAbook", "Books", new { id = id })).ToList(),
                Status = r.Status
            };
        }
    }
}