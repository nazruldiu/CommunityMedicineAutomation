﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CommunityMedicineAutomation.Model;

namespace CommunityMedicineAutomation.DAL
{
    public class TreatmentGateway
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["connectionDB"].ConnectionString;
        public int SaveObservation(Treatment aTreatment, Patient aPatient, int centerId)
        {
            int observationId = 0;
            string query = "INSERT INTO ObservationTBL VALUES('" + aTreatment.Observation + "','" + aTreatment.Date + "','" + aPatient.Id + "','" + centerId + "','" + aTreatment.DoctorId + "')";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            query = "SELECT * FROM ObservationTBL";
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                observationId = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
            connection.Close();
            return observationId;
        }
        public void SaveTreatment(Treatment aTreatment, int observationId) {
            string query = "INSERT INTO TreatmentTBL VALUES('" + aTreatment.DiseaseId + "','" + aTreatment.MedicineId + "','" + aTreatment.Dose + "','" + aTreatment.Quantity + "','" + aTreatment.Note + "','" +observationId+ "','"+aTreatment.TakenTime+"')";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public List<Treatment> GetObservationList(Patient aPatient) {
            List<Treatment> observationList = new List<Treatment>();
            string query = "SELECT * FROM ObservationTBL WHERE PatientId='"+aPatient.Id+"'";
            SqlConnection  connection = new SqlConnection(connectionString);
            SqlCommand  command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Treatment aTreatment = new Treatment();
                aTreatment.ObservationId = int.Parse(reader["Id"].ToString());
                aTreatment.Observation = reader["Observation"].ToString();
                aTreatment.Date = reader["Date"].ToString();
                aTreatment.CenterId = int.Parse(reader["CenterId"].ToString());
                aTreatment.DoctorId = int.Parse(reader["DoctorId"].ToString());
                observationList.Add(aTreatment);
            }
            reader.Close();
            connection.Close();
            return observationList;
        }
        public List<Treatment> GetTreatmentList(int observationId)
        {
            List<Treatment> treatmentList = new List<Treatment>();
            string query = "SELECT * FROM TreatmentTBL WHERE ObservationId='" + observationId + "'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Treatment aTreatment = new Treatment();
                aTreatment.DiseaseId = int.Parse(reader["DiseaseId"].ToString());
                aTreatment.MedicineId=int.Parse(reader["MedicineId"].ToString());
                aTreatment.Dose =reader["Dose"].ToString();
                aTreatment.TakenTime = reader["TakenTime"].ToString();
                aTreatment.Quantity = int.Parse(reader["Quantity"].ToString());
                aTreatment.Note = reader["Note"].ToString();
                treatmentList.Add(aTreatment);
            }
            reader.Close();
            connection.Close();
            return treatmentList;
        }
        public int GetPatientId(string date)
        {
            int patientId = 0;
            string query = "SELECT * FROM ObservationTBL WHERE Date='" +date + "'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                patientId = int.Parse(reader["PatientId"].ToString());
            }
            reader.Close();
            connection.Close();
            return patientId;
        }

        public int GetObservationId(int patientId)
        {
            int obsvId = 0;
            string query = "SELECT * FROM ObservationTBL WHERE PatientId='" + patientId + "'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                obsvId = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
            connection.Close();
            return obsvId;
        }

        public List<int> GetDiseaseId(int observationId)
        {
            List<int> diseaseIdList = new List<int>();
            string query = "SELECT * FROM TreatmentTBL WHERE ObservationId='" + observationId + "'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                
                int diseaseId = int.Parse(reader["DiseaseId"].ToString());
               
                diseaseIdList.Add(diseaseId);
            }
            reader.Close();
            connection.Close();
            return diseaseIdList;
        }
        public int GetObservationIdForDistrict(string date) {
            int obsvId = 0;
            string query = "SELECT * FROM ObservationTBL WHERE Date='" + date + "'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                obsvId = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
            connection.Close();
            return obsvId;
        }
        public int GetObsId(int observationId, int diseaseId)
        {
            int obId = 0;
            string query = "SELECT * FROM TreatmentTBL WHERE ObservationId='" + observationId + "' AND DiseaseId='" + diseaseId + "'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                obId = int.Parse(reader["ObservationId"].ToString());


            }
            reader.Close();
            connection.Close();
            return obId;
        }
        public int GetCenterIdForDistrict(int observationId)
        {
            int obsvId = 0;
            string query = "SELECT * FROM ObservationTBL WHERE Id='" + observationId + "'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                obsvId = int.Parse(reader["CenterId"].ToString());
            }
            reader.Close();
            connection.Close();
            return obsvId;
        }
    }
}