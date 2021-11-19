﻿using System.Data;
using APICafecito.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace APICafecito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ReservaController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
			            SELECT id,
                        nombre,
                        email,
                        documento,
                        personas,
                        servicio,
                        fecha,
                        hora,
                        mensaje
                        FROM reserva;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        //CREACION
        [HttpPost]
        public JsonResult Post(Models.Reserva res)
        {
            string query = @"
                        INSERT INTO reserva
                        (nombre,
                        email,
                        documento,
                        personas,
                        servicio,
                        fecha,
                        hora,
                        mensaje)
                        VALUES
                        (@ReservaNombre,
                        @ReservaEmail,
                        @ReservaDocumento,
                        @ReservaServicio,
                        @ReservaPersonas,
                        @ReservaFecha,
                        @ReservaHora,
                        @ReservaMensaje);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ReservaId", res.id);
                    myCommand.Parameters.AddWithValue("@ReservaNombre", res.nombre);
                    myCommand.Parameters.AddWithValue("@ReservaDocumento", res.documento);
                    myCommand.Parameters.AddWithValue("@ReservaServicio", res.servicio);
                    myCommand.Parameters.AddWithValue("@ReservaPersonas", res.personas);
                    myCommand.Parameters.AddWithValue("@ReservaFecha", res.fecha);
                    myCommand.Parameters.AddWithValue("@ReservaHora", res.hora);
                    myCommand.Parameters.AddWithValue("@ReservaMensaje", res.mensaje);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        //Modificacion
        [HttpPut]
        public JsonResult Put(Reserva res)
        {
            string query = @"                       
                        UPDATE empleado
                        SET
                        nombre = @EmpleadoNombre,
                        email = @ReservaEmail,
                        documento = @ReservaDocumento,
                        servicio = @ReservaServicio,
                        personas = @ReservaPersonas,
                        fecha = @ReservaFecha,
                        hora = @ReservaHora,
                        mensaje = @ReservaMensaje
                        WHERE id = @EmpleadoId;

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ReservaId", res.id);
                    myCommand.Parameters.AddWithValue("@ReservaNombre", res.nombre);
                    myCommand.Parameters.AddWithValue("@ReservaEmail", res.email);
                    myCommand.Parameters.AddWithValue("@ReservaDocumento", res.documento);
                    myCommand.Parameters.AddWithValue("@ReservaServicio", res.servicio);
                    myCommand.Parameters.AddWithValue("@ReservaPersona", res.personas);
                    myCommand.Parameters.AddWithValue("@ReservaFecha", res.fecha);
                    myCommand.Parameters.AddWithValue("@ReservaHora", res.hora);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        //ELIMINACION
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        DELETE FROM reserva 
                        WHERE id=@ReservaId;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ReservaId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}