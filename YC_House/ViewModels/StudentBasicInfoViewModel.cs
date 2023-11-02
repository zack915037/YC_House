using System.ComponentModel.DataAnnotations;

namespace YC_House.ViewModels
{
    public class StudentBasicInfoViewModel
    {
        [Display(Name = "學號")]
        [Required(ErrorMessage ="學號必填")]
        public string StudentId { get; set; }
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Display(Name = "性別")]
        public string Sex { get; set; }
        [Display(Name = "身高")]
        public string Heigh { get; set; }
        [Display(Name = "體重")]
        public string Weight { get; set; }
        [Display(Name = "戶籍地址")]
        public string Residential_Address { get; set; }
        [Display(Name = "手機號碼")]
        public string CellPhone { get; set; }
        [Display(Name = "國文成績")]
        public string ChScores { get; set; }
        [Display(Name = "數學成績")]
        public string MathScores { get; set; }
        [Display(Name = "英文成績")]
        public string EnScores { get; set; }
    }
}