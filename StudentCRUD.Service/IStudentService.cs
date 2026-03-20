using System.Collections.Generic;
using System.ServiceModel;
using StudentCRUD.Service.Models;

namespace StudentCRUD.Service
{
    // [ServiceContract] = ეს Interface არის WCF სერვისის "მენიუ"
    // კლიენტი ხედავს მხოლოდ იმას, რაც აქ არის
    [ServiceContract]
    public interface IStudentService
    {
        // [OperationContract] = ეს მეთოდი კლიენტს ხელმისაწვდომია ქსელით

        [OperationContract]
        List<Student> GetAllStudents();         // READ ALL

        [OperationContract]
        Student GetStudentById(int id);         // READ ONE

        [OperationContract]
        bool AddStudent(Student student);       // CREATE

        [OperationContract]
        bool UpdateStudent(Student student);    // UPDATE

        [OperationContract]
        bool DeleteStudent(int id);             // DELETE
    }
}
