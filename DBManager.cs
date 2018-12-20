using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Equipment_Manager
{
    class DBManager
    {
       
            //public static string  connectionString = Properties.Settings.Default.msqlCS;
            //public  static
            public static DataTable dtu;
            //public static MySqlDataReader rdr;
            public static MySqlCommand command;
            public static MySqlDataAdapter da;
            public static string errorMessage { get; set; }

        public static string removeTags(string value)
            {
                value = Regex.Replace(value, @"<[^>]+>|&nbsb;", "").Trim();
                value = Regex.Replace(value, @"\s{2,}", "");
                return value;
            }

       
        public static string RunScript(string fileName)
            {
                try
                {
                MySqlConnection conn = new MySqlConnection("Server =" + Properties.Settings.Default.mySqlServerAdress +  "; Port = " + Properties.Settings.Default.mySqlPort + "; Uid = " + Properties.Settings.Default.mySqlUserName + "; Pwd = " + Properties.Settings.Default.MysqlPassword + "; CharSet = utf8;");

           
                MySqlScript script = new MySqlScript(conn, File.ReadAllText(fileName));
                    script.Execute();
                    return "Success";
                }
                catch (Exception e)
                {

                    return "Error Occured while Creating Database. The Database may alreadys exists or connections are not correct. Modify them and try again.";
                }
            }
            public static DataTable SelectFromDatabase(string query)
            {
            MySqlConnection conn = new MySqlConnection("Server =" + Properties.Settings.Default.mySqlServerAdress +  "; Port = " + Properties.Settings.Default.mySqlPort + "; Database=" + Properties.Settings.Default.mySqlDatabase + "; Uid = " + Properties.Settings.Default.mySqlUserName + "; Pwd = " + Properties.Settings.Default.MysqlPassword + "; CharSet = utf8;");
            try
            {

                    conn.Open();
                    dtu = new DataTable();
                    command = new MySqlCommand { Connection = conn, CommandText = query };
                    command.ExecuteNonQuery();


                    da = new MySqlDataAdapter(command);
                    da.Fill(dtu);
                    conn.Close();
                    command.Dispose();
                    return dtu;
                }
                catch (Exception ex)
                {
                Home.logMessage +="\n"+DateTime.Now.ToString() +"Error occured when selecting from database because "+ex.Message + " with query = " + query;
                    return null;
                }

            }
        private static string InsertToDatabase(string query)
            {
            MySqlConnection conn = new MySqlConnection("Server =" + Properties.Settings.Default.mySqlServerAdress +  "; Port = " + Properties.Settings.Default.mySqlPort + "; Database=" + Properties.Settings.Default.mySqlDatabase + "; Uid = " + Properties.Settings.Default.mySqlUserName + "; Pwd = " + Properties.Settings.Default.MysqlPassword + "; CharSet = utf8;");

            try
            {

                    //dtu = new DataTable();
                    conn.Open();
                    command = new MySqlCommand { Connection = conn, CommandText = query };

                    int n = command.ExecuteNonQuery();
                    conn.Close();
                    command.Dispose();
                    if (n > 0)
                    {
                        return "a Success!";
                    }
                    else
                    {

                        return "not completed because no rows have been affected! " + query;
                    }
                }
                catch (Exception ex)
                {
                Home.logMessage += "\n" + DateTime.Now.ToString() + "Error occured when inserting into database because " + ex.Message+ " with query = " +query;

                return "not completed because " + ex.Message;
                }

            }

        public static bool CheckConnection()
        {
            bool ret = false;
            MySqlConnection conn = new MySqlConnection("Server =" + Properties.Settings.Default.mySqlServerAdress +  "; Port = " + Properties.Settings.Default.mySqlPort + "; Database=" + Properties.Settings.Default.mySqlDatabase + "; Uid = " + Properties.Settings.Default.mySqlUserName + "; Pwd = " + Properties.Settings.Default.MysqlPassword + "; CharSet = utf8;");

            try
            {

                conn.Open();
                ret = true;

            }
            catch (Exception ex)
            {
                ret = false;
                errorMessage = ex.Message;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return ret;
        }
        public static int CountFromDatabase(string query)
        {
            MySqlConnection conn = new MySqlConnection("Server =" + Properties.Settings.Default.mySqlServerAdress +  "; Port = " + Properties.Settings.Default.mySqlPort + "; Database=" + Properties.Settings.Default.mySqlDatabase + "; Uid = " + Properties.Settings.Default.mySqlUserName + "; Pwd = " + Properties.Settings.Default.MysqlPassword + "; CharSet = utf8;");

            try
            {

                conn.Open();
                dtu = new DataTable();
                command = new MySqlCommand { Connection = conn, CommandText = query };


                return int.Parse(command.ExecuteScalar().ToString());
            }
            catch (Exception ex)

            {
                Home.logMessage += "\n" + DateTime.Now.ToString() + "Error occured when counting from database" + ex.Message+" with query="+query;

                return 0;
            }

        }

        private static bool UpdateDatabase(string query)
        {
            MySqlConnection conn = new MySqlConnection("Server =" + Properties.Settings.Default.mySqlServerAdress +  "; Port = " + Properties.Settings.Default.mySqlPort + "; Database=" + Properties.Settings.Default.mySqlDatabase + "; Uid = " + Properties.Settings.Default.mySqlUserName + "; Pwd = " + Properties.Settings.Default.MysqlPassword + "; CharSet = utf8;");

            try
            {

                //dtu = new DataTable();
                conn.Open();
                command = new MySqlCommand { Connection = conn, CommandText = query };
                int n = command.ExecuteNonQuery();
                conn.Close();
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Home.logMessage += "\n" + DateTime.Now.ToString() + "Error occured when updating database because " + ex.Message + " with query = " + query;
                return false;
            }

        }

        internal static int countEquipments(string v)
        {
            string query = "SELECT COUNT(*) FROM equipment_list WHERE equipment_type='" + v + "'";
            return CountFromDatabase(query);
        }
        internal static int countDuplicateEquipments(string v)
        {
            string query = "SELECT COUNT(*) FROM equipment_list WHERE equipment_id='" + v + "'";
            return CountFromDatabase(query);

        }

        internal static DataTable selectIdTag(string equipmentType)
        {
            string query3 = "SELECT *  FROM equipment_type WHERE id = '" + equipmentType + "'";
            return SelectFromDatabase(query3);
        }

        internal static DataTable selectEquipmentType()
        {
            string query3 = "SELECT *  FROM equipment_type ";
            return SelectFromDatabase(query3);
        }

        internal static DataTable selectSiteLocations()
        {
            string query3 = "SELECT *  FROM project_list WHERE status!='Completed' ORDER BY Name ASC";
            return SelectFromDatabase(query3);
        }
        internal static DataTable selectEquipmentStatus()
        {
            string query3 = "SELECT *  FROM equipment_status ";
            return SelectFromDatabase(query3);
        }


         public static DataTable selectallEquipmentStatus()
        {
            string query3 = @"DROP TABLE IF EXISTS costs_table1,equipments,costs_table2,insurance_summary;
                               CREATE TEMPORARY TABLE costs_table1 SELECT maintainance_request.equipment_id, 
                            SUM(maintainance_request.maintainance_labor_cost) AS maint_labor_cost,
                            SUM(maintainance_request.maintainance_sparepart_cost) AS maint_sparepart_cost
                            FROM maintainance_request
                            GROUP BY maintainance_request.equipment_id;
                            CREATE TEMPORARY TABLE costs_table2 SELECT service_request.equipment_id, 
                            SUM(service_request.service_labor_cost) AS serv_labor_cost,
                            SUM(service_request.service_sparepart_cost) AS serv_sparepart_cost
                            FROM service_request
                            GROUP BY service_request.equipment_id;
                            CREATE TEMPORARY TABLE equipments
                            SELECT equipment_list.equipment_id, machines.machine_name,machine_functions.machine_function_name, machines.machine_capacity,machine_groups.machine_group_name,  equipment_list.plate_number, equipment_list.manufactured_year, project_list.Name,equipment_list.Investment_cost,CONCAT(equipment_list.serviced_every,' ',machines.machine_product_unit) AS 'service_every',equipment_type.type_name ,equipment_status.status_name FROM equipment_list,machines,project_list,machine_functions,machine_groups,equipment_status,equipment_type  WHERE equipment_list.machine_id=machines.machine_id  AND equipment_list.current_location=project_list.id AND machines.major_functions=machine_functions.id AND machines.machine_group= machine_groups.machine_group_id AND equipment_list.equipment_type = equipment_type.id AND equipment_list.current_status = equipment_status.status_id ;
CREATE TEMPORARY TABLE insurance_summary 
		SELECT insurance_payment.equipment_id,SUM(insurance_payment.value_payed) As tot_insurance FROM insurance_payment GROUP BY insurance_payment.equipment_id;
                            SELECT equipments.equipment_id AS 'Equipment ID',equipments.machine_name AS 'Machine Name', equipments.machine_function_name AS 'Major Functions',equipments.machine_group_name AS 'Machine Group',equipments.machine_capacity AS 'Capacity',equipments.plate_number AS 'Plate Number',equipments.manufactured_year AS 'Manufacture Year',equipments.Name AS 'Current Location',equipments.Investment_cost AS 'Investment Cost',equipments.service_every AS 'Needs Service Every',equipments.type_name AS 'Ownership Status',equipments.status_name AS 'Current Status',costs_table1.maint_labor_cost  AS 'Todate Maintainance Labor Cost',costs_table1.maint_sparepart_cost  AS 'Todate Maintainance Sparepart Cost',costs_table2.serv_labor_cost  AS 'Todate Service Labor Cost',costs_table2.serv_sparepart_cost  AS 'Todate Service Sparepart Cost',insurance_summary.tot_insurance AS 'Total Payment For Insurance' FROM equipments LEFT OUTER JOIN costs_table1 ON equipments.equipment_id = costs_table1.equipment_id LEFT OUTER JOIN costs_table2 ON  equipments.equipment_id = costs_table2.equipment_id LEFT OUTER JOIN insurance_summary ON equipments.equipment_id=insurance_summary.equipment_id";
             return SelectFromDatabase(query3);

        }
        public static DataTable selectallEquipmentYearlyStatus()
        {
            string query3 = @"DROP TABLE IF EXISTS costs_table1,equipments,costs_table2;
                               CREATE TEMPORARY TABLE costs_table1 SELECT maintainance_request.equipment_id, 
                            SUM(maintainance_request.maintainance_labor_cost) AS maint_labor_cost,
                            SUM(maintainance_request.maintainance_sparepart_cost) AS maint_sparepart_cost
                            FROM maintainance_request WHERE YEAR(maintainance_request.maintainance_completion_date)=YEAR(CURDATE())
                            GROUP BY maintainance_request.equipment_id;
                            CREATE TEMPORARY TABLE costs_table2 SELECT service_request.equipment_id, 
                            SUM(service_request.service_labor_cost) AS serv_labor_cost,
                            SUM(service_request.service_sparepart_cost) AS serv_sparepart_cost
                            FROM service_request WHERE YEAR(service_request.service_completion_date)=YEAR(CURDATE())
                            GROUP BY service_request.equipment_id;
                            CREATE TEMPORARY TABLE equipments
                            SELECT equipment_list.equipment_id, machines.machine_name,machine_functions.machine_function_name, machines.machine_capacity,machine_groups.machine_group_name,  equipment_list.plate_number, equipment_list.manufactured_year, project_list.Name,equipment_list.Investment_cost,CONCAT(equipment_list.serviced_every,' ',machines.machine_product_unit) AS 'service_every',equipment_type.type_name ,equipment_status.status_name FROM equipment_list,machines,project_list,machine_functions,machine_groups,equipment_status,equipment_type  WHERE equipment_list.machine_id=machines.machine_id  AND equipment_list.current_location=project_list.id AND machines.major_functions=machine_functions.id AND machines.machine_group= machine_groups.machine_group_id AND equipment_list.equipment_type = equipment_type.id AND equipment_list.current_status = equipment_status.status_id ;
                            SELECT equipments.equipment_id AS 'Equipment ID',equipments.machine_name AS 'Machine Name', equipments.machine_function_name AS 'Major Functions',equipments.machine_group_name AS 'Machine Group',equipments.machine_capacity AS 'Capacity',equipments.plate_number AS 'Plate Number',equipments.manufactured_year AS 'Manufacture Year',equipments.Name AS 'Current Location',equipments.Investment_cost AS 'Investment Cost',equipments.service_every AS 'Needs Service Every',equipments.type_name AS 'Ownership Status',equipments.status_name AS 'Current Status',costs_table1.maint_labor_cost  AS 'This Year Maintainance Labor Cost',costs_table1.maint_sparepart_cost  AS 'This Year Maintainance Sparepart Cost',costs_table2.serv_labor_cost  AS 'This Year Service Labor Cost',costs_table2.serv_sparepart_cost  AS 'This Year Service Sparepart Cost' FROM equipments LEFT OUTER JOIN costs_table1 ON equipments.equipment_id = costs_table1.equipment_id LEFT OUTER JOIN costs_table2 ON equipments.equipment_id = costs_table2.equipment_id";
            return SelectFromDatabase(query3);

        }
        public static DataTable selectallEquipmentWeekleyStatus()
        {
            string query3 = @"DROP TABLE IF EXISTS costs_table1,equipments,costs_table2;
                               CREATE TEMPORARY TABLE costs_table1 SELECT maintainance_request.equipment_id, 
                            SUM(maintainance_request.maintainance_labor_cost) AS maint_labor_cost,
                            SUM(maintainance_request.maintainance_sparepart_cost) AS maint_sparepart_cost
                            FROM maintainance_request WHERE YEARWEEK(maintainance_request.maintainance_completion_date)=YEARWEEK(CURDATE())
                            GROUP BY maintainance_request.equipment_id;
                            CREATE TEMPORARY TABLE costs_table2 SELECT service_request.equipment_id, 
                            SUM(service_request.service_labor_cost) AS serv_labor_cost,
                            SUM(service_request.service_sparepart_cost) AS serv_sparepart_cost
                            FROM service_request WHERE YEARWEEK(service_request.service_completion_date)=YEARWEEK(CURDATE())
                            GROUP BY service_request.equipment_id;
                            CREATE TEMPORARY TABLE equipments
                            SELECT equipment_list.equipment_id, machines.machine_name,machine_functions.machine_function_name, machines.machine_capacity,machine_groups.machine_group_name,  equipment_list.plate_number, equipment_list.manufactured_year, project_list.Name,equipment_list.Investment_cost,CONCAT(equipment_list.serviced_every,' ',machines.machine_product_unit) AS 'service_every',equipment_type.type_name ,equipment_status.status_name FROM equipment_list,machines,project_list,machine_functions,machine_groups,equipment_status,equipment_type  WHERE equipment_list.machine_id=machines.machine_id  AND equipment_list.current_location=project_list.id AND machines.major_functions=machine_functions.id AND machines.machine_group= machine_groups.machine_group_id AND equipment_list.equipment_type = equipment_type.id AND equipment_list.current_status = equipment_status.status_id ;
                            SELECT equipments.equipment_id AS 'Equipment ID',equipments.machine_name AS 'Machine Name', equipments.machine_function_name AS 'Major Functions',equipments.machine_group_name AS 'Machine Group',equipments.machine_capacity AS 'Capacity',equipments.plate_number AS 'Plate Number',equipments.manufactured_year AS 'Manufacture Year',equipments.Name AS 'Current Location',equipments.Investment_cost AS 'Investment Cost',equipments.service_every AS 'Needs Service Every',equipments.type_name AS 'Ownership Status',equipments.status_name AS 'Current Status',costs_table1.maint_labor_cost  AS 'This Week Maintainance Labor Cost',costs_table1.maint_sparepart_cost  AS 'This Week Maintainance Sparepart Cost',costs_table2.serv_labor_cost  AS 'This Week Service Labor Cost',costs_table2.serv_sparepart_cost  AS 'This Week Service Sparepart Cost' FROM equipments LEFT OUTER JOIN costs_table1 ON equipments.equipment_id = costs_table1.equipment_id LEFT OUTER JOIN costs_table2 ON equipments.equipment_id = costs_table2.equipment_id";
            return SelectFromDatabase(query3);

        }
        public static DataTable selectallEquipmentMonthlyStatus()
        {
            string query3 = @"DROP TABLE IF EXISTS costs_table1,equipments,costs_table2;
                               CREATE TEMPORARY TABLE costs_table1 SELECT maintainance_request.equipment_id, 
                            SUM(maintainance_request.maintainance_labor_cost) AS maint_labor_cost,
                            SUM(maintainance_request.maintainance_sparepart_cost) AS maint_sparepart_cost
                            FROM maintainance_request WHERE MONTH(maintainance_request.maintainance_completion_date)=MONTH(CURDATE())
                            GROUP BY maintainance_request.equipment_id;
                            CREATE TEMPORARY TABLE costs_table2 SELECT service_request.equipment_id, 
                            SUM(service_request.service_labor_cost) AS serv_labor_cost,
                            SUM(service_request.service_sparepart_cost) AS serv_sparepart_cost
                            FROM service_request WHERE MONTH(service_request.service_completion_date)=MONTH(CURDATE())
                            GROUP BY service_request.equipment_id;
                            CREATE TEMPORARY TABLE equipments
                            SELECT equipment_list.equipment_id, machines.machine_name,machine_functions.machine_function_name, machines.machine_capacity,machine_groups.machine_group_name,  equipment_list.plate_number, equipment_list.manufactured_year, project_list.Name,equipment_list.Investment_cost,CONCAT(equipment_list.serviced_every,' ',machines.machine_product_unit) AS 'service_every',equipment_type.type_name ,equipment_status.status_name FROM equipment_list,machines,project_list,machine_functions,machine_groups,equipment_status,equipment_type  WHERE equipment_list.machine_id=machines.machine_id  AND equipment_list.current_location=project_list.id AND machines.major_functions=machine_functions.id AND machines.machine_group= machine_groups.machine_group_id AND equipment_list.equipment_type = equipment_type.id AND equipment_list.current_status = equipment_status.status_id ;
                            SELECT equipments.equipment_id AS 'Equipment ID',equipments.machine_name AS 'Machine Name', equipments.machine_function_name AS 'Major Functions',equipments.machine_group_name AS 'Machine Group',equipments.machine_capacity AS 'Capacity',equipments.plate_number AS 'Plate Number',equipments.manufactured_year AS 'Manufacture Year',equipments.Name AS 'Current Location',equipments.Investment_cost AS 'Investment Cost',equipments.service_every AS 'Needs Service Every',equipments.type_name AS 'Ownership Status',equipments.status_name AS 'Current Status',costs_table1.maint_labor_cost  AS 'This Month Maintainance Labor Cost',costs_table1.maint_sparepart_cost  AS 'This Month Maintainance Sparepart Cost',costs_table2.serv_labor_cost  AS 'This Month Labor Cost',costs_table2.serv_sparepart_cost  AS 'This Month Service Sparepart Cost' FROM equipments LEFT OUTER JOIN costs_table1 ON equipments.equipment_id = costs_table1.equipment_id LEFT OUTER JOIN costs_table2 ON equipments.equipment_id = costs_table2.equipment_id";
            return SelectFromDatabase(query3);

        }
        public static DataTable selectallEquipmentStatus(int siteId)
        {
            string query3 = @"DROP TABLE IF EXISTS costs_table1,equipments,costs_table2;
                               CREATE TEMPORARY TABLE costs_table1 SELECT maintainance_request.equipment_id, 
                            SUM(maintainance_request.maintainance_labor_cost) AS maint_labor_cost,
                            SUM(maintainance_request.maintainance_sparepart_cost) AS maint_sparepart_cost
                            FROM maintainance_request
                            GROUP BY maintainance_request.equipment_id;
                            CREATE TEMPORARY TABLE costs_table2 SELECT service_request.equipment_id, 
                            SUM(service_request.service_labor_cost) AS serv_labor_cost,
                            SUM(service_request.service_sparepart_cost) AS serv_sparepart_cost
                            FROM service_request
                            GROUP BY service_request.equipment_id;
                            CREATE TEMPORARY TABLE equipments
                            SELECT equipment_list.equipment_id, machines.machine_name,machine_functions.machine_function_name, machines.machine_capacity,machine_groups.machine_group_name,  equipment_list.plate_number, equipment_list.manufactured_year, project_list.Name,equipment_list.Investment_cost,CONCAT(equipment_list.serviced_every,' ',machines.machine_product_unit) AS 'service_every',equipment_type.type_name ,equipment_status.status_name FROM equipment_list,machines,project_list,machine_functions,machine_groups,equipment_status,equipment_type  WHERE equipment_list.machine_id=machines.machine_id  AND equipment_list.current_location=project_list.id AND machines.major_functions=machine_functions.id AND machines.machine_group= machine_groups.machine_group_id AND equipment_list.equipment_type = equipment_type.id AND equipment_list.current_status = equipment_status.status_id AND equipment_list.current_location='";
            query3=query3+ siteId + "'; SELECT equipments.equipment_id AS 'Equipment ID',equipments.machine_name AS 'Machine Name', equipments.machine_function_name AS 'Major Functions',equipments.machine_group_name AS 'Machine Group',equipments.machine_capacity AS 'Capacity',equipments.plate_number AS 'Plate Number',equipments.manufactured_year AS 'Manufacture Year',equipments.Name AS 'Current Location',equipments.Investment_cost AS 'Investment Cost',equipments.service_every AS 'Needs Service Every',equipments.type_name AS 'Ownership Status',equipments.status_name AS 'Current Status',costs_table1.maint_labor_cost  AS 'Todate Maintainance Labor Cost',costs_table1.maint_sparepart_cost  AS 'Todate Maintainance Sparepart Cost',costs_table2.serv_labor_cost  AS 'Todate Service Labor Cost',costs_table2.serv_sparepart_cost  AS 'Todate Service Sparepart Cost' FROM equipments LEFT OUTER JOIN costs_table1 ON equipments.equipment_id = costs_table1.equipment_id LEFT OUTER JOIN costs_table2 ON equipments.equipment_id = costs_table2.equipment_id";

            return SelectFromDatabase(query3);

        }
        public static DataTable selectFromUsers(string email,string password)
        {
            string query3 = "SELECT *  FROM Users WHERE user_email = '"+email+"' AND user_password = '"+password+"'";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectPasswordFromUsers(string uid)
        {
            string query3 = "SELECT user_password FROM users WHERE user_id='"+uid+"'";
            return SelectFromDatabase(query3);
        }
        public static Boolean updatePasswordForUsers(string uid,string pass)
        {
            string query3 = "UPDATE users SET user_password ='"+pass+"' WHERE user_id = '" + uid + "'";
            return UpdateDatabaseQuery(query3);
        }

        public static DataTable selectDriversAndOperators()
        {
            string query3 = "SELECT user_id, CONCAT(user_id,',',user_name,' ',user_fathername) AS detail  FROM Users WHERE user_department='5' OR user_department='9' ORDER BY user_id ASC";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectMonthlyTransaction()
        {
            string query3 = "SELECT transact_id AS 'Id', transact_time AS 'Time', transact_summary AS 'Transaction Summary', userid AS 'User ID' FROM `transact_history` WHERE transact_time BETWEEN NOW()- INTERVAL 30  DAY AND NOW() ORDER BY transact_time ASC";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectOperators(string user_depts)
        {
            string query3 = "SELECT user_id, CONCAT(user_id,',',user_name,' ',user_fathername) AS detail  FROM Users WHERE user_department='" +user_depts+"' ORDER BY user_id ASC";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectMachineGroups(string mach_id)
        {
            string query3 = "SELECT machine_group  FROM machines WHERE machine_id='" + mach_id +"'";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectInsuranceStatuses()
        {
            string query3 = "SELECT *  FROM insurance_statuses";
            return SelectFromDatabase(query3);
        }
       
        public static DataTable selectFromUserLevel(int id)
        {
            string query3 = "SELECT user_level_name  FROM user_levels WHERE id='"+id+"'";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectAllocatedNumber(string id)
        {
            string query3 = "SELECT allocated_item_no FROM allocation_request WHERE `id`='" + id + "'";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectForAllocationApproval()
        {
            /* string query3 = @"DROP TABLE IF EXISTS  project_listfrom;CREATE TEMPORARY TABLE project_listfrom SELECT * FROM project_list;
 SELECT allocate_item.id AS 'Id',  allocate_item.equipment_id AS 'Equipment ID', machines.machine_name AS 'Name', project_listfrom.Name as 'Alloccate From', project_list.name AS 'Allocate To',project_list.id AS 'Allocate To ID', request_id AS 'Request ID',transportation_cost AS 'Transportation Cost'  FROM allocate_item,project_list,project_listfrom,equipment_list,machines WHERE allocate_to = project_list.id AND allocate_from = project_listfrom.id AND equipment_list.equipment_id = allocate_item.equipment_id AND equipment_list.machine_id = machines.machine_id AND allocate_item.om_approved='0'";
   */
            string query3 = @"DROP TABLE IF EXISTS equipment_status_summary;
                            CREATE TEMPORARY TABLE equipment_status_summary
                            SELECT equipment_productivity_control.equipment_id,SUM(equipment_productivity_control.morning_end_at - equipment_productivity_control.morning_start_at - equipment_productivity_control.afternoon_start_at + equipment_productivity_control.afternoon_end_at) AS total_work, SUM(Idle_time)AS tot_idle_time  FROM equipment_productivity_control WHERE(CURRENT_DATE - equipment_productivity_control.Date) < 30  GROUP BY equipment_id;
                            DROP TABLE IF EXISTS  project_listfrom;
                            CREATE TEMPORARY TABLE project_listfrom 
                            SELECT * FROM project_list;
                            DROP TABLE IF EXISTS allocate_item_full;
                            CREATE TEMPORARY TABLE allocate_item_full
                            SELECT allocate_item.id,allocate_item.equipment_id,  machines.machine_name,project_listfrom.Name AS 'name_f',project_list.name AS 'name_t',project_list.id as 'pr_id', request_id,transportation_cost  FROM allocate_item,project_list,project_listfrom,equipment_list,machines  WHERE allocate_to = project_list.id AND allocate_from = project_listfrom.id AND equipment_list.equipment_id = allocate_item.equipment_id AND equipment_list.machine_id = machines.machine_id AND allocate_item.om_approved='0';

SELECT allocate_item_full.id AS 'Id',  allocate_item_full.equipment_id AS 'Equipment ID',allocate_item_full.machine_name AS 'Name', allocate_item_full.name_f as 'Alloccate From', allocate_item_full.name_t AS 'Allocate To',allocate_item_full.pr_id AS 'Allocate To ID', allocate_item_full.request_id AS 'Request ID',allocate_item_full.transportation_cost AS 'Transportation Cost', equipment_status_summary.tot_idle_time AS 'Idle Time',equipment_status_summary.total_work AS 'Total Work' FROM allocate_item_full LEFT JOIN equipment_status_summary ON allocate_item_full.equipment_id = equipment_status_summary.equipment_id
                               ";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectForMaintainanceApproval()
        {
            string query3 = "SELECT maintainance_id AS 'Maintainance ID', equipment_id AS 'Equipment ID', maintainance_request_date AS 'Request Date', maintainance_requested_completion AS 'Due Date', maintainance_problem AS 'Maintainance Problem', work_order_Id AS 'Workorder ID', maintainance_sparepart_cost AS 'Sparepart Cost', maintainance_labor_cost AS 'Labor Cost', maintainance_priority.priority_name AS 'Priority' FROM maintainance_request,maintainance_priority WHERE maintainance_request.maintainance_priority=maintainance_priority.id AND em_approved='1' AND om_approved='0'";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectForServiceApproval()
        {
            string query3 = "SELECT id AS 'ID', equipment_id AS 'Equipment ID', request_date AS 'Request Date', service_sparepart_cost AS 'Sparepart Cost', service_labor_cost AS 'Labor Cost' FROM service_request WHERE  em_approved='1' AND om_approved='0'";
            return SelectFromDatabase(query3);
        }

        public static DataTable selectForPurchaseApproval()
        {
            string query3 = "SELECT id AS 'ID', work_id AS 'Work Order ID', item_description AS 'Item Description', CONCAT( qty,' ',Unit) AS 'Quantity' FROM `sparepart_purchase` WHERE om_approved='0'";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectSuppliers()
        {
            string query3 = "SELECT * FROM supplier_info  ORDER BY supplier_name ASC";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectFromMachines()
        {
            string query3 = "SELECT *  FROM machines ORDER BY machine_name ASC";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectFromFunctions()
        {
            string query3 = "SELECT *  FROM machine_functions";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectFromGroups()
        {
            string query3 = "SELECT *  FROM machine_groups";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectFromDepartment()
        {
            string query3 = "SELECT *  FROM department";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectFromMaintainancePriority()
        {
            string query3 = "SELECT * FROM maintainance_priority";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectFromMaintainanceStatus()
        {
            string query3 = "SELECT * FROM maintainance_status";
            return SelectFromDatabase(query3);
        }
        public static DataTable selectFromUserLevel()
        {
            string query3 = "SELECT *  FROM user_levels";
            return SelectFromDatabase(query3);
        }
        public static DataTable SelectFromAlocationRequests()
        {
            string query3 = "SELECT id, machine_id, CONCAT(quantity,' ',unit ) AS Equipments,site_id AS 'project ID', delivery_date AS 'Delivery Date', duration, allocated_item_no AS 'Allocated Items',work_type AS 'Work Type',work_zone AS 'Work Zone',volume_of_work AS 'Volume Of Work' , requested_by AS 'Requester ID' FROM allocation_request WHERE (quantity-allocated_item_no)>0 AND (request_status='1' OR request_status='2')";
            return SelectFromDatabase(query3);
        }
        public static DataTable SelectPendingMaintainanceRequests()
        {
            string query3 = " SELECT maintainance_id AS 'ID', equipment_id AS 'Equipment ID' , maintainance_request_date AS 'Request Date', maintainance_requested_completion AS 'Requested Completion Date', maintainance_problem AS 'Problem', requested_by As 'Requester ID', maintainance_status.maintainance_status AS 'Status' FROM maintainance_request,maintainance_status WHERE (maintainance_request.maintainance_status='1' OR maintainance_request.maintainance_status='2') AND maintainance_status.id=maintainance_request.maintainance_status";
            return SelectFromDatabase(query3);
        }
        public static DataTable SelectPendingServiceRequests()
        {
            string query3 = " SELECT id AS 'ID', equipment_id AS 'Equipment ID' , request_date AS 'Request Date', requested_by As 'Requester ID' FROM service_request WHERE (service_request.service_status='1' OR service_request.service_status='2')";

            return SelectFromDatabase(query3);
        }
        public static DataTable SelectEquipmentForAlocation(string machine_id, string allocateToID)
        {
            string query3 = @"DROP TABLE IF EXISTS equipment_status_summary;
                            CREATE TEMPORARY TABLE equipment_status_summary
                            SELECT equipment_productivity_control.equipment_id,SUM(equipment_productivity_control.morning_end_at - equipment_productivity_control.morning_start_at - equipment_productivity_control.afternoon_start_at + equipment_productivity_control.afternoon_end_at) AS total_work, SUM(Idle_time)AS tot_idle_time  FROM equipment_productivity_control WHERE(CURRENT_DATE - equipment_productivity_control.Date) < 30  GROUP BY equipment_id;
                            SELECT equipment_list.equipment_id AS 'Equipment ID', equipment_list.current_location, equipment_status_summary.total_work,equipment_status_summary.tot_idle_time FROM equipment_list LEFT JOIN equipment_status_summary ON equipment_list.equipment_id = equipment_status_summary.equipment_id WHERE equipment_list.machine_id='"+machine_id+"' AND equipment_list.current_location != '"+allocateToID+"';";
            return SelectFromDatabase(query3);
        }
        public static DataTable SelectForSparepartPurchase()
        {
            string query3 = @"SELECT  maintainance_request.equipment_id 'Equipment ID',maintainance_request.work_order_Id AS 'Workorder ID',sparepart_purchase.item_description AS 'Item Description',CONCAT(sparepart_purchase.qty,' ',sparepart_purchase.Unit) AS 'Quantity', machines.machine_name AS 'Equipment Type', equipment_list.plate_number AS 'Plate Number', project_list.Name AS 'Project', maintainance_status.maintainance_status AS 'Maintainance Status', maintainance_request.maintainance_request_date 'Job Open Date',maintainance_request.maintainance_completion_date AS 'Job Closed Date', (maintainance_request.maintainance_completion_date-maintainance_request.maintainance_request_date) AS 'TOTAL ELAPSED TIME',maintainance_request.maintainance_requested_completion AS 'Due Date',(maintainance_request.maintainance_completion_date-maintainance_request.maintainance_requested_completion) AS 'Delay Time' FROM maintainance_request,sparepart_purchase,equipment_list,machines,project_list,maintainance_status WHERE sparepart_purchase.work_id=maintainance_request.work_order_Id AND maintainance_request.equipment_id=equipment_list.equipment_id AND equipment_list.machine_id=machines.machine_id AND equipment_list.current_location=project_list.id AND maintainance_request.maintainance_status=maintainance_status.id  AND YEARWEEK(sparepart_purchase.date_purchased)=(YEARWEEK(CURRENT_DATE)-1)";
            return SelectFromDatabase(query3);
        }
        public static DataTable SelectForWeekleyMaintainanceReport()
        {
            string query3 = @"SELECT maintainance_id AS 'Maintainance ID', equipment_list.equipment_id AS 'Equipment ID', machines.machine_name AS 'Equiment Name', maintainance_problem AS 'Detailed Problem',maintainance_completion_date AS 'Completion Date', maintainance_sparepart_cost AS 'Sparepart Cost', maintainance_labor_cost AS 'Labor Cost'  FROM maintainance_request,equipment_list,machines WHERE equipment_list.equipment_id=maintainance_request.equipment_id AND machines.machine_id=equipment_list.machine_id AND YEARWEEK(maintainance_completion_date)=(YEARWEEK(CURRENT_DATE)-1)";
            return SelectFromDatabase(query3);
        }
        public static DataTable SelectForWeekleyServiceReport()
        {
            string query3 = @"SELECT id AS 'Service ID', equipment_id AS 'Equipment ID', service_completion_date AS 'Service Date', service_labor_cost AS 'Labor Cost', service_sparepart_cost AS 'Sparepart Cost' FROM service_request WHERE YEARWEEK(service_completion_date)=(YEARWEEK(CURRENT_DATE)-1)";
            return SelectFromDatabase(query3);
        }
        public static DataTable SelectForMonthlyEquipmentAllocationReport()
        {
            string query3 = @"SELECT project_list.Name AS 'Project', machines.machine_name AS 'Machine Name' ,machine_groups.machine_group_name AS 'Category',CONCAT(quantity,' ' , unit) AS 'Requested',CONCAT(allocated_item_no,' ' , unit) AS 'Allocated', `remark` AS 'Remarks'  FROM allocation_request,project_list,machines,machine_groups,allocate_item WHERE site_id=project_list.id AND allocation_request.machine_id=machines.machine_id AND machines.machine_group=machine_groups.machine_group_id AND allocate_item.request_id=allocation_request.id  AND
 MONTH(allocate_item.allocation_date)=MONTH(CURRENT_DATE) AND allocate_item.om_approved='1' ORDER BY project_list.Name ASC";
            return SelectFromDatabase(query3);
        }
        public static string InsertIntoTransaction(string data)
        {
            string query2 = "INSERT INTO transact_history(transact_time, transact_summary, userid) VALUES" + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoInsuranceDetails(string data)
        {
            string query2 = "INSERT INTO insurance_payment( equipment_id, value_payed, payment_date) VALUES  " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoSparepartPurchase(string data)
        {
            string query2 = "INSERT INTO sparepart_purchase (work_id, item_description, Unit, qty) VALUES  " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoAllocationTable(string data)
        {
            string query2 = "INSERT INTO allocate_item (request_id, equipment_id, allocate_from, allocate_to,transportation_cost) VALUES  " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoEquipments(string data)
        {
            string query2 = "INSERT INTO equipment_list (equipment_id,machine_id, plate_number, manufactured_year, current_location, current_status, Investment_cost, serviced_every, equipment_type,insurance_status,operator_id,registered_date) VALUES  " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoMachines(string data)
        {
            string query2 = "INSERT INTO machines (machine_name, major_functions,machine_model,machine_product_unit,machine_capacity,machine_group) VALUES " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoSupplierInfo(string data)
        {
            string query2 = "INSERT INTO supplier_info(supplier_name, supplier_address, supplier_phone1, supplier_phone2, supplier_po_box, supplier_email) VALUES " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoEmployees(string data)
        {
            string query2 = "INSERT INTO users (user_id, user_name, user_fathername, user_department, user_location, user_email, user_phone, user_password, user_level, user_status) VALUES  " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoSites(string data)
        {
            string query2 = "INSERT INTO project_list (Code, Name, Status) VALUES   " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoEquipmenMonitoring(string data)
        {
            string query2 = "INSERT INTO `equipment_productivity_control`( `Date`, `equipment_id`, `activity_detail`, `morning_start_at`, `morning_end_at`, `afternoon_start_at`, `afternoon_end_at`, `Idle_time`, `idle_reason`, `fuel_used`, `site_productivity`, `user_id`) VALUES    " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoAllocationRequestes(string data)
        {
            string query2 = "INSERT INTO allocation_request(machine_id, unit, quantity, site_id, delivery_date, duration, work_type, work_zone, volume_of_work, requested_by, remark) VALUES   " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoMaintainanceRequest(string data)
        {
            string query2 = "INSERT INTO maintainance_request( equipment_id, maintainance_request_date, maintainance_requested_completion, maintainance_problem, requested_by) VALUES    " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static string InsertIntoServiceRequest(string data)
        {
            string query2 = "INSERT INTO service_request( equipment_id, request_date, requested_by) VALUES    " + data;
            return "Inserting to databe  was " + InsertToDatabase(query2);
        }
        public static bool  UpdateDatabaseQuery(string Query)
        {
            return UpdateDatabase(Query);
        }

        public static DataTable SelectFromExcel(string fileName, string sheetName, string cellRanges)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                DataSet DtSet;
                System.Data.OleDb.OleDbDataAdapter MyCommand;
                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='"+fileName+"';Extended Properties=\"Excel 8.0;HDR=No;\"");
                MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from ["+sheetName+"$"+cellRanges+"]", MyConnection);
                MyCommand.TableMappings.Add("Table", "TestTable");
                DtSet = new DataSet();
                MyCommand.Fill(DtSet);
                return DtSet.Tables[0];
            }
            catch(Exception ex)
            {
                Home.logMessage += "\n" + DateTime.Now.ToString() + "Error occured when selecting from excel because " + ex.Message;
                return null;
            }
        }
        public static bool ExportToPdf(DataTable dataTable,string destinationPath)
        {

            try
            {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationPath, FileMode.Create));
            document.Open();

            PdfPTable table = new PdfPTable(dataTable.Columns.Count);
            table.WidthPercentage = 100;

            //Set columns names in the pdf file
            for (int k = 0; k < dataTable.Columns.Count; k++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(dataTable.Columns[k].ColumnName));

                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.BackgroundColor = new iTextSharp.text.Color(51, 102, 102);

                table.AddCell(cell);
            }

            //Add values of DataTable in pdf file
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(dataTable.Rows[i][j].ToString()));

                    //Align the cell in the center
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                    table.AddCell(cell);
                }
            }

            document.Add(table);
            document.Close();
            return true;
            }
            catch(Exception ex)
            {
                Home.logMessage += "\n" + DateTime.Now.ToString() + "Error occured when Creating pdf " + ex.Message;
                return false;
            }
        }

    }
}
