using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Configuration;
using AstraRekrutacja.DB;
using AstraRekrutacja.Models;
using AstraRekrutacja.JsonObjects;
using AstraRekrutacja.DateEvaluation;
using System.Net.Http.Headers;

namespace AstraRekrutacja.Controllers
{
    public class WorkersController : ApiController
    {
        private readonly MyDbContext db = new MyDbContext();

        // GET: api/Workers
        public IQueryable<Worker> GetWorkers()
        {
            return db.Workers;
        }
        
        // GET: api/Workers/5
        [ResponseType(typeof(Worker))]
        public async Task<IHttpActionResult> GetWorker(int id)
        {
            Worker worker = await db.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }

        // Get details about workleaves, function takes start date and end date as arguments to filter results.
        // GET WORKLEAVES: api/workers?startDate=yyyy,MM,dd&endDate=yyyy,MM,dd
        //[Route("api/workers/workleaves")] //Optional: Change of a route
        [HttpGet]
        [ResponseType(typeof(WorkleavesJson))]
        public IHttpActionResult GetWorker(DateTime startDate, DateTime endDate)
        {
            try
            {
                //Prevent Injection Attack and check if passed data format is valid
                if (DateTime.TryParseExact(startDate.ToString("yyyy,MM,dd"), "yyyy,MM,dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDateInput) &&
                    DateTime.TryParseExact(endDate.ToString("yyyy,MM,dd"), "yyyy,MM,dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDateInput))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MyDbContext"].ConnectionString;

                    string commandText =
                        "SELECT Workers.FirstName, Workers.LastName, " +                            //reader[0], reader[1]
                            "Managers.FirstName, Managers.LastName, " +                             //reader[2], reader[3]
                            "Workleaves.StartOfWorkleave, Workleaves.Status, Workleaves.Days, " +   //reader[4], reader[5], reader[6]
                            "WorkleaveTypes.WorkleaveName " +                                       //reader[7]
                        "FROM Workleaves " +
                            "INNER JOIN Workers ON Workleaves.WorkerId = Workers.WorkerId " +
                            "INNER JOIN Managers ON Workers.ManagerId = Managers.ManagerId " +
                            "INNER JOIN WorkleaveTypes ON Workleaves.WorkleaveTypeId = WorkleaveTypes.WorkleaveTypeId " +
                        "WHERE WorkleaveTypes.Active = 1 " +
                            "AND Workleaves.Days <= 4 " +                                           //Weekends do not count
                            "AND Workleaves.StartOfWorkleave <= @EndDateSpan";                      //Last Work day is counted by c# function, so i couldn't get to work it into sql command
                                                                                                    //it's checked later while reading output

                    List<WorkleavesJson> results = new List<WorkleavesJson>();

                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();

                    SqlCommand command = new SqlCommand(commandText, conn);
                    command.Parameters.AddWithValue("@EndDateSpan", WorkleaveEvaluation.workleaveSpanP.Item2);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DateTime startDateTime = reader.GetDateTime(4);
                                DateTime endDateTime = WorkleaveEvaluation.LastDayOfWorkleave(reader.GetDateTime(4), reader.GetInt32(6));

                                if (!WorkleaveEvaluation.IsInDataRange(startDateTime, endDateTime) || !(startDateInput <= startDateTime && endDateTime <= endDateInput))
                                    continue;

                                WorkleavesJson workleave = new WorkleavesJson();

                                workleave.WorkerName = reader.GetString(0) + " " + reader.GetString(1);
                                workleave.ManagerName = reader.GetString(2) + " " + reader.GetString(3);
                                workleave.WorkleaveStartDate = startDateTime;
                                workleave.WorkleaveEndDate = endDateTime;
                                workleave.WorkleaveName = reader.GetString(7);
                                workleave.Status = (WorkleaveStatus)reader.GetInt32(5);
                                workleave.DaysWithWeekends = WorkleaveEvaluation.HowManyDaysOfWorkleave(workleave.WorkleaveStartDate,
                                                                                                        workleave.WorkleaveEndDate,
                                                                                                        reader.GetInt32(6));
                                workleave.Span = WorkleaveEvaluation.WorkleaveSpan(workleave.WorkleaveStartDate, workleave.WorkleaveEndDate);

                                results.Add(workleave);
                            }
                        }
                    }
                    conn.Close();

                    return Ok(results);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return BadRequest();
            }
        }

        //Send GET request - Helper method for getting indirect json data
        public static async Task<List<WorkleavesJson>> GetWorkleavesData()
        {
            List<WorkleavesJson> data = new List<WorkleavesJson>();

            using (var client = new HttpClient())
            {
                //Configuration of data exported to excel
                string appendStartDate = "2022,01,01";
                string appendEndDate = "2023,01,01";

                client.BaseAddress = new Uri("https://localhost:44357/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/workers?startDate=" + appendStartDate + "&endDate=" + appendEndDate);
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsAsync<List<WorkleavesJson>>();
                }
            }

            return data;
        }

        // PUT: api/Workers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWorker(int id, Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != worker.WorkerId)
            {
                return BadRequest();
            }

            db.Entry(worker).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Workers
        [ResponseType(typeof(Worker))]
        public async Task<IHttpActionResult> PostWorker(Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Workers.Add(worker);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = worker.WorkerId }, worker);
        }

        // DELETE: api/Workers/5
        [ResponseType(typeof(Worker))]
        public async Task<IHttpActionResult> DeleteWorker(int id)
        {
            Worker worker = await db.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            db.Workers.Remove(worker);
            await db.SaveChangesAsync();

            return Ok(worker);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkerExists(int id)
        {
            return db.Workers.Count(e => e.WorkerId == id) > 0;
        }
    }
}