using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenacingMonsters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MenacingMonsters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonsterController : ControllerBase
    {
        private string _connectionString;

        public MonsterController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        // GET: api/Monster
        [HttpGet]
        public IEnumerable<Monster> Get()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name, eyeCount, catchPhrase FROM monster";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Monster> monsters = new List<Monster>();
                    while (reader.Read())
                    {
                        Monster newMonster = new Monster()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            EyeCount = reader.GetInt32(reader.GetOrdinal("eyeCount")),
                            CatchPhrase = reader.GetString(reader.GetOrdinal("catchPhrase"))
                        };
                        monsters.Add(newMonster);
                    }

                    reader.Close();

                    return monsters;
                }
            }
        }

        // GET: api/Monster/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, name, eyeCount, catchPhrase 
                                          FROM monster
                                         WHERE id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Monster aMonster = null;
                    if (reader.Read())
                    {
                        aMonster = new Monster()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            EyeCount = reader.GetInt32(reader.GetOrdinal("eyeCount")),
                            CatchPhrase = reader.GetString(reader.GetOrdinal("catchPhrase"))
                        };
                    }

                    reader.Close();

                    if (aMonster == null)
                    {
                        return NotFound();
                    }

                    return Ok(aMonster);
                }
            }
         }

        // POST: api/Monster
        [HttpPost]
        public void Post([FromBody] Monster newMonster)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Monster (name, eyecount, catchphrase)
                                        VALUES (@name, @eyecount, @catchphrase)";
                    cmd.Parameters.Add(new SqlParameter("@name", newMonster.Name));
                    cmd.Parameters.Add(new SqlParameter("@eyecount", newMonster.EyeCount));
                    cmd.Parameters.Add(new SqlParameter("@catchphrase", newMonster.CatchPhrase));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // PUT: api/Monster/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
