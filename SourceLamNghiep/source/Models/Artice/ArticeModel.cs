using System;
using System.ComponentModel.DataAnnotations;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.Artice
{
    public class ArticeModel : BaseModel
    {
        [Required(ErrorMessageResourceName = "TitleRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.BaiViet.BaiViet))]
        [Display(Name = "Title", ResourceType = typeof(Share.Messages.BeScreens.BaiViet.BaiViet))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Share.Messages.BeScreens.BaiViet.BaiViet))]
        public string Description { get; set; }

        [Display(Name = "Content", ResourceType = typeof(Share.Messages.BeScreens.BaiViet.BaiViet))]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public string ImagePath { get; set; }
        public ArticeStatus Status { get; set; }

        //ref

        public string OldImagePath { get; set; }
    }
}
