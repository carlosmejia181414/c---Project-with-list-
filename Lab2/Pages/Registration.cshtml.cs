using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AcademicManagement;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace Lab2.Pages
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public string? SelectedStudentId { get; set; }
        [BindProperty]
        public Student? SelectedStudentName { get; set; }
        [BindProperty]
        public List<SelectListItem>? CourseSelections { get; set; }
        [BindProperty]
        public List<Course>? SelectedCourses { get; set; }
        [BindProperty]
        public List<AcademicRecord>? AcademicRecords { get; set; }
        [BindProperty]
        public List<Course>? AcademicRecordsRegistered { get; set; }
        [BindProperty]
        public int? RecordsCount { get; set; }
        public int? alert { get; set; }

        public void OnGet()
        {
        }

        public void OnPostStudentSelected()
        {
            if (SelectedStudentId is not null) { 
    
            if (SelectedStudentId != "-1")
            {
                SelectedStudentName = DataAccess.GetAllStudents().First(c => c.StudentId == SelectedStudentId);
                CourseRegisterFunction(SelectedStudentId);
            }
            else
            {
                SelectedStudentName = null;
                AcademicRecords = new List<AcademicRecord>();
            }
            RecordsCount = DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId).Count();

            }
        }

        public void OnPostRegister(string SelectedStudentId)
        {
            SelectedCourses = new List<Course>();
            if(CourseSelections is not null) { 
            foreach (SelectListItem item in CourseSelections)
            {
                if (item.Selected)
                {
                    var course = DataAccess.GetAllCourses().First(c => c.CourseCode == item.Value);
                    SelectedCourses.Add(course);
                    var StudentRecord = new AcademicRecord(SelectedStudentId, item.Value);
                    DataAccess.AddAcademicRecord(StudentRecord);
                }
            }
            }
            RecordsCount = DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId).Count();
            alert = RecordsCount;
            CourseRegisterFunction(SelectedStudentId);
        }

        public void CourseRegisterFunction(string SelectedStudentId)
        {
            AcademicRecords = DataAccess.GetAcademicRecordsByStudentId(SelectedStudentId);
            AcademicRecordsRegistered = new List<Course>();
            foreach (var item in AcademicRecords)
            {
                var RecordCourses = DataAccess.GetAllCourses().First(c => c.CourseCode == item.CourseCode);
                AcademicRecordsRegistered.Add(RecordCourses);
            }
            return;
        }
    }
}


