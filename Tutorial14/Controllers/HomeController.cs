using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Cache;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Tutorial14.Models;

namespace Tutorial14.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection myConnection = new SqlConnection(Globals.ConnectionString);
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Insert()
        {
            return View();
        }

        public ActionResult DoInsert(string fullName, string clubName, int age, decimal fee )
        {
            try
            {
                string sql = "INSERT INTO Name(FullName, ClubName,Age,Fee) VALUES(@FullName, @ClubName,@Age,@Fee) ";
                SqlCommand cmd = new SqlCommand(sql, myConnection);

                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@ClubName",clubName);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Fee", fee);

                myConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                ViewBag.Message = "Success: " + rowsAffected + " rows added.";
            }
            catch (Exception err)
            {
                ViewBag.Message = "Error: " + err.Message;
            }
            finally
            {
                myConnection.Close();
            }
            return View("Index");
        }

        public ActionResult EditMember()
        {
            return View();
        }
        public ActionResult DoUpdate(int id)
        {
            try
            {
                string sql = "SELECT* FROM [Name] WHERE ID= @ID";
                ClubMemb member = new ClubMemb();

                using (SqlCommand cmd = new SqlCommand(sql, myConnection))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    myConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            member.Id = Convert.ToInt32(reader["ID"]);
                            member.FullName = reader["FullName"].ToString();
                            member.ClubName = reader["ClubName"].ToString();
                            member.Age = Convert.ToInt32(reader["Age"]);
                            member.Fee = Convert.ToDecimal(reader["Fee"]);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Member not found!";
                            return View("Update");
                        }
                    }
                }
                return View("EditMember", member);
            }
            catch (Exception err)
            {
                ViewBag.ErrorMessage = "Error: " + err.Message;
                return View("Update");
            }
            finally
            {
                myConnection.Close();
            }
        }

        public ActionResult Update()
        {
            return View();
        }



        public ActionResult FindMemberForUpdate(ClubMemb member)
        {
            try
            {
                string sql = @"UPDATE [Name]
                            SET FullName = @FullName, 
                                ClubName = @ClubName,  
                                Age = @Age, 
                                Fee = @Fee 
                                WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, myConnection))
                {
                    cmd.Parameters.AddWithValue("@ID",member.Id);
                    cmd.Parameters.AddWithValue("@FullName", member.FullName);
                    cmd.Parameters.AddWithValue("@ClubName", member.ClubName);
                    cmd.Parameters.AddWithValue("@Age", member.Age);
                    cmd.Parameters.AddWithValue("@Fee", member.Fee);
                    myConnection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0) {
                        ViewBag.Message = "Success: " + rowsAffected + " row updated.";
                    }
                    else
                    {
                        ViewBag.Message = "No rows were updated. Please check the ID.";
                    }
                        
                }
            }
            catch (Exception err)
            {
                ViewBag.Message = "Error: " + err.Message;
            }
            finally
            {
                myConnection.Close();
            }
            return View("Index");
        }

        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult DoDelete(int id)
        {
            try
            {                
                SqlCommand myDeleteCommand = new SqlCommand("Delete from Name where ID=" + id, myConnection);

                myConnection.Open();
                int rowsAffected = myDeleteCommand.ExecuteNonQuery();
                ViewBag.Message = "Success: "+ rowsAffected + " rows deleted.";
            }
            catch (Exception err)
            {
                ViewBag.Message = "Error: " + err.Message;
            }
            finally
            {
                myConnection.Close();
            }
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}